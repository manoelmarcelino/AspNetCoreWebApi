using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Data.UnitOfWork;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.V1.Controllers
{   
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProfessorController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfessorController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public IActionResult Get()
        {   
            return Ok(_unitOfWork.Professores.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var _professor  = _unitOfWork.Professores.Find(a => a.Id == id).FirstOrDefault();

            if(_professor == null) return NotFound("Aluno não encontrado");
            return Ok(_professor);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var _professor  = _unitOfWork.Professores.Find(a => 
                 a.Nome.Contains(nome) || a.Sobrenome.Contains(sobrenome) 
            ).FirstOrDefault();

            if(_professor == null) return NotFound("Aluno não encontrado");

            return Ok(_professor);
        }

        [HttpPost]
        public IActionResult Create(Professor professor)
        {   
            _unitOfWork.Professores.Add(professor);
            _unitOfWork.Complete();
            
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Professor professor)
        {   
            var _professor = _unitOfWork.Professores.Find(a => a.Id == id).FirstOrDefault();
            if(_professor == null) return NotFound("Aluno não encontrado");

            _professor.Nome = professor.Nome;
            _professor.Sobrenome = professor.Sobrenome;
            _professor.Telefone = professor.Telefone;

            _unitOfWork.Professores.Add(professor);
            _unitOfWork.Complete();

            return Ok("Aluno alterado com sucesso");
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {       
            Professor _professor = _unitOfWork.Professores.Find(a => a.Id == id).FirstOrDefault();
            if(_professor == null) return NotFound("Aluno não encontrado");

            _unitOfWork.Professores.Add(_professor);
            _unitOfWork.Complete();

            return Ok();
        }

    }
}