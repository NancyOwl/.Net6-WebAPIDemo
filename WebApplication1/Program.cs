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

app.MapGet("/findschool/{Id}" , async (int Id, MyDb db) => 
    await db.Schools.FindAsync(Id) is School school ? Results.Ok(school) : Results.NotFound()
);

app.MapPut("/editschool/{Id}" , async (int Id, School school, MyDb db) =>
{
    var oschool = await db.Schools.FindAsync(Id) ;
    if (oschool == null )
        return Results.NotFound();

    oschool.Logo = school.Logo;
    oschool.Address = school.Address;
    oschool.Email = school.Email;
    oschool.Name = school.Name;
    oschool.Tel = school.Tel;
        
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/removeschool/{Id}", async (int Id, MyDb db)=>
{
    var oschool = await db.Schools.FindAsync(Id) ;
    if (oschool == null )
        return Results.NotFound();
   
    db.Schools.Remove(oschool);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

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
