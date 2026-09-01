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
    IdSocio INT NOT NULL,
    IdEntrenador INT NOT NULL,

    CONSTRAINT CK_Rutina_Fechas
        CHECK (FechaFin IS NULL OR FechaInicio IS NULL OR FechaFin >= FechaInicio),

    CONSTRAINT FK_Rutina_Socio
        FOREIGN KEY (IdSocio) REFERENCES Socio(IdSocio),

    CONSTRAINT FK_Rutina_Entrenador
        FOREIGN KEY (IdEntrenador) REFERENCES UsuarioSistema(IdUsuarioSistema)
);
GO

/* Planes y membresías */

CREATE TABLE Plan (
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
        FOREIGN KEY (IdPlan) REFERENCES Plan(IdPlan),

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

/* Índices de consultas habituales */

CREATE INDEX IX_Membresia_IdSocio
    ON Membresia(IdSocio);

CREATE INDEX IX_Asistencia_IdSocio
    ON Asistencia(IdSocio);

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
    ('San', 'Martin', '30000001', NULL, NULL, 'admin',
        'ARGON2ID:19:65536:3:2:a8PBXlgeuWCgY5hXRxfEjA==:gFqIEcXhQHYZ2NEMCahoS5lWKE6IfOU0dQpeg1r+qPw=', 1, 1),
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
