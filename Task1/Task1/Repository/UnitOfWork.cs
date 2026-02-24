using Task1.Models;

namespace Task1.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IRepository<Patient> Patients { get; }
        public IRepository<Doctor> Doctors { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Patients = new Repository<Patient>(_context, _context.Patients);
            Doctors = new Repository<Doctor>(_context, _context.Doctors);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
