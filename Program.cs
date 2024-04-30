using Gridify;
using Gridify.EntityFramework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer("Server=.;Database=MTKDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;"));

var app = builder.Build();

app.MapGet("/", () => "Hello World.");

app.MapGet("/blog", async ([AsParameters] GridifyQuery query, AppDbContext db) =>
{
    return Results.Ok(await db.Blogs.GridifyAsync(query));
});

app.MapGet("/blog/{pageNo}/{pageSize}", async (int pageNo, int pageSize, AppDbContext db) =>
{
    var query = new GridifyQuery()
    {
        Page = pageNo,
        PageSize = pageSize,
    };
    return Results.Ok(await db.Blogs.GridifyAsync(query));
});

app.Run();