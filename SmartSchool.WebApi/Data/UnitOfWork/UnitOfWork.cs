namespace SmartSchool.WebApi.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SmartContext _context;

        public UnitOfWork(SmartContext context)
        {
            _context = context;
            Alunos = new AlunoRepository(_context);
            Professores = new ProfessorRepository(_context);
        }
        public IAlunoRepository Alunos { get; private set; }
        public IProfessorRepository Professores { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }   
    }
}