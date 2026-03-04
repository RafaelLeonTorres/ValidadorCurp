import { Component, signal } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-root',
  imports: [
            RouterOutlet,
            RouterModule,
            // Forms
            FormsModule,
            ReactiveFormsModule,
            // Material
            MatButtonModule,
            MatTableModule,
            MatIconModule,
            MatInputModule,
            MatSelectModule,
            MatCheckboxModule,
            MatCardModule,
            MatListModule
          ],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('CurpAngularApp');
}
