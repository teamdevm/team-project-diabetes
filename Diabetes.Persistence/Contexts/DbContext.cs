using Diabetes.Application.Interfaces;
using Diabetes.Domain;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Persistence
{
    public class DataDbContext : DbContext, IGlucoseLevelDbContext, INoteInsulinDbContext
    {
        public DbSet<GlucoseLevel> GlucoseLevels { get; set; }

        public DbSet<NoteInsulin> InsulinNotes { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> opt) : base(opt) {}
    }
}