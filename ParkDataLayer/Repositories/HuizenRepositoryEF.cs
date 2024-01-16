using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.Exceptions;
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
                HuisEF huisEF = ctx.Huizen
                    .Include(h => h.Park)
                    .ThenInclude(p => p._huis)
                    .Include(h => h._huurcontracten)
                    .ThenInclude(hc => hc.Huurder)
                    .FirstOrDefault(h => h.Id == id);
                
                return HuisMapper.ToHuis(huisEF);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("GeefHuis", ex);
            }
        }

        public bool HeeftHuis(string straat, int nummer, Park park)
        {
            try
            {
                var huis = ctx.Huizen
                             .FirstOrDefault(h => h.Straat == straat && h.Nr == nummer && h.Park.Naam == park.Naam);

                return huis != null;

            }
            catch (Exception ex)
            {

                throw new RepositoryException("Heefthuis",ex);
            }
        }

        public bool HeeftHuis(int id)
        {
            try
            {
                HuisEF huisEF = ctx.Huizen.FirstOrDefault(h => h.Id == id);
                return huisEF != null;
            }
            catch (Exception ex)
            {

                throw new RepositoryException("Heefthuis", ex);
            }
        }

        public void UpdateHuis(Huis huis)
        {
            try
            {
                if (huis == null) throw new RepositoryException("UpdateHuis: huis is null");
                HuisEF huisEF = ctx.Huizen.Find(huis.Id);
                if (huisEF == null) throw new RepositoryException("UpdateHuis: huisEF is null");

                if (huis.Park != null && huis.Park.Id != null)
                {
                    ParkEF existingPark = ctx.Parken.Find(huis.Park.Id);
                    if (existingPark != null)
                    {
                        huisEF.Park = existingPark;
                    }
                }
                HuisMapper.UpdateHuisEF(huisEF, huis);
                ctx.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new RepositoryException("Updatehuis", ex);
            }
        }

        public Huis VoegHuisToe(Huis h)
        {
            try
            {
                if (h == null) throw new RepositoryException("VoegHuisToe: huis is null");
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

                throw new RepositoryException("VoegHuisToe", ex);
            }
        }
    }
}
