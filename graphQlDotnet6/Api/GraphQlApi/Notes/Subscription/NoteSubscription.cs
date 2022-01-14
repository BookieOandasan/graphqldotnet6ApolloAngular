using GraphQL;
using GraphQL.Resolvers;
using GraphQL.Server.Transports.Subscriptions.Abstractions;
using GraphQL.Subscription;
using GraphQL.Types;
using System.Security.Claims;

namespace GraphQlApi.Notes.Subscription
{
    public class NoteSubscription : ObjectGraphType<object>
    {
        private readonly INotePublish _notePublish;

        public NoteSubscription(INotePublish notePublish)
        {
            _notePublish = notePublish;

            AddField(new EventStreamFieldType
            {
                Name = "noteAdded",
                Type = typeof(NoteType),
                Resolver = new FuncFieldResolver<Note>(ResolveNote),
                Subscriber = new EventStreamResolver<Note>(Subscribe)
            });
        }

        private IObservable<Note?> Subscribe(IResolveEventStreamContext context)
        {
            var noteContex = (MessageHandlingContext)context.UserContext;
            var user = noteContex.Get<ClaimsPrincipal>("user");

            string? sub = "Anonymous";

            if (user != null)

                sub = user.Claims.FirstOrDefault(u => u.Type == "sub")?.Value;

            return _notePublish.Notes(sub);
        }

        private Note? ResolveNote(IResolveFieldContext context)
        {
            var note = context.Source as Note;
            return note;
        }
    }
}
