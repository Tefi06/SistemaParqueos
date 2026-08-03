import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Parqueo } from '../models/parqueo.model';
import { RespuestaApi } from '../interfaces/respuesta-api.interface';
import { API_ENDPOINTS } from '../constants/api.constants';


@Injectable({
  providedIn: 'root'
})
export class ParqueoService {

  private readonly http = inject(HttpClient);

  private readonly url = API_ENDPOINTS.parqueos;


  obtenerTodos(): Observable<RespuestaApi<Parqueo[]>> {

    return this.http.get<RespuestaApi<Parqueo[]>>(
      this.url
    );

  }


  crear(
    parqueo: Parqueo
  ): Observable<RespuestaApi<Parqueo>> {

    return this.http.post<RespuestaApi<Parqueo>>(
      this.url,
      parqueo
    );

  }


  actualizar(
    id: number,
    parqueo: Parqueo
  ): Observable<RespuestaApi<null>> {

    return this.http.put<RespuestaApi<null>>(
      `${this.url}/${id}`,
      parqueo
    );

  }


  eliminar(
    id: number
  ): Observable<RespuestaApi<null>> {

    return this.http.delete<RespuestaApi<null>>(
      `${this.url}/${id}`
    );

  }

}