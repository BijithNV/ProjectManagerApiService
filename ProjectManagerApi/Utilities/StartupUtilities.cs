using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagerBusinessLayer;
using ProjectManagerDAL;
using ProjectManagerEntities;

namespace ProjectManagerApiService.Utilities
{
    public static class StartupUtilities
    {
        public static IApplicationBuilder RouteConfiguration(this IApplicationBuilder app)
        {
            return app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "swagger", action = "" });
            });
        }

        public static IApplicationBuilder DatabaseMigrate(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ProjectManagerContext>();
                context.Database.EnsureCreated();
            }

            return app;
        }

        public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<IProjectManagerContext, ProjectManagerContext>();
            services.AddTransient<IProjectMangerBL<Project>, ProjectBL>();
            services.AddTransient<IProjectMangerBL<User>, UserBL>();
            services.AddTransient<ITaskBL, TaskBL>();

            return services;
        }
    }
}
