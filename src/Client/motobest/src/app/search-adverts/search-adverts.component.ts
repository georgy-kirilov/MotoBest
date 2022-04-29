import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-search-adverts',
  templateUrl: './search-adverts.component.html',
  styleUrls: ['./search-adverts.component.css']
})
export class SearchAdvertsComponent implements OnInit {

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
  }

}
