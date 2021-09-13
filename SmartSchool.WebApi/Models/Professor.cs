using System.Collections.Generic;

namespace SmartSchool.WebApi.Models
{
    public class Professor : Pessoa
    {
        public Professor() { }

        public Professor(int professorId, string nome)
        {
            Id = professorId;
            Nome = nome;
        }

        public int DisciplinaId { get; set; }        
        public IEnumerable<Disciplina> Disciplinas { get; set; }
    }
}