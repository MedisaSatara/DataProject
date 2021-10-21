using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        NORTHWNDContext db = new NORTHWNDContext();
        private readonly IConfiguration _configuration;

        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("ShowOrders")]
        [HttpGet]
        public JsonResult ShowOrder()
        {
            //List of Orders
            string query = @"
                         select o.OrderId,o.CustomerId,o.EmployeeId,OrderDate,RequiredDate,ShippedDate,o.ShipVia,Freight,ShipName,
                         ShipAddress, ShipCity,ShipRegion,ShipPostalCode,ShipCountry
                           from Orders as o inner join Suppliers as s 
                           on s.SupplierId=o.ShipVia 		   
						   inner join [Order Details] as od on o.OrderId=od.OrderId
						   inner join Products as p on p.ProductId=od.ProductId
						   inner join Categories as c on c.CategoryID=p.CategoryId";

            DataTable table = new DataTable(query);
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand myComm = new SqlCommand(query, myConn))
                {
                    myReader = myComm.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }



            return new JsonResult(table);
        }
    }
}
