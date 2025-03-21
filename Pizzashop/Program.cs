using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pizzashop.Data;
using Pizzashop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options => 
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7026",
            ValidAudience = "https://localhost:7026",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("blah123%%=*()234124124"))
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "", Version = "v1",});
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); 
});

builder.Services.AddDbContext<PizzaShopContext>( 
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database"))
    //TODO: dev/prod config separation
);
builder.Services.AddControllers();

builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("./v1/swagger.json", "Pizza API v1");
    });
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PizzaShopContext>();
    context.Database.Migrate(); 
}

app.Run();