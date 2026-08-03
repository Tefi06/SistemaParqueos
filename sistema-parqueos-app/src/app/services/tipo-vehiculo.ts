import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { TipoVehiculo } from '../models/tipo-vehiculo.model';
import { RespuestaApi } from '../interfaces/respuesta-api.interface';
import { API_ENDPOINTS } from '../constants/api.constants';

@Injectable({
  providedIn: 'root',
})
export class TipoVehiculoService {
  private readonly http = inject(HttpClient);

  private readonly url =
    API_ENDPOINTS.tiposVehiculo;

  obtenerTodos(): Observable<
    RespuestaApi<TipoVehiculo[]>
  > {
    return this.http.get<
      RespuestaApi<TipoVehiculo[]>
    >(this.url);
  }
}