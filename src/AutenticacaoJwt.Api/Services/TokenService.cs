using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutenticacaoJwt.Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace AutenticacaoJwt.Api.Services;

public class TokenService(IConfiguration configuration)
{
    public LoginResponse GerarToken(string usuario)
    {
        var jwt = configuration.GetSection("Jwt");
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var expiraEm = DateTime.UtcNow.AddMinutes(jwt.GetValue<int>("ExpiracaoEmMinutos"));

        // O conteúdo do JWT pode ser lido por qualquer um: aqui só entra o nome, nunca a senha.
        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: [new Claim(JwtRegisteredClaimNames.Name, usuario)],
            expires: expiraEm,
            signingCredentials: new SigningCredentials(chave, SecurityAlgorithms.HmacSha256));

        return new LoginResponse(new JwtSecurityTokenHandler().WriteToken(token), expiraEm);
    }
}
