using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SnackboxAPI;
using SnackboxAPI.Models;
using DessertboxAPI.Dto;

namespace DessertboxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationdbContext _context;

        public AuthController(IConfiguration configuration, ApplicationdbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("User")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetUser()
        {
            var list = await _context.Users.ToListAsync();

            return Ok(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("UpdateUser")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateUser(UserRoleDto request)
        {
            var entity = await _context.Users.Where(x => x.Id == request.Id).FirstOrDefaultAsync();

            if (entity != null)
            {
                entity.UserRole = request.Role;

                _context.Users.Update(entity);

                await _context.SaveChangesAsync();
            }
            return Ok(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>l
        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            var user = _context.Users.Where(x => x.Username == request.Username).FirstOrDefault();

            if (user != null)
            {
                return BadRequest("User name Already Exist");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordsalt);
            var entity = new User
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordsalt,
				UserRole = request.Role
            };
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();

            return Ok(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<LoginresponseDto>> Login(UserDto request)
        {
            var user = await _context.Users.Where(x=> x.Username == request.Username).FirstOrDefaultAsync();

            if (user == null)
            {
				return BadRequest(new LoginresponseDto { Message = "User not found", StatusCode = 404 });
			}
            if (!VerifyPasswordHash(request.Password, user.PasswordHash!, user.PasswordSalt!))
            {
                return BadRequest(new LoginresponseDto { Message = "Wrong password", StatusCode = 401 });
			}
            string token = CreateToken(user);

			return Ok(new LoginresponseDto { Token = token, StatusCode = 200,Message="success",User= new UserDto { Username=user.Username!,Role=user.UserRole!} }) ;
        }

        private string CreateToken( User user)
        {
            List<Claim> Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.UserRole==null?"user":user.UserRole)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims:Claims,
                signingCredentials:cred,
                expires:DateTime.Now.AddHours(1)
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512(passwordsalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
