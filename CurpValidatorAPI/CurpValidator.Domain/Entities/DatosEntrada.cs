using CurpValidator.Domain.Enums;

namespace CurpValidator.Domain.Entities
{
    public class DatosEntrada
    {
        public string Curp { get; set; } = string.Empty; // CURP a evaluar
        public string Nombres { get; set; } = string.Empty; // Nombres de pila de la persona
        public string ApellidoPaterno { get; set; } = string.Empty; // Apellido paterno de la persona
        public string ApellidoMaterno { get; set; } = string.Empty; // Apellido materno de la persona
        public string FechaNacimiento { get; set; } = string.Empty; // Fecha de nacimiento de la persona, dato en formato ISO string "1992-07-01T06:00:00.000Z"
        public Sexo Sexo { get; set; } // Género de la persona
        public Boolean EsMexicano { get; set; } // Indica si la persona es mexicana o no
    }
}
