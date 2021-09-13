using SmartSchool.WebApi.Models;

namespace SmartSchool.WebApi.Data
{
    public class ProfessorRepository : GenericRepository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(SmartContext context) : base(context)
        {
            
        }
    }
}