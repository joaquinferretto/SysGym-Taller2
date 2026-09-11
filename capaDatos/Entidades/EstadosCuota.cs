namespace exxen2._0.capaDatos.Entidades
{
    /* Centraliza los estados válidos de las cuotas de membresía. */
    public static class EstadosCuota
    {
        public const string Pendiente = "Pendiente";
        public const string Pagada = "Pagada";
        public const string Anulada = "Anulada";
        /* Comprueba que el estado pertenezca al conjunto de valores admitidos por el modelo. */
        public static bool EsValido(string estado)
        {
            return estado == Pendiente || estado == Pagada || estado == Anulada;
        }
    }
}
