using Autofac;
using Autofac.Extensions.DependencyInjection;
using Test;

var builder = WebApplication.CreateBuilder(args);

// The UseServiceProviderFactory call attaches the
// Autofac provider to the generic hosting mechanism.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Add services to the container.
//builder.Services.AddControllersWithViews();
//Register your own things directly with Autofac here
builder.Host.ConfigureContainer<ContainerBuilder>(builder => {
    builder.RegisterType<TestClass>().As<ITestClass>();
    builder.RegisterType<TestClass2>().As<ITestClass2>();
    });

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
