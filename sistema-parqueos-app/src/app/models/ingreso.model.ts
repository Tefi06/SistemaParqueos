export interface Ingreso {
  ingresoId: number;
  vehiculoId: number;
  parqueoId: number;
  espacioId: number;
  fechaIngreso: string;
  fechaSalida?: string | null;
  estado: string;
}