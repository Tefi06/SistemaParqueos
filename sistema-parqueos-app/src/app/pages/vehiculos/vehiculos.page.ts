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

import { RouterLink } from '@angular/router';

import {
  IonButton,
  IonButtons,
  IonCard,
  IonCardContent,
  IonCardHeader,
  IonCardTitle,
  IonContent,
  IonHeader,
  IonIcon,
  IonInput,
  IonItem,
  IonLabel,
  IonList,
  IonSelect,
  IonSelectOption,
  IonSpinner,
  IonTitle,
  IonToggle,
  IonToolbar
} from '@ionic/angular/standalone';

import { addIcons } from 'ionicons';

import {
  arrowBackOutline,
  carSportOutline
} from 'ionicons/icons';

import { forkJoin } from 'rxjs';
import Swal from 'sweetalert2';

import { Cliente } from '../../models/cliente.model';
import { TipoVehiculo } from '../../models/tipo-vehiculo.model';
import { Vehiculo } from '../../models/vehiculo.model';

import { ClienteService } from '../../services/cliente';
import { TipoVehiculoService } from '../../services/tipo-vehiculo';
import { VehiculoService } from '../../services/vehiculo';

@Component({
  selector: 'app-vehiculos',
  templateUrl: './vehiculos.page.html',
  styleUrls: ['./vehiculos.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonButtons,
    IonIcon,
    IonCard,
    IonCardHeader,
    IonCardTitle,
    IonCardContent,
    IonItem,
    IonInput,
    IonSelect,
    IonSelectOption,
    IonToggle,
    IonButton,
    IonList,
    IonLabel,
    IonSpinner
  ]
})
export class VehiculosPage implements OnInit {
  private readonly vehiculoService =
    inject(VehiculoService);

  private readonly clienteService =
    inject(ClienteService);

  private readonly tipoVehiculoService =
    inject(TipoVehiculoService);

  private readonly fb =
    inject(FormBuilder);

  private readonly alerta = Swal.mixin({
    background: '#ffffff',
    color: '#20243a',

    confirmButtonColor: '#5b5fef',
    cancelButtonColor: '#6b7088',

    backdrop:
      'rgba(23, 26, 58, 0.45)',

    heightAuto: false,

    buttonsStyling: true,

    customClass: {
      popup: 'alerta-parqueos',
      confirmButton:
        'boton-confirmar-alerta',
      cancelButton:
        'boton-cancelar-alerta'
    }
  });

  vehiculos: Vehiculo[] = [];
  clientes: Cliente[] = [];
  tiposVehiculo: TipoVehiculo[] = [];

  cargando = false;
  guardando = false;

  vehiculoEditandoId: number | null =
    null;

  formulario =
    this.fb.nonNullable.group({
      vehiculoId: [0],

      clienteId: [
        0,
        [
          Validators.required,
          Validators.min(1)
        ]
      ],

      tipoVehiculoId: [
        0,
        [
          Validators.required,
          Validators.min(1)
        ]
      ],

      placa: [
        '',
        [
          Validators.required,
          Validators.maxLength(20)
        ]
      ],

      marca: [
        '',
        [
          Validators.required,
          Validators.maxLength(50)
        ]
      ],

      modelo: [
        '',
        [
          Validators.maxLength(50)
        ]
      ],

      color: [
        '',
        [
          Validators.maxLength(50)
        ]
      ],

      activo: [true]
    });

  constructor() {
    addIcons({
      arrowBackOutline,
      carSportOutline
    });
  }

  ngOnInit(): void {
    this.cargarDatosIniciales();
  }

