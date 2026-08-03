export interface Factura {
  facturaId: number;
  ingresoId: number;
  fechaFactura: string;
  horasCobradas: number;
  montoTotal: number;
  placa: string;
  vehiculo: string;
  cliente: string;
}