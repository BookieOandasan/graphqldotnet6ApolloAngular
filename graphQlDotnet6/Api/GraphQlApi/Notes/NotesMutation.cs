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
                    var note = new Note
                    {
                        Message = message,
                        CreateBy = Environment.UserName,
                        CreateDate = DateTime.Now,
                        LastModifiedBy = Environment.UserName,
                        LastModifiedDate = DateTime.Now,
                        IsUrgent = false,
                    };

                   return repository.CreateNote(note);
                   
                }
            );

            Field<StringGraphType>(
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
                     new QueryArgument<NonNullGraphType<NoteInputType>> { Name = "note" },
                     new QueryArgument<NonNullGraphType<IdGraphType>> { Name = "noteId" }
                   ),
               resolve: context =>
               {
                   var message = context.GetArgument<Note>("note");

                   var id = context.GetArgument<Guid>("noteId");
                  


                   var noteToUpdate = repository.GetNoteById(id);

                   if (noteToUpdate == null)
                   {
                      return "Note not found in the Database";
                   }

                   if (message == null)
                   {
                       return "Note input cannot b null";
                   }
                   noteToUpdate.Message = message.Message;
                   noteToUpdate.IsUrgent = message.IsUrgent;
                   noteToUpdate.LastModifiedBy = Environment.UserName;
                   noteToUpdate.LastModifiedDate = DateTime.Now;

                   return repository.UpdateNote(noteToUpdate);
               }

               );
            
        }
    }
}