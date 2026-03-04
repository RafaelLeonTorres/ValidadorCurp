namespace CurpValidator.Domain.Constants
{
    public static class CurpConstants
    {
        public const string Vocales = "AEIOU";
        public const string Consonantes = "BCDFGHJKLMNPQRSTVWXYZ";
    }

    public static class CurpPosiciones 
    {
        public const int PosLetraPaterno = 0;
        public const int PosVocalInterna = 1;
        public const int PosLetraMaterno = 2;
        public const int PosLetraNombre = 3;
        public const int PosFechaInicio = 4;
        public const int PosSexo = 10;
        public const int PosEstadoInicio = 11;
        public const int PosConsPaterno = 13;
        public const int PosConsMaterno = 14;
        public const int PosConsNombre = 15;
        public const int LongitudCurp = 18;
    }
}
