using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StarsWars_IG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
               
                Console.WriteLine("Please write the distance in mega lights (MGLT) and pres Enter to continue: ");
               
                if (int.TryParse(Console.ReadLine(), out int MGLT))
                {
                    JsonModel _model = new JsonModel();
                    _model.GetAllStarships(MGLT);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid input , Enter only number");
                }
                    
            }
            catch(IOException ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }
        }
    }
}
