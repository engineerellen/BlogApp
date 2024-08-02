using BlogApp.Entities;
using BlogApp.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserRegisterModel model)
    {
        return Ok(new { Message = "Usuário registrado com sucesso." });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginModel model)
    {
        var token = _authenticationService.Authenticate(model.Username, model.Password);
        if (token == null)
        {
            return Unauthorized();
        }
        return Ok(new { Token = token });
    }
}