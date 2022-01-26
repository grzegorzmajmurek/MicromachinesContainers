using Microsoft.EntityFrameworkCore;
using UserService.AsyncDataServices;
using UserService.Data;
using UserService.Http;
using UserService.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMemoryUserDb"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpClient<IHttpOrdersClient, HttpOrdersClient>();
builder.Services.AddHttpClient<IHttpStockClient, HttpStockClient>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

PrepDb.Initialize();

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
