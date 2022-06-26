using System.Threading;
using System.Threading.Tasks;
using Diabetes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Interfaces
{
    public interface IGlucoseLevelDbContext
    {
        DbSet<GlucoseNote> GlucoseNotes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}