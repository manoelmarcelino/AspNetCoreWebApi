using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Data
{
    public class AlunoRepository : GenericRepository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(SmartContext context) : base(context)
        {
            
        }
    }
}