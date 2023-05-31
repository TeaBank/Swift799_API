
using Swift799_API.Helpers;
using Swift799_API.Helpers.Contracts;
using Swift799_API.Services;
using Swift799_API.Services.Contracts;
using Swift799_API.SwaggerFilters;

namespace Swift799_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => options.OperationFilter<AddMessageFilter>());

            //DI for Helpers
            builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();

            //DI for Services
            builder.Services.AddScoped<IMessagesService, MessagesService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}