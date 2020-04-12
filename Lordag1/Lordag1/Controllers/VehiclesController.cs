using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Lordag1.Data;
using Lordag1.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Post(Vehicle vehicle)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
            var user = _cWheelsDbContext.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null)
            {
                return NotFound();
            }
            var vehicleObj = new Vehicle()
            {
                Title = vehicle.Title,
                Description = vehicle.Description,
                Color = vehicle.Color,
                Company = vehicle.Company,
                Condition = vehicle.Condition,
                DatePosted = vehicle.DatePosted,
                Engine = vehicle.Engine,
                Price = vehicle.Price,
                Model = vehicle.Model,
                Location = vehicle.Location,
                CategoryId = vehicle.CategoryId,
                IsFeatured=false,
                IsHotAndNew=false,
                UserId=user.Id,
            };
            _cWheelsDbContext.Vehicles.Add(vehicleObj);
            _cWheelsDbContext.SaveChanges();

            return Ok(new { vehicleId = vehicleObj.Id, message = "Vehicle added successfullly" });

        }
        [HttpGet("[action]")]
        public IActionResult HotAndNewAds()
        {
            var vehicles = from v in _cWheelsDbContext.Vehicles
            where v.IsHotAndNew == true
            select new
            {
                Id = v.Id,
                Title = v.Title,
                ImageUrl = v.Images.FirstOrDefault().ImageUrl
            };
            return Ok(vehicles);
        }
    }
}