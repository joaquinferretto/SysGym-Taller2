using System;
using System.Collections.Generic;
using System.Linq;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaDatos.Repositorios;

namespace exxen2._0.capaLogica.Analisis
{
    public sealed class DatoMensual
    {
        public DateTime Mes { get; set; }
        public decimal Valor { get; set; }
    }

    public sealed class DatoCategoria
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
    }

    public sealed class ResultadoAnalisis
    {
        public List<DatoMensual> IngresosPorMes { get; set; }
        public List<DatoCategoria> EstadoSocios { get; set; }
        public List<DatoCategoria> EstadoDeuda { get; set; }
        public List<DatoCategoria> Planes { get; set; }
        public List<DatoCategoria> Entrenadores { get; set; }
        public List<DatoCategoria> MetodosPago { get; set; }
        public List<DatoMensual> AltasPorMes { get; set; }
        public int TotalAltas { get; set; }
        public int SociosSinFechaAlta { get; set; }
        public decimal? VariacionAltasUltimoMes { get; set; }
        public List<DatoImporte> IngresosPorPlan { get; set; }
        public List<DatoCategoria> Rutinas { get; set; }
        public int SinRutina { get; set; }
        public List<DatoCategoria> EjerciciosTop { get; set; }
        public List<EjercicioSinUso> EjerciciosSinUso { get; set; }
        public decimal TotalIngresos { get; set; }
        public decimal IngresosPeriodoAnterior { get; set; }
        public decimal? VariacionIngresosPorcentual { get; set; }
        public decimal? VariacionUltimoMesPorcentual { get; set; }
        public int CantidadPagos { get; set; }
        public int SociosActivos { get; set; }
        public int SociosConDeuda { get; set; }
        public int TotalSocios { get; set; }
    }

    /// <summary>Consultas de análisis; las fechas delimitan movimientos, y las distribuciones muestran el estado actual.</summary>
    public sealed class AnalisisLogica
    {
        public ResultadoAnalisis Obtener(DateTime desde, DateTime hasta)
        {
            desde = desde.Date;
            hasta = hasta.Date;
            if (desde > hasta)
                throw new ArgumentException("Desde debe ser anterior o igual a Hasta.");
            if (hasta == DateTime.MaxValue.Date)
                throw new ArgumentException("La fecha Hasta debe ser anterior al 31/12/9999.");

            var finExclusivo = hasta.AddDays(1);
            var duracion = (finExclusivo - desde).Days;
            if (duracion > (desde - DateTime.MinValue).Days)
                throw new ArgumentException("El período es demasiado amplio para compararlo con el anterior.");
            var inicioAnterior = desde.AddDays(-duracion);
            var resultado = new ResultadoAnalisis();
            var estado = new EstadoSociosLogica().ObtenerResumenSoloLectura();
            var idsMembresiasActivas = estado.Socios.Where(s => s.Activo && s.IdMembresia > 0)
                .Select(s => s.IdMembresia).ToList();
            var idsSociosActivos = estado.Socios.Where(s => s.Activo).Select(s => s.IdSocio).ToList();

            using (var datos = new UnidadDeTrabajoGimnasio())
            {
                var pagos = datos.Pagos.ConsultarSoloLectura().Where(p =>
                    p.Estado == EstadosTransaccionPago.Aprobado && p.Fecha >= desde && p.Fecha < finExclusivo);
                var meses = pagos.GroupBy(p => new { p.Fecha.Year, p.Fecha.Month })
                    .Select(g => new { g.Key.Year, g.Key.Month, Total = g.Sum(p => p.Importe) })
                    .ToList().ToDictionary(x => new DateTime(x.Year, x.Month, 1), x => x.Total);
                resultado.IngresosPorMes = new List<DatoMensual>();
                for (var mes = new DateTime(desde.Year, desde.Month, 1); mes <= hasta; mes = mes.AddMonths(1))
                    resultado.IngresosPorMes.Add(new DatoMensual { Mes = mes, Valor = meses.ContainsKey(mes) ? meses[mes] : 0m });
                resultado.TotalIngresos = resultado.IngresosPorMes.Sum(m => m.Valor);
                if (resultado.IngresosPorMes.Count >= 2)
                {
                    var cantidadMeses = resultado.IngresosPorMes.Count;
                    resultado.VariacionUltimoMesPorcentual = CalcularVariacion(
                        resultado.IngresosPorMes[cantidadMeses - 1].Valor,
                        resultado.IngresosPorMes[cantidadMeses - 2].Valor);
                }
                resultado.CantidadPagos = pagos.Count();
                var altas = datos.Socios.ConsultarSoloLectura()
                    .Where(s => s.FechaAlta.HasValue && s.FechaAlta >= desde && s.FechaAlta < finExclusivo)
                    .GroupBy(s => new { s.FechaAlta.Value.Year, s.FechaAlta.Value.Month })
                    .Select(g => new { g.Key.Year, g.Key.Month, Cantidad = g.Count() }).ToList()
                    .ToDictionary(x => new DateTime(x.Year, x.Month, 1), x => (decimal)x.Cantidad);
                resultado.AltasPorMes = CompletarMeses(desde, hasta, altas);
                resultado.TotalAltas = (int)resultado.AltasPorMes.Sum(x => x.Valor);
                resultado.SociosSinFechaAlta = datos.Socios.ConsultarSoloLectura().Count(s => !s.FechaAlta.HasValue);
                if (resultado.AltasPorMes.Count >= 2)
                    resultado.VariacionAltasUltimoMes = CalcularVariacion(
                        resultado.AltasPorMes.Last().Valor, resultado.AltasPorMes[resultado.AltasPorMes.Count - 2].Valor);

                // El índice único de CuotaMembresia.IdRegistroPago garantiza una sola cuota por pago.
                resultado.IngresosPorPlan = datos.CuotasMembresia.ConsultarSoloLectura()
                    .Where(c => c.IdRegistroPago.HasValue && c.Pago.Estado == EstadosTransaccionPago.Aprobado &&
                        c.Pago.Fecha >= desde && c.Pago.Fecha < finExclusivo)
                    .GroupBy(c => new { c.Membresia.IdPlan, c.Membresia.Plan.Nombre })
                    .Select(g => new DatoImporte { Nombre = g.Key.Nombre, Importe = g.Sum(c => c.Pago.Importe) })
                    .OrderByDescending(p => p.Importe).ThenBy(p => p.Nombre).ToList();
                var sinCuota = resultado.TotalIngresos - resultado.IngresosPorPlan.Sum(p => p.Importe);
                if (sinCuota != 0m)
                {
                    resultado.IngresosPorPlan.Add(new DatoImporte { Nombre = "Sin cuota asociada", Importe = sinCuota });
                    resultado.IngresosPorPlan = resultado.IngresosPorPlan.OrderByDescending(p => p.Importe).ToList();
                }

                resultado.Rutinas = datos.Rutinas.ConsultarSoloLectura().Where(r => r.Estado)
                    .Select(r => new { r.Nombre, Cantidad = r.Membresias.Where(m => idsMembresiasActivas.Contains(m.IdMembresia))
                        .Select(m => m.IdSocio).Distinct().Count() })
                    .OrderByDescending(r => r.Cantidad).ThenBy(r => r.Nombre).ToList()
                    .Select(r => new DatoCategoria { Nombre = r.Nombre, Cantidad = r.Cantidad }).ToList();
                resultado.SinRutina = datos.Socios.ConsultarSoloLectura().Count(s => idsSociosActivos.Contains(s.IdSocio) &&
                    !s.Membresias.Any(m => idsMembresiasActivas.Contains(m.IdMembresia) && m.IdRutina.HasValue && m.Rutina.Estado));
                resultado.EjerciciosTop = datos.Ejercicios.ConsultarSoloLectura().Where(e => e.Estado)
                    .Select(e => new { e.Nombre, e.IdEjercicio,
                        Cantidad = e.Rutinas.Where(r => r.Estado && r.Rutina.Estado).Select(r => r.IdRutina).Distinct().Count() })
                    .Where(e => e.Cantidad > 0).OrderByDescending(e => e.Cantidad).ThenBy(e => e.Nombre).ThenBy(e => e.IdEjercicio)
                    .Take(10).ToList().Select(e => new DatoCategoria { Nombre = e.Nombre, Cantidad = e.Cantidad }).ToList();
                resultado.EjerciciosSinUso = datos.Ejercicios.ConsultarSoloLectura()
                    .Where(e => e.Estado && !e.Rutinas.Any(r => r.Estado && r.Rutina.Estado))
                    .OrderBy(e => e.Nombre).Select(e => new EjercicioSinUso { Nombre = e.Nombre, Descripcion = e.Descripcion }).ToList();
                resultado.IngresosPeriodoAnterior = datos.Pagos.ConsultarSoloLectura()
                    .Where(p => p.Estado == EstadosTransaccionPago.Aprobado &&
                        p.Fecha >= inicioAnterior && p.Fecha < desde)
                    .Select(p => (decimal?)p.Importe).Sum() ?? 0m;
                resultado.VariacionIngresosPorcentual = CalcularVariacion(
                    resultado.TotalIngresos, resultado.IngresosPeriodoAnterior);

                var metodos = pagos.GroupBy(p => new
                    {
                        p.MetodoPago.IdPagoEfectivo,
                        p.MetodoPago.IdNroPagoMP
                    })
                    .Select(g => new { g.Key.IdPagoEfectivo, g.Key.IdNroPagoMP, Cantidad = g.Count() })
                    .ToList();
                resultado.MetodosPago = metodos.Select(m => new
                    {
                        Metodo = new MetodoPago { IdPagoEfectivo = m.IdPagoEfectivo, IdNroPagoMP = m.IdNroPagoMP },
                        m.Cantidad
                    })
                    .GroupBy(m => PagoLogica.EsMetodoPagoEstructuralmenteValido(m.Metodo)
                        ? PagoLogica.ObtenerNombreTipoMetodoPago(m.Metodo) : "Sin clasificación")
                    .Select(g => new DatoCategoria { Nombre = g.Key, Cantidad = g.Sum(m => m.Cantidad) })
                    .OrderByDescending(m => m.Cantidad).ThenBy(m => m.Nombre).ToList();

                resultado.Planes = datos.Membresias.ConsultarSoloLectura()
                    .Where(m => idsMembresiasActivas.Contains(m.IdMembresia))
                    .GroupBy(m => new { m.IdPlan, m.Plan.Nombre })
                    .Select(g => new { g.Key.Nombre, Cantidad = g.Select(m => m.IdSocio).Distinct().Count() })
                    .OrderByDescending(x => x.Cantidad).ThenBy(x => x.Nombre)
                    .ToList().Select(x => new DatoCategoria { Nombre = x.Nombre, Cantidad = x.Cantidad }).ToList();

                resultado.Entrenadores = datos.MembresiasEntrenadores.ConsultarSoloLectura()
                    .Where(a => a.Estado && idsMembresiasActivas.Contains(a.IdMembresia) &&
                        a.Entrenador.Estado && a.Entrenador.Rol.Descripcion == "Entrenador")
                    .GroupBy(a => new { a.IdEntrenador, a.Entrenador.Apellido, a.Entrenador.Nombre })
                    .Select(g => new { g.Key.Apellido, g.Key.Nombre,
                        Cantidad = g.Select(a => a.Membresia.IdSocio).Distinct().Count() })
                    .ToList().Select(x => new DatoCategoria
                    {
                        Nombre = x.Apellido + ", " + x.Nombre,
                        Cantidad = x.Cantidad
                    }).ToList();
                var sinAsignar = datos.Socios.ConsultarSoloLectura()
                    .Where(s => idsSociosActivos.Contains(s.IdSocio) && !s.Membresias.Any(m =>
                        idsMembresiasActivas.Contains(m.IdMembresia) && m.Entrenadores.Any(a =>
                            a.Estado && a.Entrenador.Estado && a.Entrenador.Rol.Descripcion == "Entrenador")))
                    .Count();
                if (sinAsignar > 0)
                    resultado.Entrenadores.Add(new DatoCategoria { Nombre = "Sin asignar", Cantidad = sinAsignar });
                resultado.Entrenadores = resultado.Entrenadores.OrderByDescending(x => x.Cantidad)
                    .ThenBy(x => x.Nombre).ToList();
            }

            // EstadoSociosLogica conserva la única clasificación de actividad y el umbral de deuda del sistema.
            resultado.TotalSocios = estado.Socios.Count;
            resultado.SociosActivos = estado.SociosActivos;
            resultado.EstadoSocios = new List<DatoCategoria>
            {
                new DatoCategoria { Nombre = "Activos", Cantidad = estado.SociosActivos },
                new DatoCategoria { Nombre = "Inactivos", Cantidad = estado.SociosInactivos }
            };
            var limite = estado.LimiteAlcanzado;
            var conDeuda = estado.ConDeuda;
            resultado.SociosConDeuda = conDeuda + limite;
            resultado.EstadoDeuda = new List<DatoCategoria>
            {
                new DatoCategoria { Nombre = "Al día", Cantidad = resultado.TotalSocios - conDeuda - limite },
                new DatoCategoria { Nombre = "Con deuda", Cantidad = conDeuda },
                new DatoCategoria { Nombre = "Límite alcanzado", Cantidad = limite }
            };
            return resultado;
        }

        internal static decimal? CalcularVariacion(decimal actual, decimal anterior)
        {
            return anterior == 0m ? (decimal?)null : (actual - anterior) * 100m / anterior;
        }

        internal static List<DatoMensual> CompletarMeses(DateTime desde, DateTime hasta, IDictionary<DateTime, decimal> valores)
        {
            var resultado = new List<DatoMensual>();
            for (var mes = new DateTime(desde.Year, desde.Month, 1); mes <= hasta; mes = mes.AddMonths(1))
                resultado.Add(new DatoMensual { Mes = mes, Valor = valores.ContainsKey(mes) ? valores[mes] : 0m });
            return resultado;
        }
    }
}
