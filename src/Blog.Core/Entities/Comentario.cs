namespace Blog.Core.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public string Conteudo { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string AutorId { get; set; }
        public Autor Autor { get; set; }
    }
}
