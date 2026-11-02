namespace AutenticacaoJwt.Api.Models;

public record LoginResponse(string Token, DateTime ExpiraEm);
