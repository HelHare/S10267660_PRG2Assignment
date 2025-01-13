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
        public Dictionary<string, Airline> Airlines { get; set; }
        public Dictionary<string,Flight > Flights { get; set; }
        public Dictionary<string,BoardingGate>BoardingGates { get; set; }
        public Dictionary<string,double>GateFees { get; set; }

        public Terminal(string TN, Dictionary<string,Airline>A, Dictionary<string, Flight> F, Dictionary<string, BoardingGate> BG, Dictionary<string, double> GF)
        {
            TerminalName = TN;
            Airlines = A;
            Flights = F;
            BoardingGates = BG;
            GateFees = GF;
        }
        public override string ToString()
        {
            return "Terminal Name: " + TerminalName + " Airlines: " + Airlines + " Flights: " + Flights + " Boarding Gates " + BoardingGates + " Gate Fees: " + GateFees;

        }
    }
}
