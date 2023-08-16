using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Hospital_System
{
    public class Program
    {

        //Required Packages : 
        //a- Microsoft.EntityFrameworkCore.SqlServer(7.0.9)
        //b- Microsoft.EntityFrameworkCore.Tools(7.0.9)
        //c- Microsoft.VisualStudio.Web.CodeGeneration(7.0.8)
        //d- Microsoft.VisualStudio.Web.CodeGeneration.Design(7.0.8)
        //E- Microsoft.EntityFrameworkCore.Sqlite
        //F- Microsoft.AspNetCore.Mvc.NewtonsoftJson
        //G- Microsoft.AspNetCore.Identity.EntityFrameworkCore
        //H- Microsoft.AspNetCore.Authentication.JwtBearer
        //I- Swashbuckle.AspNetCore


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services
                .AddDbContext<HospitalDbContext>
            (opions => opions.UseSqlServer(connString));

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()

                {
                    Title = "Hospital-System",
                    Version = "v1",


                }


                    );



            });

            builder.Services.AddTransient<IHospital, HospitalService>();
            builder.Services.AddTransient<IDepartment, DepartmentService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IAppointment, AppointmentService>();
            builder.Services.AddTransient<IMedicalReport, MedicalReportService>();
            builder.Services.AddTransient<IMedicine, MedicineService>();
            builder.Services.AddTransient<INurse, NurseService>();
            builder.Services.AddTransient<IDoctor, DoctorService>();
            builder.Services.AddTransient<IPatient, PatientService>();



            var app = builder.Build();


            app.UseSwagger(options => {

                options.RouteTemplate = "/api/{documentName}/swagger.json";


            });


            app.UseSwaggerUI(options => {

                options.SwaggerEndpoint("/api/v1/swagger.json", "Hospital-System");
                options.RoutePrefix = "docs";


            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.MapRazorPages();

            app.Run();
        }
    }
}