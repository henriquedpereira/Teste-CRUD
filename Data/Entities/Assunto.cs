namespace Core.Entities
{
    public class Assunto
    {
        public int CodAs { get; set; }
        public string Descricao { get; set; }      

        public Assunto(int codas, string descricao)
        {
            CodAs = codas;
            Descricao = descricao;
        }
    }
}