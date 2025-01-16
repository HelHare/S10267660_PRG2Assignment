//==========================================================
// Student Number	: S10267660
// Student Name	: Jovan Neo
// Partner Name	: Jaden Tan
//==========================================================

using S10267660_PRG2Assignment;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;



Terminal terminal = new Terminal("Terminal 5");

// 1)	Load files (airlines and boarding gates)
Console.WriteLine("Loading Airlines...");
using (StreamReader sr = new StreamReader("airlines.csv"))
{
    bool success = false;
    int airlineCount = 0;
    string? s = sr.ReadLine();
    string[] headings = s.Split(",");
    while ((s = sr.ReadLine()) != null)
    {
        string[] airlines_info = s.Split(",");
        Airline airline = new Airline(airlines_info[0], airlines_info[1]);
        terminal.Airlines[airlines_info[1]] = airline;
        airlineCount += 1;
    }
    Console.WriteLine(airlineCount + " Airlines Loaded!");
}

Console.WriteLine("Loading Boarding Gates...");
using (StreamReader sr = new StreamReader("boardinggates.csv"))
{
    bool success = false;
    int boardingGateCount = 0;
    string? s = sr.ReadLine();
    string[] headings = s.Split(",");
    while ((s = sr.ReadLine()) != null)
    {
        string[] boardingGateInfo = s.Split(",");
        BoardingGate boardingGate = new BoardingGate(boardingGateInfo[0], Convert.ToBoolean(boardingGateInfo[2]), Convert.ToBoolean(boardingGateInfo[1]), Convert.ToBoolean(boardingGateInfo[3]), null);
        terminal.BoardingGates[boardingGateInfo[0]] = boardingGate;
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
    while ((line = streamReader.ReadLine()) != null)
    {
        bool success = false;
        string[] flightData = line.Split(",");
        string[]flightCode = flightData[0].Split(" ");
        string flightType = flightData[4];
        switch (flightType)
        {
            case "CFFT":
                Flight cfftFlight = new CFFTFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                terminal.Flights[flightData[0]] = cfftFlight;
                break;
            case "DDJB":
                Flight ddjbFlight = new DDJBFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                terminal.Flights[flightData[0]] = ddjbFlight; 
                break;
            case "LWTT":
                Flight lwttFlight = new LWTTFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                terminal.Flights[flightData[0]] = lwttFlight; 
                break;
            default:
                Flight normFlight = new NORMFlight(flightData[0], flightData[1], flightData[2], DateTime.ParseExact(flightData[3], formats, CultureInfo.InvariantCulture, DateTimeStyles.None), "On Time");
                terminal.Flights[flightData[0]] = normFlight; 
                break;
        }
        flightCount += 1;
    }
    Console.WriteLine($"{flightCount} Flights Loaded!");
}

//3)	List all flights with their basic information
void DisplayFlights()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Flights for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-10}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Departure/Arrival Time");
    foreach (KeyValuePair<string,Flight> flight in terminal.Flights)
    {
        string airlineName = flight.Value.FlightNumber.Substring(0, 2);
        foreach (KeyValuePair<string,Airline>airline in terminal.Airlines)
        {
            if (airline.Value.Code == airlineName)
            {
                airlineName = airline.Value.Name;
                break;
            }
        }
        Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-10}", flight.Value.FlightNumber, airlineName, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime);
    }
}


//4)	List all boarding gates

void DisplayBoardingGates()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Boarding Gates for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}", "Gate Name", "DDJB", "CFFT", "LWTT");
    foreach (KeyValuePair<string, BoardingGate> boardingGateInfo in terminal.BoardingGates)
    {
        Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}", boardingGateInfo.Value.GateName, boardingGateInfo.Value.SupportsCFFT, boardingGateInfo.Value.SupportsDDJB, boardingGateInfo.Value.SupportsLWTT);
    }
}

//5)	Assign a boarding gate to a flight

//6)	Create a new flight

//7)	Display full flight details from an airline

