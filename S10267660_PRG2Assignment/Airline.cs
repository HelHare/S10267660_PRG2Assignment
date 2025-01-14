﻿using System;
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
     class Airline
    {
        public string Name { get; set; }
        public string Code {  get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Airline(string name, string code, Dictionary<string, Flight> flights)
        {
            Name = name;
            Code = code;
            Flights = flights;
        }
        public Airline()
        {
            Name = "";
            Code = "";
            Flights = new Dictionary<string, Flight>();
        }
        public override string ToString()
        {
            string str = "Airline: " + Name + " (" + Code + ")\n";
            foreach (KeyValuePair<string, Flight> flight in Flights)
            {
                str += flight.Value.ToString() + "\n";
            }
            return str;
        }
    }
}
