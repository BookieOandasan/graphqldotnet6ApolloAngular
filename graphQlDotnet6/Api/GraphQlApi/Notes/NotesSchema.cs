using GraphQL.Types;

namespace GraphQlApi.Notes
{
    public class NotesSchema : Schema
    {
        public NotesSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<NotesQuery>();
            Mutation = serviceProvider.GetRequiredService<NotesMutation>();
        }
    }
}
