
namespace Core.Entities
{
    public class Livro
    {
        public int CodL { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }

        public List<SelectListItem> ListaAssuntos { get; set; }
        public List<SelectListItem> ListaAutores { get; set; }
        public List<SelectListItem> ListaFormasPag { get; set; }

        public string? Assuntos { get; set; }
        public string? Autores { get; set; }
        public string? FormasPagamento { get; set; }


        public Livro(int codl, string titulo, string editora, int edicao, string anoPublicacao)
        {
            CodL = codl;
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
        }

        public Livro(int codl, string titulo, string editora, int edicao, string anoPublicacao, string? assuntos, string? autores, string? formasPagamento)
        {
            CodL = codl;
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
            Assuntos = assuntos;
            Autores = autores;
            FormasPagamento = formasPagamento;
        }
    }
}