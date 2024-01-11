using Microsoft.EntityFrameworkCore;
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
    public class ContractenRepositoryEF : IContractenRepository
    {
        private ParkContext ctx = new ParkContext();

        public void AnnuleerContract(Huurcontract contract)
        {
            try
            {
                HuurcontractEF huurcontractEF = ctx.Huurcontracten.Find(contract.Id);
                ctx.Huurcontracten.Remove(huurcontractEF);
                ctx.Huurders.Remove(huurcontractEF.Huurder);
                ctx.Huizen.Remove(huurcontractEF.Huis);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new RepositoryException("AnnuleerContract", ex);
            }
        }

        public Huurcontract GeefContract(string id)
        {
            try
            {
                HuurcontractEF contract = ctx.Huurcontracten
                    .Include(c => c.Huurder)
                    .Include(c => c.Huis)
                    .ThenInclude(h => h.Park)
                    .FirstOrDefault(c => c.Id == id);

                return HuurcontractMapper.ToHuurcontract(contract);
            }
            catch (Exception ex)
            {

                throw new RepositoryException("GeefContract", ex);
            }

        }

        public List<Huurcontract> GeefContracten(DateTime dtBegin, DateTime? dtEinde)
        {
            try
            {
                if (dtEinde == null)
                {
                    return ctx.Huurcontracten.Where(h => h.StartDatum >= dtBegin).Select(h => HuurcontractMapper.ToHuurcontract(h)).ToList();
                }
                else
                {
                    return ctx.Huurcontracten.Where(h => h.StartDatum >= dtBegin && h.StartDatum <= dtEinde).Select(h => HuurcontractMapper.ToHuurcontract(h)).ToList();
                }
            }
            catch (Exception ex)
            {

                throw new RepositoryException("GeefContracten", ex);
            }
        }

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid)
        {
            try
            {
                HuurcontractEF contract = ctx.Huurcontracten.FirstOrDefault(h => h.Huurder.Id == huurderid && h.Huis.Id == huisid && h.StartDatum == startDatum);
                return contract != null;
            }
            catch (Exception ex)
            {

                throw new RepositoryException("HeeftContract", ex);
            }
        }

        public bool HeeftContract(string id)
        {
            try
            {
                HuurcontractEF contract = ctx.Huurcontracten.Find(id);
                return contract != null;
            }
            catch (Exception ex)
            {

                throw new RepositoryException("HeeftContract", ex);
            }
        }

        public void UpdateContract(Huurcontract contract)
        {
            try
            {
                HuurcontractEF huurcontractEF = ctx.Huurcontracten.FirstOrDefault(h => h.Id == contract.Id);
                if (huurcontractEF == null) throw new RepositoryException("UpdateContract: huurcontractEF is null");
                HuurcontractMapper.UpdateHuurcontractEF(huurcontractEF, contract);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new RepositoryException("UpdateContract", ex);
            }
        }

        public void VoegContractToe(Huurcontract contract)
        {
            try
            {
                // Assuming you have the IDs of Huurder and Huis
                var existingHuurder = ctx.Huurders.Find(contract.Huurder.Id);
                var existingHuis = ctx.Huizen.Find(contract.Huis.Id);

                // Create a new Huurcontract instance with existing entities
                var huurcontractEF = new HuurcontractEF
                {
                    // Set properties of Huurcontract
                    Huurder = existingHuurder,
                    Huis = existingHuis,
                    Id = contract.Id,
                };

                ctx.Huurcontracten.Add(huurcontractEF);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("VoegContractToe", ex);
            }
        }


    }
}
