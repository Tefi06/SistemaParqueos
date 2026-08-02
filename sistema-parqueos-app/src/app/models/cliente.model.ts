export interface Cliente {
  clienteId: number;
  nombre: string;
  apellidos: string;
  cedula: string;
  telefono?: string | null;
  correo?: string | null;
  activo: boolean;
}