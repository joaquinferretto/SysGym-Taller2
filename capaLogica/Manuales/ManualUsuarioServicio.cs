using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace exxen2._0.capaLogica.Manuales
{
    // El mismo contenido alimenta la lectura en pantalla y el PDF de cada rol.
    public sealed class TemaManual
    {
        public string Id { get; internal set; }
        public string Grupo { get; internal set; }
        public string Titulo { get; internal set; }
        public string Texto { get; internal set; }
        public override string ToString() { return Titulo; }
    }

    public sealed class ManualUsuarioServicio
    {
        public const string Version = "10/11/2026";
        public string Rol { get; private set; }
        public IList<TemaManual> Temas { get; private set; }

        public ManualUsuarioServicio(string rol)
        {
            if (rol != "Administrador" && rol != "Recepcionista" && rol != "Entrenador")
                throw new InvalidOperationException("No se reconoce el rol para mostrar su manual.");
            Rol = rol;
            using (var recurso = typeof(ManualUsuarioServicio).Assembly.GetManifestResourceStream("SysGym.ManualesPorRol.xml"))
            {
                if (recurso == null) throw new InvalidOperationException("No se encontró el contenido del manual.");
                var grupos = new[] { "Primeros pasos", "Recepción", "Entrenamiento", "Administración", "Ayuda" };
                Temas = XDocument.Load(recurso).Root.Elements("tema")
                    .Where(t => ((string)t.Attribute("roles")).Split(',').Contains(rol))
                    .OrderBy(t => Array.IndexOf(grupos, (string)t.Attribute("grupo")))
                    .Select(t => new TemaManual
                    {
                        Id = (string)t.Attribute("id"), Grupo = (string)t.Attribute("grupo"),
                        Titulo = (string)t.Attribute("titulo"), Texto = Texto(t)
                    }).ToList().AsReadOnly();
            }
        }

        private static string Texto(XElement tema)
        {
            var bloques = new List<string>();
            foreach (var bloque in tema.Elements())
            {
                if (bloque.Name == "pasos")
                    bloques.Add(string.Join(Environment.NewLine + Environment.NewLine,
                        bloque.Elements("item").Select((p, i) => (i + 1) + ". " + p.Value)));
                else if (bloque.Name == "lista")
                    bloques.Add(string.Join(Environment.NewLine, bloque.Elements("item").Select(p => "• " + p.Value)));
                else if (bloque.Name == "nota")
                    bloques.Add((string)bloque.Attribute("titulo") + ": " + bloque.Value);
                else bloques.Add(bloque.Value);
            }
            return string.Join(Environment.NewLine + Environment.NewLine, bloques);
        }

        public string NombreArchivo { get { return "Manual de usuario SysGym - " + Rol + " - 2026.pdf"; } }

        // No requiere consultar SQL: el manual sigue disponible si el servidor deja de responder.
        public void ExportarPdf(string destino)
        {
            if (string.IsNullOrWhiteSpace(destino) || !string.Equals(Path.GetExtension(destino), ".pdf", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Elegí un archivo con extensión PDF.");
            // Estos recursos se generan desde el mismo XML con la plantilla monocroma original.
            // No depende de archivos sueltos ni de Python/Word instalados en la PC del usuario.
            using (var recurso = typeof(ManualUsuarioServicio).Assembly.GetManifestResourceStream("SysGym.Manual." + Rol + ".pdf"))
            using (var memoria = new MemoryStream())
            {
                if (recurso == null) throw new InvalidOperationException("No se encontró el manual PDF del rol.");
                recurso.CopyTo(memoria);
                File.WriteAllBytes(Path.GetFullPath(destino), memoria.ToArray());
            }
        }
    }
}
