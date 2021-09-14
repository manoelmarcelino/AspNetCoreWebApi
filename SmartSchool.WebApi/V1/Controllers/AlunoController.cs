using System.Collections.Generic;
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
    public class AlunoController : ControllerBase
    {       
         private readonly IUnitOfWork _unitOfWork;

        public AlunoController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public IActionResult Get()
        {   
            return Ok(_unitOfWork.Alunos.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var aluno  = _unitOfWork.Alunos.Find(a => a.Id == id).FirstOrDefault();
            if(aluno == null) return NotFound("Aluno não encontrado");
            return Ok(aluno);
        }

        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            var aluno  = _unitOfWork.Alunos.Find(a =>  
                 a.Nome.Contains(nome) || a.Sobrenome.Contains(sobrenome) 
            ).FirstOrDefault();

            if(aluno == null) return NotFound("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Create(Aluno aluno)
        {   
             _unitOfWork.Alunos.Add(aluno);
             _unitOfWork.Complete();
            
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Aluno aluno)
        {   
            var _aluno = _unitOfWork.Alunos.Find(a => a.Id == id).FirstOrDefault();
            if(_aluno == null) return NotFound("Aluno não encontrado");

            _aluno.Nome = aluno.Nome;
            _aluno.Sobrenome = aluno.Sobrenome;
            _aluno.Telefone = aluno.Telefone;

            _unitOfWork.Alunos.Update(_aluno);
            _unitOfWork.Complete();

            return Ok("Aluno alterado com sucesso");
        }
        
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {       
            var _aluno = _unitOfWork.Alunos.Find(a => a.Id == id).FirstOrDefault();
            if(_aluno == null) return NotFound("Aluno não encontrado");

            _unitOfWork.Alunos.Remove(_aluno);
            _unitOfWork.Complete();

            return Ok();
        }
    }
}