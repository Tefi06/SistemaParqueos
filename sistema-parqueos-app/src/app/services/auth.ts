import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Preferences } from '@capacitor/preferences';

import { Login } from '../models/login.model';
import { UsuarioSesion } from '../models/usuario-sesion.model';
import { RespuestaApi } from '../interfaces/respuesta-api.interface';
import { API_ENDPOINTS } from '../constants/api.constants';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly url = API_ENDPOINTS.login;

  private readonly claveSesion = 'usuarioSesion';

  iniciarSesion(
    login: Login
  ): Observable<RespuestaApi<UsuarioSesion>> {
    return this.http.post<RespuestaApi<UsuarioSesion>>(
      this.url,
      login
    );
  }

  async guardarSesion(
    usuario: UsuarioSesion
  ): Promise<void> {
    await Preferences.set({
      key: this.claveSesion,
      value: JSON.stringify(usuario),
    });
  }

  async obtenerSesion(): Promise<UsuarioSesion | null> {
    const resultado = await Preferences.get({
      key: this.claveSesion,
    });

    if (!resultado.value) {
      return null;
    }

    try {
      return JSON.parse(
        resultado.value
      ) as UsuarioSesion;
    } catch {
      await this.cerrarSesion();
      return null;
    }
  }

  async haySesion(): Promise<boolean> {
    const usuario = await this.obtenerSesion();

    return usuario !== null;
  }

  async cerrarSesion(): Promise<void> {
    await Preferences.remove({
      key: this.claveSesion,
    });
  }
}