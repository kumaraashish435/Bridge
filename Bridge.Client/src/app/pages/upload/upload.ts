import { Component } from '@angular/core';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-upload',
  standalone: true,
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

    try {
      const response = await this.api.uploadFile(this.selectedFile);

      this.result = response; // for preview (optional)

      // Navigate to result page
      this.router.navigate(['/result'], {
        state: { data: response }
      });

    } catch (err) {
      console.error("Upload failed:", err);
    } finally {
      this.loading = false;
    }
  }
}