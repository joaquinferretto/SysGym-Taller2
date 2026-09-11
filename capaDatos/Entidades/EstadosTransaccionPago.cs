namespace exxen2._0.capaDatos.Entidades
{
    /* Centraliza los estados válidos de los pagos registrados. */
    public static class EstadosTransaccionPago
    {
        public const string Pendiente = "Pendiente";
        public const string Aprobado = "Aprobado";
        public const string Rechazado = "Rechazado";
        public const string Anulado = "Anulado";
        public const string Reembolsado = "Reembolsado";
        /* Comprueba que el estado pertenezca al conjunto de valores admitidos por el modelo. */
        public static bool EsValido(string estado)
        {
            return estado == Pendiente || estado == Aprobado || estado == Rechazado || estado == Anulado || estado == Reembolsado;
        }
    }
}
