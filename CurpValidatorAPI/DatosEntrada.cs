using System;
using CurpValidatorAPI.Domain.Enums;

public class DatosEntrada
{
    public string Curp { get; set; } = string.Empty;
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string ApellidoMaterno { get; set; } = string.Empty;
    public DateTime FechaNacimiento { get; set; }
    public Sexo Sexo { get; set; }
    public bool EsMexicano { get; set; }
}
