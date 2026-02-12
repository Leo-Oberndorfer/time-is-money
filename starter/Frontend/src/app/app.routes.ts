import { Routes } from '@angular/router';
import { TravelDetails } from './travel-details/travel-details';
import { TravelUpload } from './travel-upload/travel-upload';
import { TravelsList } from './travels-list/travels-list';

export const routes: Routes = [
  { path: 'commutes', component: TravelsList },
  { path: 'commutes/import', component: TravelUpload },
  { path: 'commutes/:id', component: TravelDetails },
  { path: '', redirectTo: '/commutes', pathMatch: 'full' },
  { path: '**', redirectTo: '/commutes' }
];
