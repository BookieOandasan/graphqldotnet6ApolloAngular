using GraphQL;
using GraphQL.Types;
using GraphQlApi.Repository;

namespace GraphQlApi.Notes
{
    public class NotesMutation : ObjectGraphType
    {
        

        public NotesMutation(IRepository repository)
        {
           
            Field<NoteType>(
                "createNote",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "message" }
                ),
                resolve: context =>
                {
                    var message = context.GetArgument<string>("message");
                    var notesContext = context.RequestServices.GetRequiredService<NotesContext>();
                    var note = new Note
                    {
                        Message = message,
                    };
                    notesContext.Notes.Add(note);
                    notesContext.SaveChanges();
                    return note;
                }
            );

            Field<NoteType>(
                "deleteNote",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "noteId"}),
                resolve: context =>
                {
                    var noteToDelete = context.GetArgument<Guid>("noteId");
                    var result = repository.DeleteNote(noteToDelete);
                    return result;
                }

                );

            Field<NoteType>(
               "updateNote",
               arguments: new QueryArguments(
                   new QueryArgument<NonNullGraphType<NoteInputType>> { Name = "noteInput" },
                   //new QueryArgument<NonNullGraphType<NoteInputType>> { Name = "isUrgent" },
                    new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "id" }
                   ),
               resolve: context =>
               {
                   //var isUrgent = context.GetArgument<Note>("isUrgent");
                   var message = context.GetArgument<Note>("message");

                   var id = context.GetArgument<Guid>("id");
                  


                   var noteToUpdate = repository.GetNoteById(id);

                   if (noteToUpdate == null)
                   {
                       new Exception("Note not found in the Database");
                   }

                   noteToUpdate.Message = message.Message;

                   return repository.UpdateNote(noteToUpdate);
               }

               );
            
        }
    }
}