import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-travel-upload',
  imports: [RouterLink],
  templateUrl: './travel-upload.html',
  styleUrl: './travel-upload.css'
})
export class TravelUpload {
  protected readonly selectedFile = signal<File | null>(null);

  protected onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.selectedFile.set(input.files?.item(0) ?? null);
  }

  protected upload(): void {
    throw new Error('Upload flow is intentionally left for the exercise.');
  }
}
