using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        NORTHWNDContext db = new NORTHWNDContext();
        private readonly IConfiguration _configuration;
        
        public SupplierController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("ShowSuppliers")]
        [HttpGet]
        public List<Supplier> ShowSuppliers()
        {
            //List of supplier
            List<Supplier> list = db.Suppliers.ToList();
            return list;
        }
    }
}
