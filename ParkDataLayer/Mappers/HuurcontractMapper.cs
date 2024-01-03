using ParkBusinessLayer.Model;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Mappers
{
    public class HuurcontractMapper
    {
        public static Huurcontract ToHuurcontract(HuurcontractEF huurcontractEF)
        {
            return new Huurcontract(
                huurcontractEF.Id,
                new Huurperiode(huurcontractEF.StartDatum, huurcontractEF.Aantaldagen),
                HuurderMapper.ToHuurder(huurcontractEF.Huurder),
                HuisMapper.ToHuis(huurcontractEF.Huis)
                );
        }
        public static HuurcontractEF ToHuurcontractEF(Huurcontract huurcontract)
        {
            return new HuurcontractEF()
            {
                Id = huurcontract.Id,
                StartDatum = huurcontract.Huurperiode.StartDatum,
                EindDatum = huurcontract.Huurperiode.EindDatum,
                Aantaldagen = huurcontract.Huurperiode.Aantaldagen,
                Huurder = HuurderMapper.ToHuurderEF(huurcontract.Huurder),
                Huis = HuisMapper.ToHuisEF(huurcontract.Huis),
            };
        }
        
        public static void UpdateHuurcontractEF(HuurcontractEF huurcontractEF, Huurcontract huurcontract)
        {
            huurcontractEF.Id = huurcontract.Id;
            huurcontractEF.StartDatum = huurcontract.Huurperiode.StartDatum;
            huurcontractEF.EindDatum = huurcontract.Huurperiode.EindDatum;
            huurcontractEF.Aantaldagen = huurcontract.Huurperiode.Aantaldagen;
            huurcontractEF.Huurder = HuurderMapper.ToHuurderEF(huurcontract.Huurder);
            huurcontractEF.Huis = HuisMapper.ToHuisEF(huurcontract.Huis);
        }
    }
}
