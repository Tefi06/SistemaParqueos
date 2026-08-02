import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import {
  Component,
  OnInit,
  inject
} from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import {
  IonButton,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonContent,
  IonHeader,
  IonInput,
  IonItem,
  IonLabel,
  IonList,
  IonSpinner,
  IonTitle,
  IonToggle,
  IonToolbar
} from '@ionic/angular/standalone';

import Swal from 'sweetalert2';

import { Cliente } from '../../models/cliente.model';
import { ClienteService } from '../../services/cliente';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.page.html',
  styleUrls: ['./clientes.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonCard,
    IonCardHeader,
    IonCardTitle,
    IonCardContent,
    IonItem,
    IonInput,
    IonToggle,
    IonButton,
    IonList,
    IonLabel,
    IonSpinner
  ]
})
export class ClientesPage implements OnInit {
  private readonly clienteService =
    inject(ClienteService);

  private readonly fb =
    inject(FormBuilder);

  private readonly alerta = Swal.mixin({
    background: '#ffffff',
    color: '#081f35',

    confirmButtonColor: '#123a63',
    cancelButtonColor: '#64748b',

    backdrop:
      'rgba(8, 31, 53, 0.45)',

    heightAuto: false,

    buttonsStyling: true,

    customClass: {
      popup: 'alerta-parqueos',
      confirmButton: 'boton-confirmar-alerta',
      cancelButton: 'boton-cancelar-alerta'
    }
  });

  clientes: Cliente[] = [];

  cargando = false;
  guardando = false;

  clienteEditandoId: number | null =
    null;

  formulario =
    this.fb.nonNullable.group({
      clienteId: [0],

      nombre: [
        '',
        [
          Validators.required
        ]
      ],

      apellidos: [
        '',
        [
          Validators.required
        ]
      ],

      cedula: [
        '',
        [
          Validators.required
        ]
      ],

      telefono: [''],

      correo: [
        '',
        [
          Validators.email
        ]
      ],

      activo: [true]
    });

  ngOnInit(): void {
    this.cargarClientes();
  }

  cargarClientes(): void {
    this.cargando = true;

    this.clienteService
      .obtenerTodos()
      .subscribe({
        next: (respuesta) => {
          this.clientes =
            respuesta.datos ?? [];

          this.cargando = false;
        },

        error: (
          error: HttpErrorResponse
        ) => {
          this.cargando = false;
          this.mostrarError(error);
        }
      });
  }

  guardar(): void {
    if (this.formulario.invalid) {
      this.formulario.markAllAsTouched();

      void this.alerta.fire({
        icon: 'warning',

        title:
          'Formulario incompleto',

        text:
          'Revisa los campos obligatorios y el correo electrónico.',

        confirmButtonText:
          'Revisar'
      });

      return;
    }

    const valores =
      this.formulario.getRawValue();

    const cliente: Cliente = {
      clienteId:
        valores.clienteId,

      nombre:
        valores.nombre.trim(),

      apellidos:
        valores.apellidos.trim(),

      cedula:
        valores.cedula.trim(),

      telefono:
        valores.telefono.trim() ||
        null,

      correo:
        valores.correo.trim() ||
        null,

      activo:
        valores.activo
    };

    this.guardando = true;

    if (
      this.clienteEditandoId !== null
    ) {
      this.actualizarCliente(
        this.clienteEditandoId,
        cliente
      );
    } else {
      this.crearCliente(cliente);
    }
  }

  private crearCliente(
    cliente: Cliente
  ): void {
    this.clienteService
      .crear(cliente)
      .subscribe({
        next: (respuesta) => {
          this.guardando = false;

          void this.alerta.fire({
            icon: 'success',

            title:
              'Cliente registrado',

            text:
              respuesta.mensaje ||
              'El cliente fue registrado correctamente.',

            confirmButtonText:
              'Aceptar'
          });

          this.limpiarFormulario();
          this.cargarClientes();
        },

        error: (
          error: HttpErrorResponse
        ) => {
          this.guardando = false;
          this.mostrarError(error);
        }
      });
  }

  private actualizarCliente(
    id: number,
    cliente: Cliente
  ): void {
    this.clienteService
      .actualizar(id, cliente)
      .subscribe({
        next: (respuesta) => {
          this.guardando = false;

          void this.alerta.fire({
            icon: 'success',

            title:
              'Cliente actualizado',

            text:
              respuesta.mensaje ||
              'Los datos fueron actualizados correctamente.',

            confirmButtonText:
              'Aceptar'
          });

          this.limpiarFormulario();
          this.cargarClientes();
        },

        error: (
          error: HttpErrorResponse
        ) => {
          this.guardando = false;
          this.mostrarError(error);
        }
      });
  }

  editar(cliente: Cliente): void {
    this.clienteEditandoId =
      cliente.clienteId;

    this.formulario.setValue({
      clienteId:
        cliente.clienteId,

      nombre:
        cliente.nombre,

      apellidos:
        cliente.apellidos,

      cedula:
        cliente.cedula,

      telefono:
        cliente.telefono ?? '',

      correo:
        cliente.correo ?? '',

      activo:
        cliente.activo
    });

    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  }

  cancelarEdicion(): void {
    this.limpiarFormulario();
  }

  async eliminar(
    cliente: Cliente
  ): Promise<void> {
    const confirmacion =
      await this.alerta.fire({
        icon: 'question',

        title:
          'Desactivar cliente',

        text:
          `¿Deseas desactivar a ${cliente.nombre} ${cliente.apellidos}?`,

        showCancelButton: true,

        confirmButtonText:
          'Sí, desactivar',

        cancelButtonText:
          'Cancelar',

        reverseButtons: true
      });

    if (!confirmacion.isConfirmed) {
      return;
    }

    this.clienteService
      .eliminar(cliente.clienteId)
      .subscribe({
        next: (respuesta) => {
          void this.alerta.fire({
            icon: 'success',

            title:
              'Cliente desactivado',

            text:
              respuesta.mensaje ||
              'El cliente fue desactivado correctamente.',

            confirmButtonText:
              'Aceptar'
          });

          this.cargarClientes();
        },

        error: (
          error: HttpErrorResponse
        ) => {
          this.mostrarError(error);
        }
      });
  }

  private limpiarFormulario(): void {
    this.clienteEditandoId = null;

    this.formulario.reset({
      clienteId: 0,
      nombre: '',
      apellidos: '',
      cedula: '',
      telefono: '',
      correo: '',
      activo: true
    });
  }

  private mostrarError(
    error: HttpErrorResponse
  ): void {
    const mensaje =
      error.error?.errores?.[0] ??
      error.error?.mensaje ??
      'No fue posible completar la operación.';

    void this.alerta.fire({
      icon: 'error',

      title:
        'Ocurrió un problema',

      text:
        mensaje,

      confirmButtonText:
        'Aceptar'
    });
  }
}