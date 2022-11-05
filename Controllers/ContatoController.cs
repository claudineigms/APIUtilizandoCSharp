using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entities;
using api.Context;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context){
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Contato contato){
            _context.Add(contato);
            _context.SaveChanges();
            return Ok(contato);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id){
            var contato = _context.Contatos.Find(id);
            if (contato == null){
                return NotFound();
            }
            else
            {
                return Ok(contato);
            }
        }

        [HttpGet("ObterPorNome")]
        public IActionResult ObterPorNome(string nome){
            var contato = _context.Contatos.Where(x => x.Nome.Contains(nome));
            return Ok(contato);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarContato(int id, Contato pessoa){
            var contatoDB = _context.Contatos.Find(id);
            if (contatoDB == null){
                return NotFound();
            }
            else{
                contatoDB.Nome = pessoa.Nome;
                contatoDB.Telefone = pessoa.Telefone;
                contatoDB.Status = pessoa.Status;


                _context.Contatos.Update(contatoDB);
                _context.SaveChanges();  
                return Ok(contatoDB);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarContato(int id){
            var contato = _context.Contatos.Find(id);
            if (contato == null){
                return NotFound();
            }
            else{
                _context.Contatos.Remove(contato);
                _context.SaveChanges();
                return NoContent();
            }
            

        }
    }
}