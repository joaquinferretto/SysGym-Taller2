/* Creación de la base SysGymDB.
   Los comentarios de tablas, columnas e índices se aplican con DocumentarSysGymDB.sql. */

CREATE DATABASE SysGymDB;
GO

USE SysGymDB;
GO
SET QUOTED_IDENTIFIER ON;
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
    Salario DECIMAL(18,2) NOT NULL DEFAULT 0,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(500) NOT NULL,
    FotoRuta NVARCHAR(260) NULL,
    Sexo CHAR(1) NULL,
    Estado BIT NOT NULL DEFAULT 1,
    IdRol INT NOT NULL,
    IdUsuarioCreador INT NULL,

    CONSTRAINT CK_UsuarioSistema_Salario
        CHECK (Salario >= 0),

    CONSTRAINT FK_UsuarioSistema_Rol
        FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),

    CONSTRAINT FK_UsuarioSistema_Creador
        FOREIGN KEY (IdUsuarioCreador) REFERENCES UsuarioSistema(IdUsuarioSistema)
);
GO

/* Auditoria */
CREATE TABLE AuditoriaOperacion (
    IdAuditoria INT IDENTITY(1,1) PRIMARY KEY,
    FechaHora DATETIME2 NOT NULL,
    IdUsuario INT NOT NULL,
    UsuarioNombre NVARCHAR(201) NOT NULL,
    Rol NVARCHAR(50) NOT NULL,
    Operacion NVARCHAR(50) NOT NULL,
    Entidad NVARCHAR(50) NOT NULL,
    IdEntidad INT NOT NULL,
    Detalle NVARCHAR(1000) NOT NULL,
    CONSTRAINT FK_AuditoriaOperacion_Usuario
        FOREIGN KEY (IdUsuario) REFERENCES UsuarioSistema(IdUsuarioSistema)
);
CREATE INDEX IX_AuditoriaOperacion_FechaHora
    ON AuditoriaOperacion (FechaHora DESC, IdAuditoria DESC);
GO

/* Socios */

CREATE TABLE Socio (
    IdSocio INT IDENTITY(1,1) PRIMARY KEY,
    DNI NVARCHAR(20) NOT NULL UNIQUE,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATETIME2 NULL,
    FechaAlta DATETIME2 NULL, -- Fecha historica opcional.
    Peso DECIMAL(6,2) NULL,
    Altura DECIMAL(5,2) NULL,
    FotoRuta NVARCHAR(260) NULL,
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
    Objetivo NVARCHAR(30) NULL,
    Nivel NVARCHAR(20) NULL,
    IdEntrenador INT NOT NULL,

    CONSTRAINT CK_Rutina_Fechas
        CHECK (FechaFin IS NULL OR FechaInicio IS NULL OR FechaFin >= FechaInicio),
    CONSTRAINT CK_Rutina_Objetivo CHECK (Objetivo IS NULL OR Objetivo IN (N'Fuerza', N'Hipertrofia', N'Acondicionamiento')),
    CONSTRAINT CK_Rutina_Nivel CHECK (Nivel IS NULL OR Nivel IN (N'Principiante', N'Intermedio', N'Avanzado')),
    CONSTRAINT CK_Rutina_ModeloPar CHECK ((Objetivo IS NULL AND Nivel IS NULL) OR (Objetivo IS NOT NULL AND Nivel IS NOT NULL)),

    CONSTRAINT FK_Rutina_Entrenador
        FOREIGN KEY (IdEntrenador) REFERENCES UsuarioSistema(IdUsuarioSistema)
);
GO

/* Planes y membresias */

CREATE TABLE [Plan] (
    IdPlan INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(500) NULL,
    Precio DECIMAL(18,2) NOT NULL,
    Estado BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Plan_Precio
        CHECK (Precio > 0)
);
GO

