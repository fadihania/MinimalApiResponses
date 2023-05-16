using Microsoft.AspNetCore.Mvc;
using MinimalApiResponses;
using MinimalApiResponses.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Json("Blog"));

// Blog API
var db = new DbData();

app.MapGet("/api/Posts", () =>
{
    return Results.Ok(db.Posts);
});

app.MapGet("/api/Posts/{id}", ([FromRoute] int id) =>
{
    var post = db.Posts.FirstOrDefault(p => p.Id == id);
    if (post is null)
        return Results.NotFound(new { Message = "Post not found!" });

    return Results.Ok(post);
});

app.MapPost("/api/Posts", ([FromBody] Post? newPost) =>
{
    if (newPost is null)
        return Results.BadRequest(new { Message = "New post details must be provided!" });

    Data.Posts.Add(newPost);

    return Results.Created("Posts", newPost.Id);
});

app.MapDelete("/api/Posts/{id}", ([FromRoute] int id) =>
{
    var post = Data.Posts.FirstOrDefault(p => p.Id == id);
    if (post is null)
        return Results.NotFound(new { Message = "Post not found!" });

    Data.Posts.Remove(post);

    return Results.NoContent();
});

app.MapPut("/api/Posts/{id}", ([FromRoute] int id, [FromBody] Post updatedPost) =>
{
    var postIndex = Data.Posts.FindIndex(p => p.Id == id);
    if (postIndex < 0)
        return Results.NotFound(new { Message = "Post not found!" });

    Data.Posts[postIndex] = updatedPost;

    return Results.NoContent();
});

app.Run();
