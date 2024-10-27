using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AppWebEspagueti
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.Configure(app =>
                    {
                        app.Run(async context =>
                        {
                            string response = "";
                            string action = context.Request.Query["action"];
                            if (action == "list")
                            {
                                response = "Listado de celulares: iPhone, Samsung, Xiaomi";
                            }
                            else if (action == "buy")
                            {
                                string item = context.Request.Query["item"];
                                response = $"Has comprado: {item}";
                            }
                            else
                            {
                                response = "Bienvenido a la tienda de celulares. Usa '?action=list' para ver productos.";
                            }
                            await context.Response.WriteAsync(response);
                        });
                    });
                })
                .Build();

            host.Run();
        }
    }
}
