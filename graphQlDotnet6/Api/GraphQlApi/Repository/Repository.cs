using GraphQlApi.Notes;

namespace GraphQlApi.Repository
{
    public class Repository : IRepository
    {
        private readonly INotesContext _notesContext;

        public Repository(INotesContext notesContext)
        {
            _notesContext = notesContext;
        }
        public string DeleteNote(Guid noteToDelete)
        {
            return _notesContext.DeleteNote(noteToDelete);
        }

        public IQueryable<Note> GetAllNotes()
        {
            return _notesContext.GetAllNotes();
        }

        public Note GetNoteById(Guid id)
        {
            return _notesContext.GetNoteById(id);
        }

        public Note? UpdateNote(Note? noteToUpdate)
        {
            return _notesContext.UpdateNote(noteToUpdate);
        }
    }
}
