using System;

public class CurpValidatorService : ICurpValidatorService
{
    private static readonly string Vocales = "AEIOU";
    private static readonly string Consonantes = "BCDFGHJKLMNPQRSTVWXYZ";

    public List<string> Validar(DatosEntrada datos)
    {
        var errores = new List<string>();

        if (datos.Curp.Length != 18)
        {
            errores.Add("La CURP debe contener 18 caracteres.");
            return errores;
        }

        datos.Curp = datos.Curp.ToUpper();

        ValidarNombre(datos, errores);
        ValidarFecha(datos, errores);
        ValidarSexo(datos, errores);
        ValidarEstado(datos, errores);

        return errores;
    }

    private void ValidarNombre(DatosEntrada datos, List<string> errores)
    {
        var primeraLetraApellido = datos.ApellidoPaterno[0];
        var primeraVocalInterna = datos.ApellidoPaterno
            .Skip(1)
            .FirstOrDefault(c => Vocales.Contains(char.ToUpper(c)));

        var primeraLetraMaterno = datos.ApellidoMaterno[0];
        var primeraLetraNombre = datos.Nombres[0];

        if (datos.Curp[0] != char.ToUpper(primeraLetraApellido))
            errores.Add("Primera letra del apellido paterno incorrecta.");

        if (datos.Curp[1] != char.ToUpper(primeraVocalInterna))
            errores.Add("Primera vocal interna del apellido paterno incorrecta.");

        if (datos.Curp[2] != char.ToUpper(primeraLetraMaterno))
            errores.Add("Primera letra del apellido materno incorrecta.");

        if (datos.Curp[3] != char.ToUpper(primeraLetraNombre))
            errores.Add("Primera letra del nombre incorrecta.");
    }

    private void ValidarFecha(DatosEntrada datos, List<string> errores)
    {
        var fechaCurp = datos.Curp.Substring(4, 6);
        var fechaEsperada = datos.FechaNacimiento.ToString("yyMMdd");

        if (fechaCurp != fechaEsperada)
            errores.Add("La fecha de nacimiento no coincide.");
    }

    private void ValidarSexo(DatosEntrada datos, List<string> errores)
    {
        var sexoCurp = datos.Curp[10];

        var esperado = datos.Sexo == Sexo.Masculino ? 'H' : 'M';

        if (sexoCurp != esperado)
            errores.Add("El sexo no coincide.");
    }

    private void ValidarEstado(DatosEntrada datos, List<string> errores)
    {
        var estado = datos.Curp.Substring(11, 2);

        if (!datos.EsMexicano && estado != "NE")
            errores.Add("Para extranjeros el estado debe ser 'NE'.");
    }
}
