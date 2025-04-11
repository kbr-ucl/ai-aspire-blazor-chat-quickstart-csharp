var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var ollama = builder.AddOllama(name: "ollama", port: null)
    .WithOpenWebUI()
    .WithDataVolume()
    .PublishAsContainer()
    .AddModel("phi3.5");

var apiService = builder.AddProject<Projects.AspireAIBlazorChatBot_ApiService>("apiservice");

builder.AddProject<Projects.AspireAIBlazorChatBot_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(ollama)
    .WithReference(apiService);

builder.Build().Run();
