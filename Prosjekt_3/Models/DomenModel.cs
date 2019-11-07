using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prosjekt_3.Models
{
    public class spørsmåls
    {


        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ. \\-]{2,200}$")]
        public string spørmål { get; set; }
        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ. \\-]{2,200}$")]
        public string svar { get; set; }
        [Required]
        [RegularExpression("^[0-9]$")]
        public int rating { get; set; }
        [Required]
        [RegularExpression("^[0-9]$")]
        public int stemmer { get; set; }
        [Required]
        [RegularExpression("^[0-9]$")]
        public int TypeId { get; set; }

    }
    public class typeSp
    {
        public int TypeId { get; set; }
        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ. \\-]{2,40}$")]
        public string type { get; set; }
        public List<spørsmåls> spørmål { get; set; }
    }
}
