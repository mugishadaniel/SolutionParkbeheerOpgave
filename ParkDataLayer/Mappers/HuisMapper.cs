using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class HuisMapper
    {
        public static Huis ToHuis(HuisEF huisEF)
        {
            Huis huis = new Huis(huisEF.Id,huisEF.Straat,huisEF.Nr,huisEF.Actief,ParkMapper.ToPark(huisEF.Park));
            foreach (var item in huisEF._huurcontracten)
            {
                huis.VoegHuurcontractToe(HuurcontractMapper.ToHuurcontractHuis(item));
            }
            return huis;
        }

        public static Huis ToHuisPark(HuisEF huisEF)
        {
            return new Huis(huisEF.Id, huisEF.Straat, huisEF.Nr, huisEF.Actief, null);
        }

        public static HuisEF ToHuisEF(Huis huis)
        {
            return new HuisEF() { 
                Id = huis.Id,
                Straat = huis.Straat,
                Nr = huis.Nr,
                Actief = huis.Actief,
                Park = ParkMapper.ToParkEF(huis.Park),               
            };
        }

        public static void UpdateHuisEF(HuisEF huisEF, Huis huis)
        {
            huisEF.Id = huis.Id;
            huisEF.Straat = huis.Straat;
            huisEF.Nr = huis.Nr;
            huisEF.Actief = huis.Actief;
        }
    }
}
