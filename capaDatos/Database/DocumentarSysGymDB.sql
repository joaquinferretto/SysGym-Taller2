/* SysGym — 30 de octubre de 2026.
   Documenta en SQL Server todas las tablas, columnas e índices mediante MS_Description.
   No cambia datos ni estructura. Es idempotente y se puede volver a ejecutar.
   SysGym no define procedimientos almacenados propios; sus reglas de integridad están en PK/FK/UNIQUE/CHECK
   y sus reglas de negocio en capaLogica. */
USE SysGymDB;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;

DECLARE @Documentacion TABLE
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Tabla SYSNAME NOT NULL,
    Columna SYSNAME NULL,
    Descripcion NVARCHAR(4000) NOT NULL
);

INSERT INTO @Documentacion (Tabla, Columna, Descripcion)
VALUES
('Rol',NULL,N'Catálogo de roles que determina las funciones habilitadas para cada cuenta del sistema.'),
('Rol','IdRol',N'Identificador interno autogenerado del rol; clave primaria.'),
('Rol','Descripcion',N'Nombre único del rol asignable: Administrador, Recepcionista o Entrenador.'),
('Rol','Estado',N'Indica si el rol puede asignarse: 1 activo, 0 inactivo.'),

('UsuarioSistema',NULL,N'Cuentas del personal que inicia sesión y ejecuta operaciones en SysGym.'),
('UsuarioSistema','IdUsuarioSistema',N'Identificador interno autogenerado de la cuenta; clave primaria.'),
('UsuarioSistema','Nombre',N'Nombre de pila de la persona usuaria del sistema.'),
('UsuarioSistema','Apellido',N'Apellido de la persona usuaria del sistema.'),
('UsuarioSistema','DNI',N'Documento de identidad único; no se puede repetir.'),
('UsuarioSistema','Telefono',N'Teléfono de contacto opcional del empleado.'),
('UsuarioSistema','FechaNacimiento',N'Fecha de nacimiento opcional del empleado.'),
('UsuarioSistema','Salario',N'Importe salarial registrado; SysGym no liquida ni paga salarios.'),
('UsuarioSistema','Username',N'Nombre de usuario único usado para iniciar sesión.'),
('UsuarioSistema','Password',N'Hash Argon2id de la contraseña; no debe contener la contraseña legible.'),
('UsuarioSistema','FotoRuta',N'Ruta relativa de la fotografía guardada en disco; el archivo no se almacena en SQL Server.'),
('UsuarioSistema','Sexo',N'Código opcional de un carácter: M o F.'),
('UsuarioSistema','Estado',N'Estado de la cuenta: 1 activa, 0 dada de baja.'),
('UsuarioSistema','IdRol',N'Clave foránea al rol que define los permisos de la cuenta.'),
('UsuarioSistema','IdUsuarioCreador',N'Clave foránea opcional a quien dio de alta esta cuenta; NULL para cuentas antiguas o iniciales.'),

('AuditoriaOperacion',NULL,N'Historial de operaciones auditadas, con responsable, fecha, entidad afectada y detalle.'),
('AuditoriaOperacion','IdAuditoria',N'Identificador interno autogenerado de la operación auditada; clave primaria.'),
('AuditoriaOperacion','FechaHora',N'Fecha y hora en que se registró la operación.'),
('AuditoriaOperacion','IdUsuario',N'Clave foránea al usuario responsable de la operación.'),
('AuditoriaOperacion','UsuarioNombre',N'Copia del nombre del responsable al momento del evento para preservar el contexto histórico.'),
('AuditoriaOperacion','Rol',N'Copia del rol del responsable al momento del evento.'),
('AuditoriaOperacion','Operacion',N'Código de la acción realizada, por ejemplo REGISTRAR_PAGO o BAJA_SOCIO.'),
('AuditoriaOperacion','Entidad',N'Tipo de registro afectado, por ejemplo Pago, Socio o Membresia.'),
('AuditoriaOperacion','IdEntidad',N'Identificador del registro afectado dentro de Entidad; no es FK porque el destino es polimórfico.'),
('AuditoriaOperacion','Detalle',N'Resumen contextual de la operación para su consulta en Auditoría.'),

