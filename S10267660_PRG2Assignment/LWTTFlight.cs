﻿using System;
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
     class LWTTFlight: Flight
    {
        public double RequestFee = 500;
        public LWTTFlight(string FN, string O, string D, DateTime ET, string S) : base(FN, O, D, ET, S)
        {
 
        }

        public override double CalculateFees()
        {
            return base.CalculateFees() + 500;
        }
        public override string ToString()
        {
            return base.ToString() + " Request Fee: " + RequestFee;
        }
    }
}
