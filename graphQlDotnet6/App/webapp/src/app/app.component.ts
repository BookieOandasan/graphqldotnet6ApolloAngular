import { Component, OnDestroy, OnInit } from '@angular/core';
import {Apollo, gql, QueryRef} from 'apollo-angular';
import { Subscription } from 'rxjs';
import { AppService } from './app.service';

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
  public createNewMessage(message: string)
  {
    this.appService.CreateNote(message).subscribe(
      () =>{console.log("Created");},
    (error)=>{ console.log("error");},
    ()=>{
      console.log("Completed");
      this.refresh();
    },
    );
  }

  refresh(){
    this.postQuery?.refetch();
  }

  public deleteMessage(selectedNote:any){

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

  public editMessage(){
    this.editMode = true;
  }

  public saveMessage(selectedNote:any){

  }

  public cancel(){
    this.editMode = false;
  }
}
