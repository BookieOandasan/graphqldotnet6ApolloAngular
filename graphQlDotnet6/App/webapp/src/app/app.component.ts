import { Component, OnDestroy, OnInit } from '@angular/core';
import { ColDef } from 'ag-grid-community';
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
  //private columnDefs:ColDef;
  
 
  columnTypes: { numberColumn: { width: number; filter: string; }; medalColumn: { width: number; columnGroupShow: string; filter: boolean; }; nonEditableColumn: { editable: boolean; }; dateColumn: { filter: string; filterParams: { comparator: (filterLocalDateAtMidnight: any, cellValue: any) => 0 | 1 | -1; }; }; };
  columnDefs: ({ field: string; sortable: boolean; filter: boolean; type?: undefined; } | { field: string; sortable: boolean; filter: boolean; type: string[]; })[];
  //columnTypes: { numberColumn: { width: number; filter: string; }; medalColumn: { width: number; columnGroupShow: string; filter: boolean; }; nonEditableColumn: { editable: boolean; }; };

  constructor(private appService: AppService) {
    this.columnDefs =  [
      { field: 'message', sortable:true, filter:true },
      { field: 'isUrgent', sortable:true, filter:true  },
      { field: 'createBy', sortable:true, filter:true },
      { field: 'createDate', sortable:true, filter:true,  type: ['dateColumn'] },
      { field: 'lastModifiedBy', sortable:true, filter:true },
      { field: 'lastModifiedDate', sortable:true, filter:true }
  ];
    this.columnTypes = {
      numberColumn: {
        width: 130,
        filter: 'agNumberColumnFilter',
      },
      medalColumn: {
        width: 100,
        columnGroupShow: 'open',
        filter: false,
      },
      nonEditableColumn: { editable: false },
      dateColumn: {
        filter: 'agDateColumnFilter',
        filterParams: {
          comparator: (filterLocalDateAtMidnight, cellValue) => {
            const dateParts = cellValue.split('-');
            const year = Number(dateParts[0]);
            const month = Number(dateParts[1]) - 1;
            const day = Number(dateParts[2]);
            const cellDate = new Date(year, month, day);
            if (cellDate < filterLocalDateAtMidnight) {
              return -1;
            } else if (cellDate > filterLocalDateAtMidnight) {
              return 1;
            } else {
              return 0;
            }
          },
        },
      },
    };
  }
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
