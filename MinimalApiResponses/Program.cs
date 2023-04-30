using MinimalApiResponses;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => Results.Json("Blog"));

// Blog API
app.MapGet("/api/Posts", () =>
{
    return Results.Ok(Data.Posts);
});

app.Run();