  cargarDatosIniciales(): void {
    this.cargando = true;

    forkJoin({
      vehiculos:
        this.vehiculoService.obtenerTodos(),

      clientes:
        this.clienteService.obtenerTodos(),

      tiposVehiculo:
        this.tipoVehiculoService.obtenerTodos()
    }).subscribe({
      next: (respuestas) => {
        this.vehiculos =
          respuestas.vehiculos.datos ?? [];

        this.clientes =
          (
            respuestas.clientes.datos ??
            []
          ).filter(
            cliente => cliente.activo
          );

        this.tiposVehiculo =
          (
            respuestas.tiposVehiculo.datos ??
            []
          ).filter(
            tipo => tipo.activo
          );

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

  cargarVehiculos(): void {
    this.cargando = true;

    this.vehiculoService
      .obtenerTodos()
      .subscribe({
        next: (respuesta) => {
          this.vehiculos =
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
          'Selecciona el cliente, el tipo de vehículo y completa la placa y la marca.',

        confirmButtonText:
          'Revisar'
      });

      return;
    }

    const valores =
      this.formulario.getRawValue();

    const vehiculo: Vehiculo = {
      vehiculoId:
        valores.vehiculoId,

      clienteId:
        valores.clienteId,

      tipoVehiculoId:
        valores.tipoVehiculoId,

      placa:
        valores.placa
          .trim()
          .toUpperCase(),

      marca:
        valores.marca.trim(),

      modelo:
        valores.modelo.trim() ||
        null,

      color:
        valores.color.trim() ||
        null,

      activo:
        valores.activo
    };

    this.guardando = true;

    if (
      this.vehiculoEditandoId !== null
    ) {
      this.actualizarVehiculo(
        this.vehiculoEditandoId,
        vehiculo
      );
    } else {
      this.crearVehiculo(vehiculo);
    }
  }

  private crearVehiculo(
    vehiculo: Vehiculo
  ): void {
    this.vehiculoService
      .crear(vehiculo)
      .subscribe({
        next: (respuesta) => {
          this.guardando = false;

          void this.alerta.fire({
            icon: 'success',

            title:
              'Vehículo registrado',

            text:
              respuesta.mensaje ||
              'El vehículo fue registrado correctamente.',

            confirmButtonText:
              'Aceptar'
          });

          this.limpiarFormulario();
          this.cargarVehiculos();
        },

        error: (
          error: HttpErrorResponse
        ) => {
          this.guardando = false;
          this.mostrarError(error);
        }
      });
  }

  private actualizarVehiculo(
    id: number,
    vehiculo: Vehiculo
  ): void {
    this.vehiculoService
      .actualizar(id, vehiculo)
      .subscribe({
        next: (respuesta) => {
          this.guardando = false;

          void this.alerta.fire({
            icon: 'success',

            title:
              'Vehículo actualizado',

            text:
              respuesta.mensaje ||
              'Los datos del vehículo fueron actualizados correctamente.',

            confirmButtonText:
              'Aceptar'
          });

          this.limpiarFormulario();
          this.cargarVehiculos();
        },

        error: (
          error: HttpErrorResponse
        ) => {
          this.guardando = false;
          this.mostrarError(error);
        }
      });
  }

  editar(
    vehiculo: Vehiculo
  ): void {
    this.vehiculoEditandoId =
      vehiculo.vehiculoId;

    this.formulario.setValue({
      vehiculoId:
        vehiculo.vehiculoId,

      clienteId:
        vehiculo.clienteId,

      tipoVehiculoId:
        vehiculo.tipoVehiculoId,

      placa:
        vehiculo.placa,

      marca:
        vehiculo.marca,

      modelo:
        vehiculo.modelo ?? '',

      color:
        vehiculo.color ?? '',

      activo:
        vehiculo.activo
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
    vehiculo: Vehiculo
  ): Promise<void> {
    const confirmacion =
      await this.alerta.fire({
        icon: 'question',

        title:
          'Desactivar vehículo',

        text:
          `¿Deseas desactivar el vehículo con placa ${vehiculo.placa}?`,

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

    this.vehiculoService
      .eliminar(vehiculo.vehiculoId)
      .subscribe({
        next: (respuesta) => {
          void this.alerta.fire({
            icon: 'success',

            title:
              'Vehículo desactivado',

            text:
              respuesta.mensaje ||
              'El vehículo fue desactivado correctamente.',

            confirmButtonText:
              'Aceptar'
          });

          this.cargarVehiculos();
        },

        error: (
          error: HttpErrorResponse
        ) => {
          this.mostrarError(error);
        }
      });
  }

  obtenerNombreCliente(
    clienteId: number
  ): string {
    const cliente =
      this.clientes.find(
        item =>
          item.clienteId === clienteId
      );

    if (!cliente) {
      return `Cliente #${clienteId}`;
    }

    return `${cliente.nombre} ${cliente.apellidos}`;
  }

  obtenerTipoVehiculo(
    tipoVehiculoId: number
  ): string {
    const tipo =
      this.tiposVehiculo.find(
        item =>
          item.tipoVehiculoId ===
          tipoVehiculoId
      );

    return tipo?.descripcion ??
      `Tipo #${tipoVehiculoId}`;
  }

  private limpiarFormulario(): void {
    this.vehiculoEditandoId = null;

    this.formulario.reset({
      vehiculoId: 0,
      clienteId: 0,
      tipoVehiculoId: 0,
      placa: '',
      marca: '',
      modelo: '',
      color: '',
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