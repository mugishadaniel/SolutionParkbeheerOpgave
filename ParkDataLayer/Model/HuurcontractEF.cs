using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class HuurcontractEF
    {

        public string Id { get;  set; }

        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        [Required]
        public int Aantaldagen { get; set; }
        public HuurderEF Huurder { get;  set; }
        public HuisEF Huis { get;  set; }
    }
}
