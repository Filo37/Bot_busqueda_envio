using System;
using System.Threading.Tasks;
using InfoBot.Data;
using InfoBot.Models;
using InfoBot.Services;

namespace InfoBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var db = new DatabaseHelper();
            var wiki = new WikipediaService();

            while (true)
            {
                Console.WriteLine("=== Bot de Información ===");
                Console.WriteLine("1. Buscar información en Wikipedia");
                Console.WriteLine("2. Ver registros guardados");
                Console.WriteLine("3. Eliminar un registro");
                Console.WriteLine("4. Buscar en Internet");
                Console.WriteLine("0. Salir");
                Console.Write("Elige una opción: ");
                string? opcion = Console.ReadLine();

                if (opcion == "1")
                {
                    Console.Write("Tema: ");
                    string? tema = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(tema))
                    {
                        Console.WriteLine("Tema no válido.");
                        continue;
                    }

                    Console.WriteLine("\nBuscando información...");
                    var resultado = await wiki.BuscarResumenAsync(tema.Trim());

                    Console.WriteLine("\n=== Resumen ===");
                    Console.WriteLine(resultado.Resumen);

                    Console.WriteLine("\n¿Guardar en la base de datos? (s/n)");
                    if (Console.ReadLine()?.Trim().ToLower() == "s")
                    {
                        db.Guardar(resultado);
                        Console.WriteLine("✅ Guardado.");
                    }
                }
                else if (opcion == "2")
                {
                    db.MostrarRegistros();
                }
                else if (opcion == "3")
                {
                    Console.Write("ID a eliminar: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        db.Eliminar(id);
                    }
                    else
                    {
                        Console.WriteLine("ID no válido.");
                    }
                }
                else if (opcion == "4")
                {
                    Console.Write("Consulta: ");
                    string? consulta = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(consulta))
                    {
                        Console.WriteLine("Consulta no válida.");
                        continue;
                    }

                    Console.WriteLine("\nBuscando en Internet...");
                    var servicio = new WebSearchService();
                    var resultado = await servicio.BuscarAsync(consulta.Trim());

                    Console.WriteLine("\n=== Resultado Web ===");
                    Console.WriteLine($"Título: {resultado.Titulo}");
                    Console.WriteLine($"Resumen: {resultado.Resumen}");
                    Console.WriteLine($"URL: {resultado.Url}");
                    Console.WriteLine($"Fuente: {resultado.Fuente}");
                    Console.WriteLine($"Fecha: {resultado.FechaConsulta}");

                    Console.WriteLine("\n¿Guardar en la base de datos? (s/n)");
                    if (Console.ReadLine()?.Trim().ToLower() == "s")
                    {
                        db.Guardar(resultado);
                        Console.WriteLine("✅ Guardado.");
                    }
                }
                else if (opcion == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Opción no válida.");
                }

                Console.WriteLine();
            }
        }
    }
}

