using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {       
        private readonly SmartContext _context;

        public AlunoController(SmartContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {   
            return Ok(_context.Alunos);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno  = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(aluno == null) return NotFound("Aluno não encontrado");
            return Ok(aluno);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno  = _context.Alunos.FirstOrDefault(a => 
                 a.Nome.Contains(nome) || a.Sobrenome.Contains(sobrenome) 
            );

            if(aluno == null) return NotFound("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {   
            _context.Add(aluno);
            _context.SaveChanges();
            
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Aluno aluno)
        {   
            var _aluno = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
            if(_aluno == null) return NotFound("Aluno não encontrado");

            _aluno.Nome = aluno.Nome;
            _aluno.Sobrenome = aluno.Sobrenome;
            _aluno.Telefone = aluno.Telefone;

            _context.Update(_aluno);
            _context.SaveChanges();

            return Ok("Aluno alterado com sucesso");
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {       
            Aluno _aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(_aluno == null) return NotFound("Aluno não encontrado");

             _context.Remove(_aluno);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("{id:int}")]
        public IActionResult Patch(int id, Aluno aluno)
        {   
            Aluno _aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
            if(_aluno == null) return NotFound("Aluno não encontrado");

            _aluno.Nome = aluno.Nome;
            _aluno.Sobrenome = aluno.Sobrenome;
            _aluno.Telefone = aluno.Telefone;

             _context.Update(_aluno);
            _context.SaveChanges();

            return Ok("Aluno alterado com sucesso");
        }
    }
}