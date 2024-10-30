using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace AppWebEspagueti
{
    // Interfaz de estrategia para las acciones
    public interface IActionStrategy
    {
        string Execute(string parameter = null);
    }

    // Estrategia para listar productos
    public class ListAction : IActionStrategy
    {
        public string Execute(string parameter = null)
        {
            return "Listado de celulares: iPhone, Samsung, Xiaomi";
        }
    }

    // Estrategia para realizar la compra de un producto
    public class BuyAction : IActionStrategy
    {
        public string Execute(string parameter)
        {
            return $"Has comprado: {parameter}";
        }
    }

    // Estrategia para la acción de bienvenida
    public class WelcomeAction : IActionStrategy
    {
        public string Execute(string parameter = null)
        {
            return "Bienvenido a la tienda de celulares. Usa '?action=list' para ver productos.";
        }
    }

    // Fábrica para seleccionar la estrategia adecuada según la acción
    public class ActionFactory
    {
        public static IActionStrategy GetActionStrategy(string action)
        {
            return action switch
            {
                "list" => new ListAction(),
                "buy" => new BuyAction(),
                _ => new WelcomeAction(),
            };
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://0.0.0.0:5000");
                    webBuilder.Configure(app =>
                    {
                        app.Run(async context =>
                        {
                            // Obtiene la acción y parámetro de la URL
                            string action = context.Request.Query["action"];
                            string parameter = context.Request.Query["item"];

                            // Usa la fábrica para obtener la estrategia correcta y ejecutar la acción
                            var actionStrategy = ActionFactory.GetActionStrategy(action);
                            string response = actionStrategy.Execute(parameter);

                            await context.Response.WriteAsync(response);
                        });
                    });
                })
                .Build();

            host.Run();
        }
    }
}
