import { Injectable } from '@angular/core';
import { Apollo, gql } from 'apollo-angular';
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
mutation deleteNote($noteToDelete:any!)
      {
        deleteNote(noteToDelete:$noteToDelete){
        id
        message
      }
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
@Injectable({providedIn: 'root'})


export class AppService {
 
    DeleteNote(selectedNote: any) {
        var noteToDelete =selectedNote;
    return this.apollo.mutate(
        {mutation: delete_note,
        variables: {selectedNote}
    });
  }

    constructor(private apollo: Apollo) { }

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


    
}