('Socio',NULL,N'Personas inscritas en el gimnasio, con sus datos personales, estado y medidas opcionales.'),
('Socio','IdSocio',N'Identificador interno autogenerado del socio; clave primaria.'),
('Socio','DNI',N'Documento de identidad único del socio; no se puede repetir.'),
('Socio','Nombre',N'Nombre de pila del socio.'),
('Socio','Apellido',N'Apellido del socio.'),
('Socio','FechaNacimiento',N'Fecha de nacimiento opcional; se usa para validar la edad mínima de alta.'),
('Socio','FechaAlta',N'Fecha de alta; NULL cuando no existe una fecha histórica confiable.'),
('Socio','Peso',N'Peso corporal opcional en kilogramos; si se informa debe ser positivo.'),
('Socio','Altura',N'Altura opcional en metros; si se informa debe ser positiva.'),
('Socio','FotoRuta',N'Ruta relativa de la fotografía guardada en disco; el archivo no se almacena en SQL Server.'),
('Socio','Sexo',N'Código opcional de un carácter: M o F.'),
('Socio','Estado',N'Estado del socio: 1 activo, 0 dado de baja; la baja conserva el historial.'),

('Ejercicio',NULL,N'Catálogo de ejercicios disponibles para construir rutinas.'),
('Ejercicio','IdEjercicio',N'Identificador interno autogenerado del ejercicio; clave primaria.'),
('Ejercicio','Nombre',N'Nombre mostrado en el catálogo y en las rutinas.'),
('Ejercicio','Descripcion',N'Descripción opcional del movimiento o los grupos musculares trabajados.'),
('Ejercicio','Estado',N'Estado del ejercicio: 1 disponible, 0 dado de baja lógicamente.'),

('Rutina',NULL,N'Rutinas de entrenamiento creadas por un entrenador; los modelos tienen objetivo y nivel.'),
('Rutina','IdRutina',N'Identificador interno autogenerado de la rutina; clave primaria.'),
('Rutina','Nombre',N'Nombre identificador de la rutina.'),
('Rutina','Descripcion',N'Descripción opcional de su finalidad o contenido.'),
('Rutina','FechaCreacion',N'Fecha de creación de la rutina.'),
('Rutina','FechaInicio',N'Inicio opcional del período previsto para la rutina.'),
('Rutina','FechaFin',N'Fin opcional del período previsto; no puede anteceder a FechaInicio si ambas existen.'),
('Rutina','Estado',N'Estado de la rutina: 1 disponible, 0 dada de baja lógicamente.'),
('Rutina','Objetivo',N'Objetivo del modelo: Fuerza, Hipertrofia o Acondicionamiento; NULL para rutina libre.'),
('Rutina','Nivel',N'Nivel del modelo: Principiante, Intermedio o Avanzado; debe acompañar a Objetivo.'),
('Rutina','IdEntrenador',N'Clave foránea al entrenador que creó y administra la rutina.'),

('Plan',NULL,N'Catálogo de planes ofrecidos por el gimnasio y su precio vigente para cuotas futuras.'),
('Plan','IdPlan',N'Identificador interno autogenerado del plan; clave primaria.'),
('Plan','Nombre',N'Nombre mostrado al crear o cambiar una membresía.'),
('Plan','Descripcion',N'Descripción opcional de los servicios incluidos.'),
('Plan','Precio',N'Precio vigente; las cuotas ya generadas conservan su importe histórico.'),
('Plan','Estado',N'Estado del plan: 1 habilitado para nuevas operaciones, 0 dado de baja.'),

