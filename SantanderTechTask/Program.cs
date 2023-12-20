using Refit;
using SantanderTechTask.ExternalApi;
using SantanderTechTask.Options;
using SantanderTechTask.Services;

var builder = WebApplication.CreateBuilder(args);

var appSettings = new AppSettingConfig();
builder.Configuration.Bind(appSettings);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRefitClient<IHackerNewsApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(appSettings.HackerNewsApi.BaseUrl));

builder.Services.AddSingleton(appSettings);
builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();
MapsterConfig.Configure();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();