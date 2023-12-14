using ParkBusinessLayer.Model;

namespace ParkConsoleLayer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Park park = new Park(1,"park","locatie");

            Huis huis = new Huis("straat", 1, park);


        }
    }
}