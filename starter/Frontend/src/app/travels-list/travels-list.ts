import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-travels-list',
  imports: [RouterLink],
  templateUrl: './travels-list.html',
  styleUrl: './travels-list.css'
})
export class TravelsList {
  protected readonly loading = signal(false);
}
