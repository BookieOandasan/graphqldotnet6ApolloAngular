import { Component, OnInit } from '@angular/core';
import {Apollo, gql} from 'apollo-angular';

@Component({
  selector: 'app-root',
  //templateUrl: './app.component.html',
  //selector: 'exchange-rates',
  template: `
    <div *ngIf="loading">
      Loading...
    </div>
    <div *ngIf="error">
      Error :(
    </div>
    <div *ngIf="notes">
      <div *ngFor="let rate of notes">
        <p>{{ rate.id }}: {{ rate.message }}</p>
      </div>
    </div>
  `,
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  notes!: any[];
  loading = true;
  error: any;

  constructor(private apollo: Apollo) {}

  ngOnInit() {
    this.apollo
      .watchQuery({
        query: gql`
        {
          notes {
          id
          message
        }
      }
        `,
      })
      .valueChanges.subscribe((result: any) => {
        this.notes = result?.data?.notes;
        this.loading = result.loading;
        this.error = result.error;
      });
  }
}
