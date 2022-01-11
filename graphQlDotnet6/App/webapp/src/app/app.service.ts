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
@Injectable({providedIn: 'root'})


export class AppService {

    constructor(private apollo: Apollo) { }

public CreateNote(message:string){
    return this.apollo.mutate(
        {mutation: create_note,
        variables: {message}
    }
    );
}
    
}