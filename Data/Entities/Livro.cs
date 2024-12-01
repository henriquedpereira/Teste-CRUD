namespace Core.Entities
{
    public class Livro
    {
        public int CodL { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicao { get; set; }

        public Livro(int codl, string titulo, string editora, int edicao, string anoPublicao)
        {
            CodL = codl;
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicao = anoPublicao;
        }
    }
}