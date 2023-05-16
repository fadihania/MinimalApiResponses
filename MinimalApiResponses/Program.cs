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

app.MapPost("/api/Posts", async ([FromBody] Post? newPost) =>
{
    if (newPost is null)
        return Results.BadRequest(new { Message = "New post details must be provided!" });

    db.Posts.Add(newPost);
    await db.SaveChangesAsync();

    return Results.Created("Posts", newPost.Id);
});

app.MapDelete("/api/Posts/{id}", async ([FromRoute] int id) =>
{
    var post = db.Posts.FirstOrDefault(p => p.Id == id);
    if (post is null)
        return Results.NotFound(new { Message = "Post not found!" });

    db.Posts.Remove(post);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/api/Posts/{id}", async ([FromRoute] int id, [FromBody] Post updatedPost) =>
{
    var post = db.Posts.FirstOrDefault(p => p.Id == id);
    if (post is null)
        return Results.NotFound(new { Message = "Post not found!" });

    post.Title = updatedPost.Title;
    post.Content = updatedPost.Content;
    post.Author = updatedPost.Author;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
