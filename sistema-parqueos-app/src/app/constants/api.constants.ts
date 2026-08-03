export const API_URL = 'https://localhost:7132/api';

export const API_ENDPOINTS = {
  clientes: `${API_URL}/Cliente`,
  vehiculos: `${API_URL}/Vehiculo`,
  tiposVehiculo: `${API_URL}/TipoVehiculo`,
  parqueos: `${API_URL}/Parqueo`,
  espaciosParqueo: `${API_URL}/EspacioParqueo`,
  tarifas: `${API_URL}/Tarifa`,
  ingresos: `${API_URL}/IngresoVehiculo`,
  facturas: `${API_URL}/Factura`,
    crearAdministradorInicial:
    `${API_URL}/Auth/CrearAdministradorInicial`,

  login:
    `${API_URL}/Auth/Login`,

};