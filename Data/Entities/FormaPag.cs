namespace Core.Entities
{
    public class FormaPag
    {
        public int CodForm { get; set; }
        public string Descricao { get; set; }      

        public FormaPag(int codform, string descricao)
        {
            CodForm = codform;
            Descricao = descricao;
        }
    }
}