using Ada.Kanban.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ada.Kanban.Db.DbContexts
{
    public class AdaKanbanDbContext : DbContext
    {
        public virtual DbSet<Card> Cards => Set<Card>();

        public AdaKanbanDbContext()
        {
        }

        public AdaKanbanDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
