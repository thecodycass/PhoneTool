using System.ComponentModel.DataAnnotations;

namespace Phonetool.Entities;

public class Person
{
    public int PersonId { get; set; }
    public int? JobId { get; set; }
    public int? ManagerId { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public bool IsManager { get; set; }
    public bool IsArchived { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    // Navigation properties
    public Job? Job { get; set; }
    public Person? Manager { get; set; }
}
