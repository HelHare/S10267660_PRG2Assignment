//==========================================================
// Student Number	: S10267660
// Student Name	: Jovan Neo
// Partner Name	: Jaden Tan
//==========================================================

using S10267660_PRG2Assignment;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;



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
        BoardingGate boardingGate = new BoardingGate(boardingGateInfo[0], Convert.ToBoolean(boardingGateInfo[2]), Convert.ToBoolean(boardingGateInfo[1]), Convert.ToBoolean(boardingGateInfo[3]), new NORMFlight());
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
    foreach (Flight flight in terminal.Flights.Values)
    {
        Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-10}", flight.FlightNumber, terminal.GetAirlineFromFlight(flight).Name, flight.Origin, flight.Destination, flight.ExpectedTime);
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
void AssignBoardingGateToFlight()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");
    Console.WriteLine("Enter Flight Number: ");
    string flightNumber = Console.ReadLine();
    Flight selectedFlight = null;
    foreach (Flight flight in terminal.Flights.Values)
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
        foreach (KeyValuePair<string, BoardingGate> kvp in terminal.BoardingGates)
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
        Console.WriteLine("No such flight exists!");
        return;
    }
    while (true)
    {
        BoardingGate selectedBoardingGate = null;
        Console.WriteLine("Enter Boarding Gate Name: ");
        string boardingGateName = Console.ReadLine();
        foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
        {
            if (boardingGate.GateName == boardingGateName)
            {
                selectedBoardingGate = boardingGate;
                break;
            }
        }
        if (selectedBoardingGate != null)
        {
            Console.WriteLine($"Boarding Gate Name: {selectedBoardingGate.GateName}");
            Console.WriteLine($"Supports DDJB: {selectedBoardingGate.SupportsDDJB}");
            Console.WriteLine($"Supports CFFT: {selectedBoardingGate.SupportsCFFT}");
            Console.WriteLine($"Supports LWTT: {selectedBoardingGate.SupportsLWTT}");
            NORMFlight comparisonFlight = new NORMFlight();
            if (selectedBoardingGate.Flight.FlightNumber == comparisonFlight.FlightNumber) // checks if the boarding gate flight number matches the one from the default constructor.
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
// do input validation for airline part of the flight number
void CreateNewFlight()
{
    bool continueRunning = true;
    while (continueRunning)
    {
    try
    {
        Console.WriteLine("Enter Flight Number: ");
        string flightNumber = Console.ReadLine();
        string[] flightNumberParts = flightNumber.Split(' ');
        if (flightNumberParts.Length != 2)
        {
            throw new FormatException("Invalid Flight Number Format!");
        }
        else if (!terminal.Airlines.ContainsValue(terminal.Airlines[flightNumberParts[0]]))
        {
            throw new FormatException("Invalid Airline Code!");
        }
        Console.WriteLine("Enter Origin: ");
        string origin = Console.ReadLine();
        Console.WriteLine("Enter Destination: ");
        string destination = Console.ReadLine();
        Console.WriteLine("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
        DateTime date = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
        string specialRequestCode = Console.ReadLine();
        if (!(specialRequestCode == "CFFT" || specialRequestCode == "DDJB" || specialRequestCode == "LWTT" || specialRequestCode == "None"))
        {
            throw new FormatException("Invalid Special Request Code!");
        }
        switch (specialRequestCode)
        {
            case "CFFT":
                CFFTFlight cfftFlight = new CFFTFlight(flightNumber, origin, destination, date, "On Time");
                terminal.Flights[flightNumber] = cfftFlight;
                    File.AppendAllText("flights.csv", $"" +
                        $"{flightNumber},{origin},{destination},{date.TimeOfDay},{specialRequestCode}\n");
                    break;
            case "DDJB":
                DDJBFlight ddjbFlight = new DDJBFlight(flightNumber, origin, destination, date, "On Time");
                terminal.Flights[flightNumber] = ddjbFlight;
                    File.AppendAllText("flights.csv", $"{flightNumber},{origin},{destination},{date.TimeOfDay},{specialRequestCode}\n");
                    break;
            case "LWTT":
                LWTTFlight lwttFlight = new LWTTFlight(flightNumber, origin, destination, date, "On Time");
                terminal.Flights[flightNumber] = lwttFlight;
                    File.AppendAllText("flights.csv", $"{flightNumber},{origin},{destination},{date.TimeOfDay},{specialRequestCode}\n");
                    break;
            case "None":
                NORMFlight normFlight = new NORMFlight(flightNumber, origin, destination, date, "On Time");
                terminal.Flights[flightNumber] = normFlight;
                    File.AppendAllText("flights.csv", $"{flightNumber},{origin},{destination},{date.TimeOfDay},\n");
                    break;
        }
            Console.WriteLine($"Flight {flightNumber} has been added!");
            while (true)
            {
                Console.WriteLine("Would you like to add another flight? (Y/N)");
                string userInput = Console.ReadLine().ToUpper();
                if (userInput == "Y")
                {
                    break;
                }
                else if (userInput == "N")
                {
                    continueRunning = false;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Input!");
                }
            }
    }
    catch (FormatException ex)
    {
        Console.WriteLine(ex.Message);
    }
}
}


//7)	Display full flight details from an airline

void DisplayFlightInfoFromAirline()
{

    foreach(KeyValuePair<string,Airline> airline in terminal.Airlines)
    {
        Console.WriteLine("{0,-10} {1,-10}", airline.Value.Code, airline.Value.Name);
    }
    bool airLineFound = false;
    bool flightFound = false;
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
                    foreach(Flight flight in terminal.Flights.Values)
                    {
                        string getAirlineCode = terminal.GetAirlineFromFlight(flight).Code;

                        if (getAirlineCode == airLineCode)
                        {
                            Console.WriteLine("{0,-16}{1,-23}{2,-23}", flight.FlightNumber, flight.Origin, flight.Destination);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    while (true)
                    {
                        Console.Write("Enter an above flight number to see full details: ");
                        string selectedFlight = Console.ReadLine();
                        foreach(Flight flight in terminal.Flights.Values)
                        {
                            string assignedBoardingGate = "Unassigned";
                            if (flight.FlightNumber == selectedFlight)
                            {
                                flightFound = true;
                                foreach (KeyValuePair<string,BoardingGate>assignedFlight in terminal.BoardingGates)
                                {
                                    if (assignedFlight.Value.Flight.FlightNumber == selectedFlight)
                                    {
                                        assignedBoardingGate = assignedFlight.Key;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", "Flight Number","Airline Name","Origin","Destination","Expected Time","Special Request Code","Boarding Gate");
                                if (flight is DDJBFlight)
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.FlightNumber, terminal.GetAirlineFromFlight(flight).Name, flight.Origin, flight.Destination, flight.ExpectedTime,"DDJB",assignedBoardingGate);
                                }
                                else if (flight is LWTTFlight)
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.FlightNumber, terminal.GetAirlineFromFlight(flight).Name, flight.Origin, flight.Destination, flight.ExpectedTime, "LWTT", assignedBoardingGate);
                                }
                                else if (flight is CFFTFlight)
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.FlightNumber, terminal.GetAirlineFromFlight(flight).Name, flight.Origin, flight.Destination, flight.ExpectedTime, "CFFT", assignedBoardingGate);
                                }
                                else
                                {
                                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flight.FlightNumber, terminal.GetAirlineFromFlight(flight).Name, flight.Origin, flight.Destination, flight.ExpectedTime, "None", assignedBoardingGate);
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
        if (airLineFound == false)
        {
            Console.WriteLine("Invalid Airline Code!");
        }
        else if (flightFound == false)
        {
            Console.WriteLine("Flight does not exist!");
        }
        else
        {
            break;
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
    bool flightFound = false;
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
                        Console.WriteLine("1. Modify Flight");
                        Console.WriteLine("2. Delete Flight");
                        Console.Write("Choose an option:\n");
                        int flightOption = Convert.ToInt32(Console.ReadLine());
                        if (flightOption == 1)
                        {
                            Console.Write("\nChoose an existing Flight to modify:\n");
                            string flightNumber = Console.ReadLine();
                            if (!terminal.Flights.ContainsKey(flightNumber))
                            {
                                Console.WriteLine("Flight does not exist!");
                                continue;
                            }
                            else 
                            {
                                flightFound = true;
                                Console.WriteLine("1. Modify Basic Information");
                                Console.WriteLine("2. Modify Status");
                                Console.WriteLine("3. Modify Special Request Code");
                                Console.WriteLine("4. Modify Boarding Gate");
                                Console.Write("Choose an option:\n");
                                int modifyFlightOption = Convert.ToInt32(Console.ReadLine());
                                //modify origin, destination, expected time
                                if (modifyFlightOption == 1)
                                {
                                    Console.Write("Enter new Origin: ");
                                    string origin = Console.ReadLine();
                                    Console.Write("Enter new Destination: ");
                                    string destination = Console.ReadLine();
                                    Console.Write("Enter new Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
                                    DateTime expectedTime = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy hh:mm", CultureInfo.InvariantCulture);
                                    foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
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
                                    List<string> statusList = new List<string> { "Delayed", "Boarding", "On Time", "Scheduled" };
                                    Console.Write("Enter new Status: ");
                                    string status = Console.ReadLine();
                                    if (!statusList.Contains(status))
                                    {
                                        Console.WriteLine("Invalid Status!");
                                        continue;
                                    }
                                    foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
                                    {
                                        if (flight.Value.FlightNumber == flightNumber)
                                        {
                                            flight.Value.Status = status;
                                            break;
                                        }
                                        else
                                        {
                                            continue;
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
                                            foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
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
                                else if (modifyFlightOption == 4)
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
                                            foreach (KeyValuePair<string, Flight> flight in terminal.Flights)
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
                                else
                                {
                                    Console.WriteLine("Invalid option!");
                                    continue;
                                }
                                bool boardingGateAssigned = false;
                                Console.WriteLine("Flight updated!");
                                foreach (Flight newFlight in terminal.Flights.Values)
                                {
                                    if (newFlight.FlightNumber == flightNumber)
                                    {
                                        Console.WriteLine("Flight Number: " + newFlight.FlightNumber);

                                        Console.WriteLine("Airline Name: " + terminal.GetAirlineFromFlight(newFlight).Name);
                                        Console.WriteLine("Origin: " + newFlight.Origin);
                                        Console.WriteLine("Destination: " + newFlight.Destination);
                                        Console.WriteLine("Expected Time: " + newFlight.ExpectedTime);
                                        Console.WriteLine("Status: " + newFlight.Status);
                                        foreach (BoardingGate assignedFlight in terminal.BoardingGates.Values)
                                        {
                                            if (assignedFlight.Flight.FlightNumber == flightNumber)
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
                            }
                        }
                        // Delete flight
                        else if (flightOption == 2)
                        {
                            Console.Write("Select an existing flight to delete: ");
                            string flightToDelete = Console.ReadLine();
                            if (!terminal.Flights.ContainsKey(flightToDelete))
                            {
                                Console.WriteLine("Flight does not exist!");
                                continue;
                            }
                            else
                            {
                                flightFound = true;
                                Console.Write("Are you sure you want to delete? [Y/N]: ");
                                string confirmation = Console.ReadLine();
                                if (confirmation == "Y")
                                {
                                    terminal.Flights.Remove(flightToDelete);
                                    Console.WriteLine("Flight deleted!");
                                }
                                else if (confirmation == "N")
                                {
                                    Console.WriteLine(flightToDelete + " has not been deleted");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option!");
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid option!");
                            continue;
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
        if (airLineFound == false)
        {
            Console.WriteLine("Invalid Airline Code!");
        }
        else if (flightFound == false)
        {
            Console.WriteLine("Flight does not exist!");
        }
        else
        {
            break;
        }
    }
}

//9) Display flights in chronological order

void DisplayFlightSchedule()
{
    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5, -10}", "Flight Number", "Airline", "Origin", "Destination", "Expected Time", "Boarding Gate");
List<Flight> sortedFlightList = new List<Flight>();
    sortedFlightList = terminal.Flights.Values.ToList<Flight>();
    sortedFlightList.Sort();

    foreach (Flight flight in sortedFlightList)
    {
        BoardingGate? assignedBoardingGate = null;
        foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
        {
            if (boardingGate.Flight == flight) //check if any boarding gate has been assigned to the current flight
            {
                assignedBoardingGate = boardingGate;
            }    
        }
        if (assignedBoardingGate == null)
        {
            Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}", flight.FlightNumber, terminal.GetAirlineFromFlight(flight).Name, flight.Origin, flight.Destination, flight.ExpectedTime);
        }
        else
        {
            Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5, -10}", flight.FlightNumber, terminal.GetAirlineFromFlight(flight).Name, flight.Origin, flight.Destination, flight.ExpectedTime, assignedBoardingGate.GateName);
        }
    }
}

//(a)	Process all unassigned flights to boarding gates in bulk
void AssignFlightsToBoardingGates()
{
    int unassignedFlights = 0;
    int unassignedBoardingGates = 0;
    Queue<Flight> flightWithoutBoardingGate = new Queue<Flight>();

    foreach (Flight flight in terminal.Flights.Values)
    {
        bool assignedFlight = false;
        foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
        {
            if (boardingGate.Flight.FlightNumber == flight.FlightNumber)
            {
                assignedFlight = true;
                continue;
            }
            else
            {
                continue;
            }
        }
        if (assignedFlight == false)
        {
            flightWithoutBoardingGate.Enqueue(flight);
            unassignedFlights += 1;
        }
    }
    Console.WriteLine(unassignedFlights + " Flights do not have a Boarding Gate assigned to them!");

    foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
    {
        bool assignedBoardingGate = false;
        foreach (Flight flight in terminal.Flights.Values)
        {
            if (boardingGate.Flight.FlightNumber == flight.FlightNumber)
            {
                assignedBoardingGate = true;
                continue;
            }
            else
            {
                continue;
            }
        }
        if (assignedBoardingGate == false)
        {
            unassignedBoardingGates += 1;
        }
    }
    Console.WriteLine(unassignedBoardingGates + " Boarding Gates do not have a Flight Number assigned to them!\n");
    if (unassignedFlights != 0)
    {
        Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", "Flight Number", "Airline Name", "Origin", "Destination", "Expected Time", "Special Request Code", "Boarding Gate");
    }
    int processedFlightsBoardingGates = 0;
    while (flightWithoutBoardingGate.Count > 0)
    {
        Flight flightToAssign = flightWithoutBoardingGate.Dequeue();
        if (flightToAssign is NORMFlight)
        {
            foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
            {
                if ((boardingGate.SupportsDDJB == false) && (boardingGate.SupportsCFFT == false) && (boardingGate.SupportsLWTT == false) && (boardingGate.Flight.FlightNumber == ""))
                {
                    boardingGate.Flight = flightToAssign;
                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flightToAssign.FlightNumber, terminal.GetAirlineFromFlight(flightToAssign).Name, flightToAssign.Origin, flightToAssign.Destination, flightToAssign.ExpectedTime, "None", boardingGate.GateName);
                }
                else
                {
                    continue;
                }
                processedFlightsBoardingGates += 1;
                break;
            }
        }
        else
        {
            foreach (BoardingGate boardingGate in terminal.BoardingGates.Values)
            {
                if ((boardingGate.SupportsDDJB == true) && (flightToAssign is DDJBFlight) && (boardingGate.Flight.FlightNumber == ""))
                {
                    boardingGate.Flight = flightToAssign;
                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flightToAssign.FlightNumber, terminal.GetAirlineFromFlight(flightToAssign).Name, flightToAssign.Origin, flightToAssign.Destination, flightToAssign.ExpectedTime, "DDJB", boardingGate.GateName);
                }
                else if ((boardingGate.SupportsCFFT == true) && (flightToAssign is CFFTFlight) && (boardingGate.Flight.FlightNumber == ""))
                {
                    boardingGate.Flight = flightToAssign;
                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flightToAssign.FlightNumber, terminal.GetAirlineFromFlight(flightToAssign).Name, flightToAssign.Origin, flightToAssign.Destination, flightToAssign.ExpectedTime, "CFFT", boardingGate.GateName);
                }
                else if ((boardingGate.SupportsLWTT == true) && (flightToAssign is LWTTFlight) && (boardingGate.Flight.FlightNumber == ""))
                {
                    boardingGate.Flight = flightToAssign;
                    Console.WriteLine("{0,-16}{1,-23}{2,-23}{3,-23}{4,-23}{5,-23}{6,-23}", flightToAssign.FlightNumber, terminal.GetAirlineFromFlight(flightToAssign).Name, flightToAssign.Origin, flightToAssign.Destination, flightToAssign.ExpectedTime, "LWTT", boardingGate.GateName);
                }
                else
                {
                    continue;
                }
                processedFlightsBoardingGates += 1;
                break;
            }
        }
    }
    try
    {
    Console.WriteLine(processedFlightsBoardingGates + " Flight(s) and Boarding Gate(s) was/were processed and assigned!");
    Console.WriteLine("A total of {0}% of Flights and Boarding Gates were processed and assigned automatically!", ((processedFlightsBoardingGates / (terminal.Flights.Count * 1.00)) * 100).ToString("0.00"));
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine("A total of 0% of Flights and Boarding Gates were processed and assigned automatically!");
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
    Console.WriteLine("8. Assign all Flights to Boarding Gates");
    Console.WriteLine("9. Print Airline Fees");
    Console.WriteLine("0. Exit");
    try
    {
        Console.Write("Please select your option:\n");
        int option = Convert.ToInt32(Console.ReadLine());
        if (option == 1)
        {
            DisplayFlights();
        }
        else if (option == 2)
        {
            DisplayBoardingGates();
        }
        else if (option == 3)
        {
            AssignBoardingGateToFlight();
        }
        else if (option == 4)
        {
            CreateNewFlight();
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
            DisplayFlightSchedule();
        }
        else if (option == 8)
        {
            AssignFlightsToBoardingGates();
        }
        else if (option == 9)
        { 
            terminal.PrintAirlineFees();
        }
        else if (option == 0)
        {
            Console.WriteLine("\nGoodbye!");
            break;
        }
        else
        {
            Console.WriteLine("Invalid option!");
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
