/* Creación de la base de datos */

CREATE DATABASE SysGymDB;
GO

USE SysGymDB;
GO

/* Roles y usuarios */

CREATE TABLE Rol (
    IdRol INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(50) NOT NULL UNIQUE,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE UsuarioSistema (
    IdUsuarioSistema INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    DNI NVARCHAR(20) NOT NULL UNIQUE,
    Telefono NVARCHAR(30) NULL,
    FechaNacimiento DATETIME2 NULL,
    Salario DECIMAL(18,2) NOT NULL CONSTRAINT DF_UsuarioSistema_Salario DEFAULT 0,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(500) NOT NULL,
    Foto VARBINARY(MAX) NULL,
    Sexo CHAR(1) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdRol INT NOT NULL,

    CONSTRAINT CK_UsuarioSistema_Salario
        CHECK (Salario >= 0),

    CONSTRAINT FK_UsuarioSistema_Rol
        FOREIGN KEY (IdRol) REFERENCES Rol(IdRol)
);
GO

/* Socios */

CREATE TABLE Socio (
    IdSocio INT IDENTITY(1,1) PRIMARY KEY,
    DNI NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATETIME2 NULL,
    Peso DECIMAL(6,2) NULL,
    Altura DECIMAL(5,2) NULL,
    Foto VARBINARY(MAX) NULL,
    Sexo CHAR(1) NULL,
    Estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Socio_Peso
        CHECK (Peso IS NULL OR Peso > 0),

    CONSTRAINT CK_Socio_Altura
        CHECK (Altura IS NULL OR Altura > 0)
);
GO

/* Ejercicios y rutinas */

CREATE TABLE Ejercicio (
    IdEjercicio INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Estado BIT NOT NULL DEFAULT 1
);
GO

CREATE TABLE Rutina (
    IdRutina INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    FechaCreacion DATETIME2 NOT NULL,
    FechaInicio DATETIME2 NULL,
    FechaFin DATETIME2 NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdEntrenador INT NOT NULL,

    CONSTRAINT CK_Rutina_Fechas
        CHECK (FechaFin IS NULL OR FechaInicio IS NULL OR FechaFin >= FechaInicio),

    CONSTRAINT FK_Rutina_Entrenador
        FOREIGN KEY (IdEntrenador) REFERENCES UsuarioSistema(IdUsuarioSistema)
);
GO

/* Planes y membresías */

CREATE TABLE [Plan] (
    IdPlan INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Precio DECIMAL(18,2) NOT NULL,
    IncluyeEntrenador BIT NOT NULL DEFAULT 0,
    IncluyeRutinaPersonal BIT NOT NULL DEFAULT 0,
    Estado BIT NOT NULL DEFAULT 1,
    IdRutina INT NOT NULL,

    CONSTRAINT CK_Plan_Precio
        CHECK (Precio > 0),

    CONSTRAINT FK_Plan_Rutina
        FOREIGN KEY (IdRutina) REFERENCES Rutina(IdRutina)
);
GO

/* Catalogo de rutinas disponibles por plan, sin duplicados ni borrado en cascada. */
CREATE TABLE PlanRutina (
    IdPlan INT NOT NULL,
    IdRutina INT NOT NULL,
    CONSTRAINT PK_PlanRutina PRIMARY KEY (IdPlan, IdRutina),
    CONSTRAINT FK_PlanRutina_Plan FOREIGN KEY (IdPlan) REFERENCES [Plan](IdPlan),
    CONSTRAINT FK_PlanRutina_Rutina FOREIGN KEY (IdRutina) REFERENCES Rutina(IdRutina)
);
GO

CREATE TABLE Membresia (
    IdMembresia INT IDENTITY(1,1) PRIMARY KEY,
    FechaInicio DATETIME2 NOT NULL,
    FechaVencimiento DATETIME2 NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdPlan INT NOT NULL,
    IdSocio INT NOT NULL,
    IdUsuarioSistema INT NOT NULL,

    CONSTRAINT CK_Membresia_Fechas
        CHECK (FechaVencimiento >= FechaInicio),

    CONSTRAINT FK_Membresia_Plan
        FOREIGN KEY (IdPlan) REFERENCES [Plan](IdPlan),

    CONSTRAINT FK_Membresia_Socio
        FOREIGN KEY (IdSocio) REFERENCES Socio(IdSocio),

    CONSTRAINT FK_Membresia_UsuarioSistema
        FOREIGN KEY (IdUsuarioSistema) REFERENCES UsuarioSistema(IdUsuarioSistema)
);
GO

CREATE TABLE MembresiaEntrenador (
    IdMembresiaEntrenador INT IDENTITY(1,1) PRIMARY KEY,
    Estado BIT NOT NULL DEFAULT 1,
    IdMembresia INT NOT NULL,
    IdEntrenador INT NOT NULL,

    CONSTRAINT FK_MembresiaEntrenador_Membresia
        FOREIGN KEY (IdMembresia) REFERENCES Membresia(IdMembresia),

    CONSTRAINT FK_MembresiaEntrenador_UsuarioSistema
        FOREIGN KEY (IdEntrenador) REFERENCES UsuarioSistema(IdUsuarioSistema)
);
GO

/* Pagos */

CREATE TABLE Divisa (
    IdDivisa INT IDENTITY(1,1) PRIMARY KEY,
    CambioHoy DECIMAL(18,2) NOT NULL,
    Nombre NVARCHAR(50) NULL,
    Estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Divisa_Cambio
        CHECK (CambioHoy > 0)
);
GO

CREATE TABLE PagoEfectivo (
    IdPagoEfectivo INT IDENTITY(1,1) PRIMARY KEY,
    Estado BIT NOT NULL DEFAULT 1,
    IdDivisa INT NOT NULL,

    CONSTRAINT FK_PagoEfectivo_Divisa
        FOREIGN KEY (IdDivisa) REFERENCES Divisa(IdDivisa)
);
GO

CREATE TABLE MercadoPago (
    IdNroPagoMP INT IDENTITY(1,1) PRIMARY KEY,
    MercadoPagoPaymentId NVARCHAR(100) NULL,
    MercadoPagoPreferenceId NVARCHAR(100) NULL,
    ExternalReference NVARCHAR(150) NULL,
    StatusDetail NVARCHAR(200) NULL,
    FechaAprobacion DATETIME2 NULL
);
GO

CREATE TABLE MetodoPago (
    IdMetodoPago INT IDENTITY(1,1) PRIMARY KEY,
    Estado BIT NOT NULL DEFAULT 1,
    Observaciones NVARCHAR(500) NULL,
    IdNroPagoMP INT NULL,
    IdPagoEfectivo INT NULL,

    CONSTRAINT CK_MetodoPago_NoAmbosDetalles
        CHECK (NOT (IdNroPagoMP IS NOT NULL AND IdPagoEfectivo IS NOT NULL)),

    CONSTRAINT FK_MetodoPago_MercadoPago
        FOREIGN KEY (IdNroPagoMP) REFERENCES MercadoPago(IdNroPagoMP),

    CONSTRAINT FK_MetodoPago_PagoEfectivo
        FOREIGN KEY (IdPagoEfectivo) REFERENCES PagoEfectivo(IdPagoEfectivo)
);
GO

CREATE TABLE Pago (
    IdRegistroPago INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Importe DECIMAL(18,2) NOT NULL,
    Estado NVARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    IdMetodoPago INT NOT NULL,

    CONSTRAINT CK_Pago_Importe
        CHECK (Importe > 0),

    CONSTRAINT CK_Pago_Estado
        CHECK (Estado IN ('Pendiente', 'Aprobado', 'Rechazado', 'Anulado', 'Reembolsado')),

    CONSTRAINT FK_Pago_MetodoPago
        FOREIGN KEY (IdMetodoPago) REFERENCES MetodoPago(IdMetodoPago)
);
GO

CREATE TABLE CuotaMembresia (
    IdCuotaMembresia INT IDENTITY(1,1) PRIMARY KEY,
    FechaDesde DATETIME2 NOT NULL,
    FechaHasta DATETIME2 NOT NULL,
    Importe DECIMAL(18,2) NOT NULL,
    EstadoPago NVARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    IdRegistroPago INT NULL,
    IdMembresia INT NOT NULL,

    CONSTRAINT UQ_CuotaMembresia_Periodo
        UNIQUE (IdMembresia, FechaDesde),

    CONSTRAINT CK_CuotaMembresia_Fechas
        CHECK (FechaHasta >= FechaDesde),

    CONSTRAINT CK_CuotaMembresia_Importe
        CHECK (Importe > 0),

    CONSTRAINT CK_CuotaMembresia_Estado
        CHECK (EstadoPago IN ('Pendiente', 'Pagada', 'Anulada')),

    CONSTRAINT FK_CuotaMembresia_Pago
        FOREIGN KEY (IdRegistroPago) REFERENCES Pago(IdRegistroPago),

    CONSTRAINT FK_CuotaMembresia_Membresia
        FOREIGN KEY (IdMembresia) REFERENCES Membresia(IdMembresia)
);
GO

/* Asistencias */

CREATE TABLE Asistencia (
    IdAsistencia INT IDENTITY(1,1) PRIMARY KEY,
    Fecha DATETIME2 NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdSocio INT NOT NULL,

    CONSTRAINT FK_Asistencia_Socio
        FOREIGN KEY (IdSocio) REFERENCES Socio(IdSocio)
);
GO

/* Asociación entre rutinas y ejercicios */

CREATE TABLE RutinaEjercicio (
    IdRutinaEjercicio INT IDENTITY(1,1) PRIMARY KEY,
    Series INT NULL,
    Repeticiones INT NULL,
    Peso DECIMAL(8,2) NULL,
    Descanso INT NOT NULL,
    Orden INT NOT NULL,
    DiaSemana INT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdRutina INT NOT NULL,
    IdEjercicio INT NOT NULL,

    CONSTRAINT CK_RutinaEjercicio_Series
        CHECK (Series IS NULL OR Series > 0),

    CONSTRAINT CK_RutinaEjercicio_Repeticiones
        CHECK (Repeticiones IS NULL OR Repeticiones > 0),

    CONSTRAINT CK_RutinaEjercicio_Peso
        CHECK (Peso IS NULL OR Peso >= 0),

    CONSTRAINT CK_RutinaEjercicio_Descanso
        CHECK (Descanso >= 0),

    CONSTRAINT CK_RutinaEjercicio_Orden
        CHECK (Orden > 0),

    /* 1 lunes a 5 viernes; nulo cuando el ejercicio todavia no tiene dia */
    CONSTRAINT CK_RutinaEjercicio_DiaSemana
        CHECK (DiaSemana IS NULL OR DiaSemana BETWEEN 1 AND 5),

    CONSTRAINT FK_RutinaEjercicio_Rutina
        FOREIGN KEY (IdRutina) REFERENCES Rutina(IdRutina),

    CONSTRAINT FK_RutinaEjercicio_Ejercicio
        FOREIGN KEY (IdEjercicio) REFERENCES Ejercicio(IdEjercicio)
);
GO

/* Una misma plantilla de rutina puede asignarse a muchas membresías */

CREATE TABLE RutinaAsignacion (
    IdRutinaAsignacion INT IDENTITY(1,1) PRIMARY KEY,
    FechaAsignacion DATETIME2 NOT NULL,
    FechaFin DATETIME2 NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdRutina INT NOT NULL,
    IdMembresia INT NOT NULL,

    CONSTRAINT CK_RutinaAsignacion_Fechas
        CHECK (FechaFin IS NULL OR FechaFin >= FechaAsignacion),

    CONSTRAINT FK_RutinaAsignacion_Rutina
        FOREIGN KEY (IdRutina) REFERENCES Rutina(IdRutina),

    CONSTRAINT FK_RutinaAsignacion_Membresia
        FOREIGN KEY (IdMembresia) REFERENCES Membresia(IdMembresia)
);
GO

/* Índices de consultas habituales */

CREATE INDEX IX_Membresia_IdSocio
    ON Membresia(IdSocio);

CREATE INDEX IX_Asistencia_IdSocio
    ON Asistencia(IdSocio);

CREATE INDEX IX_RutinaAsignacion_IdMembresia
    ON RutinaAsignacion(IdMembresia);

CREATE INDEX IX_RutinaAsignacion_Activas
    ON RutinaAsignacion(IdRutina, Estado);

/* Un pago no puede asociarse a dos cuotas, pero las cuotas pendientes pueden tener NULL */

CREATE UNIQUE INDEX UX_CuotaMembresia_IdRegistroPago
    ON CuotaMembresia(IdRegistroPago)
    WHERE IdRegistroPago IS NOT NULL;
GO

/* Datos iniciales */

INSERT INTO Rol (Descripcion, Estado)
VALUES
    ('Administrador', 1),
    ('Recepcionista', 1),
    ('Entrenador', 1);

INSERT INTO UsuarioSistema
    (Nombre, Apellido, DNI, Telefono, FechaNacimiento, Salario, Username, Password, Estado, IdRol)
VALUES
    ('San', 'Martin', '30000001', NULL, NULL, 0, 'SanMartin',
        'ARGON2ID:19:65536:3:2:fk55JlSsiyw0cDMfppSqSg==:+OMMPicxIZ0iPl0t7h0mNrg59Ysq3F5HKt/gNrYPuqU=', 1, 1),
    ('Recepcionista', 'SysGym', '30000002', NULL, NULL, 0, 'recepcion',
        'ARGON2ID:19:65536:3:2:PxEAEGmIlOWwbIMiTQt6KQ==:GurlFqP0USkOFKzxDKBzefLcWrzcSwtb59QsBkwTAV4=', 1, 2),
    ('San', 'Martin', '30000003', NULL, NULL, 0, 'entrenador',
        'ARGON2ID:19:65536:3:2:f3gcWfMkxMhs7qrkxClS+Q==:c16O2cpT/vzSNPtQeZrgIrcc3RfQhekoOBuhEJxI3pI=', 1, 3);

INSERT INTO Divisa (CambioHoy, Nombre, Estado)
VALUES (1, 'Peso argentino', 1);

INSERT INTO PagoEfectivo (Estado, IdDivisa)
VALUES (1, 1);

INSERT INTO MercadoPago (ExternalReference)
VALUES ('SYSGYM_MANUAL');

INSERT INTO MetodoPago (Estado, Observaciones, IdNroPagoMP, IdPagoEfectivo)
VALUES
    (1, 'Mercado Pago', 1, NULL),
    (1, 'Pago en efectivo', NULL, 1);
GO

/* Catálogo inicial de ejercicios y rutinas generales.
   Las rutinas son plantillas reutilizables y no pertenecen a un socio. */

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @IdEntrenador INT;

SELECT @IdEntrenador = u.IdUsuarioSistema
FROM UsuarioSistema AS u
INNER JOIN Rol AS r ON r.IdRol = u.IdRol
WHERE u.Username = N'entrenador'
  AND u.Estado = 1
  AND r.Estado = 1
  AND r.Descripcion = N'Entrenador';

IF @IdEntrenador IS NULL
BEGIN
    RAISERROR(N'No se encontro un entrenador activo con el usuario indicado.', 16, 1);
    RETURN;
END;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @Ejercicios TABLE
    (
        Nombre NVARCHAR(100) NOT NULL,
        Descripcion NVARCHAR(500) NULL
    );

    INSERT INTO @Ejercicios (Nombre, Descripcion)
    VALUES
        (N'Press de banca', N'Pecho, triceps y deltoides anterior.'),
        (N'Press inclinado con mancuernas', N'Pecho superior y estabilizadores.'),
        (N'Aperturas con mancuernas', N'Aislamiento de pectorales.'),
        (N'Fondos en paralelas', N'Pecho y triceps con peso corporal.'),
        (N'Jalon al pecho', N'Dorsales mediante polea alta.'),
        (N'Remo con barra', N'Espalda media, dorsales y biceps.'),
        (N'Remo sentado en polea', N'Dorsales y espalda media.'),
        (N'Face pull', N'Deltoides posterior y estabilizadores.'),
        (N'Press militar', N'Hombros y triceps.'),
        (N'Elevaciones laterales', N'Deltoides lateral.'),
        (N'Curl de biceps con barra', N'Aislamiento de biceps.'),
        (N'Curl martillo', N'Biceps, braquial y antebrazo.'),
        (N'Extension de triceps en polea', N'Aislamiento de triceps.'),
        (N'Sentadilla con barra', N'Cuadriceps, gluteos y zona media.'),
        (N'Prensa de piernas', N'Cuadriceps y gluteos en maquina.'),
        (N'Peso muerto rumano', N'Isquiotibiales, gluteos y cadena posterior.'),
        (N'Extension de cuadriceps', N'Aislamiento de cuadriceps.'),
        (N'Curl femoral', N'Aislamiento de isquiotibiales.'),
        (N'Hip thrust', N'Extension de cadera con enfasis en gluteos.'),
        (N'Zancadas', N'Ejercicio unilateral de piernas.'),
        (N'Elevacion de talones', N'Gemelos y soleo.'),
        (N'Plancha abdominal', N'Estabilidad de la zona media.'),
        (N'Crunch abdominal', N'Flexion controlada para abdominales.'),
        (N'Escaladores', N'Acondicionamiento y zona media.');

    INSERT INTO Ejercicio (Nombre, Descripcion, Estado)
    SELECT x.Nombre, x.Descripcion, 1
    FROM @Ejercicios AS x
    WHERE NOT EXISTS (SELECT 1 FROM Ejercicio AS e WHERE e.Nombre = x.Nombre);

    DECLARE @Rutinas TABLE
    (
        Nombre NVARCHAR(100) NOT NULL,
        Descripcion NVARCHAR(500) NULL
    );

    INSERT INTO @Rutinas (Nombre, Descripcion)
    VALUES
        (N'Comienzo 1', N'Adaptacion general para iniciar el entrenamiento.'),
        (N'Hipertrofia 1', N'Volumen moderado para desarrollar masa muscular.'),
        (N'Hipertrofia 2', N'Progresion de hipertrofia con mayor volumen.'),
        (N'Fuerza', N'Mejora de fuerza con ejercicios compuestos.'),
        (N'Powerlifting', N'Sentadilla, press banca y peso muerto.'),
        (N'Cardio', N'Entrenamiento cardiovascular y acondicionamiento.');

    INSERT INTO Rutina
        (Nombre, Descripcion, FechaCreacion, FechaInicio, FechaFin, Estado, IdEntrenador)
    SELECT x.Nombre, x.Descripcion, SYSDATETIME(), NULL, NULL, 1, @IdEntrenador
    FROM @Rutinas AS x
    WHERE NOT EXISTS
    (
        SELECT 1 FROM Rutina AS r
        WHERE r.Nombre = x.Nombre AND r.IdEntrenador = @IdEntrenador
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;

    DECLARE @MensajeError NVARCHAR(4000);
    SELECT @MensajeError = ERROR_MESSAGE();

    RAISERROR(N'%s', 16, 1, @MensajeError);
END CATCH;
GO

/* Inserciones manuales de prueba. Ultima actualizacion: 9 de septiembre de 2026.
   Ejecutar este bloque UNA SOLA VEZ, despues del esquema y catalogo inicial.
   En una base existente, seleccionar solo desde este comentario hasta el GO final.
   Los usernames y DNI ficticios escritos abajo deben estar libres. Normal y Premium tampoco deben existir: no se borran planes.
   Fechas fijas: septiembre de 2026. Fotos vacias para probar la carga desde la app.
   Usuarios de prueba: clave Prueba123! (hash Argon2id). Nunca usar en produccion.
   Los INSERT son explicitos: no hay ciclos, procedimientos ni funciones propias.
   La transaccion evita una carga parcial si algun dato entra en conflicto. */

SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;

BEGIN TRY
BEGIN TRANSACTION;

/* Inserciones de Rol y Divisa: se reutilizan los tres roles y el peso argentino
   del bloque inicial. No corresponde inventar veinte roles o cotizaciones. */

/* Inserciones de UsuarioSistema: Lucia es administradora; Mateo, Sofia, Tomas
   y Valentina son recepcionistas; los demas, entrenadores. Matias esta de baja. */
INSERT INTO UsuarioSistema (Nombre, Apellido, DNI, Telefono, FechaNacimiento, Salario, Username, Password, Foto, Sexo, Estado, IdRol)
VALUES
    (N'Lucia', N'Garcia', N'32487169', N'1148296307', '19810315', 710000, N'lucia.garcia', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Administrador')),
    (N'Mateo', N'Lopez', N'41730582', N'1163074829', '19820315', 720000, N'mateo_lopez', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Recepcionista')),
    (N'Sofia', N'Perez', N'36912407', N'1159621843', '19830315', 730000, N'sofi.perez', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Recepcionista')),
    (N'Tomas', N'Gomez', N'45826031', N'1141738062', '19840315', 740000, N'tomasg', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Recepcionista')),
    (N'Valentina', N'Diaz', N'33749618', N'1168542091', '19850315', 750000, N'vale.diaz', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, NULL, 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Recepcionista')),
    (N'Benjamin', N'Fernandez', N'40218573', N'1150287394', '19860315', 760000, N'benja.fernandez', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Martina', N'Romero', N'38164729', N'1146923185', '19870315', 770000, N'martiromero', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Santiago', N'Alvarez', N'47593016', N'1162159048', '19880315', 780000, N'santi.alvarez', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Camila', N'Torres', N'35072841', N'1157840263', '19890315', 790000, N'cami_torres', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Joaquin', N'Ruiz', N'42981635', N'1149306821', '19900315', 800000, N'joaquin.ruiz', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, NULL, 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Julieta', N'Acosta', N'31256984', N'1164827509', '19910315', 810000, N'juliacosta', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Nicolas', N'Medina', N'46381720', N'1153671942', '19920315', 820000, N'nico.medina', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Catalina', N'Herrera', N'39502468', N'1142089637', '19930315', 830000, N'cata_herrera', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Agustin', N'Sosa', N'44167892', N'1167934208', '19940315', 840000, N'agustin.sosa', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Delfina', N'Castro', N'32890517', N'1151408762', '19950315', 850000, N'delfi.castro', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, NULL, 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Lucas', N'Rojas', N'48621309', N'1148653091', '19960315', 860000, N'lucasrojas', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Emilia', N'Molina', N'37415826', N'1160297483', '19970315', 870000, N'emi.molina', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Franco', N'Ortiz', N'40973251', N'1154926810', '19980315', 880000, N'franco_ortiz', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'M', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Victoria', N'Silva', N'35689104', N'1147312056', '19990315', 890000, N'vicky.silva', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, N'F', 1, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador')),
    (N'Matias', N'Navarro', N'49204673', N'1165083927', '20000315', 900000, N'matias.navarro', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', NULL, NULL, 0, (SELECT IdRol FROM Rol WHERE Descripcion = N'Entrenador'));

/* Inserciones de Socio: 20 socios; 19 y 20 de baja para probar reactivacion */
INSERT INTO Socio (DNI, Nombre, Apellido, FechaNacimiento, Peso, Altura, Foto, Sexo, Estado)
VALUES
    (N'39827416', N'Lucia', N'Garcia', '19860510', 56, 1.61, NULL, N'F', 1),
    (N'45160382', N'Mateo', N'Lopez', '19870510', 57, 1.62, NULL, N'M', 1),
    (N'32791854', N'Sofia', N'Perez', '19880510', 58, 1.63, NULL, N'F', 1),
    (N'47306219', N'Tomas', N'Gomez', '19890510', 59, 1.64, NULL, N'M', 1),
    (N'36548207', N'Valentina', N'Diaz', '19900510', 60, 1.65, NULL, NULL, 1),
    (N'41893562', N'Benjamin', N'Fernandez', '19910510', 61, 1.66, NULL, N'M', 1),
    (N'30674198', N'Martina', N'Romero', '19920510', 62, 1.67, NULL, N'F', 1),
    (N'44250731', N'Santiago', N'Alvarez', '19930510', 63, 1.68, NULL, N'M', 1),
    (N'38912645', N'Camila', N'Torres', '19940510', 64, 1.69, NULL, N'F', 1),
    (N'46783520', N'Joaquin', N'Ruiz', '19950510', 65, 1.70, NULL, NULL, 1),
    (N'34197086', N'Julieta', N'Acosta', '19960510', 66, 1.71, NULL, N'F', 1),
    (N'40925817', N'Nicolas', N'Medina', '19970510', 67, 1.72, NULL, N'M', 1),
    (N'48231694', N'Catalina', N'Herrera', '19980510', 68, 1.73, NULL, N'F', 1),
    (N'35760428', N'Agustin', N'Sosa', '19990510', 69, 1.74, NULL, N'M', 1),
    (N'43189256', N'Delfina', N'Castro', '20000510', 70, 1.75, NULL, NULL, 1),
    (N'31946873', N'Lucas', N'Rojas', '20010510', 71, 1.76, NULL, N'M', 1),
    (N'49607215', N'Emilia', N'Molina', '20020510', 72, 1.77, NULL, N'F', 1),
    (N'37651940', N'Franco', N'Ortiz', '20030510', 73, 1.78, NULL, N'M', 1),
    (N'42378069', N'Victoria', N'Silva', '20040510', 74, 1.79, NULL, N'F', 0),
    (N'34821597', N'Matias', N'Navarro', '20050510', 75, 1.80, NULL, NULL, 0);

/* Inserciones de Ejercicio: el catalogo anterior ya aporta 24 ejercicios.
   Se reutilizan esos registros, sin duplicarlos. */

/* Inserciones de Rutina: 20 plantillas adicionales */
INSERT INTO Rutina (Nombre, Descripcion, FechaCreacion, FechaInicio, FechaFin, Estado, IdEntrenador)
VALUES
    (N'Pecho con barra', N'Trabajo de pectorales con press de banca.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Pecho inclinado', N'Trabajo inclinado con mancuernas.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Aperturas de pecho', N'Aislamiento de pectorales con mancuernas.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Fondos de tren superior', N'Fondos con peso corporal.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Espalda en polea', N'Jalones para el trabajo de dorsales.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Remo de fuerza', N'Remo con barra y tecnica controlada.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Espalda controlada', N'Remo sentado en polea.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Estabilidad de hombros', N'Trabajo posterior del hombro con face pull.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Press de hombros', N'Press militar para hombros y triceps.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Hombros laterales', N'Elevaciones laterales con carga moderada.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Biceps con barra', N'Curl de biceps con barra.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Brazos con mancuernas', N'Curl martillo para brazos y antebrazos.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Triceps en polea', N'Extensiones de triceps con polea.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Sentadilla inicial', N'Practica de sentadilla con barra.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Piernas en prensa', N'Trabajo de cuadriceps y gluteos en prensa.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Cadena posterior', N'Peso muerto rumano para cadena posterior.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Cuadriceps en maquina', N'Extension de cuadriceps en maquina.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Isquiotibiales', N'Flexion de rodilla con curl femoral.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Gluteos en banco', N'Extension de cadera con hip thrust.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (N'Piernas unilaterales', N'Zancadas para trabajo unilateral.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez'));

/* Inserciones de Ejercicio: 21 ejercicios adicionales para armar dias por grupo muscular */
INSERT INTO Ejercicio (Nombre, Descripcion, Estado)
VALUES
    (N'Press plano con mancuernas', N'Pecho con recorrido libre y estabilizadores.', 1),
    (N'Cruce en polea', N'Aislamiento de pectoral con tension continua.', 1),
    (N'Press declinado con barra', N'Pecho inferior y triceps.', 1),
    (N'Dominadas', N'Dorsales y biceps con peso corporal.', 1),
    (N'Remo con mancuerna', N'Dorsal unilateral y espalda media.', 1),
    (N'Pullover en polea', N'Dorsal ancho con hombro extendido.', 1),
    (N'Press Arnold', N'Deltoides anterior y lateral con rotacion.', 1),
    (N'Elevaciones frontales', N'Deltoides anterior.', 1),
    (N'Pajaro con mancuernas', N'Deltoides posterior.', 1),
    (N'Curl en banco inclinado', N'Biceps con hombro extendido.', 1),
    (N'Curl concentrado', N'Aislamiento unilateral de biceps.', 1),
    (N'Curl en polea baja', N'Biceps con tension continua.', 1),
    (N'Press frances', N'Triceps con codos fijos.', 1),
    (N'Patada de triceps', N'Aislamiento unilateral de triceps.', 1),
    (N'Fondos en banco', N'Triceps con peso corporal.', 1),
    (N'Sentadilla bulgara', N'Cuadriceps y gluteos unilateral.', 1),
    (N'Peso muerto convencional', N'Cadena posterior completa.', 1),
    (N'Abductores en maquina', N'Gluteo medio y abductores.', 1),
    (N'Elevacion de piernas colgado', N'Abdomen inferior.', 1),
    (N'Rueda abdominal', N'Zona media con antiextension.', 1),
    (N'Giro ruso', N'Oblicuos y rotacion de tronco.', 1);

/* Inserciones de RutinaEjercicio: cada plantilla reparte siete ejercicios por dia,
   agrupados por musculo, de lunes a viernes. Un mismo ejercicio puede repetirse en
   dias distintos. Orden es la posicion dentro del dia. */

/* Comienzo 1: Lunes pecho y biceps / Martes espalda y triceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Hipertrofia 1: Lunes pecho y biceps / Martes espalda y triceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Hipertrofia 2: Lunes espalda y triceps / Martes pecho y biceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 120, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Fuerza: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Powerlifting: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Cardio: Lunes pecho y biceps / Martes espalda y triceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Pecho con barra: Lunes pecho y biceps / Martes espalda y triceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Pecho inclinado: Lunes pecho y biceps / Martes espalda y triceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Aperturas de pecho: Lunes pecho y biceps / Martes espalda y triceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Fondos de tren superior: Lunes pecho y biceps / Martes espalda y triceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Espalda en polea: Lunes espalda y triceps / Martes pecho y biceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 120, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Remo de fuerza: Lunes espalda y triceps / Martes pecho y biceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 120, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Espalda controlada: Lunes espalda y triceps / Martes pecho y biceps / Miercoles piernas / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 120, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Estabilidad de hombros: Lunes hombros y core / Martes pecho y biceps / Miercoles espalda y triceps / Jueves piernas / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Press de hombros: Lunes hombros y core / Martes pecho y biceps / Miercoles espalda y triceps / Jueves piernas / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Hombros laterales: Lunes hombros y core / Martes pecho y biceps / Miercoles espalda y triceps / Jueves piernas / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Biceps con barra: Lunes hombros y core / Martes pecho y biceps / Miercoles espalda y triceps / Jueves piernas / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Brazos con mancuernas: Lunes hombros y core / Martes pecho y biceps / Miercoles espalda y triceps / Jueves piernas / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Triceps en polea: Lunes hombros y core / Martes pecho y biceps / Miercoles espalda y triceps / Jueves piernas / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 10, NULL, 90, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 150, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Sentadilla inicial: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Piernas en prensa: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Cadena posterior: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Cuadriceps en maquina: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Isquiotibiales: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Gluteos en banco: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Piernas unilaterales: Lunes piernas / Martes pecho y biceps / Miercoles espalda y triceps / Jueves hombros y core / Viernes full */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (4, 8, NULL, 150, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 120, 2, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 75, 3, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 4, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 120, 5, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 6, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 90, 7, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 4, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 60, 5, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 2, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 8, NULL, 120, 1, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 2, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 3, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 4, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 12, NULL, 60, 5, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 6, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 45, 7, 3, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 10, NULL, 90, 1, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 2, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold' AND Estado = 1 ORDER BY IdEjercicio)),
    (4, 15, NULL, 60, 3, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 45, NULL, 45, 5, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 20, NULL, 45, 6, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 60, 7, 4, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 1, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 2, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 75, 3, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 15, NULL, 60, 4, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 12, NULL, 90, 5, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 8, NULL, 150, 6, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional' AND Estado = 1 ORDER BY IdEjercicio)),
    (3, 30, NULL, 45, 7, 5, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT TOP (1) IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores' AND Estado = 1 ORDER BY IdEjercicio));

/* Inserciones de Plan: solo 2 planes: Normal y Premium */
INSERT INTO [Plan] (Nombre, Descripcion, Precio, IncluyeEntrenador, IncluyeRutinaPersonal, Estado, IdRutina)
VALUES
    (N'Normal', N'Acceso al gimnasio con rutina base.', 15000, 0, 0, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra')),
    (N'Premium', N'Acceso, entrenador y rutina personalizada.', 25000, 1, 1, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'));

/* Inserciones de PlanRutina: Normal dispone de tres rutinas y Premium de las
   26 del catalogo inicial y manual. Las futuras se seleccionan desde Planes. */
INSERT INTO PlanRutina (IdPlan, IdRutina)
VALUES
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 2')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Powerlifting')),
    ((SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio'));


/* Inserciones de Membresia: 20 membresias; diez Premium y diez Normal */
INSERT INTO Membresia (FechaInicio, FechaVencimiento, Estado, IdPlan, IdSocio, IdUsuarioSistema)
VALUES
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'39827416'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'45160382'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'32791854'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'47306219'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'36548207'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'41893562'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'30674198'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'44250731'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'38912645'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'46783520'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'34197086'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'40925817'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'48231694'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'35760428'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'43189256'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'31946873'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'49607215'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'37651940'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 0, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'42378069'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez')),
    ('20260901', '20260930', 0, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'34821597'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'mateo_lopez'));

/* Inserciones de MercadoPago: 10 referencias ficticias; no son cobros reales */
INSERT INTO MercadoPago (ExternalReference, StatusDetail, FechaAprobacion)
VALUES
    (N'CUOTA-SEP26-39827416', N'Pago acreditado', '20260901'),
    (N'CUOTA-SEP26-32791854', N'Pago acreditado', '20260901'),
    (N'CUOTA-SEP26-36548207', N'Pago acreditado', '20260901'),
    (N'CUOTA-SEP26-30674198', N'Pago acreditado', '20260901'),
    (N'CUOTA-SEP26-38912645', N'Pago acreditado', '20260901'),
    (N'CUOTA-SEP26-34197086', N'Esperando acreditacion', NULL),
    (N'CUOTA-SEP26-48231694', N'Esperando acreditacion', NULL),
    (N'CUOTA-SEP26-43189256', N'Esperando acreditacion', NULL),
    (N'CUOTA-SEP26-49607215', N'Operacion rechazada', NULL),
    (N'CUOTA-SEP26-42378069', N'Operacion cancelada', NULL);

/* Inserciones de PagoEfectivo: se reutiliza el detalle en pesos del bloque inicial.
   Los diez metodos nuevos de efectivo comparten esa divisa. */

/* Inserciones de MetodoPago: 20 metodos, diez por cada medio y un solo detalle por metodo */
INSERT INTO MetodoPago (Estado, Observaciones, IdNroPagoMP, IdPagoEfectivo)
VALUES
    (1, N'Mercado Pago - lucia.garcia - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-39827416'), NULL),
    (1, N'Efectivo - mateo_lopez - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - sofi.perez - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-32791854'), NULL),
    (1, N'Efectivo - tomasg - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - vale.diaz - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-36548207'), NULL),
    (1, N'Efectivo - benja.fernandez - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - martiromero - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-30674198'), NULL),
    (1, N'Efectivo - santi.alvarez - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - cami_torres - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-38912645'), NULL),
    (1, N'Efectivo - joaquin.ruiz - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - juliacosta - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-34197086'), NULL),
    (1, N'Efectivo - nico.medina - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - cata_herrera - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-48231694'), NULL),
    (1, N'Efectivo - agustin.sosa - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - delfi.castro - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-43189256'), NULL),
    (1, N'Efectivo - lucasrojas - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - emi.molina - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-49607215'), NULL),
    (1, N'Efectivo - franco_ortiz - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo)),
    (1, N'Mercado Pago - vicky.silva - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-42378069'), NULL),
    (1, N'Efectivo - matias.navarro - septiembre', NULL, (SELECT TOP (1) pe.IdPagoEfectivo FROM PagoEfectivo pe INNER JOIN Divisa d ON d.IdDivisa = pe.IdDivisa WHERE pe.Estado = 1 AND d.Estado = 1 AND d.Nombre = N'Peso argentino' AND d.CambioHoy = 1 ORDER BY pe.IdPagoEfectivo));

/* Inserciones de Pago: 20 pagos: 10 aprobados, 5 pendientes, 3 rechazados y 2 anulados */
INSERT INTO Pago (Fecha, Descripcion, Importe, Estado, IdMetodoPago)
VALUES
    ('20260901', N'Cuota septiembre - lucia.garcia', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - lucia.garcia - septiembre')),
    ('20260901', N'Cuota septiembre - mateo_lopez', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - mateo_lopez - septiembre')),
    ('20260901', N'Cuota septiembre - sofi.perez', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - sofi.perez - septiembre')),
    ('20260901', N'Cuota septiembre - tomasg', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - tomasg - septiembre')),
    ('20260901', N'Cuota septiembre - vale.diaz', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - vale.diaz - septiembre')),
    ('20260901', N'Cuota septiembre - benja.fernandez', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - benja.fernandez - septiembre')),
    ('20260901', N'Cuota septiembre - martiromero', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - martiromero - septiembre')),
    ('20260901', N'Cuota septiembre - santi.alvarez', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - santi.alvarez - septiembre')),
    ('20260901', N'Cuota septiembre - cami_torres', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - cami_torres - septiembre')),
    ('20260901', N'Cuota septiembre - joaquin.ruiz', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - joaquin.ruiz - septiembre')),
    ('20260901', N'Cuota septiembre - juliacosta', 15000, N'Pendiente', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - juliacosta - septiembre')),
    ('20260901', N'Cuota septiembre - nico.medina', 15000, N'Pendiente', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - nico.medina - septiembre')),
    ('20260901', N'Cuota septiembre - cata_herrera', 15000, N'Pendiente', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - cata_herrera - septiembre')),
    ('20260901', N'Cuota septiembre - agustin.sosa', 15000, N'Pendiente', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - agustin.sosa - septiembre')),
    ('20260901', N'Cuota septiembre - delfi.castro', 15000, N'Pendiente', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - delfi.castro - septiembre')),
    ('20260901', N'Cuota septiembre - lucasrojas', 15000, N'Rechazado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - lucasrojas - septiembre')),
    ('20260901', N'Cuota septiembre - emi.molina', 15000, N'Rechazado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - emi.molina - septiembre')),
    ('20260901', N'Cuota septiembre - franco_ortiz', 15000, N'Rechazado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - franco_ortiz - septiembre')),
    ('20260901', N'Cuota septiembre - vicky.silva', 15000, N'Anulado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - vicky.silva - septiembre')),
    ('20260901', N'Cuota septiembre - matias.navarro', 15000, N'Anulado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - matias.navarro - septiembre'));

/* Inserciones de CuotaMembresia: 20 cuotas mensuales; solo diez pagadas */
INSERT INTO CuotaMembresia (FechaDesde, FechaHasta, Importe, EstadoPago, IdRegistroPago, IdMembresia)
VALUES
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - lucia.garcia'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39827416') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - mateo_lopez'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'45160382') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - sofi.perez'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'32791854') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - tomasg'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'47306219') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - vale.diaz'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36548207') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - benja.fernandez'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41893562') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - martiromero'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'30674198') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - santi.alvarez'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'44250731') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - cami_torres'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38912645') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - joaquin.ruiz'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'46783520') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - juliacosta'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34197086') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - nico.medina'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40925817') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - cata_herrera'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'48231694') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - agustin.sosa'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'35760428') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - delfi.castro'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'43189256') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - lucasrojas'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31946873') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - emi.molina'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'49607215') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - franco_ortiz'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37651940') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - vicky.silva'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'42378069') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - matias.navarro'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34821597') AND FechaInicio = '20260901'));

/* Inserciones de MembresiaEntrenador: 10 asignaciones, solo para Premium */
INSERT INTO MembresiaEntrenador (Estado, IdMembresia, IdEntrenador)
VALUES
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39827416') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'45160382') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'32791854') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'47306219') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36548207') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41893562') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'30674198') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'44250731') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38912645') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'46783520') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'benja.fernandez'));