('EjercicioImagen',NULL,N'Imágenes asociadas a ejercicios, guardadas como rutas relativas y posiciones de presentación.'),
('EjercicioImagen','IdEjercicioImagen',N'Identificador interno autogenerado de la imagen asociada; clave primaria.'),
('EjercicioImagen','IdEjercicio',N'Clave foránea al ejercicio al que pertenece la imagen.'),
('EjercicioImagen','RutaRelativa',N'Ruta única del archivo dentro del almacenamiento de SysGym; la imagen queda fuera de SQL Server.'),
('EjercicioImagen','Orden',N'Posición positiva en que se muestra la imagen del ejercicio.'),

('Membresia',NULL,N'Vínculo histórico entre un socio, un plan, quien la registró y una rutina opcional.'),
('Membresia','IdMembresia',N'Identificador interno autogenerado de la membresía; clave primaria.'),
('Membresia','FechaInicio',N'Fecha desde la que comienza la membresía.'),
('Membresia','FechaVencimiento',N'Fecha de vencimiento registrada; debe ser igual o posterior al inicio.'),
('Membresia','Estado',N'Estado de la membresía: 1 activa, 0 inactiva; se conserva el historial.'),
('Membresia','IdPlan',N'Clave foránea al plan asociado.'),
('Membresia','IdSocio',N'Clave foránea al socio titular.'),
('Membresia','IdUsuarioSistema',N'Clave foránea al Administrador o Recepcionista que la registró.'),
('Membresia','IdRutina',N'Clave foránea opcional a la rutina asignada; NULL mientras no se elija una.'),

('MembresiaEntrenador',NULL,N'Asignaciones históricas de entrenadores a membresías; la baja lógica conserva los cambios anteriores.'),
('MembresiaEntrenador','IdMembresiaEntrenador',N'Identificador interno autogenerado de la asignación; clave primaria.'),
('MembresiaEntrenador','Estado',N'Estado de la asignación: 1 vigente, 0 finalizada o dada de baja.'),
('MembresiaEntrenador','IdMembresia',N'Clave foránea a la membresía cuyo socio recibe seguimiento.'),
('MembresiaEntrenador','IdEntrenador',N'Clave foránea al usuario con rol Entrenador asignado.'),

('PagoEfectivo',NULL,N'Detalle identificador de un método de pago en efectivo.'),
('PagoEfectivo','IdPagoEfectivo',N'Identificador interno autogenerado del detalle de efectivo; clave primaria.'),
('PagoEfectivo','Estado',N'Indica si el método de efectivo está habilitado: 1 sí, 0 no.'),

('MercadoPago',NULL,N'Referencias opcionales de Mercado Pago; SysGym no inicia cobros contra su API.'),
('MercadoPago','IdNroPagoMP',N'Identificador interno autogenerado del registro; clave primaria.'),
('MercadoPago','MercadoPagoPaymentId',N'Identificador opcional del pago informado por Mercado Pago.'),
('MercadoPago','MercadoPagoPreferenceId',N'Identificador opcional de la preferencia informada por Mercado Pago.'),
('MercadoPago','ExternalReference',N'Referencia externa opcional para relacionar el pago con otro sistema o contexto.'),
('MercadoPago','StatusDetail',N'Detalle opcional del estado recibido desde Mercado Pago.'),
('MercadoPago','FechaAprobacion',N'Fecha y hora opcional de aprobación informada para el pago.'),

('MetodoPago',NULL,N'Catálogo de métodos de pago y su detalle específico, efectivo o Mercado Pago.'),
('MetodoPago','IdMetodoPago',N'Identificador interno autogenerado del método; clave primaria.'),
('MetodoPago','Estado',N'Estado del método: 1 disponible para registrar pagos, 0 deshabilitado.'),
('MetodoPago','Observaciones',N'Etiqueta que distingue la opción de pago mostrada al operador.'),
('MetodoPago','IdNroPagoMP',N'Clave foránea opcional al detalle Mercado Pago; no puede coexistir con IdPagoEfectivo.'),
('MetodoPago','IdPagoEfectivo',N'Clave foránea opcional al detalle efectivo; no puede coexistir con IdNroPagoMP.'),

