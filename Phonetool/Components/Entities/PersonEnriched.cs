namespace Phonetool.Entities;

public class PersonEnriched
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int? ManagerId { get; set; }
    public string JobTitle { get; set; }
    public int JobLevel { get; set; }
    public bool IsManager { get; set; }
}