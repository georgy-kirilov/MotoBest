import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-search-adverts',
  templateUrl: './search-adverts.component.html',
  styleUrls: ['./search-adverts.component.css']
})
export class SearchAdvertsComponent implements OnInit {

  selectedEngine: string = '';
  selectedTransmission: string = '';
  selectedBodyStyle: string = '';

  engines: string[] = [];
  transmissions: string[] = [];
  bodyStyles: string[] = [];

  constructor(private httpClient: HttpClient) { }

  ngOnInit(): void {
      this.getAllItems(this.engines, 'engines');
      this.getAllItems(this.transmissions, 'transmissions');
      this.getAllItems(this.bodyStyles, 'body-styles');
  }

  getAllItems(items: string[], route: string) {
    this.httpClient
        .get<string[]>(`${environment.BASE_URL}/Features/${route}`)
        .subscribe(response => {
          items.length = 0;
          items.push('');
          response.forEach(x => items.push(x));
        });
  }

  format(item: string): string {
    return item == '' ? 'Без значение' : `${item.toUpperCase()[0]}${item.substring(1)}`;
  }

}
