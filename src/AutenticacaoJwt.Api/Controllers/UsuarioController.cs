using AutenticacaoJwt.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutenticacaoJwt.Api.Controllers;

[ApiController]
[Route("api/usuario")]
public class UsuarioController : ControllerBase
{
    [Authorize]
    [HttpGet("perfil")]
    public ActionResult<PerfilResponse> ObterPerfil()
    {
        var usuario = User.Identity!.Name!;

        return Ok(new PerfilResponse(usuario, $"Olá, {usuario}! Você está autenticado."));
    }
}
