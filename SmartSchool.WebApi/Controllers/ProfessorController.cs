using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly SmartContext _context;

        public ProfessorController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {   
            return Ok(_context.Professores);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var _professor  = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(_professor == null) return NotFound("Aluno não encontrado");
            return Ok(_professor);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var _professor  = _context.Professores.FirstOrDefault(a => 
                 a.Nome.Contains(nome) || a.Sobrenome.Contains(sobrenome) 
            );

            if(_professor == null) return NotFound("Aluno não encontrado");

            return Ok(_professor);
        }

        [HttpPost]
        public IActionResult Create(Professor aluno)
        {   
            _context.Add(aluno);
            _context.SaveChanges();
            
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Professor professor)
        {   
            var _professor = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(_professor == null) return NotFound("Aluno não encontrado");

            _professor.Nome = professor.Nome;
            _professor.Sobrenome = professor.Sobrenome;
            _professor.Telefone = professor.Telefone;

            _context.Update(_professor);
            _context.SaveChanges();

            return Ok("Aluno alterado com sucesso");
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {       
            Professor _professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(_professor == null) return NotFound("Aluno não encontrado");

             _context.Remove(_professor);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, Professor professor)
        {   
            Professor _professor = _context.Professores.FirstOrDefault(a => a.Id == id);
            if(_professor == null) return NotFound("Professor não encontrado");

            _professor.Nome = professor.Nome;
            _professor.Sobrenome = professor.Sobrenome;
            _professor.Telefone = professor.Telefone;

             _context.Update(_professor);
            _context.SaveChanges();

            return Ok("Professor alterado com sucesso");
        }
    }
}