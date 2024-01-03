using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ParkEF
    {
        public ParkEF(string id, string naam, string locatie)
        {
            Id = id;
            Naam = naam;
            Locatie = locatie;
        }

        public ParkEF()
        {
        }

        [MaxLength(20)]
        public string Id { get;  set; }

        [Required]
        [MaxLength(250)]
        public string Naam { get;  set; }

        [MaxLength(500)]
        public string Locatie { get;  set; }

        public List<HuisEF> _huis { get;  set;}
    }
}
