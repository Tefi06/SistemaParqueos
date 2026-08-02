import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Cliente } from '../models/cliente.model';
import { RespuestaApi } from '../interfaces/respuesta-api.interface';
import { API_ENDPOINTS } from '../constants/api.constants';

@Injectable({
  providedIn: 'root',
})
export class ClienteService {
  private readonly http = inject(HttpClient);
  private readonly url = API_ENDPOINTS.clientes;

  obtenerTodos(): Observable<RespuestaApi<Cliente[]>> {
    return this.http.get<RespuestaApi<Cliente[]>>(this.url);
  }

  obtenerPorId(id: number): Observable<RespuestaApi<Cliente>> {
    return this.http.get<RespuestaApi<Cliente>>(
      `${this.url}/${id}`
    );
  }

  crear(cliente: Cliente): Observable<RespuestaApi<Cliente>> {
    return this.http.post<RespuestaApi<Cliente>>(
      this.url,
      cliente
    );
  }

  actualizar(
    id: number,
    cliente: Cliente
  ): Observable<RespuestaApi<null>> {
    return this.http.put<RespuestaApi<null>>(
      `${this.url}/${id}`,
      cliente
    );
  }

  eliminar(id: number): Observable<RespuestaApi<null>> {
    return this.http.delete<RespuestaApi<null>>(
      `${this.url}/${id}`
    );
  }
}