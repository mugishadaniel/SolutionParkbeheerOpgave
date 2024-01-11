using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class HuisEF
    {
        public HuisEF(string straat, int nr, bool actief)
        {
            Straat = straat;
            Nr = nr;
            Actief = actief;
        }

        public HuisEF()
        {
        }
 
        public int Id { get;  set; }
        [MaxLength(250)]
        public string Straat { get;  set; }
        [Required]
        public int Nr { get;  set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Actief { get; set; }
        public ParkEF Park { get;  set; }
        public List<HuurcontractEF> _huurcontracten { get; set; }
    }
}
