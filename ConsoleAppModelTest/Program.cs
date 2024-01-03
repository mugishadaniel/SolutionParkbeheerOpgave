using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer;
using ParkDataLayer.Context;
using ParkDataLayer.Model;
using ParkDataLayer.Repositories;
using System;

namespace ConsoleAppModelTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ParkContext ctx = new ParkContext();
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            IHuizenRepository hrepo = new HuizenRepositoryEF();
            BeheerHuizen bh = new BeheerHuizen(hrepo);
            Park p = new Park("p2", "Binnenhoeve", "Gent");
            bh.VoegNieuwHuisToe("parklaan", 1, p);
            bh.VoegNieuwHuisToe("parklaan", 2, p);
            bh.VoegNieuwHuisToe("parklaan", 3, p);
            var x = bh.GeefHuis(1);
            Console.WriteLine($"Id : {x.Id}, straat : {x.Straat} {x.Nr}, park : {x.Park.Naam}");
            x.ZetStraat("Kerkstraat");
            x.ZetNr(11);
            bh.UpdateHuis(x);
            bh.ArchiveerHuis(x);
            Huis h1 = new Huis();
            ParkEF pdb = new ParkEF("p3","naam","locatie");
            HuisEF hdb = new HuisEF("straat", 5, true);
            hdb.Park = pdb;
            ctx.Huizen.Add(hdb);
            ctx.SaveChanges();
            //huurder
            IHuurderRepository rhuur = new HuurderRepositoryEF();
            BeheerHuurders bhuur = new BeheerHuurders(rhuur);
            bhuur.VoegNieuweHuurderToe("jos", new Contactgegevens("email1", "tel", "adres"));
            bhuur.VoegNieuweHuurderToe("jef", new Contactgegevens("email2", "tel", "adres"));

            IContractenRepository crepo = new ContractenRepositoryEF();
            BeheerContracten bc = new BeheerContracten(crepo);
            Huurperiode hp = new Huurperiode(DateTime.Now, 10);
            Huurder h = new Huurder(2, "Jos", new Contactgegevens("email1", "tel", "adres"));
            Park pp = new Park("p1", "Buitenhoeve", "Deinze");
            Huis huis = new Huis(1, "Kerkstraat", 5, true, pp);
            bc.MaakContract("c2", hp, h, huis);

            var y = bc.GeefContract("c2");
            var t = bh.GeefHuis(1);
            Console.WriteLine($"Id : {t.Id}, straat : {t.Straat} {t.Nr}, park : {t.Park.Naam}");
            Console.WriteLine($"Id : {y.Id}, Huurder : {y.Huurder.Naam}, Huurperiode : {y.Huurperiode.StartDatum} - {y.Huurperiode.EindDatum},straat : {y.Huis.Straat} {y.Huis.Nr}, park : {y.Huis.Park.Naam} ");
            bc.AnnuleerContract(y);
        }
    }
}
