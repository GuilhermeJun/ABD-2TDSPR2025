namespace _2TDSPR25.Domain;

public class Todo
{
    public int Id { get;set; }
    public string? Name { get;set; }
    public bool IsComplete {get;set;}
    public string? Secret { get;set; }
}