# Lista de Exercícios: .NET 10, C# e JWT

API em ASP.NET Core com autenticação JWT e uma rota protegida. Não usa banco de dados: o usuário e a senha são fixos no código, exclusivamente para fins didáticos.

As respostas dos exercícios teóricos e o resumo do exercício prático estão em [docs/lista-de-exercicios.docx](docs/lista-de-exercicios.docx).

## Como executar

É preciso ter o [SDK do .NET 10](https://dotnet.microsoft.com/download/dotnet/10.0).

**Visual Studio:** abra `ListaAutenticacaoH1.slnx`, selecione o perfil `http` e pressione F5.

**Linha de comando:**

```bash
dotnet run --project src/AutenticacaoJwt.Api
```

A API fica disponível em `http://localhost:5188`.

## Rotas

| Método | Rota | Proteção | Resposta |
|---|---|---|---|
| POST | `/api/auth/login` | Pública | `200` com o token, ou `401` se as credenciais estiverem incorretas |
| GET | `/api/usuario/perfil` | `[Authorize]` | `200` com o nome do usuário, ou `401` sem token válido |

Credenciais de teste:

```json
{ "usuario": "aluno", "senha": "senha123" }
```

## Como testar no Postman

1. Com a API em execução, importe `postman/lista-autenticacao-h1.postman_collection.json` no Postman.
2. Execute as requisições na ordem. O Teste 1 guarda o token, e o Teste 4 usa esse token em **Authorization → Bearer Token**.

| Teste | O que faz | Resultado esperado |
|---|---|---|
| 1. Login válido | Envia usuário e senha corretos | `200 OK` e um token JWT |
| 2. Login inválido | Envia uma senha incorreta | `401 Unauthorized` |
| 3. Acesso sem token | Chama a rota protegida sem token | `401 Unauthorized` |
| 4. Acesso com token válido | Chama a rota protegida com o token | `200 OK` e a mensagem com o nome do usuário |

Para rodar a coleção pela linha de comando, com a API em execução:

```bash
npx newman run postman/lista-autenticacao-h1.postman_collection.json
```

## Evidências

- Coleção exportada: [postman/lista-autenticacao-h1.postman_collection.json](postman/lista-autenticacao-h1.postman_collection.json)
- Resultado da execução: [docs/evidencias/resultado-dos-testes.txt](docs/evidencias/resultado-dos-testes.txt)

## Estrutura

```text
src/AutenticacaoJwt.Api/
  Controllers/AuthController.cs     login e validação das credenciais fixas
  Controllers/UsuarioController.cs  rota protegida
  Services/TokenService.cs          geração do token JWT
  Models/                           objetos de entrada e saída das rotas
  Program.cs                        configuração da autenticação JWT
```

## Segurança

A chave em `appsettings.json` é um valor didático, sem uso real. Em um sistema real, a chave ficaria fora do repositório e as senhas seriam guardadas com hash em um banco de dados.
