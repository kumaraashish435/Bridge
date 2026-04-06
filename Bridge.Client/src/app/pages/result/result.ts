import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-result',
  standalone: true, // ✅ IMPORTANT FIX
  imports: [CommonModule, FormsModule],
  templateUrl: './result.html',
  styleUrl: './result.css',
})
export class Result {
  result: any;
  selectedPage = 1;
  objectKeys = Object.keys;
  

  constructor() {
    this.result = history.state.data;

    if (!this.result) {
      console.error("No data received");
      return;
    }

    //  THIS IS CRITICAL
    if (this.result.data) {
      this.result.data = JSON.parse(this.result.data);
    }
  }
}