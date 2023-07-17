using Task.Scheduler;
//using FastEndpoints.Swagger;
using Neo4j.Driver;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder();

//builder.Services.AddFastEndpoints();
//builder.Services.SwaggerDocument();


builder.Services.AddSingleton(
    GraphDatabase.Driver(
        builder.Configuration["NEO4J_SERVER"], 
        AuthTokens.Basic(builder.Configuration["NEO4J_USER"], builder.Configuration["NEO4J_PASSWORD"])
    )
);

var app = builder.Build();

var sampleTodos = TodoGenerator.GenerateTodos().ToArray();

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

//app.MapGet("/test", () =>
//{
//    return Environment.GetEnvironmentVariables();
//});

app.MapGet("/conf", (IConfiguration conf) =>
{
    return conf.GetValue<string>("NEO4J_USER");
});

app.MapGet("/test2", async (IDriver driver) =>
{
    await driver.VerifyConnectivityAsync();
    var t = await driver.ExecutableQuery("CREATE (n) RETURN n").ExecuteAsync();
    return "Hello";
});

//app.UseFastEndpoints();
//app.UseSwaggerGen();
app.Run();
