namespace InfoBot.Models
{
    public class InfoRecord
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Resumen { get; set; } = "";
        public string Url { get; set; } = "";
        public string Fuente { get; set; } = "";   // Wikipedia o Internet
        public DateTime FechaConsulta { get; set; } = DateTime.Now;
    }
}
