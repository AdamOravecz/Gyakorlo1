using Microsoft.EntityFrameworkCore;
using Oravecz_Adam_BackEnd.Models;

namespace Oravecz_Adam_BackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(c => { c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
            builder.Services.AddControllers();

            // Register EF Core DbContext with a connection string from configuration
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                   ?? "server=localhost;database=librarydb;user=root;password=;";
            builder.Services.AddDbContext<LibrarydbContext>(options =>
                options.UseMySQL(connectionString)); // Ensure provider matches installed package

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

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}