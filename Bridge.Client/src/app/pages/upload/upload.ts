import { Component } from '@angular/core';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-upload',
  standalone: true, // ✅ IMPORTANT
  imports: [CommonModule],
  templateUrl: './upload.html',
  styleUrl: './upload.css',
})
export class Upload {

  selectedFile: File | null = null;
  loading = false;
  result: any;

  constructor(private api: Api, private router: Router) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  async upload() {
    if (!this.selectedFile) return;

    this.loading = true;

    const response = await this.api.uploadFile(this.selectedFile);

    this.loading = false;

    // ✅ Navigate to result page with data
    this.router.navigate(['/result'], {
      state: { data: response }
    });
  }
}