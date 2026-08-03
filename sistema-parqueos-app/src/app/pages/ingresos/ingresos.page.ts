import { CommonModule } from '@angular/common';
import {
  FacturaService
} from '../../services/factura';

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
  IonSelect,
  IonSelectOption,
  IonSpinner,
  IonTitle,
  IonToolbar
} from '@ionic/angular/standalone';

import {
  addIcons
} from 'ionicons';

import {
  arrowBackOutline
} from 'ionicons/icons';


import {
  Ingreso
} from '../../models/ingreso.model';

import {
  IngresoService
} from '../../services/ingreso';

import {
  Vehiculo
} from '../../models/vehiculo.model';

import {
  VehiculoService
} from '../../services/vehiculo';

import {
  Parqueo
} from '../../models/parqueo.model';

import {
  ParqueoService
} from '../../services/parqueo';

import {
  EspacioParqueo
} from '../../models/espacio-parqueo.model';

import {
  EspacioParqueoService
} from '../../services/espacio-parqueo';


@Component({
  selector: 'app-ingresos',
  templateUrl: './ingresos.page.html',
  styleUrls: ['./ingresos.page.scss'],
  standalone: true,

  imports: [

    CommonModule,
    ReactiveFormsModule,
    RouterLink,

    IonContent,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonButtons,
    IonIcon,

    IonCard,
    IonCardHeader,
    IonCardTitle,
    IonCardContent,

    IonItem,
    IonLabel,

    IonButton,

    IonList,

    IonSelect,
    IonSelectOption,

    IonSpinner

  ]

})


export class IngresosPage implements OnInit {


  private readonly fb =
    inject(FormBuilder);


  private readonly ingresoService =
    inject(IngresoService);


  private readonly vehiculoService =
    inject(VehiculoService);


  private readonly parqueoService =
    inject(ParqueoService);


  private readonly espacioService =
    inject(EspacioParqueoService);

    private readonly facturaService =
  inject(FacturaService);



  ingresos: Ingreso[] = [];

  vehiculos: Vehiculo[] = [];

  todosLosVehiculos: Vehiculo[] = [];

  parqueos: Parqueo[] = [];

  espacios: EspacioParqueo[] = [];

  espaciosDisponibles: EspacioParqueo[] = [];


  cargando = false;

  guardando = false;



  formulario =
    this.fb.nonNullable.group({

      vehiculoId: [

        0,

        [

          Validators.required,

          Validators.min(1)

        ]

      ],


      parqueoId: [

        0,

        [

          Validators.required,

          Validators.min(1)

        ]

      ],


      espacioId: [

        0,

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

    this.cargarDatos();

  }



  cargarDatos(): void {


    this.ingresoService
      .obtenerTodos()
      .subscribe({

        next: (respuesta) => {


          this.ingresos =
            respuesta.datos ?? [];



          this.cargarVehiculos();

          this.cargarParqueos();

          this.cargarEspacios();


        },


        error: (error) => {

          console.error(error);

        }


      });


  }



  cargarVehiculos(): void {


    this.vehiculoService
      .obtenerTodos()
      .subscribe({

        next: (respuesta) => {


          const todos =
            respuesta.datos ?? [];



          this.todosLosVehiculos =
            todos;



          this.vehiculos =
            todos;


        },


        error: (error) => {

          console.error(error);

        }


      });


  }
    cargarParqueos(): void {

    this.parqueoService
      .obtenerTodos()
      .subscribe({

        next: (respuesta) => {

          this.parqueos =
            respuesta.datos ?? [];

        },

        error: (error) => {

          console.error(error);

        }

      });

  }



  cargarEspacios(): void {

    this.espacioService
      .obtenerTodos()
      .subscribe({

        next: (respuesta) => {

          this.espacios =
            respuesta.datos ?? [];

        },

        error: (error) => {

          console.error(error);

        }

      });

  }



  cambioParqueo(): void {

    const id =
      this.formulario.value.parqueoId;



    this.espaciosDisponibles =
      this.espacios.filter(

        espacio =>

          espacio.parqueoId === id &&

          espacio.disponible &&

          espacio.activo

      );



    this.formulario.patchValue({

      espacioId: 0

    });

  }




  registrarIngreso(): void {


    if (this.formulario.invalid) {

      this.formulario.markAllAsTouched();

      return;

    }



    const datos =
      this.formulario.getRawValue();



    const ingreso: Ingreso = {


      ingresoId: 0,


      vehiculoId:
        datos.vehiculoId,


      parqueoId:
        datos.parqueoId,


      espacioId:
        datos.espacioId,


      fechaIngreso:
        new Date().toISOString(),


      fechaSalida: null,


      estado:
        'Activo'


    };



    this.guardando = true;



    this.ingresoService
      .registrarIngreso(ingreso)
      .subscribe({

        next: () => {


          this.guardando = false;



          this.formulario.reset({

            vehiculoId: 0,

            parqueoId: 0,

            espacioId: 0

          });



          this.espaciosDisponibles = [];



          this.cargarDatos();


        },


        error: (error) => {


          this.guardando = false;


          console.error(error);


        }


      });


  }





  registrarSalida(
  ingreso: Ingreso
): void {


  this.ingresoService
    .registrarSalida(
      ingreso.ingresoId
    )
    .subscribe({

      next: () => {


        this.facturaService
          .generar(
            ingreso.ingresoId
          )
          .subscribe({

            next: (respuesta) => {

              console.log(
                'Factura generada',
                respuesta
              );

              this.cargarDatos();

            },


            error: (error) => {

              console.error(
                'Error generando factura',
                error
              );

            }

          });


      },


      error: (error) => {


        console.error(
          'Error registrando salida',
          error
        );


      }

    });


}





  obtenerPlaca(
    vehiculoId: number
  ): string {


    const vehiculo =
      this.todosLosVehiculos.find(

        v =>

          v.vehiculoId === vehiculoId

      );



    return vehiculo?.placa ??
      'Sin información';


  }





  obtenerDescripcionVehiculo(
    vehiculoId: number
  ): string {


    const vehiculo =
      this.todosLosVehiculos.find(

        v =>

          v.vehiculoId === vehiculoId

      );



    if (!vehiculo) {

      return 'Sin información';

    }



    return `${vehiculo.placa} - ${vehiculo.marca} ${vehiculo.modelo ?? ''}`;


  }


}