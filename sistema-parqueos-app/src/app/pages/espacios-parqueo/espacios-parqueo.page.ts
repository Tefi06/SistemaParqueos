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
  ActivatedRoute,
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
  IonInput,
  IonItem,
  IonLabel,
  IonList,
  IonSpinner,
  IonTitle,
  IonToolbar
} from '@ionic/angular/standalone';


import { addIcons } from 'ionicons';

import {
  arrowBackOutline
} from 'ionicons/icons';


import Swal from 'sweetalert2';


import {
  HttpErrorResponse
} from '@angular/common/http';


import {
  EspacioParqueo
} from '../../models/espacio-parqueo.model';


import {
  EspacioParqueoService
} from '../../services/espacio-parqueo';



@Component({

  selector: 'app-espacios-parqueo',

  templateUrl: './espacios-parqueo.page.html',

  styleUrls: ['./espacios-parqueo.page.scss'],

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


    IonList,

    IonItem,

    IonInput,

    IonLabel,


    IonButton,

    IonSpinner

  ]

})


export class EspaciosParqueoPage implements OnInit {


  private readonly route =
    inject(ActivatedRoute);



  private readonly espacioService =
    inject(EspacioParqueoService);



  private readonly fb =
    inject(FormBuilder);



  private readonly alerta =
    Swal.mixin({

      background:'#ffffff',

      color:'#20243a',

      confirmButtonColor:'#5b5fef',

      cancelButtonColor:'#6b7088',

      heightAuto:false

    });



  parqueoId!: number;



  espacios: EspacioParqueo[] = [];



  cargando = false;



  guardando = false;



  espacioEditandoId:
    number | null = null;




  formulario =
    this.fb.nonNullable.group({


      espacioId:[0],


      numeroEspacio:[

        '',

        [

          Validators.required

        ]

      ]


    });




  constructor(){


    addIcons({

      arrowBackOutline

    });


  }




  ngOnInit(): void {



    this.parqueoId =

      Number(

        this.route.snapshot.paramMap.get('id')

      );



    this.cargarEspacios();


  }




  cargarEspacios():void{


    this.cargando = true;



    this.espacioService

      .obtenerTodos()

      .subscribe({



        next:(respuesta)=>{



          this.espacios =

            (respuesta.datos ?? [])

            .filter(

              espacio =>

              espacio.parqueoId === this.parqueoId

            );



          this.cargando = false;



        },



        error:(error:HttpErrorResponse)=>{


          this.cargando=false;


          this.mostrarError(error);



        }



      });



  }




  guardar():void{


    if(this.formulario.invalid){


      this.formulario.markAllAsTouched();


      return;


    }



    const valores =

      this.formulario.getRawValue();




    const espacio: EspacioParqueo = {



      espacioId:

        valores.espacioId,



      parqueoId:

        this.parqueoId,



      numeroEspacio:

        valores.numeroEspacio.trim(),



      disponible:

        true,



      activo:

        true


    };



    this.guardando=true;



    if(this.espacioEditandoId !== null){



      this.espacioService

        .actualizar(

          this.espacioEditandoId,

          espacio

        )

        .subscribe({


          next:()=>{


            this.guardando=false;


            this.limpiar();


            this.cargarEspacios();



          },


          error:(error)=>{


            this.guardando=false;


            this.mostrarError(error);


          }



        });



    }else{



      this.espacioService

        .crear(espacio)

        .subscribe({


          next:()=>{


            this.guardando=false;


            this.limpiar();


            this.cargarEspacios();


          },


          error:(error)=>{


            this.guardando=false;


            this.mostrarError(error);


          }



        });


    }



  }
    editar(
    espacio: EspacioParqueo
  ):void{


    this.espacioEditandoId =

      espacio.espacioId;



    this.formulario.setValue({


      espacioId:

        espacio.espacioId,



      numeroEspacio:

        espacio.numeroEspacio


    });



  }





  async eliminar(
    espacio: EspacioParqueo
  ):Promise<void>{



    const confirmacion =

      await this.alerta.fire({



        icon:'question',



        title:

          'Desactivar espacio',



        text:

          `¿Deseas desactivar el espacio ${espacio.numeroEspacio}?`,



        showCancelButton:true,



        confirmButtonText:

          'Sí, desactivar',



        cancelButtonText:

          'Cancelar'



      });





    if(!confirmacion.isConfirmed){

      return;

    }






    this.espacioService

      .eliminar(

        espacio.espacioId

      )

      .subscribe({



        next:()=>{



          this.cargarEspacios();



        },



        error:(error)=>{



          this.mostrarError(error);



        }



      });




  }







  cancelar():void{


    this.limpiar();



  }







  private limpiar():void{



    this.espacioEditandoId = null;



    this.formulario.reset({



      espacioId:0,



      numeroEspacio:''



    });



  }







  private mostrarError(

    error:HttpErrorResponse

  ):void{



    const mensaje =



      error.error?.errores?.[0] ??



      error.error?.mensaje ??



      'No fue posible completar la operación.';






    void this.alerta.fire({



      icon:'error',



      title:

        'Ocurrió un problema',



      text:

        mensaje,



      confirmButtonText:

        'Aceptar'



    });



  }





}