void DisplayFlightInfoFromAirline()
{

    foreach(KeyValuePair<string,Airline> airline in terminal.Airlines)
    {
        Console.WriteLine("{0,-10} {1,-10}", airline.Value.Code, airline.Value.Name);
    }
    bool airLineFound = false;
    while (true)
    {
        try
        {
            Console.Write("Enter Airline Code: ");
            string airLineCode = Console.ReadLine();
            foreach (KeyValuePair<string, Airline> airline in terminal.Airlines)
            {
                if (airline.Value.Code == airLineCode)
                {
                    airLineFound = true;
                    Console.WriteLine("=============================================");
                    Console.WriteLine("List of Flights for " + airline.Value.Name);
                    Console.WriteLine("=============================================");
                    Console.WriteLine("{0,-16}{1,-23}{2,-23}", "Airline Number", "Origin", "Destination");
                    foreach(KeyValuePair<string,Flight> flight in terminal.Flights)
                    {
                        string[] flightCodeNumber = flight.Value.FlightNumber.Split(' ');
                        if (flightCodeNumber[0] == airLineCode)
                        {
                            Console.WriteLine("{0,-16}{1,-23}{2,-23}", flight.Value.FlightNumber, flight.Value.Origin, flight.Value.Destination);
                        }
                    }
                    while (true)
                    {
                        Console.Write("Enter an above flight number to see full details: ");
                        string selectedFlight = Console.ReadLine();
                        foreach(KeyValuePair<string,Flight> flight in terminal.Flights)
                        {
                            string assignedBoardingGate = "Unassigned";
                            if (flight.Value.FlightNumber == selectedFlight)
                            {
                                foreach(KeyValuePair<string,BoardingGate>assignedFlight in terminal.BoardingGates)
                                {
                                    if (assignedFlight.Value.Flight == null)
                                    {
                                        continue;
                                    }
                                    else if (assignedFlight.Value.Flight.FlightNumber == selectedFlight)
                                    {
                                        assignedBoardingGate = assignedFlight.Key;
                                    }
                                }
                                Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", "Flight Number","Airline Name","Origin","Destination","Expected Time","Special Request Code","Boarding Gate");
                                if (flight.Value is DDJBFlight)
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.Value.FlightNumber, airline.Value.Name, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime,"DDJB",assignedBoardingGate);
                                }
                                else if (flight.Value is LWTTFlight)
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.Value.FlightNumber, airline.Value.Name, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime, "LWTT", assignedBoardingGate);
                                }
                                else if (flight.Value is CFFTFlight)
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.Value.FlightNumber, airline.Value.Name, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime, "CFFT", assignedBoardingGate);
                                }
                                else
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.Value.FlightNumber, airline.Value.Name, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime, "None", assignedBoardingGate);
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        break;
                    }
                }
                else
                {
                    continue;
                }
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
        if (airLineFound == true)
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid Airline Code!");
        }
    }
}


//8)	Modify flight details

