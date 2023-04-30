using Microsoft.AspNetCore.Mvc;
using MinimalApiResponses;
using MinimalApiResponses.Models;

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

app.MapPost("/api/Posts", ([FromBody] Post? newPost) =>
{
    if (newPost is null)
        return Results.BadRequest(new { Message = "New post details must be provided" });

    Data.Posts.Add(newPost);

    return Results.Created("Posts", newPost.Id);
});

app.Run();
