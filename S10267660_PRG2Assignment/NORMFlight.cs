using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
//==========================================================
// Student Number: S10268361
// Student Name: Jaden Tan Si An
// Partner Name: Jovan Zheng Shuan Neo
//==========================================================
namespace S10267660_PRG2Assignment
{
     class NORMFlight : Flight
    {
        public double RequestFee { get; set; }
        public NORMFlight(string flightNumber, string origin, string destination, DateTime expectedTime, string status) : base(flightNumber, origin, destination, expectedTime, status)
        {
            RequestFee = 500;
            // placeholder value until CalculateFees is implemented
        }
        public NORMFlight()
        {
            RequestFee = 500;
            // placeholder value until CalculateFees is implemented
        }
        public override double CalculateFees()
        {
            throw new NotImplementedException();
        }
        public override string ToString()
        {
            return $"Flight Number: {FlightNumber}, Origin: {Origin}, Destination: {Destination}, Expected Time: {ExpectedTime}, Status: {Status}, Request Fee: {RequestFee}";
        }
    }
}
