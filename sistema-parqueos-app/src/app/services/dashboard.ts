import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private http = inject(HttpClient);

  private apiUrl =
    'https://localhost:7132/api/Dashboard';


  obtenerDatos() {

    return this.http.get<any>(
      this.apiUrl
    );

  }

}