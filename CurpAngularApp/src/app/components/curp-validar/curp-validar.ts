import { Component, signal, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CurpValidarService } from '../../services/curpValidar.service';

import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { DatosCurp, Sexo } from '../../models/datosCurp';

@Component({
  selector: 'app-curp-validar',
  standalone: true,
  imports: [
    FormsModule,
    MatButtonModule,
    MatTableModule,
    MatIconModule,
    MatInputModule,
    MatSelectModule,
    MatCheckboxModule,
    MatCardModule,
    MatListModule
  ],
  templateUrl: './curp-validar.html',
  styleUrl: './curp-validar.css',
})
export class CurpValidar {

  @ViewChild('form') form!: NgForm; // referencia al formulario
  errores: string[] = [];
  mostrarErrores = signal(false);

  sexos = [
    { id: Sexo.Masculino, descripcion: 'Masculino' },
    { id: Sexo.Femenino, descripcion: 'Femenino' }
  ];

  datosCurp : DatosCurp = {
    curp: '',
    nombres: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    fechaNacimiento: '',
    sexo: Sexo.Masculino,
    esMexicano: true
  };

  constructor(
    private curpValidarService: CurpValidarService
  ) {}

  ngAfterViewInit() {
    // actualizar el estado si se modifica el formulario
    this.form.valueChanges?.subscribe(() => {
      this.mostrarErrores.set(false);
    });
  }

  validarCurp() {
  this.curpValidarService.validarCurp(this.datosCurp)
    .subscribe({
      next: (response) => {
        this.errores = response;
        this.mostrarErrores.set(true);
      },
      error: (error) => {
        console.log(error.error);
      }
    });
  }
}
