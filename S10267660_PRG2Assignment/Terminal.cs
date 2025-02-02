using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            string[]flightCodeNumber = flight.FlightNumber.Split(' ');
            foreach(Airline airline in Airlines.Values)
            {
                if (airline.Code == flightCodeNumber[0])
                {
                    return airline;
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
            bool continueRunning = true;
            while (continueRunning)
            {
                foreach (Flight flight in Flights.Values)
                {
                    BoardingGate? assignedBoardingGate = null;
                    foreach (BoardingGate boardingGate in BoardingGates.Values)
                    {
                        if (boardingGate.Flight == flight)
                        {
                            assignedBoardingGate = boardingGate; // this lets us check for a flight without a boarding gate. an unassigned flight will have a null boarding gate
                            break;
                        }
                    }
                    if (assignedBoardingGate == null)
                    {
                        Console.WriteLine("Flight " + flight.FlightNumber + " has no assigned boarding gate. Please ensure every flight has been assigned before using this feature.");
                        continueRunning = false;
                        return;
                    }
                } // if we reach this point, all flights have been assigned a boarding gate
                Dictionary<Airline, double> airlineBills  = new Dictionary<Airline, double>(); // this dictionary takes every airline and their respective fees
                Dictionary<Airline, List<Flight>> airlineFlights = new Dictionary<Airline, List<Flight>>();
                double totalEligibleDiscount = 0;
                double totalFinalFee = 0;

                foreach (Airline airline in Airlines.Values)
                {
                    airlineBills[airline] = 0;
                }
                foreach (Airline airline in Airlines.Values)
                {
                    airlineFlights[airline] = new List<Flight>();
                }
                foreach (Flight flight in Flights.Values) // this adds every flight to a list assigned to an airline.
                {
                    Airline airline = GetAirlineFromFlight(flight);
                    airlineFlights[airline].Add(flight);
                }
                foreach (Flight flight in Flights.Values) // this is the bill calculation part. we do the printing here for debugging.
                {
                    double fee;
                    fee = flight.CalculateFees();
                    fee += 300;
                    Airline airline = GetAirlineFromFlight(flight);
                    airlineBills[airline] += fee;
                    // Console.WriteLine(fee);
                }
                foreach (Airline airline in Airlines.Values)
                {
                    // Console.WriteLine("Airline " + airline.Name + " has a total fee of " + airlineBills[airline]);
                }
                double originalTotal = 0;
                foreach (Airline airline in Airlines.Values) // this is important. this is where we will calculate the discount that each airline is eligible for.
                {
                    double eligibleDiscount = 0;
                    List<Flight> flights = airlineFlights[airline];
                    eligibleDiscount += 350 * (flights.Count / 3); //per 3 flights discount
                    bool fiveOrMoreFlights = flights.Count > 4;
                    int earlyOrLateCount = 0;
                    int fromDubaiOrOtherCount = 0;
                    int NORMFlightCount = 0;
                    foreach (Flight flight in flights)
                    {
                        if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21) { earlyOrLateCount += 1;  }
                        if (flight.Origin == "Dubai (DXB)" || flight.Origin == "Bangkok (BKK)" || flight.Origin == "Tokyo (NRT)") { fromDubaiOrOtherCount += 1;  }
                        if (flight is NORMFlight) { NORMFlightCount += 1;  }
                    }
                    eligibleDiscount += earlyOrLateCount * 110 + fromDubaiOrOtherCount * 25 + NORMFlightCount * 50;
                    double originalFee = airlineBills[airline];
                    originalTotal += originalFee;
                    if (fiveOrMoreFlights) { airlineBills[airline] *= 0.97;  }
                    Console.WriteLine("Airline " + airline.Name + " is eligible for a discount of $" + eligibleDiscount);
                    airlineBills[airline] -= eligibleDiscount;
                    double discountedAmount = originalFee - airlineBills[airline];
                    totalEligibleDiscount += eligibleDiscount;
                    Console.WriteLine($"Total final fee for {airline.Name} is ${airlineBills[airline]}, discounted by ${discountedAmount} from ${originalFee}\n");
                }
                foreach (Airline airline in Airlines.Values)
                {
                    totalFinalFee += airlineBills[airline];
                }
                Console.WriteLine($"Total final fee: ${totalFinalFee}, discounted by ${totalEligibleDiscount} ");
                Console.WriteLine($"Discount percentage: {totalEligibleDiscount/originalTotal * 100:f3}%");
                continueRunning = false;
            }
        }
        public override string ToString()
        {
            return "Terminal Name: " + TerminalName + " Airlines: " + Airlines.Values + " Flights: " + Flights.Values + " Boarding Gates " + BoardingGates.Values + " Gate Fees: " + GateFees.Values;

        }
    }
}
