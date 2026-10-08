using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace exxen2._0.capaDatos.Entidades
{
    public sealed class ConfiguracionSistema
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdConfiguracion { get; set; }
        [Range(1, 120)]
        public int MaxCuotasVencidasPermitidas { get; set; }
        [Range(0, 120)]
        public int MaxMesesAnticipacionCuotas { get; set; }
        [Range(0, 365)]
        public int DiasAvisoVencimiento { get; set; }
    }
}
