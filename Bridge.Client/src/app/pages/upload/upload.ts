import { Component } from '@angular/core';
import { Api } from '../../services/api';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router'; // ✅ ADD

@Component({
  selector: 'app-upload',
  imports: [CommonModule],
  templateUrl: './upload.html',
  styleUrl: './upload.css',
})
export class Upload {

  selectedFile: File | null = null;
  loading = false;
  result: any;

  constructor(private api: Api , private router: Router) {}

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  async upload() {
  if (!this.selectedFile) return;

  this.loading = true;

  const response = await this.api.uploadFile(this.selectedFile);

  this.loading = false;

  // ✅ navigate with data
  this.router.navigate(['/result'], {
    state: { data: response }
  });
}
}
