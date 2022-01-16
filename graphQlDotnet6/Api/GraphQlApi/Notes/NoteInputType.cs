using GraphQL.Types;

namespace GraphQlApi.Notes
{
    public class NoteInputType : InputObjectGraphType
    {
        public NoteInputType()
        {
            Name = "noteInput";
            Field<NonNullGraphType<StringGraphType>>("id");
            Field<NonNullGraphType<StringGraphType>>("message");
            Field<NonNullGraphType<StringGraphType>>("isUrgent");
        }
    }
}
