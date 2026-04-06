import { Routes } from '@angular/router';
import { Upload } from './pages/upload/upload';
import { Result } from './pages/result/result';

export const routes: Routes = [
    { path: '', component: Upload },
    { path: 'result', component: Result }
];
