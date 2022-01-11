import { Component, OnInit } from '@angular/core';
import {Apollo, gql} from 'apollo-angular';
import { AppService } from './app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})

export class AppComponent implements OnInit {
  notesFromEF!: any[];
  loading = true;
  error: any;

  constructor(private apollo: Apollo, private appService: AppService) {}

  ngOnInit() {
    this.apollo
      .watchQuery({
        query: gql`
        {
          notesFromEF {
          id
          message
        }
      }
        `,
      })
      .valueChanges.subscribe((result: any) => {
        this.notesFromEF = result?.data?.notesFromEF;
        console.log(result?.data);
        this.loading = result.loading;
        this.error = result.error;
      });
  }

  public createNewMessage(message: string)
  {
    this.appService.CreateNote(message).subscribe(() =>{
      console.log("Created")
    })
  }
}
