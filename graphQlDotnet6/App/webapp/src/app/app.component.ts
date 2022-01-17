import { Component, OnDestroy, OnInit } from '@angular/core';
import {Apollo, gql, QueryRef} from 'apollo-angular';
import { Subscription } from 'rxjs';
import { AppService } from './app.service';
import { NoteInput } from './noteInput.Model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit, OnDestroy {
  notesFromEF!: any[];
  loading = true;
  postQuery : QueryRef<any> | undefined;
  editMode = false;

  private querySubscription: Subscription | undefined;
  error: any;
  noteCreatedMessage: string = "Test";
  showNoteCreatedMessage: boolean = false;
  inputText:string ='';

  constructor(private appService: AppService) {}
  ngOnDestroy(): void {
    this.querySubscription?.unsubscribe();
  }

  ngOnInit() {
    this.postQuery = this.appService.GetNotes();

    this.querySubscription = this.postQuery.valueChanges
    .subscribe(({data, loading}) => {
      this.loading = loading;
      this.notesFromEF = data.notesFromEF;
    });
      
  
  }
  public createNewNote(message: string)
  {
    this.appService.CreateNote(message).subscribe(
      () =>{console.log("Created")
      
      ;},
    (error)=>{ console.log("error");},
    ()=>{
      console.log("Completed");
      
      this.refresh();
      this.noteCreatedMessage ="Note successfully created!"
      this.showNoteCreatedMessage = true;
      this.inputText ='';
    },
    );
  }

  refresh(){
    this.postQuery?.refetch();
  }

  public deleteNote(selectedNote:any){

    var test = selectedNote;
    this.appService.DeleteNote(selectedNote.id).subscribe(
      () =>{console.log("Created");},
    (error)=>{ console.log("error");},
    ()=>{
      console.log("Completed");
      this.refresh();
    },
    );

  }

  public editNote(){
    this.editMode = true;
  }

  public updateNote(selectedNote:any, editMessage:any){
     //TODO: set isUrgent
    var note:NoteInput = new NoteInput() ;
        note.message = editMessage;
      note.isUrgent = selectedNote.isUrgent? "true" : "false";
    this.appService.UpdateNote(note, selectedNote.id).subscribe(
      () =>{console.log("Upate");},
    (error)=>{ 
      var er = error.
      console.log(error);},
    ()=>{
      console.log("Upate");
      this.refresh();
    },
    );
  }

  public cancel(){
    this.editMode = false;
  }
}