/* Inserciones de RutinaAsignacion: 10 asignaciones, solo para Premium */
INSERT INTO RutinaAsignacion (FechaAsignacion, FechaFin, Estado, IdRutina, IdMembresia)
VALUES
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39827416') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'45160382') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'32791854') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'47306219') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36548207') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41893562') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'30674198') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'44250731') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38912645') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'46783520') AND FechaInicio = '20260901'));

/* Inserciones de RutinaAsignacion: los diez socios restantes tambien reciben plantilla,
   de modo que los veinte socios tienen rutina semanal */
INSERT INTO RutinaAsignacion (FechaAsignacion, FechaFin, Estado, IdRutina, IdMembresia)
VALUES
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31946873') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34197086') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34821597') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'35760428') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37651940') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40925817') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'42378069') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'43189256') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'48231694') AND FechaInicio = '20260901')),
    ('20260901', NULL, 1, (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'49607215') AND FechaInicio = '20260901'));

/* Inserciones de Asistencia: 20 asistencias de socios con cuota pagada; diez por cada dia */
INSERT INTO Asistencia (Fecha, Descripcion, Estado, IdSocio)
VALUES
    ('20260908 09:00:00', N'Entrenamiento de fuerza', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'39827416')),
    ('20260908 09:00:00', N'Sesion de movilidad', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'45160382')),
    ('20260908 09:00:00', N'Entrenamiento de piernas', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'32791854')),
    ('20260908 09:00:00', N'Trabajo de tren superior', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'47306219')),
    ('20260908 09:00:00', N'Sesion de acondicionamiento', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'36548207')),
    ('20260908 09:00:00', N'Entrenamiento de fuerza', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'41893562')),
    ('20260908 09:00:00', N'Sesion de movilidad', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'30674198')),
    ('20260908 09:00:00', N'Entrenamiento de piernas', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'44250731')),
    ('20260908 09:00:00', N'Trabajo de tren superior', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'38912645')),
    ('20260908 09:00:00', N'Sesion de acondicionamiento', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'46783520')),
    ('20260909 09:00:00', N'Entrenamiento de fuerza', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'39827416')),
    ('20260909 09:00:00', N'Sesion de movilidad', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'45160382')),
    ('20260909 09:00:00', N'Entrenamiento de piernas', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'32791854')),
    ('20260909 09:00:00', N'Trabajo de tren superior', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'47306219')),
    ('20260909 09:00:00', N'Sesion de acondicionamiento', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'36548207')),
    ('20260909 09:00:00', N'Entrenamiento de fuerza', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'41893562')),
    ('20260909 09:00:00', N'Sesion de movilidad', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'30674198')),
    ('20260909 09:00:00', N'Entrenamiento de piernas', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'44250731')),
    ('20260909 09:00:00', N'Trabajo de tren superior', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'38912645')),
    ('20260909 09:00:00', N'Sesion de acondicionamiento', 1, (SELECT IdSocio FROM Socio WHERE DNI = N'46783520'));

COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

