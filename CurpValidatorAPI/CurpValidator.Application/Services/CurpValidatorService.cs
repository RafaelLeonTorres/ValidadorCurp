using CurpValidator.Application.Interfaces;
using CurpValidator.Domain.Entities;
using CurpValidator.Domain.Enums;
using CurpValidator.Domain.Constants;
using System.Globalization;

namespace CurpValidator.Application.Services;

public class CurpValidatorService : ICurpValidatorService
{
    public List<string> Validar(DatosEntrada datos)
    {
        var errores = new List<string>();

        if (string.IsNullOrWhiteSpace(datos.Curp) || datos.Curp.Length != 18)
        {
            errores.Add("El CURP debe contener 18 caracteres.");
            return errores;
        }

        var curp = datos.Curp.ToUpperInvariant();

        ValidarNombre(curp, datos, errores);
        ValidarFecha(curp, datos, errores);
        ValidarSexo(curp, datos, errores);
        ValidarEstado(curp, datos, errores);
        ValidarConsonantesInternas(curp, datos, errores);

        return errores;
    }

    private static void ValidarNombre(string curp, DatosEntrada datos, List<string> errores)
    {
        var letraPaterno = char.ToUpperInvariant(datos.ApellidoPaterno[0]);
        var vocalInterna = ObtenerPrimeraVocalInterna(datos.ApellidoPaterno);
        var letraMaterno = char.ToUpperInvariant(datos.ApellidoMaterno[0]);
        var letraNombre = char.ToUpperInvariant(datos.Nombres[0]);

        if (curp[CurpPosiciones.PosLetraPaterno] != letraPaterno)
            errores.Add("Primera letra del apellido paterno incorrecta.");

        if (curp[CurpPosiciones.PosVocalInterna] != vocalInterna)
            errores.Add("Primera vocal interna del apellido paterno incorrecta.");

        if (curp[CurpPosiciones.PosLetraMaterno] != letraMaterno)
            errores.Add("Primera letra del apellido materno incorrecta.");

        if (curp[CurpPosiciones.PosLetraNombre] != letraNombre)
            errores.Add("Primera letra del nombre incorrecta.");
    }

    private static void ValidarFecha(string curp, DatosEntrada datos, List<string> errores)
    {
        var fechaCurp = curp.Substring(CurpPosiciones.PosFechaInicio, 6);

        if (!DateTimeOffset.TryParse(datos.FechaNacimiento, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset fechaEsperada))
        {
            errores.Add("La fecha de nacimiento tiene un formato inválido.");
            return;
        }

        var fechaFormateada = fechaEsperada.ToString("yyMMdd", CultureInfo.InvariantCulture);

        if (fechaCurp != fechaFormateada)
        {
            errores.Add("La fecha de nacimiento no coincide.");
        }
    }

    private static void ValidarSexo(string curp, DatosEntrada datos, List<string> errores)
    {
        var sexoCurp = curp[CurpPosiciones.PosSexo];
        var esperado = datos.Sexo == Sexo.Masculino ? 'H' : 'M';

        if (sexoCurp != esperado)
            errores.Add("El sexo no coincide.");
    }

    private static void ValidarEstado(string curp, DatosEntrada datos, List<string> errores)
    {
        var estadoStr = curp.Substring(CurpPosiciones.PosEstadoInicio, 2);

        if (!Enum.TryParse<CurpEstado>(estadoStr, out var estado))
        {
            errores.Add($"La clave de estado '{estadoStr}' no es válida en la CURP.");
            return;
        }

        // Validación para extranjeros
        if (!datos.EsMexicano && estado != CurpEstado.NE)
        {
            errores.Add("Para extranjeros el estado debe ser 'NE'.");
        }

        // Validación para mexicanos
        if (datos.EsMexicano && estado == CurpEstado.NE)
        {
            errores.Add("Para mexicanos el estado no puede ser 'NE'.");
        }
    }

    private static void ValidarConsonantesInternas(string curp, DatosEntrada datos, List<string> errores)
    {
        var consPaterno = ObtenerPrimeraConsonanteInterna(datos.ApellidoPaterno);
        var consMaterno = ObtenerPrimeraConsonanteInterna(datos.ApellidoMaterno);
        var consNombre = ObtenerPrimeraConsonanteInterna(datos.Nombres);

        if (curp[CurpPosiciones.PosConsPaterno] != consPaterno)
            errores.Add("Consonante interna del apellido paterno incorrecta.");

        if (curp[CurpPosiciones.PosConsMaterno] != consMaterno)
            errores.Add("Consonante interna del apellido materno incorrecta.");

        if (curp[CurpPosiciones.PosConsNombre] != consNombre)
            errores.Add("Consonante interna del nombre incorrecta.");
    }

    private static char ObtenerPrimeraVocalInterna(string texto)
    {
        return texto
            .Skip(1)
            .Select(c => char.ToUpperInvariant(c))
            .FirstOrDefault(c => CurpConstants.Vocales.Contains(c));
    }

    private static char ObtenerPrimeraConsonanteInterna(string texto)
    {
        return texto
            .Skip(1)
            .Select(c => char.ToUpperInvariant(c))
            .FirstOrDefault(c => CurpConstants.Consonantes.Contains(c));
    }
}