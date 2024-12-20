using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTaskManager.Application.Commands;
using MyTaskManager.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace MyTaskManager.API.Controllers
{
    /// <summary>
    /// Controlador responsável por gerenciar tarefas (todos) do usuário autenticado.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Cria uma nova tarefa associada ao usuário autenticado.
        /// </summary>
        /// <param name="command">Objeto com os dados da tarefa a ser criada.</param>
        /// <returns>Dados da tarefa criada.</returns>
        /// <response code="200">Retorna os dados da tarefa criada.</response>
        /// <response code="400">Dados inválidos para a criação da tarefa.</response>
        [HttpPost]
        [SwaggerOperation(Summary = "Cria uma nova tarefa", Description = "Associa uma nova tarefa ao usuário autenticado.")]
        [SwaggerResponse(200, "Tarefa criada com sucesso.", typeof(object))]
        [SwaggerResponse(400, "Dados inválidos para a criação da tarefa.")]
        public async Task<IActionResult> Create([FromBody] CreateTodoCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// Retorna todas as tarefas do usuário autenticado.
        /// </summary>
        /// <returns>Lista de tarefas.</returns>
        /// <response code="200">Lista de tarefas do usuário.</response>
        [HttpGet("All")]
        [SwaggerOperation(Summary = "Retorna todas as tarefas do usuário", Description = "Busca todas as tarefas associadas ao usuário autenticado.")]
        [SwaggerResponse(200, "Lista de tarefas do usuário.", typeof(IEnumerable<object>))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTodosQuery());
            return Ok(result);
        }

        /// <summary>
        /// Retorna uma tarefa específica pelo ID, se pertencer ao usuário autenticado.
        /// </summary>
        /// <param name="id">ID da tarefa.</param>
        /// <returns>Dados da tarefa encontrada.</returns>
        /// <response code="200">Retorna os dados da tarefa encontrada.</response>
        /// <response code="404">Tarefa não encontrada.</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Busca uma tarefa pelo ID", Description = "Busca uma tarefa específica pelo ID associado ao usuário autenticado.")]
        [SwaggerResponse(200, "Tarefa encontrada.", typeof(object))]
        [SwaggerResponse(404, "Tarefa não encontrada.")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetTodoByIdQuery { Id = id });

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// Atualiza uma tarefa existente pelo ID, se pertencer ao usuário autenticado.
        /// </summary>
        /// <param name="id">ID da tarefa.</param>
        /// <param name="command">Objeto com os dados atualizados da tarefa.</param>
        /// <returns>Resultado da atualização da tarefa.</returns>
        /// <response code="204">Tarefa atualizada com sucesso.</response>
        /// <response code="400">Dados inválidos para a atualização da tarefa.</response>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Atualiza uma tarefa", Description = "Atualiza uma tarefa existente associada ao usuário autenticado.")]
        [SwaggerResponse(204, "Tarefa atualizada com sucesso.")]
        [SwaggerResponse(400, "Dados inválidos para a atualização da tarefa.")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTodoCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            command.Id = id;

            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa existente pelo ID, se pertencer ao usuário autenticado.
        /// </summary>
        /// <param name="id">ID da tarefa.</param>
        /// <returns>Status de exclusão.</returns>
        /// <response code="204">Tarefa excluída com sucesso.</response>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Exclui uma tarefa", Description = "Exclui uma tarefa existente associada ao usuário autenticado.")]
        [SwaggerResponse(204, "Tarefa excluída com sucesso.")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTodoCommand { Id = id });

            return NoContent();
        }
    }
}
