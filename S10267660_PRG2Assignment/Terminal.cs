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
     class Terminal
    {
        public string TerminalName { get; set; }
        public Dictionary<string, Airline> Airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> BoardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> GateFees { get; set; } = new Dictionary<string, double>();

        public Terminal(string TN)
        {
            TerminalName = TN;
        }

        public bool AddAirline(Airline airline)
        {
            try
            {
                Airlines[airline.Code] = airline;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not add airline!");
                return false;
            }
        }

        public bool AddBoardingGate(BoardingGate boardinggate)
        {
            try
            {
                BoardingGates[boardinggate.GateName] = boardinggate;
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not add boarding gate!");
                return false;
            }
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (Airline airlineInfo in Airlines.Values)
            {
                if (airlineInfo.Flights.ContainsKey(flight.FlightNumber))
                {
                    return airlineInfo;
                }
                else
                {
                    continue;
                }
            }
            return null;
        }

        public void PrintAirlineFees()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return "Terminal Name: " + TerminalName + " Airlines: " + Airlines.Values + " Flights: " + Flights.Values + " Boarding Gates " + BoardingGates.Values + " Gate Fees: " + GateFees.Values;

        }
    }
}
