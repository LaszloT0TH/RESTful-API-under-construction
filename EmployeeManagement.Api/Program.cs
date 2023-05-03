using EmployeeManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

            // EN
            // We are using AddScoped() method because we want the instance to be alive and available for the entire scope of the given HTTP request. For another new HTTP request, a new instance of EmployeeRepository class will be provided and it will be available throughout the entire scope of that HTTP request.
            // GE
            // Wir verwenden die AddScoped()-Methode, weil wir m�chten, dass die Instanz aktiv und f�r den gesamten Bereich der angegebenen HTTP-Anforderung verf�gbar ist. F�r eine weitere neue HTTP-Anforderung wird eine neue Instanz der EmployeeRepository-Klasse bereitgestellt, die im gesamten Bereich dieser HTTP-Anforderung verf�gbar ist.
            // HU
            // Haszn�ljuk AddScoped() met�dust, mert azt szeretn�nk, hogy a p�ld�ny �l� �s el�rhet� legyen az adott HTTP-k�r�s teljes hat�k�r�ben. Egy m�sik �j HTTP-k�r�s eset�n a EmployeeRepository class lesz megadva, �s a HTTP-k�r�s teljes hat�k�r�ben el�rhet� lesz.
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

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