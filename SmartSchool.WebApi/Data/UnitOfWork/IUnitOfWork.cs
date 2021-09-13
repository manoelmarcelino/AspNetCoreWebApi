using System;

namespace SmartSchool.WebApi.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IAlunoRepository Alunos {get;}
        IProfessorRepository Professores {get;}
        int Complete();
    }
}