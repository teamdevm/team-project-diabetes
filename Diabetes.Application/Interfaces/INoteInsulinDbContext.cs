using System.Threading;
using System.Threading.Tasks;
using Diabetes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Interfaces
{
    public interface INoteInsulinDbContext
    {
        DbSet<InsulinNote> InsulinNotes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
