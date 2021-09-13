using System.Collections.Generic;

namespace SmartSchool.WebApi.Models
{
    public class Aluno : Pessoa
    {   
        public Aluno() { }
        public Aluno(int id, string nome, string sobrenome, string telefone) : base(id, nome, sobrenome, telefone) { }
        public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; }
    }
}