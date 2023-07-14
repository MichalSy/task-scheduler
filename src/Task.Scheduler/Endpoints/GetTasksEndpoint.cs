namespace Task.Scheduler.Endpoints;
using Microsoft.AspNetCore.Authorization;

using System.Data;
using System.Threading.Tasks;

[HttpGet("/tasks")]
[AllowAnonymous]
public class GetTasksEndpoint : EndpointWithoutRequest<TaskResponse>
{
    public override async Task HandleAsync(CancellationToken ct)
    {
        await Task.Delay(1000, ct);
        Response = new TaskResponse(Guid.NewGuid());
    }
}