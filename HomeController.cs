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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Colour()
        {
            ViewBag.Message = "Your application description page.";
            Bus bus = new Bus("B2", 40, "R1", 18);
            ColourCode.color colour = (bus.Occupancy > 90 ?ColourCode.color.red : bus.Occupancy > 70 ? ColourCode.color.orange : ColourCode.color.green);
            return View(colour);
        }



        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            Bus bus = new Bus("B1", 40, "R1", 18);
            try
            {
                if (ModelState.IsValid)
                {


                    if (AddBus(bus))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
            return View();
        }

        //
        private SqlConnection con;
        //To Handle connection related activities
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        public ActionResult AddNewBus()
        {
            Bus bus = new Bus("",40,"R1",18);
            try
            {
                if (ModelState.IsValid)
                {


                    if (AddBus(bus))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }


       

        //To Add Employee details
        public bool AddBus(Bus obj)
        {

            //connection();
            //SqlCommand com = new SqlCommand("AddNewBusDetails", con);
            ////com.CommandType = com.;
            //com.Parameters.AddWithValue("@BusId", obj.BusId);
            //com.Parameters.AddWithValue("@Capacity", obj.Capacity);
            //com.Parameters.AddWithValue("@Occupancy", obj.Occupancy);
            //com.Parameters.AddWithValue("@RouteId", obj.RouteId);
            //com.Parameters.AddWithValue("@FuelConsumption", obj.FuelConsumption);

            //con.Open();
            //int i = com.ExecuteNonQuery();
            //con.Close();
            //if (i >= 1)
            //{

            //    return true;

            //}
            //else
            //{

            //    return false;
            //}
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
                    //cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = txtFirstName.Text;
                    //cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }

        }


    }
}