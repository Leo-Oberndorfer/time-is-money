import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-travel-details',
  imports: [RouterLink],
  templateUrl: './travel-details.html',
  styleUrl: './travel-details.css'
})
export class TravelDetails {
  protected readonly notImplementedMessage = signal('Detail-View wird in der Übung implementiert.');
}
