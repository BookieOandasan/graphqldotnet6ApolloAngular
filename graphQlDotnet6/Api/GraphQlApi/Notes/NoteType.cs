using GraphQL.Types;

namespace GraphQlApi.Notes
{
    public class NoteType : ObjectGraphType<Note>
    {
        public NoteType()
        {
            Name = "Note";
            Description = "Note Type";
            Field(d => d.Id, nullable: false).Description("Note Id");
            Field(d => d.Message, nullable: true).Description("Note Message");
            Field(d => d.CreateBy, nullable: true).Description("Note CreateBy");
            Field(d => d.CreateDate, nullable: true).Description("Note CreateDate");
            Field(d => d.LastModifiedDate, nullable: true).Description("Note LastModifiedDate");
            Field(d => d.LastModifiedBy, nullable: true).Description("Note LastModifiedBy");
            Field(d => d.IsUrgent, nullable: true).Description("Note IsUrgent");
        }
    }
}
