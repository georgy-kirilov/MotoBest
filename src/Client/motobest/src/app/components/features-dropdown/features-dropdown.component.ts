import { Component, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Observable } from 'rxjs';
import { FeatureResponseModel as FeatureResponseModel } from 'src/app/models/feature.response.model';
import { DisplayMessagesService } from 'src/app/services/display-messages.service';

@Component({
  selector: 'app-features-dropdown',
  templateUrl: './features-dropdown.component.html',
  styleUrls: ['./features-dropdown.component.css']
})
export class FeaturesDropdownComponent implements OnInit, OnChanges {
  
  @Input() title: string = '';
  @Input() propertyName: string = '';
  @Input() property: any | null = null;
  @Input() dividedByGroups: boolean = false;
  @Input() observablesList: Observable<FeatureResponseModel[]> = new Observable();
  @Output() onChangeHandler = new EventEmitter();
  
  optionsList: FeatureResponseModel[] = [];
  groupsByLetter: Map<string, FeatureResponseModel[]> = new Map();

  constructor(public messagesService: DisplayMessagesService) { }

  ngOnInit(): void {
    this.observablesList.subscribe(responseOptions => {
      this.optionsList = responseOptions.filter((option): option is FeatureResponseModel => option !== null)
      this.createGroupMap(this.groupsByLetter);
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.ngOnInit();
  }

  onChange() {
    this.onChangeHandler?.emit({
      propertyName: this.propertyName,
      option: this.property
    });
  }

  private createGroupMap(optionsByGroups: Map<string, FeatureResponseModel[]>) {
    optionsByGroups.clear();
    const uniqueLetters = new Set(this.optionsList.map(option => option.name.charAt(0)));
    uniqueLetters.forEach(letter => optionsByGroups.set(letter, []));
    this.optionsList.forEach(option => {
      let letter = option.name.charAt(0);
      optionsByGroups.get(letter)?.push(option);
    });
  }
}
