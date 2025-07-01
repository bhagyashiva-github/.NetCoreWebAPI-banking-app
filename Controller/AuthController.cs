#region JWT Authentication Logic - Implementation in Progress
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.IdentityModel.Tokens;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;

// namespace coreAPIData.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     [ApiExplorerSettings(GroupName = "v1")]
//     public class AuthController : ControllerBase
//     {
//         private readonly IConfiguration _config;

//         public AuthController(IConfiguration config)
//         {
//             _config = config;
//         }

//         [HttpPost("login")]
//         public IActionResult Login([FromBody] LoginRequest request)
//         {
//             if (request.Username != "admin" || request.Password != "password123")
//                 return Unauthorized("Invalid credentials.");

//             var claims = new[]
//             {
//                 new Claim(ClaimTypes.Name, request.Username),
//                 new Claim(ClaimTypes.Role, "Admin") // Optional roles
//             };
//             var keyString = _config["Jwt:Key"];
//             if (string.IsNullOrWhiteSpace(keyString))
//                 throw new InvalidOperationException("JWT Key is missing or empty in configuration.");

//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));

//             var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//             var token = new JwtSecurityToken(
//                 issuer: _config["Jwt:Issuer"],
//                 audience: _config["Jwt:Audience"],
//                 claims: claims,
//                 expires: DateTime.UtcNow.AddHours(2),
//                 signingCredentials: creds);

//             return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
//         }
//     }
// }
#endregion