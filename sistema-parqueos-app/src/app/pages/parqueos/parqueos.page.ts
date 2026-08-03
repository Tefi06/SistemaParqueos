import { CommonModule } from '@angular/common';

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
  Router,
  RouterLink
} from '@angular/router';

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
  IonItem,
  IonLabel,
  IonList,
  IonSpinner,
  IonTitle,
  IonToolbar
} from '@ionic/angular/standalone';

import { addIcons } from 'ionicons';
import { arrowBackOutline } from 'ionicons/icons';

import Swal from 'sweetalert2';

import { HttpErrorResponse } from '@angular/common/http';

import { Parqueo } from '../../models/parqueo.model';
import { ParqueoService } from '../../services/parqueo';


@Component({
  selector: 'app-parqueos',
  templateUrl: './parqueos.page.html',
  styleUrls: ['./parqueos.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,

    IonContent,
    IonHeader,
    IonTitle,
    IonToolbar,
    IonButtons,
    IonIcon,

    IonCard,
    IonCardHeader,
    IonCardTitle,
    IonCardContent,

    IonItem,
    IonButton,

    IonList,
    IonLabel,

    IonSpinner
  ]
})
export class ParqueosPage implements OnInit {


  private readonly parqueoService =
    inject(ParqueoService);


  private readonly router =
    inject(Router);


  private readonly fb =
    inject(FormBuilder);


  private readonly alerta =
    Swal.mixin({

      background: '#ffffff',
      color: '#20243a',

      confirmButtonColor: '#5b5fef',
      cancelButtonColor: '#6b7088',

      backdrop:
        'rgba(23,26,58,0.45)',

      heightAuto: false

    });



  parqueos: Parqueo[] = [];

  cargando = false;

  guardando = false;


  parqueoEditandoId:
    number | null = null;



  formulario =
    this.fb.nonNullable.group({

      parqueoId: [0],

      nombreParqueo: [
        '',
        [
          Validators.required
        ]
      ],


      direccion: [
        '',
        [
          Validators.required
        ]
      ],


      telefono: [
        ''
      ],


      capacidadTotal: [
        1,
        [
          Validators.required,
          Validators.min(1)
        ]
      ]

    });



  constructor() {

    addIcons({

      arrowBackOutline

    });

  }



  ngOnInit(): void {

    this.cargarParqueos();

  }



  cargarParqueos(): void {

    this.cargando = true;


    this.parqueoService
      .obtenerTodos()
      .subscribe({

        next: (respuesta) => {

          this.parqueos =
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



  irAEspacios(id: number): void {

    this.router.navigate([
      '/espacios-parqueo',
      id
    ]);

  }



  guardar(): void {


    if (this.formulario.invalid) {

      this.formulario.markAllAsTouched();

      return;

    }


    const valores =
      this.formulario.getRawValue();



    const parqueo: Parqueo = {

      parqueoId:
        valores.parqueoId,

      nombreParqueo:
        valores.nombreParqueo.trim(),

      direccion:
        valores.direccion.trim(),

      telefono:
        valores.telefono.trim() || null,

      capacidadTotal:
        valores.capacidadTotal,

      espaciosDisponibles:
        0,

      espaciosOcupados:
        0,

      activo:
        true

    };



    this.guardando = true;



    if (
      this.parqueoEditandoId !== null
    ) {


      this.actualizar(
        this.parqueoEditandoId,
        parqueo
      );


    } else {


      this.crear(parqueo);


    }


  }



  private crear(
    parqueo: Parqueo
  ): void {


    this.parqueoService
      .crear(parqueo)
      .subscribe({

        next: () => {

          this.guardando = false;

          this.limpiarFormulario();

          this.cargarParqueos();

        },


        error: (
          error: HttpErrorResponse
        ) => {

          this.guardando = false;

          this.mostrarError(error);

        }

      });


  }



  private actualizar(
    id: number,
    parqueo: Parqueo
  ): void {


    this.parqueoService
      .actualizar(id, parqueo)
      .subscribe({

        next: () => {

          this.guardando = false;

          this.limpiarFormulario();

          this.cargarParqueos();

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
    parqueo: Parqueo
  ): void {


    this.parqueoEditandoId =
      parqueo.parqueoId;


    this.formulario.setValue({

      parqueoId:
        parqueo.parqueoId,

      nombreParqueo:
        parqueo.nombreParqueo,

      direccion:
        parqueo.direccion,

      telefono:
        parqueo.telefono ?? '',

      capacidadTotal:
        parqueo.capacidadTotal

    });


  }



  async eliminar(
    parqueo: Parqueo
  ): Promise<void> {


    const confirmacion =
      await this.alerta.fire({

        icon: 'question',

        title:
          'Desactivar parqueo',

        text:
          `¿Deseas desactivar ${parqueo.nombreParqueo}?`,

        showCancelButton: true,

        confirmButtonText:
          'Sí, desactivar',

        cancelButtonText:
          'Cancelar'

      });



    if (!confirmacion.isConfirmed) {

      return;

    }



    this.parqueoService
      .eliminar(
        parqueo.parqueoId
      )
      .subscribe({

        next: () => {

          this.cargarParqueos();

        },


        error: (
          error: HttpErrorResponse
        ) => {

          this.mostrarError(error);

        }

      });


  }



  cancelarEdicion(): void {

    this.limpiarFormulario();

  }



  private limpiarFormulario(): void {


    this.parqueoEditandoId = null;


    this.formulario.reset({

      parqueoId: 0,

      nombreParqueo: '',

      direccion: '',

      telefono: '',

      capacidadTotal: 1

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