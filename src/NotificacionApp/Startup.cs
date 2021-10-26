using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NotificacionApp.Common;
using NotificacionApp.Databases;
using NotificacionApp.Databases.DummyData;
using NotificacionApp.Repositories;
using NotificacionApp.Services;

namespace NotificacionApp
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private const string LocalhostSpecificOrigins = "_localhostSpecificOrigins";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="configuration">Configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configurations.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("NotificacionDb");

            services.AddTransient<ITeacherRepository, TeacherDbRepository>();
            services.AddTransient<ITeacherService, TeacherService>();

            services.AddTransient<IStudentRepository, StudentDbRepository>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddTransient<IQuestionRepository, QuestionDbRepository>();
            services.AddTransient<IQuestionService, QuestionService>();


            services.AddTransient<ISecurityService, SecurityService>();
            services.AddSingleton<ISessionManager, SessionManager>();

            services.AddDbContext<NotificacionDbContext>(options => options.UseSqlServer(connectionString));
            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => $"{type.Name}_{System.Guid.NewGuid()}");
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificacionApp", Version = "v1" });
                options.OperationFilter<SecurityTokenOperationFilter>();
            });

            services.AddAuthentication("x")
                         .AddScheme<SecurityTokenSchemeOptions, SecurityTokenAuthenticationHandler>("x", null);

            services.AddCors(options =>
            {
                options.AddPolicy(name: LocalhostSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                           .AllowAnyMethod()
                                           .AllowAnyHeader();
                                  });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <param name="env"><see cref="IWebHostEnvironment"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotificacionApp v1"));
            }

            app.UseDummyData();

            app.UseStaticFiles();
            app.UseCors(LocalhostSpecificOrigins);
            app.UseHttpsRedirection();
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
