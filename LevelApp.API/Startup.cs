using LevelApp.API.Middleware;
using LevelApp.BLL.Base;
using LevelApp.BLL.Base.Executor;
using LevelApp.DAL.DataAccess;
using LevelApp.DAL.Repositories.User;
using LevelApp.DAL.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace LevelApp.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Swagger service
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "LevelApp API", Version = "v1"}); });

            // Database context service
            services.AddDbContext<MainContext>(options =>
                options.UseMySql(Configuration["ConnectionStrings:DevConnection"]));

            // DI container (DAL)
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUserRepository, UserRepository>();

            // DI container (BLL)
            services.AddTransient<IOperationExecutor, OperationExecutor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            // Exception handler
            app.ConfigureCustomExceptionMiddleware();

            app.UseHttpsRedirection();
            app.UseMvc();

            //Swagger setup
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "LevelApp API V1"); });
        }
    }
}
