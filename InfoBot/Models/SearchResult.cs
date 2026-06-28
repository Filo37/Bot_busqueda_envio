namespace InfoBot.Models
{
    public class SearchResult
    {
        public string Titulo { get; set; } = "";
        public string Resumen { get; set; } = "";
        public string Url { get; set; } = "";
        public string Fuente { get; set; } = "";   // DuckDuckGo o Wikipedia
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
    }
}
