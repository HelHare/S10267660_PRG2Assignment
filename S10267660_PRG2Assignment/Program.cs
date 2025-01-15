//==========================================================
// Student Number	: S10267660
// Student Name	: Jovan Neo
// Partner Name	: Jaden Tan
//==========================================================

using S10267660_PRG2Assignment;
using System.Collections.Generic;
using System.Globalization;

//1)	Load files (airlines and boarding gates)

List<int> optionList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
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
        airlineDict[airlines_info[1]] = airline;
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
Console.WriteLine("Loading Flights...");
using (StreamReader streamReader = new StreamReader("flights.csv"))
{
    string line;
    string s = streamReader.ReadLine(); // read the first line
    int flightCount = 0;
    string[] formats = ["hh:mm tt", "h:mm tt"];
    List<NORMFlight> flights = new List<NORMFlight>();
    while ((line = streamReader.ReadLine()) != null)
    {
        string[] flightData = line.Split(",");
        flights.Add(new NORMFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture), flightData[4]));
        flightCount += 1;
    }
    Console.WriteLine($"{flightCount} Flights Loaded!");
}
//3)	List all flights with their basic information

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

//6)	Create a new flight

//7)	Display full flight details from an airline

void DisplayFlightInfoFromAirline()
{
    
    foreach(KeyValuePair<string,Airline>airLineInfo in airlineDict)
    {
        Console.WriteLine("{0,-10} {1,-10}", airLineInfo.Value.Code, airLineInfo.Value.Name);
    }
    while (true)
    {
        Console.Write("Enter Airline Code: ");
        string airLineCode = Console.ReadLine();
        if (airlineDict.ContainsKey(airLineCode))
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for " + airlineDict[airLineCode].Name);
            Console.WriteLine("=============================================");

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
    foreach (KeyValuePair<string, Airline> airLineInfo in airlineDict)
    {
        Console.WriteLine("{0,-10} {1,-10}", airLineInfo.Value.Code, airLineInfo.Value.Name);
    }
    while (true)
    {
        Console.Write("Enter Airline Code: \n");
        string airLineCode = Console.ReadLine();
        if (airlineDict.ContainsKey(airLineCode))
        {
            Console.WriteLine("List of Flights for " + airlineDict[airLineCode].Name);
            
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
    Console.WriteLine("\n \n \n \n \n =============================================");
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