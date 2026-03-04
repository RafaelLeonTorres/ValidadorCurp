import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DatosCurp } from '../models/datosCurp';

@Injectable({providedIn: 'root'})
export class CurpValidarService {

  private urlBase = 'https://localhost:7190/api/Curp';

  constructor(private http: HttpClient) { }

  validarCurp(datosCurp: DatosCurp) : Observable<string[]> {
    return this.http.post<string[]>(this.urlBase + '/' + 'validar', datosCurp);
  }
}
