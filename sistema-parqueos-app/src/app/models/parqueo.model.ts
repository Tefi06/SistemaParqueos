export interface Parqueo {
  parqueoId: number;
  nombreParqueo: string;
  direccion: string;
  telefono?: string | null;
  capacidadTotal: number;
  espaciosDisponibles: number;
  espaciosOcupados: number;
  activo: boolean;
}