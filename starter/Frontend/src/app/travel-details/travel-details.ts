import { Component, computed, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-travel-details',
  imports: [RouterLink],
  templateUrl: './travel-details.html',
  styleUrl: './travel-details.css'
})
export class TravelDetails {
  private readonly route = inject(ActivatedRoute);
  protected readonly commuteId = computed(() => this.route.snapshot.paramMap.get('id'));
}
