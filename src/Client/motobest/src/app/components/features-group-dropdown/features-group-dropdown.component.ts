import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { DisplayMessagesService } from 'src/app/services/display-messages-service';

@Component({
  selector: 'app-features-group-dropdown',
  templateUrl: './features-group-dropdown.component.html',
  styleUrls: ['./features-group-dropdown.component.css']
})
export class FeaturesGroupDropdownComponent implements OnInit {
  
  @Input() title: string = '';
  @Input() input: string | null = null;

  @Input() observableOptionsList: Observable<(string | null)[]> = new Observable();
  @Output() onChangeHandler = new EventEmitter<string | null>();
  
  optionGroupsByLetter: Map<string, string[]> = new Map();

  constructor(public messagesService: DisplayMessagesService) { }

  ngOnInit(): void {
    this.observableOptionsList.subscribe(res => {
      const filteredOptionsList = res.filter((option): option is string => option !== null)
      this.createOptionsGroupMap(this.optionGroupsByLetter, filteredOptionsList);
    });
  }

  onChange() {
    this.onChangeHandler?.emit(this.input);
  }

  createOptionsGroupMap(optionsGroupMap: Map<string, string[]>, optionsSourceList: string[]) {
    optionsGroupMap.clear();
    const uniqueLetters = new Set(optionsSourceList.map(option => option?.charAt(0)));
    uniqueLetters.forEach(letter => optionsGroupMap.set(letter, []));
    optionsSourceList.forEach(option => {
      let letter = option.charAt(0);
      optionsGroupMap.get(letter)?.push(option);
    });
  }
}
