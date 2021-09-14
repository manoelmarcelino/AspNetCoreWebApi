using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebApi.Data;
using SmartSchool.WebApi.Data.UnitOfWork;
using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.V2.Controllers
{   
    [ApiController]
    [ApiVersion("2")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AlunoController : ControllerBase
    {       
         private readonly IUnitOfWork _unitOfWork;

        public AlunoController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {   
            return Ok(await _unitOfWork.Alunos.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAsyncById(int id)
        {
            var aluno = await _unitOfWork.Alunos.FindAsync(a => a.Id == id);
            if(aluno == null) return NotFound("Aluno não encontrado");
            return Ok(aluno);
        }

        [HttpGet("ByName")]
        public async Task<IActionResult> GetAsyncByName(string nome, string sobrenome)
        {
            var aluno  = await _unitOfWork.Alunos.FindAsync(a =>  
                 a.Nome.Contains(nome) || a.Sobrenome.Contains(sobrenome) 
            );

            if(aluno == null) return NotFound("Aluno não encontrado");

            return Ok(aluno);
        }

        [HttpPost]
        public async Task<IActionResult>  CreateAsync(Aluno aluno)
        {   
             await _unitOfWork.Alunos.AddAsync(aluno);
             _unitOfWork.Complete();
            
            return Ok(aluno);
        }      
    }
}