('Pago',NULL,N'Intentos de pago de cuotas con estado financiero e historial de anulaciones o reembolsos.'),
('Pago','IdRegistroPago',N'Identificador interno autogenerado del pago; clave primaria.'),
('Pago','Fecha',N'Fecha y hora en que el operador registró el pago en SysGym.'),
('Pago','Descripcion',N'Detalle opcional del pago o su contexto.'),
('Pago','Importe',N'Importe positivo asociado a este intento de pago.'),
('Pago','Estado',N'Estado: Pendiente, Aprobado, Rechazado, Anulado o Reembolsado.'),
('Pago','IdMetodoPago',N'Clave foránea al método utilizado.'),
('Pago','IdUsuarioRegistro',N'Clave foránea opcional al usuario que registró el pago; NULL en datos históricos sin responsable.'),
('Pago','IdCuotaMembresia',N'Clave foránea opcional a la cuota correspondiente; se conserva al anular o reembolsar.'),

('CuotaMembresia',NULL,N'Cargo de un período de membresía con importe histórico y estado de pago.'),
('CuotaMembresia','IdCuotaMembresia',N'Identificador interno autogenerado de la cuota; clave primaria.'),
('CuotaMembresia','FechaDesde',N'Primer día del período cobrado.'),
('CuotaMembresia','FechaHasta',N'Último día incluido en el período cobrado.'),
('CuotaMembresia','Importe',N'Precio guardado al generar la cuota; no cambia con el precio futuro del plan.'),
('CuotaMembresia','EstadoPago',N'Estado de la cuota: Pendiente, Pagada o Anulada.'),
('CuotaMembresia','IdRegistroPago',N'Clave foránea opcional al pago vigente que determina el estado de la cuota.'),
('CuotaMembresia','IdMembresia',N'Clave foránea a la membresía propietaria del período.'),

('RutinaEjercicio',NULL,N'Parámetros de un ejercicio dentro de una rutina, ordenados por día y posición.'),
('RutinaEjercicio','IdRutinaEjercicio',N'Identificador interno autogenerado del detalle de rutina; clave primaria.'),
('RutinaEjercicio','Series',N'Cantidad opcional de series; si se informa debe ser mayor que cero.'),
('RutinaEjercicio','Repeticiones',N'Repeticiones opcionales por serie; si se informan deben ser mayores que cero.'),
('RutinaEjercicio','Peso',N'Peso opcional de trabajo en kilogramos; no puede ser negativo.'),
('RutinaEjercicio','Descanso',N'Segundos de descanso; debe ser cero o mayor.'),
('RutinaEjercicio','Orden',N'Posición positiva del ejercicio dentro de la rutina.'),
('RutinaEjercicio','DiaSemana',N'Día opcional de entrenamiento: 1 a 5 corresponde a lunes a viernes.'),
('RutinaEjercicio','Estado',N'Estado del detalle: 1 incluido, 0 quitado lógicamente.'),
('RutinaEjercicio','IdRutina',N'Clave foránea a la rutina que contiene el ejercicio.'),
('RutinaEjercicio','IdEjercicio',N'Clave foránea al ejercicio del catálogo.'),

('ConfiguracionSistema',NULL,N'Parámetros globales de deuda, anticipación y avisos. Solo existe la fila IdConfiguracion = 1.'),
('ConfiguracionSistema','IdConfiguracion',N'Identificador fijo de la configuración global; PK y CHECK restringen el valor a 1.'),
('ConfiguracionSistema','MaxCuotasVencidasPermitidas',N'Cantidad límite de cuotas pendientes vencidas; rango 1 a 120.'),
('ConfiguracionSistema','MaxMesesAnticipacionCuotas',N'Meses futuros permitidos al generar cuotas, además del período actual; rango 0 a 120.'),
('ConfiguracionSistema','DiasAvisoVencimiento',N'Días previos al vencimiento incluidos en los avisos; rango 0 a 365.');

