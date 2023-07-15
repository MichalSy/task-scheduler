using Task.Scheduler;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

var app = builder.Build();

var sampleTodos = TodoGenerator.GenerateTodos().ToArray();

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.MapGet("/test", () =>
{
    return Environment.GetEnvironmentVariables();
});


app.UseFastEndpoints();
app.UseSwaggerGen();
app.Run();
