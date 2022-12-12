using Auth;
using IdentityServer4.Services;
using IdentityServer4.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IResourceOwnerPasswordValidator, PasswordValidator>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddControllers();

builder.Services.AddIdentityServer()
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddResourceOwnerValidator<PasswordValidator>()
    .AddProfileService<ProfileService>()
    .AddDeveloperSigningCredential();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseIdentityServer();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
