using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTaskManager.Application.Commands;
using Swashbuckle.AspNetCore.Annotations;

namespace MyTaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gera um token JWT para o usuário autenticado.
        /// </summary>
        /// <param name="loginDto">Objeto com as credenciais do usuário.</param>
        /// <returns>Token JWT caso as credenciais sejam válidas.</returns>
        /// <response code="200">Retorna o token JWT.</response>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpPost("token")]
        [SwaggerOperation(Summary = "Autentica um usuário", Description = "Valida as credenciais do usuário e retorna um token JWT.")]
        [SwaggerResponse(200, "Token gerado com sucesso.", typeof(object))]
        [SwaggerResponse(401, "Credenciais inválidas.")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginDto)
        {
            var token = await _mediator.Send(loginDto);
            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token });
        }

        /// <summary>
        /// Registra um novo usuário no sistema.
        /// </summary>
        /// <param name="registerDto">Objeto com os dados do usuário a ser registrado.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        /// <response code="200">Usuário registrado com sucesso.</response>
        /// <response code="400">Dados inválidos ou erro de operação.</response>
        [HttpPost("register")]
        [SwaggerOperation(Summary = "Registra um novo usuário", Description = "Adiciona um novo usuário ao sistema.")]
        [SwaggerResponse(200, "Usuário registrado com sucesso.")]
        [SwaggerResponse(400, "Erro ao registrar o usuário.")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand registerDto)
        {
            try
            {
                await _mediator.Send(registerDto);
                return Ok("User registered successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
