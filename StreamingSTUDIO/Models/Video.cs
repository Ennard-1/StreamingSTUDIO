namespace StreamingSTUDIO.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public string NomeArquivo { get; set; }
        public string Thumbnail { get; set; } // Thumbnail como string (URL)
        public int UsuarioID { get; set; }
    }
}
