using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.Exceptions;
using ParkDataLayer.Mappers;
using ParkDataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class HuurderRepositoryEF : IHuurderRepository
    {
        private ParkContext ctx = new ParkContext();

        public Huurder GeefHuurder(int id)
        {
            try
            {
               HuurderEF huurderEF = ctx.Huurders.Find(id);
               return HuurderMapper.ToHuurder(huurderEF);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Huurder niet gevonden",ex);
            }   
        }

        public List<Huurder> GeefHuurders(string naam)
        {
            try
            {
                List<Huurder> huurders = new List<Huurder>();
                List<HuurderEF> huurdersEF = ctx.Huurders.Where(h => h.Naam == naam).ToList();
                foreach (HuurderEF huurderEF in huurdersEF)
                {
                    huurders.Add(HuurderMapper.ToHuurder(huurderEF));
                }
                return huurders;
                
            }
            catch (Exception ex)
            {

                throw new RepositoryException("Huurders niet gevonden", ex);
            }
        }

        public bool HeeftHuurder(string naam, Contactgegevens contact)
        {
            try
            {
                return ctx.Huurders.Any(h => h.Naam == naam);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("Invalid Huurder",ex);
            }
        }

        public bool HeeftHuurder(int id)
        {
            try
            {
                return ctx.Huurders.Any(h => h.Id == id);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("Heefthuurder", ex);
            }
        }

        public void UpdateHuurder(Huurder huurder)
        {
            try
            {
                HuurderEF huurderEF = ctx.Huurders.FirstOrDefault(h => h.Id == huurder.Id);
                huurderEF.Naam = huurder.Naam;
                huurderEF.Adres = huurder.Contactgegevens.Adres;
                huurderEF.Email = huurder.Contactgegevens.Email;
                huurderEF.Tel = huurder.Contactgegevens.Tel;
                ctx.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new RepositoryException("UpdateHuurder",ex);
            }
        }

        public Huurder VoegHuurderToe(Huurder h)
        {
            try
            {
                HuurderEF huurderEF = HuurderMapper.ToHuurderEF(h);
                ctx.Huurders.Add(huurderEF);
                return h;
            }
            catch (Exception ex)
            {

                throw new RepositoryException("VoegHuurder",ex);
            }
        }
    }
}
