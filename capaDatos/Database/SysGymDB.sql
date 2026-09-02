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
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(500) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdRol INT NOT NULL,

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
    (Nombre, Apellido, DNI, Telefono, FechaNacimiento, Username, Password, Estado, IdRol)
VALUES
    ('San', 'Martin', '30000001', NULL, NULL, 'SanMartin',
        'ARGON2ID:19:65536:3:2:fk55JlSsiyw0cDMfppSqSg==:+OMMPicxIZ0iPl0t7h0mNrg59Ysq3F5HKt/gNrYPuqU=', 1, 1),
    ('Recepcionista', 'SysGym', '30000002', NULL, NULL, 'recepcion',
        'ARGON2ID:19:65536:3:2:PxEAEGmIlOWwbIMiTQt6KQ==:GurlFqP0USkOFKzxDKBzefLcWrzcSwtb59QsBkwTAV4=', 1, 2),
    ('San', 'Martin', '30000003', NULL, NULL, 'entrenador',
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
    THROW 50002, 'No se encontro un entrenador activo con el usuario indicado.', 1;

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

    DECLARE @Detalle TABLE
    (
        RutinaNombre NVARCHAR(100) NOT NULL,
        EjercicioNombre NVARCHAR(100) NOT NULL,
        Series INT NULL,
        Repeticiones INT NULL,
        Descanso INT NOT NULL,
        Orden INT NOT NULL
    );

    INSERT INTO @Detalle (RutinaNombre, EjercicioNombre, Series, Repeticiones, Descanso, Orden)
    VALUES
        (N'Comienzo 1', N'Sentadilla con barra', 3, 12, 90, 1),
        (N'Comienzo 1', N'Press de banca', 3, 12, 90, 2),
        (N'Comienzo 1', N'Jalon al pecho', 3, 12, 90, 3),
        (N'Comienzo 1', N'Plancha abdominal', 3, 30, 60, 4),
        (N'Hipertrofia 1', N'Press de banca', 4, 10, 90, 1),
        (N'Hipertrofia 1', N'Remo con barra', 4, 10, 90, 2),
        (N'Hipertrofia 1', N'Sentadilla con barra', 4, 10, 120, 3),
        (N'Hipertrofia 1', N'Peso muerto rumano', 3, 10, 120, 4),
        (N'Hipertrofia 2', N'Press inclinado con mancuernas', 4, 10, 90, 1),
        (N'Hipertrofia 2', N'Jalon al pecho', 4, 10, 90, 2),
        (N'Hipertrofia 2', N'Prensa de piernas', 4, 12, 120, 3),
        (N'Hipertrofia 2', N'Hip thrust', 4, 12, 120, 4),
        (N'Fuerza', N'Sentadilla con barra', 5, 5, 180, 1),
        (N'Fuerza', N'Press de banca', 5, 5, 180, 2),
        (N'Fuerza', N'Remo con barra', 4, 6, 150, 3),
        (N'Fuerza', N'Peso muerto rumano', 4, 6, 180, 4),
        (N'Powerlifting', N'Sentadilla con barra', 5, 3, 240, 1),
        (N'Powerlifting', N'Press de banca', 5, 3, 240, 2),
        (N'Powerlifting', N'Peso muerto rumano', 4, 3, 240, 3),
        (N'Powerlifting', N'Plancha abdominal', 3, 45, 60, 4),
        (N'Cardio', N'Escaladores', 4, 30, 45, 1),
        (N'Cardio', N'Elevacion de talones', 4, 15, 45, 2),
        (N'Cardio', N'Zancadas', 4, 12, 60, 3),
        (N'Cardio', N'Plancha abdominal', 3, 45, 60, 4);

    INSERT INTO RutinaEjercicio
        (Series, Repeticiones, Peso, Descanso, Orden, Estado, IdRutina, IdEjercicio)
    SELECT d.Series, d.Repeticiones, NULL, d.Descanso, d.Orden, 1,
           r.IdRutina, e.IdEjercicio
    FROM @Detalle AS d
    INNER JOIN Rutina AS r ON r.Nombre = d.RutinaNombre AND r.IdEntrenador = @IdEntrenador
    INNER JOIN Ejercicio AS e ON e.Nombre = d.EjercicioNombre
    WHERE NOT EXISTS
    (
        SELECT 1 FROM RutinaEjercicio AS re
        WHERE re.IdRutina = r.IdRutina AND re.IdEjercicio = e.IdEjercicio AND re.Estado = 1
    );

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
