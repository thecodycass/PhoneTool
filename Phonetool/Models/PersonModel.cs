using Phonetool.Data;
using Phonetool.Entities;

namespace Phonetool.Models;

/*
 * Adds a new Person
 */
public class PersonModel(AppDbContext database)
{
    private const int USER_ID = 7;
    private const int CEO_LEVEL = 99;
    
    public void Add(Person person)
    {
        var existingPerson = database.Persons.Find(person.PersonId);
        if (existingPerson != null) return;

        person.DateCreated = DateTime.UtcNow;
        person.DateModified = DateTime.UtcNow;
        person.IsArchived = false;

        database.Persons.Add(person);
        database.SaveChanges();
    }
    
    /*
     * Instead of deleting users, we will just archive them instead
     */
    public void Archive(int personId)
    {
        if (personId <= 0) return;
        
        var person = database.Persons.Find(personId); 
        if (person == null) return;
        
        person.IsArchived = true;
        database.Update(person);
        database.SaveChanges();
    }

    /**
     * Update Person
     */
    public void Update(int personId, Person person)
    {
        var userToUpdate = database.Persons.Find(personId);
        if (userToUpdate == null) return;

        userToUpdate.FirstName = person.FirstName;
        userToUpdate.LastName = person.LastName;
        userToUpdate.Age = person.Age;
        userToUpdate.IsManager = person.IsManager;
        userToUpdate.JobId = person.JobId;
        if (person.IsManager) userToUpdate.ManagerId = person.ManagerId;
        userToUpdate.ManagerId = person.ManagerId;
        userToUpdate.DateModified = DateTime.UtcNow;

        database.Persons.Update(userToUpdate);
        database.SaveChanges();
    }
    
    /*
     * Get Person By Person ID
     */
    public Person? GetPersonByPersonId(int personId) => database.Persons.Find(personId);
    
    /*
     * Get Manager employees
     */
    public List<Person> GetEmployeesByManager(int managerId)
    {
        return database.Persons.Where(p => p.ManagerId == managerId).ToList();
    }

    public List<PersonNode> GetHierarchyTree()
    {
        var allPeople = database.Persons
            .Join(
                database.Jobs,
                p => p.JobId,
                j => j.JobId,
                (p, j) => new PersonEnriched
                {
                    PersonId = p.PersonId,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    ManagerId = p.ManagerId,
                    JobTitle = j.Title,
                    JobLevel = j.JobLevel,
                    IsManager = p.IsManager
                })
            .ToList();

        // Create nodes for all people
        var nodes = allPeople.ToDictionary(p => p.PersonId, p => new PersonNode { Data = p });

        // Build the hierarchy
        foreach (var person in allPeople)
        {
            if (person.ManagerId.HasValue && nodes.ContainsKey(person.ManagerId.Value))
            {
                nodes[person.ManagerId.Value].Employees.Add(nodes[person.PersonId]);
            }
        }

        // Return top-level managers (those with no manager)
        return nodes.Values
            .Where(n => !n.Data.ManagerId.HasValue)
            .OrderByDescending(n => n.Data.JobLevel)
            .ToList();
    }
}
