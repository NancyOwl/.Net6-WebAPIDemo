using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.DataContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDb>(opt => opt.UseInMemoryDatabase("MyDbList"));

//var connectionString = builder.Configuration.GetConnectionString("MyDb") ?? "Data Source=MyDb.db";
//builder.Services.AddDbContext<MyDb>(opt => opt.UseSqlite(connectionString));

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

public partial class Program { }