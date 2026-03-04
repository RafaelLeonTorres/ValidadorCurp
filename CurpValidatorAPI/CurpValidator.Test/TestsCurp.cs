using CurpValidator.Application.Services;
using CurpValidator.Domain.Entities;
using CurpValidator.Domain.Enums;

namespace CurpValidator.Tests;

[TestClass]
public sealed class TestsCurp
{
    private readonly CurpValidatorService _validator = new();

    [TestMethod]
    public void Curp_SinErrores()
    {
        var datos = new DatosEntrada
        {
            Curp = "SABC921215HMCLRR09",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = true
        };

        var resultado = _validator.Validar(datos);

        Assert.AreEqual(0, resultado.Count);
    }

    [TestMethod]
    public void Curp_LongitudIncorrectaError()
    {
        var datos = new DatosEntrada
        {
            Curp = "ABC123",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = true
        };

        var resultado = _validator.Validar(datos);

        Assert.AreEqual(1, resultado.Count);
        Assert.AreEqual("El CURP debe contener 18 caracteres.", resultado[0]);
    }

    [TestMethod]
    public void Curp_LetraApellidoPaternoIncorrectaError()
    {
        var datos = new DatosEntrada
        {
            Curp = "XABC921215HMCLRR09",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = true
        };

        var resultado = _validator.Validar(datos);

        Assert.IsTrue(resultado.Contains("Primera letra del apellido paterno incorrecta."));
    }

    [TestMethod]
    public void Curp_FechaIncorrectaError()
    {
        var datos = new DatosEntrada
        {
            Curp = "SABC991215HMCLRR09",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = true
        };

        var resultado = _validator.Validar(datos);

        Assert.IsTrue(resultado.Contains("La fecha de nacimiento no coincide."));
    }

    [TestMethod]
    public void Curp_FormatoFechaIncorrectoError()
    {
        var datos = new DatosEntrada
        {
            Curp = "SABC991215HMCLRR09",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "ABCDEFHI",
            Sexo = Sexo.Masculino,
            EsMexicano = true
        };

        var resultado = _validator.Validar(datos);

        Assert.IsTrue(resultado.Contains("La fecha de nacimiento tiene un formato inválido."));
    }

    [TestMethod]
    public void Curp_SexoIncorrectoError()
    {
        var datos = new DatosEntrada
        {
            Curp = "SABC921215MMCLRR09",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = true
        };

        var resultado = _validator.Validar(datos);

        Assert.IsTrue(resultado.Contains("El sexo no coincide."));
    }

    [TestMethod]
    public void Curp_ConsonanteInternaIncorrectaError()
    {
        var datos = new DatosEntrada
        {
            Curp = "SABC921215HMCXXX09",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = true
        };

        var resultado = _validator.Validar(datos);

        Assert.IsTrue(resultado.Count > 0);
    }

    [TestMethod]
    public void Curp_ExtranjeroConEstadoIncorrectoError()
    {
        var datos = new DatosEntrada
        {
            Curp = "SABC921215HMCLRR09".Replace(" ", ""),
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = false
        };

        var resultado = _validator.Validar(datos);

        Assert.IsTrue(resultado.Contains("Para extranjeros el estado debe ser 'NE'."));
    }

    [TestMethod]
    public void Curp_TodosLosErrores()
    {
        var datos = new DatosEntrada
        {
            Curp = "LETR851118FMXZHJ09",
            Nombres = "Carlos",
            ApellidoPaterno = "Salgado",
            ApellidoMaterno = "Briseño",
            FechaNacimiento = "1992-12-15T06:00:00.000Z",
            Sexo = Sexo.Masculino,
            EsMexicano = false
        };

        var resultado = _validator.Validar(datos);

        Assert.AreEqual(10, resultado.Count);
    }
}
