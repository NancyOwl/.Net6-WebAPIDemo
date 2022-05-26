using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDb>(opt => opt.UseInMemoryDatabase("MyDbList"));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/", () => "Hello ASP.NET Core WebApplication API~~~");

app.MapPost("/addschool", async (School school, MyDb db) =>
{
    db.Schools.Add(school);
    await db.SaveChangesAsync();

    return Results.Created($"/addschool/{school.Id}", school);
});

app.MapGet("/schools", async (MyDb db) => await db.Schools.ToListAsync());

app.Run();



public class School
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    public string Address { get; set; }
    public string Tel { get; set; }
    public string Email { get; set; }
}



class MyDb : DbContext
{
    public MyDb(DbContextOptions<MyDb> options) : base(options)
    { }

    public DbSet<School> Schools => Set<School>();
}
