using GraphQlApi.Notes;
using Microsoft.EntityFrameworkCore;

namespace GraphQlApi.Repository
{
    public class NotesContext : DbContext, INotesContext
    {
        public DbSet<Note> Notes { get; set; }

        public NotesContext(DbContextOptions options) : base(options)
        {

        }

        public string DeleteNote(Guid noteToDelete)
        {
            
            var note = Notes.FirstOrDefault(n => n.Id == noteToDelete);

            if (note == null)
            {
                return "Cound not find note in the db";
            }
            Notes.Remove(note);
            SaveChanges();

            return $"The note with id: {noteToDelete} has been successfully delete from db";
        }

        public Note? UpdateNote(Note noteToUpdate)
        {
            var note = Notes.FirstOrDefault(n => n.Id == noteToUpdate.Id);

            if (note == null)
            {
                new Exception("Cound not find note in the db");
                return null;
            }

            Notes.Attach(noteToUpdate);
            SaveChanges();

            return note;
        }

        public Note GetNoteById(Guid id)
        {
            return Notes.FirstOrDefault(f => f.Id == id);
        }

        public IQueryable<Note> GetAllNotes()
        {
            return Notes.AsQueryable();
        }
    }
}

