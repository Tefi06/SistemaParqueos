export interface Vehiculo {
  vehiculoId: number;
  clienteId: number;
  tipoVehiculoId: number;
  placa: string;
  marca: string;
  modelo?: string | null;
  color?: string | null;
  activo: boolean;
}