using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace GraphQlApi.Notes.Subscription
{
    public class NotePublish : INotePublish
    {
        private readonly ISubject<Note> _noteStream = new ReplaySubject<Note>();

        public ConcurrentStack<Note> AllNotes { get; }
        public ConcurrentDictionary<string, string> Users { get; set; }

        public NotePublish()
        {
            AllNotes = new ConcurrentStack<Note>();
            Users = new ConcurrentDictionary<string, string>
            {
                ["1"] = "developer",
                ["2"] = "tester"
            };
        }
        public Note AddNote(Note note)
        {
           AllNotes.Push(note);

            _noteStream.OnNext(note);
            return note;
        }

        public IObservable<Note> Notes(string? message)
        {
            return _noteStream
                .Select(note =>
                {
                    note.Message = message;
                    return note;
                }).AsObservable();
        }
    }
}