void ModifyFlightInfo()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    foreach(KeyValuePair<string,Airline>airline in terminal.Airlines)
    {
        Console.WriteLine("{0,-10} {1,-10}", airline.Value.Code, airline.Value.Name);
    }
    bool airLineFound = false;
    while (true)
    {
        Console.Write("Enter Airline Code: ");
        string airLineCode = Console.ReadLine();
        foreach(KeyValuePair<string, Airline> airline in terminal.Airlines)
        {
            if (airline.Value.Code == airLineCode)
            {
                airLineFound = true;
                Console.WriteLine("List of Flights for " + airline.Value.Name);
                Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Time");
                foreach(KeyValuePair<string,Flight>flight in terminal.Flights)
                {
                    string[] flightCodeNumber = flight.Value.FlightNumber.Split(' ');
                    if (flightCodeNumber[0] == airLineCode)
                    {
                        Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-10}", flight.Value.FlightNumber, airline.Value.Name, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime);
                    }
                    else
                    {
                        continue;
                    }
                }
                while (true)
                {
                    try
                    {
                        List<int> flightOptionList = new List<int> { 1, 2 };
                        Console.Write("Choose an existing Flight to modify or delete:\n");
                        string flightNumber = Console.ReadLine();
                        if (!terminal.Flights.ContainsKey(flightNumber))
                        {
                            Console.WriteLine("Flight does not exist!");
                            continue;
                        }
                        string[] flightCodeNumber = flightNumber.Split(" ");
                        Console.WriteLine("1. Modify Flight");
                        Console.WriteLine("2. Delete Flight");
                        Console.Write("Choose an option:\n");
                        int flightOption = Convert.ToInt32(Console.ReadLine());
                        if (!flightOptionList.Contains(flightOption))
                        {
                            Console.WriteLine("Invalid option!");
                            continue;
                        }
                        else if (flightOption == 1)
                        {
                            List<int> modifyFlightOptionList = new List<int> { 1, 2, 3, 4 };
                            Console.WriteLine("1. Modify Basic Information");
                            Console.WriteLine("2. Modify Status");
                            Console.WriteLine("3. Modify Special Request Code");
                            Console.WriteLine("4. Modify Boarding Gate");
                            Console.Write("Choose an option:\n");
                            int modifyFlightOption = Convert.ToInt32(Console.ReadLine());
                            if (!modifyFlightOptionList.Contains(modifyFlightOption))
                            {
                                Console.WriteLine("Invalid option!");
                                continue;
                            }
                            //modify origin, destination, expected time
                            else if (modifyFlightOption == 1)
                            {
                                Console.Write("Enter new Origin: ");
                                string origin = Console.ReadLine();
                                Console.Write("Enter new Destination: ");
                                string destination = Console.ReadLine();
                                Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                                DateTime expectedTime = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
                                foreach(KeyValuePair<string,Flight> flight in terminal.Flights)
                                {
                                    if (flight.Value.FlightNumber == flightNumber)
                                    {
                                        flight.Value.Origin = origin;
                                        flight.Value.Destination = destination;
                                        flight.Value.ExpectedTime = expectedTime;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                            }
                            //modify status
                            else if (modifyFlightOption == 2)
                            {
                                List<string> statusList = new List<string> { "Delayed", "Boarding", "On Time","Scheduled" };
                                Console.Write("Enter new Status: ");
                                string status = Console.ReadLine();
                                if (!statusList.Contains(status))
                                {
                                    Console.WriteLine("Invalid Status!");
                                    continue;
                                }
                                foreach(KeyValuePair<string,Flight> flight in terminal.Flights)
                                {
                                    if (flight.Value.FlightNumber == flightNumber)
                                    {
                                        flight.Value.Status = status;
                                        break;
                                    }
                                }
                            }
                            // modify Request Code
                            else if (modifyFlightOption == 3)
                            {
                                List<string> specialRequestCodes = new List<string> { "CFFT", "LWTT", "DDJB", "NORM" };
                                while (true)
                                {
                                    Console.Write("Enter new Request Code: ");
                                    string requestCode = Console.ReadLine();
                                    if (!specialRequestCodes.Contains(requestCode))
                                    {
                                        Console.WriteLine("Invalid Request Code!");
                                    }
                                    else
                                    {
                                        foreach(KeyValuePair<string,Flight> flight in terminal.Flights)
                                        {
                                            if (flightNumber == flight.Value.FlightNumber)
                                            {
                                                if (requestCode == "CFFT")
                                                {
                                                    terminal.Flights[flightNumber] = new CFFTFlight(terminal.Flights[flight.Value.FlightNumber].FlightNumber, terminal.Flights[flight.Value.FlightNumber].Origin, terminal.Flights[flight.Value.FlightNumber].Destination, terminal.Flights[flight.Value.FlightNumber].ExpectedTime, terminal.Flights[flight.Value.FlightNumber].Status);
                                                }
                                                else if (requestCode == "LWTT")
                                                {
                                                    terminal.Flights[flightNumber] = new LWTTFlight(terminal.Flights[flight.Value.FlightNumber].FlightNumber, terminal.Flights[flight.Value.FlightNumber].Origin, terminal.Flights[flight.Value.FlightNumber].Destination, terminal.Flights[flight.Value.FlightNumber].ExpectedTime, terminal.Flights[flight.Value.FlightNumber].Status);
                                                }
                                                else if (requestCode == "DDJB")
                                                {
                                                    terminal.Flights[flightNumber] = new DDJBFlight(terminal.Flights[flight.Value.FlightNumber].FlightNumber, terminal.Flights[flight.Value.FlightNumber].Origin, terminal.Flights[flight.Value.FlightNumber].Destination, terminal.Flights[flight.Value.FlightNumber].ExpectedTime, terminal.Flights[flight.Value.FlightNumber].Status);
                                                }
                                                else
                                                {
                                                    terminal.Flights[flightNumber] = new NORMFlight(terminal.Flights[flight.Value.FlightNumber].FlightNumber, terminal.Flights[flight.Value.FlightNumber].Origin, terminal.Flights[flight.Value.FlightNumber].Destination, terminal.Flights[flight.Value.FlightNumber].ExpectedTime, terminal.Flights[flight.Value.FlightNumber].Status);
                                                }
                                                break;
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            // modify Boarding Gate
                            else
                            {
                                while (true)
                                {
                                    Console.Write("Assign new Boarding Gate: ");
                                    string boardingGate = Console.ReadLine();
                                    if (!terminal.BoardingGates.ContainsKey(boardingGate))
                                    {
                                        Console.WriteLine("Invalid Boarding Gate!");
                                        continue;
                                    }
                                    else
                                    {
                                        foreach(KeyValuePair<string,Flight>flight in terminal.Flights)
                                        {
                                            if (flightNumber == flight.Value.FlightNumber)
                                            {
                                                terminal.BoardingGates[boardingGate].Flight = flight.Value;
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        // Delete flight
                        else
                        {
                            Console.Write("Select an existing flight to delete: ");
                            string flightToDelete = Console.ReadLine();
                            if (!terminal.Flights.ContainsKey(flightNumber))
                            {
                                Console.WriteLine("Flight does not exist!");
                            }
                            else
                            {
                                Console.Write("Are you sure you want to delete? [Y/N]: ");
                                string confirmation = Console.ReadLine();
                                if (confirmation == "Y")
                                {
                                    terminal.Flights.Remove(flightNumber);
                                }
                                else if (confirmation == "N")
                                {
                                    Console.WriteLine(flightNumber + "has not been deleted");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option!");
                                    continue;
                                }
                            }

                        }
                        bool boardingGateAssigned = false;
                        Console.WriteLine("Flight updated!");
                        foreach (KeyValuePair<string, Flight> newFlight in terminal.Flights)
                        {
                            if (newFlight.Value.FlightNumber == flightNumber)
                            {
                                Console.WriteLine("Flight Number: " + newFlight.Value.FlightNumber);

                                foreach (KeyValuePair<string, Airline> getAirLine in terminal.Airlines)
                                {
                                    if (flightCodeNumber[0] == getAirLine.Value.Code)
                                    {
                                        Console.WriteLine("Airline Name: " + getAirLine.Value.Name);
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                Console.WriteLine("Origin: " + newFlight.Value.Origin);
                                Console.WriteLine("Destination: " + newFlight.Value.Destination);
                                Console.WriteLine("Expected Time: " + newFlight.Value.ExpectedTime);
                                Console.WriteLine("Status: " + newFlight.Value.Status);
                                foreach (BoardingGate assignedFlight in terminal.BoardingGates.Values)
                                {
                                    if (assignedFlight.Flight == null)
                                    {
                                        continue;
                                    }
                                    else if (assignedFlight.Flight.FlightNumber == flightNumber)
                                    {
                                        Console.WriteLine("Boarding Gate: " + assignedFlight.GateName);
                                        boardingGateAssigned = true;
                                        break;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (boardingGateAssigned == false)
                                {
                                    Console.WriteLine("Boarding Gate: Unassigned");
                                }
                            }
                        }
                        break;
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

//(a)	Process all unassigned flights to boarding gates in bulk

void AssignFlightsToBoardingGates()
{
    foreach(KeyValuePair<string,BoardingGate>assignedFlight in terminal.BoardingGates)
    {
        if (assignedFlight.Value.Flight == null)
        {

        }
    }
}

List<int> mainMenuOptionList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
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
    Console.WriteLine("8. Assign all Flights to Boarding Gates");
    Console.WriteLine("0. Exit");
    try
    {
        Console.Write("Please select your option:\n");
        int option = Convert.ToInt32(Console.ReadLine());
        if (!mainMenuOptionList.Contains(option))
        {
            Console.WriteLine("Invalid option!");
            continue;
        }
        else if (option == 1)
        {
            DisplayFlights();
        }
        else if (option == 2)
        {
            DisplayBoardingGates();
        }
        else if (option == 3)
        {

        }
        else if (option == 4)
        {

        }
        else if (option == 5)
        {
            DisplayFlightInfoFromAirline();
        }
        else if (option == 6)
        {
            ModifyFlightInfo();
        }
        else if (option == 7)
        {

        }
        else if (option == 8)
        {

        }
        else if (option == 9)
        {

        }
        else
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


