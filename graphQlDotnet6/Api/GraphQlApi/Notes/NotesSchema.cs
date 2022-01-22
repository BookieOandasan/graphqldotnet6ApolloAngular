using GraphQL.Types;
using GraphQlApi.Notes.Subscription;
using GraphQlApi.Security;

namespace GraphQlApi.Notes
{
    public class NotesSchema : Schema
    {
        public NotesSchema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<NotesQuery>();
            Mutation   = serviceProvider.GetRequiredService<NotesMutation>();
            Mutation = serviceProvider.GetRequiredService<AuthMutation>();
            Subscription = serviceProvider.GetRequiredService<NoteSubscription>();
        }
    }
}
