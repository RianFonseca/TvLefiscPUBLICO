namespace TvLefisc.Models
{
    public class Tv_Lefisc
    {
        public int idTv { get; set; }
        public string? titulo { get; set; }
        public string? descricao { get; set; }
        public string? link { get; set; }
        public int visualizacoes { get; set; }
        public string? miniatura { get; set; }
        public DateTime data { get; set; }
        public int idCategoria { get; set; }
    }
}
