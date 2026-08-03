import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

import {
  IonButton,
  IonContent,
  IonHeader,
  IonIcon,
  IonToolbar
} from '@ionic/angular/standalone';

import { addIcons } from 'ionicons';

import {
  businessOutline,
  carSportOutline,
  chevronForwardOutline,
  gridOutline,
  logInOutline,
  logOutOutline,
  peopleOutline,
  receiptOutline,
  documentTextOutline
} from 'ionicons/icons';

import { AuthService } from '../../services/auth';
import { DashboardService } from '../../services/dashboard';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.page.html',
  styleUrls: ['./dashboard.page.scss'],
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    IonHeader,
    IonToolbar,
    IonContent,
    IonButton,
    IonIcon
  ]
})
export class DashboardPage implements OnInit {


  private readonly authService =
    inject(AuthService);


  private readonly router =
    inject(Router);


  private readonly dashboardService =
    inject(DashboardService);



  vehiculosIngresados = 0;

  ingresosRegistrados = 0;

  espaciosDisponibles = 0;

  facturacionDiaria = 0;

  facturacionMensual = 0;



  constructor() {

    addIcons({

      businessOutline,
      carSportOutline,
      chevronForwardOutline,
      gridOutline,
      logInOutline,
      logOutOutline,
      peopleOutline,
      receiptOutline,
      documentTextOutline

    });

  }

  ngOnInit(): void {

    this.cargarDashboard();

  }

  cargarDashboard(): void {


    this.dashboardService.obtenerDatos()
      .subscribe({

        next: (respuesta:any) => {
  console.log(respuesta);

          const datos =
            respuesta.datos;



          this.vehiculosIngresados =
            datos.vehiculosIngresadosHoy;



          this.ingresosRegistrados =
            datos.ingresosRegistrados;



          this.espaciosDisponibles =
            datos.espaciosDisponibles;



          this.facturacionDiaria =
            datos.facturacionDiaria;



          this.facturacionMensual =
            datos.facturacionMensual;


        },


        error: (error) => {

          console.error(
            'Error cargando datos del dashboard',
            error
          );

        }


      });


  }



  async cerrarSesion(): Promise<void> {


    await this.authService.cerrarSesion();


    await this.router.navigateByUrl(
      '/login',
      {
        replaceUrl: true
      }
    );


  }


}