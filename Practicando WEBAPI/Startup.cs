using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Practicando_WEBAPI.Data;
using Practicando_WEBAPI.Data.Repository;
using Practicando_WEBAPI.Services;

namespace Practicando_WEBAPI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IBreedsService, BreedsService>();
            services.AddTransient<IHeroService, HeroService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<ILibraryRepository, LibraryRepository>();
            services.AddScoped<IUserService, UserService>();

            //ENTITY FRAMEWORK
            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("WarcraftConnecction"));
            });
            //SEGURIDAD
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {   options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<LibraryDbContext>()
          .AddDefaultTokenProviders();

            //JWT  config
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true
                };
            });


            //AUTOMAPPER
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options => { options.AllowAnyOrigin(); options.AllowAnyMethod(); options.AllowAnyHeader(); });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}