using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number: S10268361
// Student Name: Jaden Tan Si An
// Partner Name: Jovan Zheng Shuan Neo
//==========================================================
namespace S10267660_PRG2Assignment
{
     class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, Flight flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            Flight = flight;
        }
        public BoardingGate()
        {
            GateName = "";
            SupportsCFFT = false;
            SupportsDDJB = false;
            Flight = null;
        }
        public override string ToString()
        {
            string str = "Gate: " + GateName + "\n";
            str += "Supports CFFT: " + SupportsCFFT + "\n";
            str += "Supports DDJB: " + SupportsDDJB + "\n";
            str += Flight.ToString() + "\n";
            return str;
        }

    }
}