DECLARE @IndicesAdicionales TABLE
(
    Tabla SYSNAME NOT NULL,
    Indice SYSNAME NOT NULL,
    Descripcion NVARCHAR(4000) NOT NULL,
    PRIMARY KEY (Tabla, Indice)
);
INSERT INTO @IndicesAdicionales (Tabla, Indice, Descripcion)
VALUES
('AuditoriaOperacion','IX_AuditoriaOperacion_FechaHora',N'Acelera consultas del historial por fecha descendente y resuelve empates por IdAuditoria descendente.'),
('Membresia','IX_Membresia_IdSocio',N'Acelera la búsqueda de membresías pertenecientes a un socio.'),
('CuotaMembresia','UX_CuotaMembresia_IdRegistroPago',N'Índice único filtrado: impide asociar el mismo pago vigente a más de una cuota; solo incluye IdRegistroPago no nulos.'),
('Pago','IX_Pago_IdCuotaMembresia',N'Acelera la consulta del historial de intentos de pago por cuota; solo incluye IdCuotaMembresia no nulos.');

DECLARE
    @Tabla SYSNAME,
    @Columna SYSNAME,
    @Descripcion NVARCHAR(4000),
    @ObjetoId INT,
    @MinorId INT,
    @Indice SYSNAME,
    @IndiceId INT,
    @EsPK BIT,
    @EsUniqueConstraint BIT,
    @Columnas NVARCHAR(1000),
    @Filtro NVARCHAR(1000),
    @Existe BIT;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE cursorDocumentacion CURSOR LOCAL FAST_FORWARD FOR
        SELECT Tabla, Columna, Descripcion FROM @Documentacion ORDER BY Id;
    OPEN cursorDocumentacion;
    FETCH NEXT FROM cursorDocumentacion INTO @Tabla, @Columna, @Descripcion;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @ObjetoId = OBJECT_ID(N'dbo.' + QUOTENAME(@Tabla), N'U');
        IF @ObjetoId IS NULL
            RAISERROR('No existe la tabla dbo.%s requerida por la documentación.', 16, 1, @Tabla);

        IF @Columna IS NULL
        BEGIN
            SET @MinorId = 0;
            SET @Existe = CASE WHEN EXISTS
                (SELECT 1 FROM sys.extended_properties WHERE class=1 AND major_id=@ObjetoId AND minor_id=0 AND name=N'MS_Description')
                THEN 1 ELSE 0 END;
            IF @Existe = 1
                EXEC sys.sp_updateextendedproperty @name=N'MS_Description', @value=@Descripcion,
                    @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@Tabla;
            ELSE
                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@Descripcion,
                    @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@Tabla;
        END
        ELSE
        BEGIN
            SELECT @MinorId=column_id FROM sys.columns WHERE object_id=@ObjetoId AND name=@Columna;
            IF @MinorId IS NULL
                RAISERROR('No existe dbo.%s.%s requerida por la documentación.', 16, 1, @Tabla, @Columna);
            SET @Existe = CASE WHEN EXISTS
                (SELECT 1 FROM sys.extended_properties WHERE class=1 AND major_id=@ObjetoId AND minor_id=@MinorId AND name=N'MS_Description')
                THEN 1 ELSE 0 END;
            IF @Existe = 1
                EXEC sys.sp_updateextendedproperty @name=N'MS_Description', @value=@Descripcion,
                    @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@Tabla,
                    @level2type=N'COLUMN', @level2name=@Columna;
            ELSE
                EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@Descripcion,
                    @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@Tabla,
                    @level2type=N'COLUMN', @level2name=@Columna;
        END;
        FETCH NEXT FROM cursorDocumentacion INTO @Tabla, @Columna, @Descripcion;
    END;
    CLOSE cursorDocumentacion;
    DEALLOCATE cursorDocumentacion;

    DECLARE cursorIndices CURSOR LOCAL FAST_FORWARD FOR
        SELECT t.name, i.name, i.index_id, i.is_primary_key, i.is_unique_constraint,
               i.filter_definition,
               STUFF((
                   SELECT N', ' + QUOTENAME(c.name)
                   FROM sys.index_columns AS ic
                   INNER JOIN sys.columns AS c ON c.object_id=ic.object_id AND c.column_id=ic.column_id
                   WHERE ic.object_id=i.object_id AND ic.index_id=i.index_id AND ic.is_included_column=0
                   ORDER BY ic.key_ordinal
                   FOR XML PATH(''), TYPE
               ).value('.', 'nvarchar(1000)'), 1, 2, N'')
        FROM sys.indexes AS i
        INNER JOIN sys.tables AS t ON t.object_id=i.object_id
        WHERE t.schema_id=SCHEMA_ID(N'dbo') AND i.index_id>0 AND i.is_hypothetical=0;
    OPEN cursorIndices;
    FETCH NEXT FROM cursorIndices INTO @Tabla, @Indice, @IndiceId, @EsPK, @EsUniqueConstraint, @Filtro, @Columnas;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        SET @ObjetoId=OBJECT_ID(N'dbo.' + QUOTENAME(@Tabla), N'U');
        SET @MinorId=@IndiceId;
        SET @Descripcion=NULL;
        IF @EsPK=1
            SET @Descripcion=N'Índice de clave primaria que identifica cada fila de dbo.' + QUOTENAME(@Tabla) + N' mediante ' + ISNULL(@Columnas,N'su clave') + N'.';
        ELSE IF @EsUniqueConstraint=1
            SET @Descripcion=N'Índice único que impide repetir la combinación ' + ISNULL(@Columnas,N'de clave') + N' en dbo.' + QUOTENAME(@Tabla) + N'.';
        ELSE
            SELECT @Descripcion=Descripcion FROM @IndicesAdicionales WHERE Tabla=@Tabla AND Indice=@Indice;
        IF @Descripcion IS NULL
            SET @Descripcion=N'Índice que permite localizar filas de dbo.' + QUOTENAME(@Tabla) + N' por ' + ISNULL(@Columnas,N'sus claves') + N'.';
        IF @Filtro IS NOT NULL
            SET @Descripcion=@Descripcion + N' Filtro SQL: ' + @Filtro + N'.';

        SET @Existe=CASE WHEN EXISTS
            (SELECT 1 FROM sys.extended_properties WHERE class=7 AND major_id=@ObjetoId AND minor_id=@MinorId AND name=N'MS_Description')
            THEN 1 ELSE 0 END;
        IF @Existe=1
            EXEC sys.sp_updateextendedproperty @name=N'MS_Description', @value=@Descripcion,
                @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@Tabla,
                @level2type=N'INDEX', @level2name=@Indice;
        ELSE
            EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=@Descripcion,
                @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=@Tabla,
                @level2type=N'INDEX', @level2name=@Indice;
        FETCH NEXT FROM cursorIndices INTO @Tabla, @Indice, @IndiceId, @EsPK, @EsUniqueConstraint, @Filtro, @Columnas;
    END;
    CLOSE cursorIndices;
    DEALLOCATE cursorIndices;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF CURSOR_STATUS('local','cursorDocumentacion') > -1
    BEGIN
        CLOSE cursorDocumentacion;
        DEALLOCATE cursorDocumentacion;
    END;
    IF CURSOR_STATUS('local','cursorIndices') > -1
    BEGIN
        CLOSE cursorIndices;
        DEALLOCATE cursorIndices;
    END;
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    DECLARE @MensajeError NVARCHAR(4000)=ERROR_MESSAGE();
    RAISERROR('%s', 16, 1, @MensajeError);
END CATCH;
GO
