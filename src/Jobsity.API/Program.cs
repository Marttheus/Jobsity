using Jobsity.API.Configurations;
using Jobsity.Infra.CrossCutting.Identity.Configurations;
using NetDevPack.Identity;
using NetDevPack.Identity.User;

const string allowSpecificOrigins = "allowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Cors Config
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsProduction())
    {
        options.AddPolicy(allowSpecificOrigins, policyBuilder =>
        {
            policyBuilder.WithOrigins("https://example.com")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    }
    else
    {
        options.AddPolicy(allowSpecificOrigins, policyBuilder =>
        {
            policyBuilder.WithOrigins("http://localhost:25181")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .SetIsOriginAllowed((host) => true)
                .AllowCredentials();
        });
    }
});

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseConfiguration(builder.Configuration);

// ASP.NET Identity Settings & JWT
builder.Services.AddApiIdentityConfiguration(builder.Configuration);

// Interactive AspNetUser (logged in)
// NetDevPack.Identity dependency
builder.Services.AddAspNetUserConfiguration();

// AutoMapper Settings
builder.Services.AddAutoMapperConfiguration();

// .NET Native DI Abstraction
builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseCors(allowSpecificOrigins);

//app.UseAuthorization();
app.UseAuthConfiguration();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.AddChatEndpoints();

app.Run();
