using Dommel;

namespace Core.Entities
{
    public class SelectListItem
    {
        public string text { get; set; }
        public string value { get; set; }

        [Ignore]
        public int? value2 { get; set; }
        public bool selected { get; set; }
    }
}