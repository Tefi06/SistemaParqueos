import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Vehiculo } from '../models/vehiculo.model';
import { RespuestaApi } from '../interfaces/respuesta-api.interface';
import { API_ENDPOINTS } from '../constants/api.constants';

@Injectable({
  providedIn: 'root',
})
export class VehiculoService {
  private readonly http = inject(HttpClient);
  private readonly url = API_ENDPOINTS.vehiculos;

  obtenerTodos(): Observable<RespuestaApi<Vehiculo[]>> {
    return this.http.get<RespuestaApi<Vehiculo[]>>(
      this.url
    );
  }

  obtenerPorId(
    id: number
  ): Observable<RespuestaApi<Vehiculo>> {
    return this.http.get<RespuestaApi<Vehiculo>>(
      `${this.url}/${id}`
    );
  }

  crear(
    vehiculo: Vehiculo
  ): Observable<RespuestaApi<Vehiculo>> {
    return this.http.post<RespuestaApi<Vehiculo>>(
      this.url,
      vehiculo
    );
  }

  actualizar(
    id: number,
    vehiculo: Vehiculo
  ): Observable<RespuestaApi<null>> {
    return this.http.put<RespuestaApi<null>>(
      `${this.url}/${id}`,
      vehiculo
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
