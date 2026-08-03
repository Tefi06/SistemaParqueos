import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { EspacioParqueo } from '../models/espacio-parqueo.model';

import { RespuestaApi } from '../interfaces/respuesta-api.interface';

import { API_ENDPOINTS } from '../constants/api.constants';


@Injectable({
  providedIn: 'root',
})
export class EspacioParqueoService {


  private readonly http = inject(HttpClient);


  private readonly url =
    API_ENDPOINTS.espaciosParqueo;



  obtenerTodos():
  Observable<RespuestaApi<EspacioParqueo[]>> {

    return this.http.get<
      RespuestaApi<EspacioParqueo[]>
    >(this.url);

  }



  obtenerPorId(
    id: number
  ):
  Observable<RespuestaApi<EspacioParqueo>> {

    return this.http.get<
      RespuestaApi<EspacioParqueo>
    >(`${this.url}/${id}`);

  }



  crear(
    espacio: EspacioParqueo
  ):
  Observable<RespuestaApi<EspacioParqueo>> {

    return this.http.post<
      RespuestaApi<EspacioParqueo>
    >(
      this.url,
      espacio
    );

  }



  actualizar(
    id: number,
    espacio: EspacioParqueo
  ):
  Observable<RespuestaApi<null>> {

    return this.http.put<
      RespuestaApi<null>
    >(
      `${this.url}/${id}`,
      espacio
    );

  }



  eliminar(
    id: number
  ):
  Observable<RespuestaApi<null>> {

    return this.http.delete<
      RespuestaApi<null>
    >(
      `${this.url}/${id}`
    );

  }


}