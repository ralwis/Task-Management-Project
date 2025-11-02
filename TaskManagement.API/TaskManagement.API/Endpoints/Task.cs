using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.feature.Task.Commands.CreateTask;
using TaskManagement.Application.feature.Task.Commands.DeleteTask;
using TaskManagement.Application.feature.Task.Commands.MarkTaskCompleted;
using TaskManagement.Application.feature.Task.Commands.UpdateTask;
using TaskManagement.Application.feature.Task.Queries.GetAllTasks;
using TaskManagement.Application.feature.Task.Queries.GetStatuses;
using TaskManagement.Application.feature.Task.Queries.GetTaskById;

namespace TaskManagement.API.Endpoints
{
    public static class Task
    {
        public static void MapTaskManagement(this WebApplication app)
        {
            app.MapGet("GetTasks", async (ISender sender) =>
            {
                var resp = await sender.Send(new GetAllTasksQuery());
                return Results.Ok(resp);
            })
            .WithTags("Task");

            app.MapGet("/tasks/{id:int}", async (int id, ISender sender) =>
            {
                var resp = await sender.Send(new GetTaskByIdQuery(id));
                return Results.Ok(resp);
            })
            .WithTags("Task");

            app.MapPost("/tasks", async (CreateTaskCommand command, ISender sender) =>
            {
                var resp = await sender.Send(command);
                return Results.Created($"/tasks/{resp.Id}", resp);
            })
            .WithTags("Task");

            app.MapPut("/tasks/{id:int}", async (int id, UpdateTaskCommand command, ISender sender) =>
            {
                if (id != command.taskDTO.Id)
                    return Results.BadRequest();

                var resp = await sender.Send(command);
                return resp is not null ? Results.Ok(resp) : Results.NotFound();
            })
            .WithTags("Task");

            app.MapDelete("/tasks/{id:int}", async (int id, ISender sender) =>
            {
                var resp = await sender.Send(new DeleteTaskCommand(id));
                return resp is not null ? Results.Ok(resp) : Results.NotFound();
            })
            .WithTags("Task");

            app.MapPatch("/tasks/{id:int}/complete", async (int id, ISender sender) =>
            {
                var resp = await sender.Send(new MarkTaskCompletedCommand(id));
                return resp is not null ? Results.Ok(resp) : Results.NotFound();
            })
            .WithTags("Task");

            app.MapGet("GetStatuses", async (ISender sender) =>
            {
                var resp = await sender.Send(new GetStatusesQuery());
                return Results.Ok(resp);
            })
            .WithTags("Status");
        }
    }
}
