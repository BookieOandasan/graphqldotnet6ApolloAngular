using GraphQlApi.Notes;

namespace GraphQlApi.Repository
{
    public interface INotesContext
    {
        string DeleteNote(Guid noteToDelete);
        Note? UpdateNote(Note noteToDelete);
        Note? GetNoteById(Guid id);
        IQueryable<Note> GetAllNotes();
        Note? CreateNote(Note note);
    }
}