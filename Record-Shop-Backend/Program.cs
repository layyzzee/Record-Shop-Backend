using Microsoft.EntityFrameworkCore;
using Record_Shop_Backend.MVC_Repositories;
using Record_Shop_Backend.MVC_Services;
using Record_Shop_Backend.Data;

namespace Record_Shop_Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AlbumDbContext>(options => options.UseSqlServer(connectionString));
            //Need to avoid duplicates being added

            builder.Services.AddScoped<IAlbumService, AlbumService>();
            builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
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
        }
    }
}
