using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Data;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuizenRepositoryEF : IHuizenRepository
    {
        private ParkContext ctx = new ParkContext();

        public Huis GeefHuis(int id)
        {
            try
            {
                HuisEF huisEF = ctx.Huizen.Include(h => h.Park).FirstOrDefault(h => h.Id == id);
                return HuisMapper.ToHuis(huisEF);
            }
            catch (Exception ex)
            {

                throw new Exception("GeefHuis", ex);
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            try
            {
                // LINQ query to find the HuisEF entity
                var huis = ctx.Huizen
                             .FirstOrDefault(h => h.Straat == straat && h.Nr == nummer && h.Park.Naam == park.Naam);

                // Return true if a matching HuisEF entity is found, false otherwise
                return huis != null;

            }
            catch (Exception ex)
            {

                throw new DataException("Heefthuis",ex);
            }
        }

        public bool HeeftHuis(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateHuis(Huis huis)
        {
            throw new NotImplementedException();
        }

        public Huis VoegHuisToe(Huis h)
        {
            try
            {
                if (h == null) throw new DataException("VoegHuisToe: huis is null");
                HuisEF huis = HuisMapper.ToHuisEF(h);
                ParkEF existingPark = ctx.Parken.Find(h.Park.Id);
                if (existingPark != null)
                {
                    // Use the existing tracked Park instance
                    huis.Park = existingPark;
                }

                ctx.Huizen.Add(huis);
                ctx.SaveChanges();
                return h;
            }
            catch (Exception ex)
            {

                throw new DataException("VoegHuisToe", ex);
            }
        }
    }
}
