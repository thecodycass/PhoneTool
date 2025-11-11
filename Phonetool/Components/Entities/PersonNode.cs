namespace Phonetool.Entities;

public class PersonNode
{
    public PersonEnriched Data { get; set; }
    public List<PersonNode> Employees { get; set; } = new();
}
