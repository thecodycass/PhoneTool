using System.ComponentModel.DataAnnotations;

namespace Phonetool.Entities;

public class Job
{
    public int JobId { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public int JobLevel { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }

    // Navigation properties
    public ICollection<Person> Persons { get; set; } = new List<Person>();
}
