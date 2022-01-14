using System.Collections.Concurrent;

namespace GraphQlApi.Notes.Subscription
{
    public interface INotePublish
    {
        IObservable<Note> Notes(string? sub);

        Note AddNote(Note note);

        ConcurrentStack<Note> AllNotes { get; }


    }
}