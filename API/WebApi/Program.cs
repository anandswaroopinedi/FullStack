using AspWebApi.Controllers;
using AspWebApi.Validations;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Managers;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Services;
using FluentValidation.AspNetCore;
using Models;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
public class Program
{
    public static void Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                   ValidateIssuerSigningKey=true
                };

            });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                  .AllowCredentials());
        });
        // Add services to the container.
        builder.Services.AddTransient<IEmployeeManager, EmployeeManager>();
        builder.Services.AddTransient<IStatusManager, StatusManager>();
        builder.Services.AddTransient<IRoleManager, RoleManager>();

        builder.Services.AddTransient<IDepartmentManager, DepartmentManager>();
        builder.Services.AddTransient<ILocationManager, LocationManager>();
        builder.Services.AddTransient<EmployeeDirectoryContext, EmployeeDirectoryContext>();
        builder.Services.AddTransient<IProjectManager, ProjectManager>();

        builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddTransient<IProjectRepository, ProjectRepository>();
        builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
        builder.Services.AddTransient<ILocationRepository, LocationRepository>();
        builder.Services.AddTransient<IRoleDepLocLinkRepository, RoleDepLocLinkRepository>();
        builder.Services.AddTransient<IRoleRepository, RoleRepository>();
        builder.Services.AddTransient<IStatusRepository, StatusRepository>();
        builder.Services.AddControllers();
        builder.Services.AddControllers().AddFluentValidation(fv => {
            fv.RegisterValidatorsFromAssemblyContaining<Employee>();
        });        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(AutoMapperConfig));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseCors("AllowSpecificOrigin");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.MapControllers();

        app.Run();
    }
}
