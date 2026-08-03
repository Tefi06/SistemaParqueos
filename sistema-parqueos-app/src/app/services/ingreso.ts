import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Ingreso } from '../models/ingreso.model';

import { RespuestaApi } from '../interfaces/respuesta-api.interface';

import { API_ENDPOINTS } from '../constants/api.constants';



@Injectable({
  providedIn: 'root'
})
export class IngresoService {


  private readonly http =
    inject(HttpClient);



  private readonly url =
    API_ENDPOINTS.ingresos;



  obtenerTodos():
  Observable<RespuestaApi<Ingreso[]>> {


    return this.http.get<
      RespuestaApi<Ingreso[]>
    >(
      this.url
    );


  }



  obtenerPorId(
    id:number
  ):
  Observable<RespuestaApi<Ingreso>> {


    return this.http.get<
      RespuestaApi<Ingreso>
    >(
      `${this.url}/${id}`
    );


  }



  registrarIngreso(
    ingreso:Ingreso
  ):
  Observable<RespuestaApi<Ingreso>> {


    return this.http.post<
      RespuestaApi<Ingreso>
    >(
      this.url,
      ingreso
    );


  }



  registrarSalida(
    id:number
  ):
  Observable<RespuestaApi<Ingreso>> {


    return this.http.put<
      RespuestaApi<Ingreso>
    >(
      `${this.url}/${id}/salida`,
      {}
    );


  }


}