export interface DatosCurp {
    curp: string;
    nombres: string;
    apellidoPaterno: string;
    apellidoMaterno: string;
    fechaNacimiento: string;
    sexo: Sexo;
    esMexicano: boolean;
}

export enum Sexo {
    Masculino = 1,
    Femenino = 2
}
