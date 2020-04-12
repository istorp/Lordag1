using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthenticationPlugin;
using Lordag1.Data;
using Lordag1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Lordag1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private CWheelsDbContext _cWheelsDbContext;
        private IConfiguration _configuration;
        private readonly AuthService _auth;
        public AccountsController(IConfiguration configuration, CWheelsDbContext cWheelsDbContext)
        {
            _cWheelsDbContext = cWheelsDbContext;
            _configuration = configuration;
            _auth = new AuthService(_configuration);
        }

        [HttpPost]
        public IActionResult Register([FromBody]User user)
        {
            var userWhitSameEmail= _cWheelsDbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWhitSameEmail !=null)
            {
                return BadRequest("User with same email already exists");
            }
            var userObj = new User()
            {
                Name=user.Name,
                Email=user.Email,
                Password= SecurePasswordHasherHelper.Hash (user.Password),
            };
            _cWheelsDbContext.Users.Add(userObj);
            _cWheelsDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
          var userEmail = _cWheelsDbContext.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userEmail==null)
            {
                return NotFound();
            }
            if (SecurePasswordHasherHelper.Verify(user.Password, userEmail.Password))
            {
                return Unauthorized();
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
            };
            var token = _auth.GenerateAccessToken(claims);
            return new ObjectResult(new
            {
                access_token = token.AccessToken,
                expires_in = token.ExpiresIn,
                token_type = token.TokenType,
                creation_Time = token.ValidFrom,
                expiration_Time = token.ValidTo,
                user_id=userEmail.Id,


            });

        }
    }
}