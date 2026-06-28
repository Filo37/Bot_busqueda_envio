using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using InfoBot.Models;

namespace InfoBot.Services
{
    public class WebSearchService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<InfoRecord> BuscarAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new InfoRecord { Resumen = "Consulta no válida." };

            string url = $"https://api.duckduckgo.com/?q={Uri.EscapeDataString(query)}&format=json";

            try
            {
                string result = await client.GetStringAsync(url);
                JObject json = JObject.Parse(result);

                string titulo = json["Heading"]?.ToString() ?? query;
                string resumen = json["AbstractText"]?.ToString() ?? "";
                string enlace = json["AbstractURL"]?.ToString() ?? "";

                // Si no hay resumen, intentar con RelatedTopics
                if (string.IsNullOrWhiteSpace(resumen))
                {
                    var related = json["RelatedTopics"]?.FirstOrDefault();
                    if (related != null)
                    {
                        titulo = related["Text"]?.ToString() ?? query;
                        enlace = related["FirstURL"]?.ToString() ?? "";
                        resumen = "Información relacionada encontrada.";
                    }
                }

                // Fallback a Wikipedia si sigue vacío
                if (string.IsNullOrWhiteSpace(resumen))
                {
                    var wiki = new WikipediaService();
                    var wikiRes = await wiki.BuscarResumenAsync(query);
                    return wikiRes;
                }

                return new InfoRecord
                {
                    Titulo = string.IsNullOrWhiteSpace(titulo) ? query : titulo,
                    Url = enlace,
                    Resumen = string.IsNullOrWhiteSpace(resumen) ? "No se encontró información." : resumen,
                    Fuente = "DuckDuckGo",
                    FechaConsulta = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                return new InfoRecord { Resumen = $"Error en la búsqueda: {ex.Message}" };
            }
        }
    }
}
