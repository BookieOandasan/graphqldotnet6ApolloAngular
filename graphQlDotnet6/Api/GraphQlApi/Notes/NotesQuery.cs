using GraphQL.Types;
using GraphQlApi.Repository;

namespace GraphQlApi.Notes
{
    public class NotesQuery : ObjectGraphType<Note>
    {
        public NotesQuery(IRepository repository)
        {
            Field<ListGraphType<NoteType>>("notesFromEF", resolve: context =>
            {
                return repository.GetAllNotes();
            }
        );
        }
    }
}
