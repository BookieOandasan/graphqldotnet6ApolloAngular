using GraphQL.Types;

namespace GraphQlApi.Security
{
    public class SessionType :ObjectGraphType<Session>
    {
        public SessionType ()
        {
            Field(t=>t.IsAuthenticated);
        }
    }
}
