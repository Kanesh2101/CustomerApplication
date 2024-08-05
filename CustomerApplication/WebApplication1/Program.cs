using CustomerApplication.Data;
using CustomerApplication.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CustomerDbContext>(options=>
{
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DatabaseName")!);
});

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

//builder.Services.AddCors(options => options.AddPolicy(name: "AngularApp",
//    policy =>
//    {
//        policy.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
//    }));

builder.Services.AddCors(options => 
{
    options.AddPolicy("AngularApp", policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    });
});

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



app.UseCors("AngularApp");


app.Run();
