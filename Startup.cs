using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Net5CoreMvc
{
    public class Startup
    {
        

       

        
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();
            app.Map("/titik", routeRewrite);
            app.Run(async context =>
            {
                Console.WriteLine("entered route:"+context.Request.Path.ToString());
                Console.WriteLine("pipeline did not branch");
                context.Response.Redirect("/titik");
                
            });
            /*app.Use((context,next) =>
            {

                Console.WriteLine(context.Request.Path.ToString());

                if (context.Request.Path.ToString() != "/titik")
                {
                    app.Run(context =>
                    {
                        context.Response.Redirect("/titik");
                        return null;
                    });
                    Console.WriteLine("inside route manipulator middleware");
                    Console.WriteLine("original route:" + context.Request.Path.ToString());
                    context.Response.Redirect("/titik");

                    Console.WriteLine("redirecting");

                    context.Request.Path = "/titik";
                    Console.WriteLine("manipulated route:" + context.Request.Path.ToString());

                }
                return next();
            });*/
            
        }

        private void routeRewrite(IApplicationBuilder app)
        {
            app.Use((context,next)=>
            {
                Console.WriteLine("inside new branch");
                return next();
            });
            app.UseMvc();
        }
    }
}
