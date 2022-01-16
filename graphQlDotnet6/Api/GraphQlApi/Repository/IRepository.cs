using GraphQlApi.Notes;

namespace GraphQlApi.Repository
{
    public interface IRepository
    {
        string DeleteNote(Guid noteToDelete);
        Note? UpdateNote(Note? noteToUpdate);
        Note GetNoteById(Guid id);
        IQueryable<Note> GetAllNotes();
    }
}