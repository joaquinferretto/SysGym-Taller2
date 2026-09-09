using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using exxen2._0.capaDatos.Entidades;
using exxen2._0.capaLogica;

namespace exxen2._0.capaVisual.Compartido
{
    /* Presenta la rutina semanal de un socio y atiende sus acciones mediante eventos de Windows Forms. */
    [DesignerCategory("Form")]
    public partial class RutinaSemanalFormulario : Form
    {
        private readonly RutinaEjercicioLogica ejerciciosRutina = new RutinaEjercicioLogica();
        private readonly int idSocio;
        private readonly string nombreSocio;

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public RutinaSemanalFormulario() : this(0, "Socio de diseno", Color.FromArgb(79, 70, 229))
        {
        }

        /* Inicializa los componentes existentes y las dependencias de la pantalla sin consultar la base de datos. */
        public RutinaSemanalFormulario(int idSocio, string nombreSocio, Color colorPrimario)
        {
            this.idSocio = idSocio;
            this.nombreSocio = string.IsNullOrWhiteSpace(nombreSocio) ? "Socio" : nombreSocio;
            InitializeComponent();
            panelEncabezado.BackColor = colorPrimario;
            btnVolver.ForeColor = colorPrimario;
            lblTitulo.Text = "Rutina semanal de " + this.nombreSocio;
        }

        /* Consulta la rutina vigente del socio y la distribuye en la grilla de la semana. */
        private void Cargar()
        {
            try
            {
                MostrarSemana(ejerciciosRutina.ListarSemanaPorSocio(idSocio));
            }
            catch (Exception ex)
            {
                AyudaFormularioVisual.MostrarError(lblEstado, ex);
            }
        }

        /* Distribuye los ejercicios en columnas por día y filas por posición dentro de ese día. */
        private void MostrarSemana(List<RutinaEjercicio> ejercicios)
        {
            var dias = ValidacionesGimnasio.UltimoDiaRutina - ValidacionesGimnasio.PrimerDiaRutina + 1;
            var porDia = new List<RutinaEjercicio>[dias];
            var sinDia = 0;
            for (var indice = 0; indice < dias; indice++)
                porDia[indice] = new List<RutinaEjercicio>();
            foreach (var ejercicio in ejercicios)
            {
                if (!ejercicio.DiaSemana.HasValue)
                {
                    sinDia++;
                    continue;
                }

                porDia[ejercicio.DiaSemana.Value - ValidacionesGimnasio.PrimerDiaRutina].Add(ejercicio);
            }

            var filas = 0;
            for (var indice = 0; indice < dias; indice++)
                if (porDia[indice].Count > filas)
                    filas = porDia[indice].Count;

            tablaSemana.Rows.Clear();
            for (var fila = 0; fila < filas; fila++)
            {
                tablaSemana.Rows.Add();
                for (var indice = 0; indice < dias; indice++)
                    if (fila < porDia[indice].Count)
                        tablaSemana.Rows[fila].Cells[indice].Value = Describir(porDia[indice][fila]);
            }

            tablaSemana.ClearSelection();
            if (filas == 0)
                lblEstado.Text = "El socio no tiene una rutina semanal cargada.";
            else if (sinDia > 0)
                lblEstado.Text = filas + " ejercicio(s) por dia. Hay " + sinDia + " ejercicio(s) sin dia asignado que no se muestran.";
            else
                lblEstado.Text = ejercicios.Count + " ejercicio(s) repartidos de lunes a viernes.";
        }

        /* Compone el texto de la celda con el ejercicio, su volumen y el peso cuando está cargado. */
        private static string Describir(RutinaEjercicio ejercicio)
        {
            var texto = ejercicio.Ejercicio == null ? "Ejercicio" : ejercicio.Ejercicio.Nombre;
            if (ejercicio.Series.HasValue && ejercicio.Repeticiones.HasValue)
                texto += "  " + ejercicio.Series.Value + "x" + ejercicio.Repeticiones.Value;
            if (ejercicio.Peso.HasValue && ejercicio.Peso.Value > 0)
                texto += "  " + ejercicio.Peso.Value.ToString("0.##") + " kg";
            return texto;
        }

        /* Al cargar la pantalla en ejecución, prepara sus datos iniciales sin realizar consultas desde el diseñador. */
        private void RutinaSemanalFormulario_Load(object origen, EventArgs e)
        {
            if (AyudaFormularioVisual.EnModoDisenio(this) || idSocio == 0)
                return;
            Cargar();
        }

        /* Al hacer clic en btnVolver, cierra la consulta y devuelve el control al formulario anterior. */
        private void btnVolver_Click(object origen, EventArgs e)
        {
            Close();
        }
    }
}
