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
        public IActionResult Get()   // IEnumerable<Vehicle>
        {
            //return _cWheelsDbContext.Vehicles;
            return Ok(_cWheelsDbContext.Vehicles);
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult  Get(int id)
        {
           var vehicle = _cWheelsDbContext.Vehicles.Find(id);
            if (vehicle==null)
            {
                return NotFound("Dident find it");
            }
            return Ok(vehicle);
        }

        // POST: api/Vehicles
        [HttpPost]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            _cWheelsDbContext.Vehicles.Add(vehicle);
            _cWheelsDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Vehicle vehicle)
        {
            var entity = _cWheelsDbContext.Vehicles.Find(id);
            if (entity==null)
            {
                return NotFound("no reckord was found vid that id");
            }
            else
            {
                entity.Title = vehicle.Title;
                entity.Price = vehicle.Price;
                entity.Color = vehicle.Color;
                _cWheelsDbContext.SaveChanges();
                return Ok("Reckord was Updated Nicely");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var vehicle = _cWheelsDbContext.Vehicles.Find(id);
            if (vehicle==null)
            {
                return NotFound("no good record try again!");
            }
            else
            {
            _cWheelsDbContext.Vehicles.Remove(vehicle);
            _cWheelsDbContext.SaveChanges();
            return Ok("Post was deleted nicely");
            }
        }
    }
}
