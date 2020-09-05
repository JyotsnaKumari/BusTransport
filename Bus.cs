using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusTransportService.Models
{
    public class Bus
    {
        public string BusId { get; set; }
         
        public int Capacity { get; set; }

        public int Occupancy { get; set; }

        public string RouteId { get; set; }

        public double FuelConsumption { get; set; }

        public Bus(string busId, int occupancy, string routeId, double fuelCons)
        {
            this.BusId = busId;
            this.Capacity = 100;
            this.Occupancy = occupancy;
            this.RouteId = routeId;
            this.FuelConsumption = fuelCons;

        }

    }
}