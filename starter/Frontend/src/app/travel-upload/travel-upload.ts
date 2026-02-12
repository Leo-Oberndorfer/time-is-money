import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-travel-upload',
  imports: [RouterLink],
  templateUrl: './travel-upload.html',
  styleUrl: './travel-upload.css'
})
export class TravelUpload {
  protected readonly selectedFileName = signal<string | null>(null);

  protected onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    const file = input.files?.item(0) ?? null;
    this.selectedFileName.set(file?.name ?? null);
  }

  protected importCommute(): void {
    throw new Error('Not implemented yet.');
  }
}
