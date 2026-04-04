using Microsoft.EntityFrameworkCore;

namespace _2TDSPR25.Domain;

public class TodoDb : DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options) : base(options)
    { }
    
    public DbSet<Todo> Todos { get; set; }
}