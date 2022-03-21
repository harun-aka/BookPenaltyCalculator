using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarsListed             = "Cars Listed";
        public static string BusesListed            = "Buses Listed";
        public static string BoatsListed            = "BoatsListed";
        public static string BoatsListedByColor     = "Boats Listed By Color";
        public static string BusesListedByColor     = "Buses Listed By Color";
        public static string CarsListedByColor      = "Cars Listed By Color";
        public static string CarDeleted             = "Car Deleted";
        public static string CarNotFound            = "Car Not Found";
        public static string CarHighlightsToggled   = "Car Highlights Toggled";
        public static string CountriesReturnedNull;
        internal static CalculatedPenaltyDto NonBusinesDaysNotFound;
        internal static CountryCurrencyDto CountryNotFound;
    }
}
