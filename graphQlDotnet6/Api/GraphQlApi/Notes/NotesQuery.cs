using GraphQL.Types;
using GraphQlApi.Repository;
using GraphQL.Authorization;
using GraphQL;

namespace GraphQlApi.Notes
{
    public class NotesQuery : ObjectGraphType<Note>
    {
        
        public NotesQuery()
        {
            //this.AuthorizeWith("");    
            //Field(o => o.Message).Resolve(o => "")
            Field<ListGraphType<NoteType>>("notes", resolve: context => new List<Note> {
            new Note { Id = Guid.NewGuid(), Message = "Hello World!" },
            new Note { Id = Guid.NewGuid(), Message = "Hello World! How are you?" }
        });
            Field<ListGraphType<NoteType>>("notesFromEF", resolve: context =>
            {
                var notesContext = context.RequestServices.GetRequiredService<NotesContext>();
                return notesContext.Notes.ToList();
            }
        );
        }
    }
}
