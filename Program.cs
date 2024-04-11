using Azure.Identity;
using Azure.ResourceManager;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.


app.MapGet("/resourcegroups", async ([FromServices] IConfiguration config) =>
{
    var client = new ArmClient(new ClientSecretCredential(config["tenantId"], config["clientId"], config["clientSecret"]));
    var subscription = await client.GetDefaultSubscriptionAsync();
    var resourceGroups = subscription.GetResourceGroups().ToList();

    return Results.Json(resourceGroups);
});

app.Run();

