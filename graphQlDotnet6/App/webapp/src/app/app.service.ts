import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
import { NoteInput } from './noteInput.Model';

const create_note = gql `
mutation createNote($message:String!)
      {
        createNote(message:$message){
        id
        message
      }
  }
`
const delete_note = gql `
mutation($noteId:ID!)
{
  deleteNote(noteId:$noteId)
 
}
`
  
const get_Notes = gql `{
    notesFromEF {
        id,
        message,
        createBy,
        createDate,
        lastModifiedBy,
        lastModifiedDate,
        isUrgent
  }
}`

const update_note = gql `
mutation($note: noteInput!,$noteId:ID!)
{
  updateNote(note:$note,noteId:$noteId)
  {
    id,
    message,
    isUrgent,
    lastModifiedBy,
    lastModifiedDate
  }
}
`
@Injectable({providedIn: 'root'})


export class AppService {
 
  constructor(private apollo: Apollo) { }
  
  public DeleteNote(noteId: any) {
        var noteToDelete =noteId;
    return this.apollo.mutate(
        {mutation: delete_note,
        variables: {noteId}
    });
  }

public CreateNote(message:string){
    return this.apollo.mutate(
        {mutation: create_note,
        variables: {message}
    });
}

public GetNotes(){
    return this.apollo.watchQuery({
        query: get_Notes,
        pollInterval : 500
    });
  }

public UpdateNote(note: NoteInput, noteId:any) {
   //TODO: set isUrgent
return this.apollo.mutate(
    {mutation: update_note,
    variables: {note, noteId}
});
}
    
}