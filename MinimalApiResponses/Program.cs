using Microsoft.AspNetCore.Mvc;
using MinimalApiResponses;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Json("Blog"));

// Blog API
app.MapGet("/api/Posts", () =>
{
    return Results.Ok(Data.Posts);
});

app.MapGet("/api/Posts/{id}", ([FromRoute] int id) =>
{
    var post = Data.Posts.FirstOrDefault(p => p.Id == id);
    if (post is null)
        return Results.NotFound(new { Message = "Post not found!" });

    return Results.Ok(post);
});

app.Run();