CREATE TABLE EjercicioImagen (
    IdEjercicioImagen INT IDENTITY(1,1) PRIMARY KEY,
    IdEjercicio INT NOT NULL,
    RutaRelativa NVARCHAR(260) NOT NULL,
    Orden INT NOT NULL,

    CONSTRAINT CK_EjercicioImagen_Orden
        CHECK (Orden > 0),

    CONSTRAINT UQ_EjercicioImagen_Ruta
        UNIQUE (RutaRelativa),

    CONSTRAINT FK_EjercicioImagen_Ejercicio
        FOREIGN KEY (IdEjercicio) REFERENCES Ejercicio(IdEjercicio)
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
    IdRutina INT NULL,

    CONSTRAINT CK_Membresia_Fechas
        CHECK (FechaVencimiento >= FechaInicio),

    CONSTRAINT FK_Membresia_Plan
        FOREIGN KEY (IdPlan) REFERENCES [Plan](IdPlan),

    CONSTRAINT FK_Membresia_Socio
        FOREIGN KEY (IdSocio) REFERENCES Socio(IdSocio),

    CONSTRAINT FK_Membresia_UsuarioSistema
        FOREIGN KEY (IdUsuarioSistema) REFERENCES UsuarioSistema(IdUsuarioSistema),

    CONSTRAINT FK_Membresia_Rutina
        FOREIGN KEY (IdRutina) REFERENCES Rutina(IdRutina)
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

CREATE TABLE PagoEfectivo (
    IdPagoEfectivo INT IDENTITY(1,1) PRIMARY KEY,
    Estado BIT NOT NULL DEFAULT 1
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
    IdUsuarioRegistro INT NULL,
    IdCuotaMembresia INT NULL,

    CONSTRAINT CK_Pago_Importe
        CHECK (Importe > 0),

    CONSTRAINT CK_Pago_Estado
        CHECK (Estado IN ('Pendiente', 'Aprobado', 'Rechazado', 'Anulado', 'Reembolsado')),

    CONSTRAINT FK_Pago_MetodoPago
        FOREIGN KEY (IdMetodoPago) REFERENCES MetodoPago(IdMetodoPago),

    CONSTRAINT FK_Pago_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro) REFERENCES UsuarioSistema(IdUsuarioSistema)
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

/* La cuota ya referencia Pago; esta segunda FK se agrega después de crear ambas tablas. */
ALTER TABLE Pago WITH CHECK ADD CONSTRAINT FK_Pago_CuotaMembresia
    FOREIGN KEY (IdCuotaMembresia) REFERENCES CuotaMembresia(IdCuotaMembresia);
GO

/* RutinaEjercicio */

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

    /* Dias de rutina */
    CONSTRAINT CK_RutinaEjercicio_DiaSemana
        CHECK (DiaSemana IS NULL OR DiaSemana BETWEEN 1 AND 5),

    CONSTRAINT FK_RutinaEjercicio_Rutina
        FOREIGN KEY (IdRutina) REFERENCES Rutina(IdRutina),

    CONSTRAINT FK_RutinaEjercicio_Ejercicio
        FOREIGN KEY (IdEjercicio) REFERENCES Ejercicio(IdEjercicio)
);
GO

/* Índices: aceleran los recorridos habituales y protegen relaciones de pago.
   Las descripciones detalladas de cada índice se guardan como MS_Description. */

/* Membresías de un socio, incluidas sus membresías históricas. */
CREATE INDEX IX_Membresia_IdSocio
    ON Membresia(IdSocio);

/* Un pago vigente puede determinar como máximo una cuota; NULL no participa en la unicidad. */

CREATE UNIQUE INDEX UX_CuotaMembresia_IdRegistroPago
    ON CuotaMembresia(IdRegistroPago)
    WHERE IdRegistroPago IS NOT NULL;
GO

/* Búsqueda de todos los intentos asociados a una cuota; omite pagos no vinculados. */
CREATE INDEX IX_Pago_IdCuotaMembresia
    ON Pago(IdCuotaMembresia)
    WHERE IdCuotaMembresia IS NOT NULL;
GO

SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF OBJECT_ID(N'dbo.ConfiguracionSistema', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ConfiguracionSistema (
        IdConfiguracion INT NOT NULL CONSTRAINT PK_ConfiguracionSistema PRIMARY KEY,
        MaxCuotasVencidasPermitidas INT NOT NULL CONSTRAINT DF_Configuracion_MaxVencidas DEFAULT 2,
        MaxMesesAnticipacionCuotas INT NOT NULL CONSTRAINT DF_Configuracion_MaxAnticipacion DEFAULT 2,
        DiasAvisoVencimiento INT NOT NULL CONSTRAINT DF_Configuracion_DiasAviso DEFAULT 7,
        CONSTRAINT CK_Configuracion_Unica CHECK (IdConfiguracion = 1),
        CONSTRAINT CK_Configuracion_MaxVencidas CHECK (MaxCuotasVencidasPermitidas BETWEEN 1 AND 120),
        CONSTRAINT CK_Configuracion_MaxAnticipacion CHECK (MaxMesesAnticipacionCuotas BETWEEN 0 AND 120),
        CONSTRAINT CK_Configuracion_DiasAviso CHECK (DiasAvisoVencimiento BETWEEN 0 AND 365)
    );
END;
IF NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionSistema WHERE IdConfiguracion = 1)
    INSERT INTO dbo.ConfiguracionSistema (IdConfiguracion, MaxCuotasVencidasPermitidas, MaxMesesAnticipacionCuotas, DiasAvisoVencimiento)
    VALUES (1, 2, 2, 7);
COMMIT TRANSACTION;

GO
/* Roles iniciales; los usuarios y datos de ejemplo se cargan desde SysGymDatos.sql. */
INSERT INTO Rol (Descripcion, Estado)
VALUES
    (N'Administrador', 1),
    (N'Recepcionista', 1),
    (N'Entrenador', 1);
GO
