using Hospital_System.Data;
using Hospital_System.Models;
using Hospital_System.Models.Interfaces;
using Hospital_System.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hospital_System
{
    public class Program
    {
        public IConfiguration Configuration { get; }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureAppServices(builder.Services, builder.Configuration);
            var app = builder.Build();

            ConfigureApp(app);

            app.Run();
        }

        private static void ConfigureAppServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            string connString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HospitalDbContext>(options => options.UseSqlServer(connString));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hospital-System",
                    Version = "v1",
                });
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<HospitalDbContext>();

            services.AddScoped<JwtTokenService>();

            // Add other services
            services.AddTransient<IHospital, HospitalService>();
            services.AddTransient<IDepartment, DepartmentService>();
            services.AddTransient<IRoom, RoomService>();
            services.AddTransient<IAppointment, AppointmentService>();
            services.AddTransient<IMedicalReport, MedicalReportService>();
            services.AddTransient<IMedicine, MedicineService>();
            services.AddTransient<INurse, NurseService>();
            services.AddTransient<IDoctor, DoctorService>();
            services.AddTransient<IPatient, PatientService>();
            services.AddTransient<IUser, IdentityUserService>();


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtTokenService.GetValidationPerameters(configuration);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
                options.AddPolicy("deposit", policy => policy.RequireClaim("permissions", "deposit"));
            });
        }

        private static void ConfigureApp(WebApplication app)
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Hospital-System");
                options.RoutePrefix = "Docs";
            });

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Main}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
