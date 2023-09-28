using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestPostgreSqlApi.Context;
using TestPostgreSqlApi.Entities;
using TestPostgreSqlApi.Models;

namespace TestPostgreSqlApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly DBContext _context;

        public ClienteController(ILogger<ClienteController> logger, DBContext context) 
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("Cadastrar")]
        public async Task<IActionResult> Cadastrar(CadastrarClienteRequest clienteRequest)
        {
            try
            {
                _logger.LogInformation("Chamou CadastrarCliente.");

                if (await _context.Clientes.AnyAsync(c => c.Email == clienteRequest.Email))
                    return BadRequest($"Email {clienteRequest.Email} já esta cadastrado.");

                var clienteEntity = new ClienteEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Nome = clienteRequest.Nome,
                    Email = clienteRequest.Email,
                    Telefone = clienteRequest.Telefone,
                };

                await _context.Clientes.AddAsync(clienteEntity);
                await _context.SaveChangesAsync();

                return Ok(clienteEntity.Id);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("Obter/{id}")]
        public async Task<IActionResult> Obter([FromRoute]string id)
        {
            try
            {
                _logger.LogInformation("Chamou ObterCliente.");

                var cliente = await _context.Clientes.SingleOrDefaultAsync(c => c.Id == id);
                
                if(cliente == null)
                    return BadRequest("Cliente não encontrado.");

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                _logger.LogInformation("Chamou ListarClientes.");

                var clientes = await _context.Clientes.ToListAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}