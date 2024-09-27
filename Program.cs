using Microsoft.EntityFrameworkCore;
using Npgsql;
using src.Database;
using src.Repository;

var builder = WebApplication.CreateBuilder(args);
var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
).Build();
builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseContext>(Options =>
{
    Options.UseNpgsql(dataSourceBuilder);
});
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// test if database is connected or not
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    try
    {
        // Check if the application can connect to the database
        if (dbContext.Database.CanConnect())
        {
            Console.WriteLine("Database is connected");
        }
        else
        {
            Console.WriteLine("Unable to connect to the database.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database connection failed: {ex.Message}");
    }
}


// step 2: use 
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();



