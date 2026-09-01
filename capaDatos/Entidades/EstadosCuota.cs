namespace exxen2._0.capaDatos.Entidades
{
    public static class EstadosCuota
    {
        public const string Pendiente = "Pendiente";
        public const string Pagada = "Pagada";
        public const string Anulada = "Anulada";

        public static bool EsValido(string estado)
        {
            return estado == Pendiente || estado == Pagada || estado == Anulada;
        }
    }
}
