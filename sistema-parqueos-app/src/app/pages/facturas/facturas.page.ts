import {
  CommonModule
} from '@angular/common';

import {
  Component,
  OnInit,
  inject
} from '@angular/core';

import {
  RouterLink
} from '@angular/router';

import {
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
  IonTitle,
  IonToolbar
} from '@ionic/angular/standalone';

import {
  addIcons
} from 'ionicons';

import {
  arrowBackOutline,
  documentTextOutline
} from 'ionicons/icons';

import {
  Factura
} from '../../models/factura.model';

import {
  FacturaService
} from '../../services/factura';



@Component({
  selector: 'app-facturas',
  templateUrl: './facturas.page.html',
  styleUrls: ['./facturas.page.scss'],
  standalone: true,

  imports: [
    CommonModule,
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
    IonLabel
  ]

})


export class FacturasPage implements OnInit {


  private readonly facturaService =
    inject(FacturaService);



  facturas: Factura[] = [];

  cargando = false;



  constructor() {

    addIcons({

      arrowBackOutline

    });

  }



  ngOnInit(): void {

    this.cargarFacturas();

  }



  cargarFacturas(): void {


    this.cargando = true;


    this.facturaService
      .obtenerTodos()
      .subscribe({

        next: (respuesta) => {


          this.facturas =
            respuesta.datos ?? [];


          this.cargando = false;


        },


        error: (error) => {


          console.error(error);


          this.cargando = false;


        }


      });


  }


}