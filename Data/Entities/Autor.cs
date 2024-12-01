namespace Core.Entities
{
    public class Autor
    {
        public int CodAu { get; set; }
        public string Nome { get; set; }      

        public Autor(int codau, string nome)
        {
            CodAu = codau;
            Nome = nome;
        }
    }
}