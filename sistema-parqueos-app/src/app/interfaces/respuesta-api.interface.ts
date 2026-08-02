export interface RespuestaApi<T> {
  exito: boolean;
  mensaje: string;
  datos: T | null;
  errores: string[];
}