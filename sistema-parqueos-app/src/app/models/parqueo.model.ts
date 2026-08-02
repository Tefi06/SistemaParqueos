export interface Parqueo {
  parqueoId: number;
  nombreParqueo: string;
  direccion: string;
  telefono?: string | null;
  capacidadTotal: number;
  activo: boolean;
}