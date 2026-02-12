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
    this.selectedFileName.set(input.files?.item(0)?.name ?? null);
  }

  protected upload(): void {
    throw new NotImplementedException('Upload wiring is part of the exercise.');
  }
}

class NotImplementedException extends Error {
  public constructor(message: string) {
    super(message);
    this.name = 'NotImplementedException';
  }
}
