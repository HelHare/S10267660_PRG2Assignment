using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number	: S10267660
// Student Name	: Jovan Neo
// Partner Name	: Jaden Tan
//==========================================================
namespace S10267660_PRG2Assignment
{

     abstract class Flight : IComparable<Flight>

    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }


        public Flight(string FN, string O, string D, DateTime ET,string S)

        {
            FlightNumber = FN;
            Origin = O;
            Destination = D;
            ExpectedTime = ET;
            Status = S;
        }

        public Flight()
        {
            FlightNumber = "";
            Origin = "";
            Destination = "";
            ExpectedTime = new DateTime();
            Status = "";
        }
    public int CompareTo(Flight other)
    {
            return ExpectedTime.CompareTo(other.ExpectedTime);
    }
        public virtual double CalculateFees()
        {
            string truncatedDestination = Destination.Substring(0, 9);
            string truncatedOrigin = Origin.Substring(0, 9);

            if (truncatedDestination == "Singapore") // flights that end in Singapore
            {
                return 500;
            }
            else // flights that start in Singapore
            {
                return 800;
            }
        }
        public override string ToString()
        {
            return "Flight Number: " + FlightNumber + " Origin: " + Origin +" Destination: "+Destination+" Expected Time: "+ExpectedTime+" Status: "+ Status;


        }
    }
}