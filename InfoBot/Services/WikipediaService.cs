using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using InfoBot.Models;

namespace InfoBot.Services
{
    public class WikipediaService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<InfoRecord> BuscarResumenAsync(string tema)
        {
            if (string.IsNullOrWhiteSpace(tema))
            {
                return new InfoRecord { Resumen = "Tema no válido." };
            }

            string encodedTema = Uri.EscapeDataString(tema.Trim());
            string url = $"https://es.wikipedia.org/api/rest_v1/page/summary/{encodedTema}";

            // Configurar un User-Agent válido
            client.DefaultRequestHeaders.UserAgent.Clear();
            client.DefaultRequestHeaders.UserAgent.ParseAdd("InfoBot/1.0 (contact: david.sanabria@example.com)");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new InfoRecord { Resumen = $"Error HTTP {(int)response.StatusCode}: {response.ReasonPhrase}" };
                }

                string result = await response.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(result);

                string? extracto = json["extract"]?.ToString();
                string? titulo = json["title"]?.ToString();
                string? urlWiki = json["content_urls"]?["desktop"]?["page"]?.ToString();

                return new InfoRecord
                {
                    Titulo = titulo ?? tema.Trim(),
                    Resumen = !string.IsNullOrWhiteSpace(extracto) ? extracto : "No se encontró información.",
                    Url = urlWiki ?? $"https://es.wikipedia.org/wiki/{encodedTema}",
                    Fuente = "Wikipedia",
                    FechaConsulta = DateTime.Now
                };
            }
            catch (HttpRequestException ex)
            {
                return new InfoRecord { Resumen = $"Error de red: {ex.Message}" };
            }
            catch (Exception ex)
            {
                return new InfoRecord { Resumen = $"Error al procesar la respuesta: {ex.Message}" };
            }
        }
    }
}
