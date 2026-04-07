import { Injectable } from '@angular/core';
import axios from 'axios';

@Injectable({
  providedIn: 'root',
})
export class Api {

  baseUrl = 'http://localhost:5073/api';

  async uploadFile(file: File) {
    const formData = new FormData();
    formData.append('file', file);

    const response = await axios.post(`${this.baseUrl}/File/upload`, formData, {
      headers: { 'Content-Type': 'multipart/form-data' }
    });

    return response.data;
  }
}
