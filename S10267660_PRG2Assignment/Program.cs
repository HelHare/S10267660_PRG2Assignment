﻿//==========================================================
// Student Number	: S10267660
// Student Name	: Jovan Neo
// Partner Name	: Jaden Tan
//==========================================================

using S10267660_PRG2Assignment;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


//1)	Load files (airlines and boarding gates)

List<int> optionList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
List<Airline> airLineList = new List<Airline>();
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();

Console.WriteLine("Loading Airlines...");
using (StreamReader sr = new StreamReader("airlines.csv"))
{
    int airlineCount = 0;
    string? s = sr.ReadLine();
    string[] headings = s.Split(",");
    while ((s = sr.ReadLine()) != null)
    {
        string[]airlines_info = s.Split(",");
        Airline airline = new Airline(airlines_info[0], airlines_info[1]);
        airLineList.Add(airline);
        airlineCount += 1;
    }
    Console.WriteLine(airlineCount + " Airlines Loaded!");
}

Console.WriteLine("Loading Boarding Gates...");
using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    int boardingGateCount = 0;
    string? s = sr.ReadLine();
    string[] headings = s.Split(",");
    while ((s = sr.ReadLine()) != null)
    {
        string[]boardingGateInfo = s.Split(",");
        BoardingGate boardingGate = new BoardingGate(boardingGateInfo[0], Convert.ToBoolean(boardingGateInfo[2]), Convert.ToBoolean(boardingGateInfo[1]), Convert.ToBoolean(boardingGateInfo[3]),null);
        boardingGateDict[boardingGateInfo[0]] = boardingGate;
        boardingGateCount += 1;
    }
    Console.WriteLine(boardingGateCount + " Boarding Gates Loaded!");
}

//2)	Load files(flights)
List<Flight> flightList = new List<Flight>();
Console.WriteLine("Loading Flights...");
using (StreamReader streamReader = new StreamReader("flights.csv"))
{
    string line;
    string s = streamReader.ReadLine(); // read the first line
    int flightCount = 0;
    string[] formats = ["hh:mm tt", "h:mm tt"];
    while ((line = streamReader.ReadLine()) != null)
    {
        string[] flightData = line.Split(",");
        string flightType = flightData[4];
        switch (flightType)
        {
            case "CFFT":
                Flight cfftFlight = new CFFTFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                flightList.Add(cfftFlight);
                break;
            case "DDJB":
                Flight ddjbFlight = new DDJBFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                flightList.Add(ddjbFlight);
                break;
            case "LWTT":
                Flight lwttFlight = new LWTTFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                flightList.Add(lwttFlight);
                break;
            default:
                Flight normFlight = new NORMFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                flightList.Add(normFlight);
                break;
        }
        flightCount += 1;
    }
    Console.WriteLine($"{flightCount} Flights Loaded!");
}
DisplayFlights();
//3)	List all flights with their basic information
void DisplayFlights()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (Flight flight in flightList)
    {
        string airlineCode = flight.FlightNumber.Substring(0, 2);
        foreach (Airline airline in airLineList)
        {
            if (airline.Code == airlineCode)
            {
                airlineCode = airline.Name;
                break;
            }
        }
        Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-10}", flight.FlightNumber, airlineCode, flight.Origin, flight.Destination, flight.ExpectedTime);
    }
}


    //4)	List all boarding gates

    void DisplayBoardingGates()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}","Gate Name","DDJB","CFFT","LWTT");
    foreach (KeyValuePair<string, BoardingGate> boardingGateInfo in boardingGateDict)
    {
        Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}",boardingGateInfo.Value.GateName,boardingGateInfo.Value.SupportsCFFT,boardingGateInfo.Value.SupportsDDJB,boardingGateInfo.Value.SupportsLWTT);
    }
}


