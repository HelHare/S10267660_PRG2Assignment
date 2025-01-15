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
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }
        public CFFTFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status, double requestFee) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = requestFee;
        }
        public CFFTFlight()
        {
            RequestFee = 0;
        }
        public override double CalculateFees()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"{base.ToString} Request Fee:{RequestFee}"
            ;
        }
    }
}   