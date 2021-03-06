﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lordag1.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lordag1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private CWheelsDbContext _cWheelsDbContext;
        public CategoriesController(CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
           var categories= _cWheelsDbContext.Categories;
            return Ok(categories);
        }
    }
}