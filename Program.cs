using ex3___ApiLoginECors.Data;
using ex3___ApiLoginECors.Interfaces;
using ex3___ApiLoginECors.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:5038", "https://localhost:5038");

    });
});


builder.Services.AddDbContext<AppData>(options =>
{
   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 

});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();