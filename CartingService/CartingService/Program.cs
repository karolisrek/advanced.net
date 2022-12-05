using Cart.BLL.BackgroundTasks;
using Cart.BLL.Managers;
using Cart.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<CartManager>();
builder.Services.AddTransient<CartRepository>();
builder.Services.AddSingleton<ProductQueueListener>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);
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

var queueListener = app.Services.GetService<ProductQueueListener>()!;
queueListener.StartListnening();

app.Run();
