using AutenticacaoJwt.Api.Models;
using AutenticacaoJwt.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoJwt.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(TokenService tokenService) : ControllerBase
{
    // Credenciais fixas exclusivamente para fins didáticos, como pede o enunciado.
    private const string UsuarioFixo = "aluno";
    private const string SenhaFixa = "senha123";

    [HttpPost("login")]
    public ActionResult<LoginResponse> Login(LoginRequest request)
    {
        if (request.Usuario != UsuarioFixo || request.Senha != SenhaFixa)
        {
            return Unauthorized();
        }

        return Ok(tokenService.GerarToken(request.Usuario));
    }
}
