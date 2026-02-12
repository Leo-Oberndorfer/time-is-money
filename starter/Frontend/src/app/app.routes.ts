import { Routes } from '@angular/router';
import { TravelDetails } from './travel-details/travel-details';
import { TravelUpload } from './travel-upload/travel-upload';
import { TravelsList } from './travels-list/travels-list';
import { Statistics } from './statistics/statistics';

export const routes: Routes = [
  { path: 'commutes', component: TravelsList },
  { path: 'commutes/upload', component: TravelUpload },
  { path: 'commutes/statistics', component: Statistics },
  { path: 'commutes/:id', component: TravelDetails },
  { path: '', redirectTo: '/commutes', pathMatch: 'full' },
  { path: '**', redirectTo: '/commutes' }
];
