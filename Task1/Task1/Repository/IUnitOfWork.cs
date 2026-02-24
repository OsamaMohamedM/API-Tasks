using Task1.Models;

namespace Task1.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Patient> Patients { get; }
        IRepository<Doctor> Doctors { get; }
        Task<int> SaveChangesAsync();
    }
}
