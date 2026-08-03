import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import {
  IonButton,
  IonContent,
  IonHeader,
  IonIcon,
  IonInput,
  IonSpinner,
  IonTitle,
  IonToolbar,
} from '@ionic/angular/standalone';

import { addIcons } from 'ionicons';

import {
  carSportOutline,
  eyeOffOutline,
  eyeOutline,
  lockClosedOutline,
  mailOutline,
} from 'ionicons/icons';

import { Login } from '../../models/login.model';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonInput,
    IonButton,
    IonIcon,
    IonSpinner,
  ],
})
export class LoginPage {
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);

  credenciales: Login = {
    correo: '',
    clave: '',
  };

  cargando = false;
  mostrarClave = false;
  mensajeError = '';

  constructor() {
    addIcons({
      carSportOutline,
      mailOutline,
      lockClosedOutline,
      eyeOutline,
      eyeOffOutline,
    });
  }

  cambiarVisibilidadClave(): void {
    this.mostrarClave = !this.mostrarClave;
  }

  iniciarSesion(): void {
    if (this.cargando) {
      return;
    }

    this.mensajeError = '';

    const correo = this.credenciales.correo.trim();
    const clave = this.credenciales.clave;

    if (!correo || !clave) {
      this.mensajeError =
        'Debes ingresar el correo y la contraseña.';

      return;
    }

    this.cargando = true;

    this.authService
      .iniciarSesion({
        correo,
        clave,
      })
      .subscribe({
        next: async (respuesta) => {
          if (!respuesta.exito || !respuesta.datos) {
            this.mensajeError =
              respuesta.errores?.[0] ||
              respuesta.mensaje ||
              'No fue posible iniciar sesión.';

            this.cargando = false;
            return;
          }

          await this.authService.guardarSesion(
            respuesta.datos
          );

          this.cargando = false;

          await this.router.navigateByUrl(
            '/dashboard',
            {
              replaceUrl: true,
            }
          );
        },

        error: (error) => {
          this.cargando = false;

          this.mensajeError =
            error?.error?.errores?.[0] ||
            error?.error?.mensaje ||
            'No fue posible comunicarse con la API.';
        },
      });
  }
}
