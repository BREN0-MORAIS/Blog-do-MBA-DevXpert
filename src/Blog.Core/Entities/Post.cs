namespace Blog.Core.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Conteudo { get; set; }
        public DateTime Data { get; set; }
        public string AutorId { get; set; }
        public Autor Autor { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
    }
}
