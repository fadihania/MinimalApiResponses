using Microsoft.EntityFrameworkCore;

namespace MinimalApiResponses;

public class DbData : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "server=localhost;user=root;password=123456;database=blog", // Connection string
            new MySqlServerVersion(new Version(5, 7, 42)));             // MySQL version
    }
}
