using AutoMapper;
using MyWebAPI.Helpers;
using MyWebAPI.Models;
using MyWebAPI.ViewModels;
using MyWebAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        #region Private Properties
        private readonly IUserRepository UserRepository;
        private readonly JwtService JwtService;
        private readonly PasswordService PasswordService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public AuthController(IUserRepository userRepository,
            JwtService jwtService,
            PasswordService passwordService,
            IMapper mapper)
        {
            UserRepository = userRepository;
            JwtService = jwtService;
            PasswordService = passwordService;
            _mapper = mapper;
        }
        #endregion

        #region Public Methods
        [HttpPost("register")]
        public async Task<IActionResult> Register(string Username, string Password)
        {
            var userExist = await UserRepository.GetByUsername(Username);

            if (userExist != null)
            {
                return BadRequest("User already exists.");
            }

            PasswordService.CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            return Created("success", await UserRepository.Register(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string Username, string Password)
        {
            var user = await UserRepository.GetByUsername(Username);

            if (user == null)
            {
                return BadRequest("User not found!");
            }

            if (!PasswordService.VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Password is incorrect!");
            }

            var jwt = JwtService.GenerateToken(user);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true
            });

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.JwtToken = jwt;

            return Ok(userViewModel);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var jwtToken = Request.Cookies["jwt"];

                var token = JwtService.ValidateToken(jwtToken);

                var issuerId = token.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                       .Select(c => c.Value).SingleOrDefault();
                var user = await UserRepository.GetById(int.Parse(issuerId));

                return Ok(user);
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok("Success");

        }
        #endregion
    }
}