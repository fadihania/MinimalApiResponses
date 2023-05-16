# Minimal API Routing
A sample .NET 7 Minimal API app that shows the different ways to prepare responses, then use basic Entity Framework setup to connect and query MySQL DB

**Check commits for steps and how to apply different concepts**

- Use Entity Framework for database access
- Use LINQ with:
  + Entity Framework Core
  + Collections
- To Use MySQL DB:
  + `Install Pomelo.EntityFrameworkCore.MySql`
  + Create `DbContext` class
  + Use MySQL connection string: `"server=localhost;user=root;password=1234;database=blog"`
  + Use your database version: `new MySqlServerVersion(new Version(8, 0, 31))`
  + Add migration and update database