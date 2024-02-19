import { Component } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {

  constructor() { }

  onSubmit(form: any): void {
    // Logic to handle form submission
    console.log(form.value);
  }
}
