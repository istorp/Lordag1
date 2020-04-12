using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lordag1.Data;
using Lordag1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lordag1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private CWheelsDbContext _cWheelsDbContext;
        public VehiclesController(CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
        }
        // GET: api/Vehicles
        [HttpGet]
        public IEnumerable<Vehicle> Get()
        {
            return _cWheelsDbContext.Vehicles;
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}", Name = "Get")]
        public Vehicle Get(int id)
        {
           var vehicle = _cWheelsDbContext.Vehicles.Find(id);
            return vehicle;
        }

        // POST: api/Vehicles
        [HttpPost]
        public void Post([FromBody] Vehicle vehicle)
        {
            _cWheelsDbContext.Vehicles.Add(vehicle);
            _cWheelsDbContext.SaveChanges();
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Vehicle vehicle)
        {
            var entity = _cWheelsDbContext.Vehicles.Find(id);
            entity.Title = vehicle.Title;
            entity.Price = vehicle.Price;
            _cWheelsDbContext.SaveChanges();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
