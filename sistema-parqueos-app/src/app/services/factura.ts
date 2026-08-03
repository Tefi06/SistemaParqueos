import { Injectable, inject } from '@angular/core';

import {
  HttpClient
} from '@angular/common/http';

import {
  Observable
} from 'rxjs';

import {
  Factura
} from '../models/factura.model';

import {
  RespuestaApi
} from '../interfaces/respuesta-api.interface';

import {
  API_ENDPOINTS
} from '../constants/api.constants';



@Injectable({
  providedIn: 'root',
})
export class FacturaService {


  private readonly http =
    inject(HttpClient);



  private readonly url =
    API_ENDPOINTS.facturas;



  obtenerTodos():
  Observable<RespuestaApi<Factura[]>> {


    return this.http.get<
      RespuestaApi<Factura[]>
    >(
      this.url
    );


  }



  obtenerPorId(
    id:number
  ):
  Observable<RespuestaApi<Factura>> {


    return this.http.get<
      RespuestaApi<Factura>
    >(
      `${this.url}/${id}`
    );


  }



  generar(
    ingresoId:number
  ):
  Observable<RespuestaApi<Factura>> {


    return this.http.post<
      RespuestaApi<Factura>
    >(
      `${this.url}/generar/${ingresoId}`,
      {}
    );


  }


}