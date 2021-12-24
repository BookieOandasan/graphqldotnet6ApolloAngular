using GraphQlApi.Notes;
using Microsoft.EntityFrameworkCore;

namespace GraphQlApi.Repository
{
    public class NotesContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesContext(DbContextOptions options) : base(options)
        {

        }
    }
}
