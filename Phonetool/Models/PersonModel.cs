using Phonetool.Data;
using Phonetool.Entities;

namespace Phonetool.Models;

/*
 * Adds a new Person
 */
public class PersonModel(AppDbContext database)
{
    public void Add(Person person)
    {
        var existingPerson = database.Persons.Find(person.PersonId);
        if (existingPerson != null) return;

        person.DateCreated = DateTime.UtcNow;
        person.DateModified = DateTime.UtcNow;

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
        userToUpdate.Tenure = person.Tenure;
        userToUpdate.IsManager = person.IsManager;
        userToUpdate.JobId = person.JobId;
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
}
