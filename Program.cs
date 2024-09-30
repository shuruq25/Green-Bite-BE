using Microsoft.EntityFrameworkCore;
using Npgsql;
using src.Controllers;
using src.Database;
using src.Repository;
using src.Services;
using src.Services.category;
using src.Services.product;
using src.Services.review;
using src.Services.UserService;
using src.Utils;
using static src.Entity.User;

var builder = WebApplication.CreateBuilder(args);

var dataSourceBuilder = new NpgsqlDataSourceBuilder(
    builder.Configuration.GetConnectionString("Local")
);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSourceBuilder.Build());
});
dataSourceBuilder.MapEnum<Role>();

builder
    .Services.AddAutoMapper(typeof(OrderMapperProfile).Assembly)
    .AddAutoMapper(typeof(MapperProfile).Assembly);

builder
    .Services.AddScoped<ICategoryService, CategoryService>()
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<IOrderRepository, OrderRepository>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IPaymentRepository, PaymentRepository>()
    .AddScoped<IPaymentService, PaymentService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IAddressService, AddressService>()
    .AddScoped<AddressRepository, AddressRepository>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<UserRepository, UserRepository>()
    .AddScoped<IReviewService, ReviewService>()
    .AddScoped<ReviewRepository, ReviewRepository>()
    .AddScoped<ICouponService, CouponService>()
    .AddScoped<CouponRepository, CouponRepository>();
;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

    try
    {
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

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
