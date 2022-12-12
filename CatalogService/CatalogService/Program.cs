using Autofac;
using Autofac.Extensions.DependencyInjection;
using Catalog.BLL.Handlers;
using Catalog.BLL.Interfaces.Handlers;
using Catalog.BLL.Interfaces.Managers;
using Catalog.BLL.Interfaces.Repository;
using Catalog.BLL.Managers;
using Catalog.DAL.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var connString = builder.Configuration.GetConnectionString("CatalogDb");
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder
        .RegisterType<ProductRepository>()
        .As<IProductRepository>()
        .WithParameter("connString", connString);

    builder
        .RegisterType<CategoryRepository>()
        .As<ICategoryRepository>()
        .WithParameter("connString", connString);

    builder.RegisterType<ProductManager>().As<IProductManager>();
    builder.RegisterType<CategoryManager>().As<ICategoryManager>();
    builder.RegisterType<RabbitMQMessagePublisher>().As<IQueueMessagePublisher>();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication("Bearer")
    .AddIdentityServerAuthentication("Bearer", options =>
    {
        options.ApiName = "catalog";
        options.Authority = "https://localhost:44322";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Role", policy => policy.RequireClaim("Role"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