//5)	Assign a boarding gate to a flight
void AssignBoardingGateToFlight()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");
    Console.WriteLine("Enter Flight Number: ");
    string flightNumber = Console.ReadLine();
    Flight selectedFlight = null;
    foreach (Flight flight in flightList)
    {
        if (flight.FlightNumber == flightNumber)
        {
            selectedFlight = flight;
            break;
        }
    }
    if (selectedFlight != null)
    {
        Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
        Console.WriteLine($"Origin: {selectedFlight.Origin}");
        Console.WriteLine($"Destination: {selectedFlight.Destination}");
        Console.WriteLine($"Expected Time: {selectedFlight.ExpectedTime}");
        foreach (KeyValuePair<string, BoardingGate> kvp in boardingGateDict)
        {
            if (selectedFlight == kvp.Value.Flight) // check if flight is already assigned to a boarding gate
            {
                Console.WriteLine("Flight is already assigned to a boarding gate!");
                return;
            }
        }
    }
    if (selectedFlight is NORMFlight)
    {
        Console.WriteLine("Special Request Code: None");
    }
    else if (selectedFlight is CFFTFlight)
    {
        Console.WriteLine("Special Request Code: CFFT");
    }
    else if (selectedFlight is DDJBFlight)
    {
        Console.WriteLine("Special Request Code: DDJB");
    }
    else if (selectedFlight is LWTTFlight)
    {
        Console.WriteLine("Special Request Code: LWTT");
    }
    else
    {
        Console.WriteLine("No such flight exists you are delusional"); // change later
        return;
    }
    while (true)
    {
        BoardingGate selectedBoardingGate = null;
        Console.WriteLine("Enter Boarding Gate Name: ");
        string boardingGateName = Console.ReadLine();
        foreach (KeyValuePair<string, BoardingGate> boardingGate in boardingGateDict)
        {
            if (boardingGate.Key == boardingGateName)
            {
                selectedBoardingGate = boardingGate.Value;
                break;
            }
        }
        if (selectedBoardingGate != null)
        {
            Console.WriteLine($"Boarding Gate Name: {selectedBoardingGate.GateName}");
            Console.WriteLine($"Supports DDJB: {selectedBoardingGate.SupportsDDJB}");
            Console.WriteLine($"Supports CFFT: {selectedBoardingGate.SupportsCFFT}");
            Console.WriteLine($"Supports LWTT: {selectedBoardingGate.SupportsLWTT}");
            if (selectedBoardingGate.Flight == null)
            {
                selectedBoardingGate.Flight = selectedFlight;
                break;
            }
            else
            {
                Console.WriteLine("Boarding Gate already in use by another flight!");
            }
        }
        else
        {
            Console.WriteLine("Boarding Gate does not exist!");
        }
    }
    Console.WriteLine("Would you like to update the status of this flight? (Y/N)");
    string userInput = Console.ReadLine();
    if (userInput.ToUpper() == "Y")
    {
        Console.WriteLine("1. Delayed");
        Console.WriteLine("2. Boarding");
        Console.WriteLine("3. On Time");
        Console.WriteLine("Please select the new status of the flight:");
        bool continueRun = true;
        while (continueRun)
        {
            int newStatus = Convert.ToInt32(Console.ReadLine());
            switch (newStatus)
            {
                case 1:
                    selectedFlight.Status = "Delayed";
                    continueRun = false;
                    break;
                case 2:
                    selectedFlight.Status = "Boarding";
                    continueRun = false;
                    break;
                case 3:
                    selectedFlight.Status = "On Time";
                    continueRun = false;
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
    else
    {
    selectedFlight.Status = "On Time";
    }
    Console.WriteLine("Flight has been successfully assigned to a boarding gate!");
}

    //6)	Create a new flight

    //7)	Display full flight details from an airline

    void DisplayFlightInfoFromAirline()
    {

        for (int i = 0; i < airLineList.Count; i++)
        {
            Console.WriteLine("{0,-10} {1,-10}", airLineList[i].Code, airLineList[i].Name);
        }
        bool airLineFound = false;
        while (true)
        {
            Console.Write("Enter Airline Code: ");
            string airLineCode = Console.ReadLine();
            for (int i = 0; i < airLineList.Count; i++)
            {
                if (airLineList[i].Code == airLineCode)
                {
                    airLineFound = true;
                    Console.WriteLine("=============================================");
                    Console.WriteLine("List of Flights for " + airLineList[i].Name);
                    Console.WriteLine("=============================================");

                    break;
                }
                else
                {
                    continue;
                }
            }
            if (airLineFound == true)
            {
                break;
            }
            else
            {
                Console.WriteLine("Airline does not exist!");
            }
        }
    }

    //8)	Modify flight details

    void ModifyFlightInfo()
    {
        Console.WriteLine("=============================================");
        Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
        Console.WriteLine("=============================================");
        for (int i = 0; i < airLineList.Count; i++)
        {
            Console.WriteLine("{0,-10} {1,-10}", airLineList[i].Code, airLineList[i].Name);
        }
        bool airLineFound = false;
        while (true)
        {
            Console.Write("Enter Airline Code: ");
            string airLineCode = Console.ReadLine();
            for (int i = 0; i < airLineList.Count; i++)
            {
                if (airLineList[i].Code == airLineCode)
                {
                    airLineFound = true;
                    Console.WriteLine("List of Flights for " + airLineList[i].Name);

                    break;
                }
                else
                {
                    continue;
                }
            }
            if (airLineFound == true)
            {
                break;
            }
            else
            {
                Console.WriteLine("Airline does not exist!");
            }
        }
    }

while (true)
{
    Console.WriteLine("\n \n \n \n \n=============================================");
    Console.WriteLine("Welcome to Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("1. List All Flights");
    Console.WriteLine("2. List Boarding Gates");
    Console.WriteLine("3. Assign a Boarding Gate to a Flight");
    Console.WriteLine("4. Create Flight");
    Console.WriteLine("5. Display Airline Flights");
    Console.WriteLine("6. Modify Flight Details");
    Console.WriteLine("7. Display Flight Schedule");
    Console.WriteLine("0. Exit");
    try
    {
        Console.Write("Please select your option:\n");
        int option = Convert.ToInt32(Console.ReadLine());
        if (!optionList.Contains(option))
        {
            Console.WriteLine("Invalid option!");
            continue;
        }
        if (option == 0)
        {
            Console.WriteLine("\nGoodbye!");
            break;
        }
        else if (option == 2)
        {
            DisplayBoardingGates();
        }
        else if (option == 3)
        {
            AssignBoardingGateToFlight();
        }
        else if (option == 5)
        {
            DisplayFlightInfoFromAirline();
        }
        else if (option == 6)
        {
            ModifyFlightInfo();
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}
