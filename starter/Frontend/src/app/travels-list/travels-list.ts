import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-travels-list',
  imports: [RouterLink],
  templateUrl: './travels-list.html',
  styleUrl: './travels-list.css'
})
export class TravelsList {
  protected readonly isLoading = signal(false);
  protected readonly notImplementedMessage = signal('List-View wird in der Übung implementiert.');
}
