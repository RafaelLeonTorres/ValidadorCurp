using CurpValidator.Domain.Entities;

namespace CurpValidator.Application.Interfaces
{
    public interface ICurpValidatorService
    {
        List<string> Validar(DatosEntrada datos);
    }
}
