using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Controllers;

public class TokenService
{
    private const string SecretKey = "my_super_secret_key_that_is_long_enough_to_be_secure"; // Замените на ваш секретный ключ
    private readonly SymmetricSecurityKey _signingKey;

    public TokenService()
    {
        _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }

    public string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                // Добавьте любые другие необходимые утверждения
            }),
            
            SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        // Проверьте учетные данные пользователя (например, через базу данных)
        // Здесь используется простой пример с жестко закодированными данными

        if (loginRequest.Username == "Admin" && loginRequest.Password == "secret_admin")
        {
            var token = _tokenService.GenerateJwtToken(loginRequest.Username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
