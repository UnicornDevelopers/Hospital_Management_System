using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.DTOs;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<HospitalDbContext>();

            builder.Services.AddScoped<JwtTokenService>();

            builder.Services.AddTransient<IHospital, HospitalService>();
            builder.Services.AddTransient<IDepartment, DepartmentService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IAppointment, AppointmentService>();
            builder.Services.AddTransient<IMedicalReport, MedicalReportService>();
            builder.Services.AddTransient<IMedicine, MedicineService>();
            builder.Services.AddTransient<INurse, NurseService>();
            builder.Services.AddTransient<IDoctor, DoctorService>();
            builder.Services.AddTransient<IPatient, PatientService>();
            builder.Services.AddTransient<IUser, IdentityUserService>();



            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                // Tell the authenticaion scheme "how/where" to validate the token + secret
                options.TokenValidationParameters = JwtTokenService.GetValidationPerameters(builder.Configuration);
            });


            builder.Services.AddAuthorization(options =>
            {
                // Add "Name of Policy", and the Lambda returns a definition
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
                options.AddPolicy("deposit", policy => policy.RequireClaim("permissions", "deposit"));
            });


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