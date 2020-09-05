using BusTransportService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusTransportService.Controllers
{
    public class HomeController : Controller
    {

        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Colour()
        {
            ViewBag.Message = "Your application description page.";
            Bus bus = new Bus("B5", 40, "R1", 18);
            ColourCode.color colour = (bus.Occupancy > 90 ?ColourCode.color.red : bus.Occupancy > 70 ? ColourCode.color.orange : ColourCode.color.green);
            return View(colour);
        }

        public ActionResult AddBus()
        {
            ViewBag.Message = "Your add bus page.";
            Bus bus = new Bus("B3", 40, "R1", 18);
            List <Bus> busList = new List<Bus>();
            try
            {
                if (ModelState.IsValid)
                {


                    if (AddBus(bus))
                    {
                        ViewBag.Message = "Employee details added successfully";
                        busList.Add(bus);
                    }
                }

                return View(bus);
            }
            catch (Exception ex)
            {
                return View();
            }
        }


 

        //To Add Employee details
        private bool AddBus(Bus obj)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["getconn"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("AddNewBusDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BusId", obj.BusId);
                    cmd.Parameters.AddWithValue("@Capacity", obj.Capacity);
                    cmd.Parameters.AddWithValue("@Occupancy", obj.Occupancy);
                    cmd.Parameters.AddWithValue("@RouteId", obj.RouteId);
                    cmd.Parameters.AddWithValue("@FuelConsumption", obj.FuelConsumption);

                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    return true;
                }
            }

        }

        //Return Bus List
        public ActionResult ViewBusList()
        {
            return View();
        }


    }
}