/* Creacion de la base */

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

/* Indices */

CREATE INDEX IX_Membresia_IdSocio
    ON Membresia(IdSocio);

/* Indice de cuotas */

CREATE UNIQUE INDEX UX_CuotaMembresia_IdRegistroPago
    ON CuotaMembresia(IdRegistroPago)
    WHERE IdRegistroPago IS NOT NULL;
GO

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
/* Datos iniciales */

INSERT INTO Rol (Descripcion, Estado)
VALUES
    (N'Administrador', 1),
    (N'Recepcionista', 1),
    (N'Entrenador', 1);

/* Usuarios del sistema */
INSERT INTO UsuarioSistema (Nombre, Apellido, DNI, Telefono, FechaNacimiento, Salario, Username, Password, FotoRuta, Sexo, Estado, IdRol)
VALUES
    (N'San', N'Martin', N'30000001', N'3794651082', '19840716', 1350000, N'san.martin', N'ARGON2ID:19:65536:3:2:3/HafUE7Hs7qtb19PEE5lg==:Ddmcc2XJ0UW2O6IAvOgE1dvLfpN0Q7ZGJQAMiex0+Ws=', NULL, N'M', 1, 1),
    (N'Klaus', N'Schneider', N'28451736', N'1147218365', '19800612', 950000, N'klaus.schneider', N'ARGON2ID:19:65536:3:2:H5Pvgpdu7mZ3PqfybaPqww==:YpexbCtYb9/FXFA5w6+i8bXtds9IN23ccIF8/mm0b40=', N'Imagenes\Usuarios\7e665f53c5f54ac1ba1192da2609e7b1.jpg', N'M', 1, 1),
    (N'Greta', N'Hoffmann', N'33617290', N'1158340172', '19880923', 720000, N'greta.hoffmann', N'ARGON2ID:19:65536:3:2:LakJj1tgK8KYhU4t6HKcZg==:q230nzMnE+8P3fx2DvS0Y4+MbXO6kuf4m26JdknhmZU=', N'Imagenes\Usuarios\1b5f13461f324ba59a3f927204fa637a.jpg', N'F', 1, 2),
    (N'Lukas', N'Becker', N'36072814', N'1163927458', '19920304', 810000, N'lukas.becker', N'ARGON2ID:19:65536:3:2:scAe6Ok6nEP/5KsIkIzG4g==:NA+/8oS/XNQqagO/6byvaDw28qT9RpQia6McWQN2V3w=', N'Imagenes\Usuarios\31c5235ceec7401cb94ab63107735658.jpg', N'M', 1, 3);

INSERT INTO PagoEfectivo (Estado)
VALUES (1);

INSERT INTO MercadoPago (ExternalReference)
VALUES (N'SYSGYM_MANUAL');

INSERT INTO MetodoPago (Estado, Observaciones, IdNroPagoMP, IdPagoEfectivo)
VALUES
    (1, N'Mercado Pago', 1, NULL),
    (1, N'Pago en efectivo', NULL, 1);

/* Ejercicios */
INSERT INTO Ejercicio (Nombre, Descripcion, Estado)
VALUES
    -- Pecho
    (N'Press de banca', N'Pecho, triceps y deltoides anterior.', 1),
    (N'Press inclinado con mancuernas', N'Pecho superior y estabilizadores.', 1),
    (N'Aperturas con mancuernas', N'Aislamiento de pectorales.', 1),
    (N'Fondos en paralelas', N'Pecho y triceps con peso corporal.', 1),
    (N'Press plano con mancuernas', N'Pecho con recorrido libre y estabilizadores.', 1),
    (N'Cruce en polea', N'Aislamiento de pectoral con tension continua.', 1),
    (N'Press declinado con barra', N'Pecho inferior y triceps.', 1),
    (N'Press inclinado con barra', N'Pecho superior con carga libre.', 1),
    (N'Contractora de pecho', N'Aislamiento de pectorales en maquina pec deck.', 1),
    (N'Flexiones de brazos', N'Pecho, triceps y zona media con peso corporal.', 1),
    -- Espalda
    (N'Jalon al pecho', N'Dorsales mediante polea alta.', 1),
    (N'Remo con barra', N'Espalda media, dorsales y biceps.', 1),
    (N'Remo sentado en polea', N'Dorsales y espalda media.', 1),
    (N'Face pull', N'Deltoides posterior y estabilizadores.', 1),
    (N'Dominadas', N'Dorsales y biceps con peso corporal.', 1),
    (N'Remo con mancuerna', N'Dorsal unilateral y espalda media.', 1),
    (N'Pullover en polea', N'Dorsal ancho con hombro extendido.', 1),
    (N'Peso muerto convencional', N'Cadena posterior completa.', 1),
    (N'Remo en T', N'Espalda media y dorsales con barra anclada.', 1),
    (N'Jalon con agarre cerrado', N'Dorsales con agarre neutro en polea alta.', 1),
    (N'Hiperextensiones lumbares', N'Zona lumbar, gluteos e isquiotibiales en banco romano.', 1),
    (N'Encogimientos para trapecio', N'Trapecio superior con mancuernas o barra.', 1),
    -- Hombros
    (N'Press militar', N'Hombros y triceps.', 1),
    (N'Elevaciones laterales', N'Deltoides lateral.', 1),
    (N'Press Arnold', N'Deltoides anterior y lateral con rotacion.', 1),
    (N'Elevaciones frontales', N'Deltoides anterior.', 1),
    (N'Pajaro con mancuernas', N'Deltoides posterior.', 1),
    (N'Press de hombros con mancuernas', N'Deltoides y triceps sentado con respaldo.', 1),
    (N'Press de hombros en maquina', N'Deltoides con recorrido guiado.', 1),
    (N'Remo al menton', N'Deltoides lateral y trapecio.', 1),
    -- Biceps, triceps y antebrazo
    (N'Curl de biceps con barra', N'Aislamiento de biceps.', 1),
    (N'Curl martillo', N'Biceps, braquial y antebrazo.', 1),
    (N'Curl en banco inclinado', N'Biceps con hombro extendido.', 1),
    (N'Curl concentrado', N'Aislamiento unilateral de biceps.', 1),
    (N'Curl en polea baja', N'Biceps con tension continua.', 1),
    (N'Curl en banco Scott', N'Biceps con brazo apoyado en banco predicador.', 1),
    (N'Extension de triceps en polea', N'Aislamiento de triceps.', 1),
    (N'Press frances', N'Triceps con codos fijos.', 1),
    (N'Patada de triceps', N'Aislamiento unilateral de triceps.', 1),
    (N'Fondos en banco', N'Triceps con peso corporal.', 1),
    (N'Extension de triceps sobre la cabeza', N'Cabeza larga del triceps con mancuerna o polea.', 1),
    (N'Press de banca agarre cerrado', N'Triceps y pecho con barra.', 1),
    (N'Curl de antebrazo', N'Flexores de la muneca con barra o mancuernas.', 1),
    -- Piernas y gluteos
    (N'Sentadilla con barra', N'Cuadriceps, gluteos y zona media.', 1),
    (N'Prensa de piernas', N'Cuadriceps y gluteos en maquina.', 1),
    (N'Peso muerto rumano', N'Isquiotibiales, gluteos y cadena posterior.', 1),
    (N'Extension de cuadriceps', N'Aislamiento de cuadriceps.', 1),
    (N'Curl femoral', N'Aislamiento de isquiotibiales.', 1),
    (N'Hip thrust', N'Extension de cadera con enfasis en gluteos.', 1),
    (N'Zancadas', N'Ejercicio unilateral de piernas.', 1),
    (N'Elevacion de talones', N'Gemelos y soleo.', 1),
    (N'Sentadilla bulgara', N'Cuadriceps y gluteos unilateral.', 1),
    (N'Abductores en maquina', N'Gluteo medio y abductores.', 1),
    (N'Sentadilla frontal', N'Cuadriceps y zona media con barra al frente.', 1),
    (N'Sentadilla hack', N'Cuadriceps con recorrido guiado en maquina.', 1),
    (N'Sentadilla goblet', N'Sentadilla con mancuerna o kettlebell al pecho.', 1),
    (N'Peso muerto sumo', N'Gluteos, aductores y cadena posterior con postura amplia.', 1),
    (N'Aductores en maquina', N'Aductores de cadera.', 1),
    (N'Subida al cajon', N'Cuadriceps y gluteos unilateral sobre cajon.', 1),
    (N'Puente de gluteos', N'Extension de cadera en el suelo.', 1),
    (N'Patada de gluteo en polea', N'Aislamiento de gluteo mayor.', 1),
    (N'Elevacion de talones sentado', N'Soleo en maquina sentado.', 1),
    -- Zona media
    (N'Plancha abdominal', N'Estabilidad de la zona media.', 1),
    (N'Crunch abdominal', N'Flexion controlada para abdominales.', 1),
    (N'Escaladores', N'Acondicionamiento y zona media.', 1),
    (N'Elevacion de piernas colgado', N'Abdomen inferior.', 1),
    (N'Rueda abdominal', N'Zona media con antiextension.', 1),
    (N'Giro ruso', N'Oblicuos y rotacion de tronco.', 1),
    (N'Plancha lateral', N'Oblicuos y estabilidad lateral.', 1),
    (N'Crunch en polea', N'Abdominales con carga en polea alta.', 1),
    (N'Bicho muerto', N'Control lumbopelvico y abdomen profundo.', 1),
    -- Cardio y acondicionamiento
    (N'Cinta de correr', N'Caminata o trote en cinta.', 1),
    (N'Bicicleta fija', N'Cardio de bajo impacto en bicicleta.', 1),
    (N'Eliptica', N'Cardio de bajo impacto con brazos y piernas.', 1),
    (N'Remo ergometro', N'Cardio de cuerpo completo en maquina de remo.', 1),
    (N'Escaladora', N'Subida continua de escalones en maquina.', 1),
    (N'Salto con soga', N'Coordinacion y resistencia cardiovascular.', 1),
    (N'Burpees', N'Acondicionamiento de cuerpo completo.', 1),
    (N'Salto al cajon', N'Potencia de piernas con salto.', 1),
    (N'Swing con kettlebell', N'Extension explosiva de cadera.', 1),
    (N'Sogas de batalla', N'Resistencia de tren superior y cardio.', 1),
    (N'Caminata del granjero', N'Agarre, trapecio y zona media cargando peso.', 1);

/* Imagenes de ejercicios */
INSERT INTO EjercicioImagen (IdEjercicio, RutaRelativa, Orden)
VALUES
        ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca'), N'Imagenes\Ejercicios\1\a77d71e063e147a1addfbf5434c62d7e.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca'), N'Imagenes\Ejercicios\1\1eaa56f2a078406caff66f4763c6bc92.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca'), N'Imagenes\Ejercicios\1\a330c06675024d589dc37bef03f8dde6.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas'), N'Imagenes\Ejercicios\2\6417327a7ed44808b674ebb36ca0d279.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas'), N'Imagenes\Ejercicios\2\76bc5f83831f4b378a4edd366e1f636e.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas'), N'Imagenes\Ejercicios\2\6fc7983111df481bbf6629ca9929c52e.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas'), N'Imagenes\Ejercicios\3\dcdecc3939aa4c05b77e9fe91c13a0e2.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas'), N'Imagenes\Ejercicios\3\7b5bac744b0a4f74887f378976d88ec9.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas'), N'Imagenes\Ejercicios\3\196d776401c64f2bb8dd77b02b33bfcc.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en paralelas'), N'Imagenes\Ejercicios\4\a6f25d89e77849fda3134aba1f4136cc.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en paralelas'), N'Imagenes\Ejercicios\4\1347b8020b2c4855b116fc3210c1e7b2.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en paralelas'), N'Imagenes\Ejercicios\4\7c2fc78a83624cf783eb3876345ba0d1.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas'), N'Imagenes\Ejercicios\5\c04dbee37a8b48a4b27135f6d64d4d9f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas'), N'Imagenes\Ejercicios\5\4830c202ec5c4285ae6c401284fd0862.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas'), N'Imagenes\Ejercicios\5\ddc0818e747440fe8c26cee8ea53c69f.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea'), N'Imagenes\Ejercicios\6\0a050caff3bb443cbf8941bbe18f88e5.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea'), N'Imagenes\Ejercicios\6\52c21518e2744506beb494c05b2f57a8.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cruce en polea'), N'Imagenes\Ejercicios\6\081dd93a4c424e03bf3b6ce058c383c4.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra'), N'Imagenes\Ejercicios\7\c36efa0b58b446ca8199a4e4be112701.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra'), N'Imagenes\Ejercicios\7\5898e28ae60f4312b6830ba2e82dde6a.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra'), N'Imagenes\Ejercicios\7\99699e2f244c4231a3780a70296d3571.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con barra'), N'Imagenes\Ejercicios\8\01fadddcc67c4eea8bf3387c455afa91.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con barra'), N'Imagenes\Ejercicios\8\f225e388215940599d56f47e0848732f.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con barra'), N'Imagenes\Ejercicios\8\fd90109303c240739c424722b51b89c0.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Contractora de pecho'), N'Imagenes\Ejercicios\9\4f7b7ad2c00649a6bd78f8a9c4e857de.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Contractora de pecho'), N'Imagenes\Ejercicios\9\650685969af14a10848623323799bf6c.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Contractora de pecho'), N'Imagenes\Ejercicios\9\e02f4866642c46e998c137f685dd5872.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Flexiones de brazos'), N'Imagenes\Ejercicios\10\4083dc90ff4547f3b7b8cdf61336828a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Flexiones de brazos'), N'Imagenes\Ejercicios\10\386faca6b1dc444593390e09bca7e8e2.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Flexiones de brazos'), N'Imagenes\Ejercicios\10\d9a9748ed0d14ae2a50f71365e882e91.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho'), N'Imagenes\Ejercicios\11\77f528dbac0f4687a7063f6925a54c17.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho'), N'Imagenes\Ejercicios\11\3797cc7703ca40658b16f4f72bdf2130.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho'), N'Imagenes\Ejercicios\11\39405257e6f7496988a72ce05cf2e498.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra'), N'Imagenes\Ejercicios\12\1169cf1f4f9c41ff9e113b82282a925f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra'), N'Imagenes\Ejercicios\12\aee77cff0c0246469c59dccb20dd0703.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra'), N'Imagenes\Ejercicios\12\2c7c04317a864e9688014262cd06f8b0.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea'), N'Imagenes\Ejercicios\13\02b7da6becc8411a9332bf6f76eca253.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea'), N'Imagenes\Ejercicios\13\774948f3b8364fc297f38787ebfd7c14.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea'), N'Imagenes\Ejercicios\13\6a3f2d1109b34a40bb38f00994d68027.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Face pull'), N'Imagenes\Ejercicios\14\8688c182723c455086b418ca6f6f98ec.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Face pull'), N'Imagenes\Ejercicios\14\4d7846c2c6734baeb53f68746eb378fa.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Face pull'), N'Imagenes\Ejercicios\14\ed26a336f82541dda268b093dd36181e.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas'), N'Imagenes\Ejercicios\15\24989379f0ec41a8ba3495b990e2c60b.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas'), N'Imagenes\Ejercicios\15\df3b893a156d4424ac8582b2ef03ea8e.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas'), N'Imagenes\Ejercicios\15\196e654690844893b21ef94b967b3bcc.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna'), N'Imagenes\Ejercicios\16\910856d804d743bcb8129159253449cd.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna'), N'Imagenes\Ejercicios\16\38065f4302b145e38be3aa9f3a6632e2.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna'), N'Imagenes\Ejercicios\16\3778df020d5f4174b5293acbef9e7317.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea'), N'Imagenes\Ejercicios\17\1ae21b7dbc4e4e409e9f3256eebf5aed.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional'), N'Imagenes\Ejercicios\18\1140d889b4b44212b490a7881c8d2b18.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional'), N'Imagenes\Ejercicios\18\1d7db49d934f40d8be7f3b8c5e837c5e.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional'), N'Imagenes\Ejercicios\18\cea40f54ad83479f86eb095b04f86a02.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo en T'), N'Imagenes\Ejercicios\19\2673dd889a364886b5fee9c358ae7d4e.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo en T'), N'Imagenes\Ejercicios\19\525c7771bbe248cc9a288b225b846776.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo en T'), N'Imagenes\Ejercicios\19\97492a13f5684c02b2c9abbc0851873d.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon con agarre cerrado'), N'Imagenes\Ejercicios\20\b521556117704ae089884f79e6c71f80.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon con agarre cerrado'), N'Imagenes\Ejercicios\20\82f4cf475faa4c659ae7a154f722ebd1.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon con agarre cerrado'), N'Imagenes\Ejercicios\20\2c92816e97b44498a873c38cc23e7ba2.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hiperextensiones lumbares'), N'Imagenes\Ejercicios\21\79b11cdaa7ac48bc92d3e002e4d2fbc0.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hiperextensiones lumbares'), N'Imagenes\Ejercicios\21\b6f094a553ab4d9eb16d52e29c442d1f.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hiperextensiones lumbares'), N'Imagenes\Ejercicios\21\439bee90a13a40a88cf1c8011aa6f926.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Encogimientos para trapecio'), N'Imagenes\Ejercicios\22\07c77c184fa246669cf94c076db182e8.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Encogimientos para trapecio'), N'Imagenes\Ejercicios\22\95fd89c23a9b4c0993db3860b3604a05.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Encogimientos para trapecio'), N'Imagenes\Ejercicios\22\93d529b499ed44f5a9da173513530dbe.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar'), N'Imagenes\Ejercicios\23\7d9f5b1752d94cd6bd3d1bc407c71681.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar'), N'Imagenes\Ejercicios\23\b6aefe9f9a9b48d7acbb367a4329fa96.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar'), N'Imagenes\Ejercicios\23\14ab734265ba40ab8094f793b4e19e31.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales'), N'Imagenes\Ejercicios\24\d5517455e5c14289bd849340e023e64d.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales'), N'Imagenes\Ejercicios\24\e39712f064a84a618b838938b5fa13b4.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales'), N'Imagenes\Ejercicios\24\f58f292f2f304301a5549fcbff41967c.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold'), N'Imagenes\Ejercicios\25\006a9356e405454e9b9dbc4b557f0606.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold'), N'Imagenes\Ejercicios\25\327f55368e434cfe83c43d539d6ee0d9.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold'), N'Imagenes\Ejercicios\25\d223383d3fac462f8fea1578ea927752.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones frontales'), N'Imagenes\Ejercicios\26\ed5962884a764a66abab095eb31dad55.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones frontales'), N'Imagenes\Ejercicios\26\f2af3bdff2f1495b917953b1bbd513e0.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones frontales'), N'Imagenes\Ejercicios\26\253736e51b1147b8b9927df485c5bb1b.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas'), N'Imagenes\Ejercicios\27\92bac0388be14614b5665fbff0e06b1c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas'), N'Imagenes\Ejercicios\27\e85519bb9b924082bae3dc15d18b88c7.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas'), N'Imagenes\Ejercicios\27\68854d05b6d0494a9fcb08dbebdafaa8.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros con mancuernas'), N'Imagenes\Ejercicios\28\20b41b4652954fe59db148bfa79c1487.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros con mancuernas'), N'Imagenes\Ejercicios\28\7de2b0a5c4e04bc695857e9c88952f90.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros con mancuernas'), N'Imagenes\Ejercicios\28\507b68613f684bf8a6042ab5a12a524c.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros en maquina'), N'Imagenes\Ejercicios\29\0008c1228a8343a6b0f4278bed8f062a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros en maquina'), N'Imagenes\Ejercicios\29\7d2e952025484b718f89eda066cf23c0.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros en maquina'), N'Imagenes\Ejercicios\29\6160502720cb4468aa07b595baa8fbb4.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo al menton'), N'Imagenes\Ejercicios\30\af4446b996144d2ea90980669efc350b.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo al menton'), N'Imagenes\Ejercicios\30\fc3fcae315f74cc0a048cfefcb9e7360.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo al menton'), N'Imagenes\Ejercicios\30\adeb08f2ed304e8393bea726ddd750ee.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra'), N'Imagenes\Ejercicios\31\188d3a421a8e49458f5fccda94e79c19.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra'), N'Imagenes\Ejercicios\31\7237143dc66c4d5eb7d420dd4a02eab2.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra'), N'Imagenes\Ejercicios\31\36d3b11cfa5741e8a4b4886fe9c84b4b.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo'), N'Imagenes\Ejercicios\32\68af05a8bddb4ccd9bb6f92e57fda822.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo'), N'Imagenes\Ejercicios\32\427be59ca47544b3a264375f87e0845d.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo'), N'Imagenes\Ejercicios\32\d5f96e56587b43adacec06b1144b7100.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado'), N'Imagenes\Ejercicios\33\0a8c01f4167d42d39d0954ac08afa4a0.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado'), N'Imagenes\Ejercicios\33\99ef265d51b1457e851ad9b75b89a702.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco inclinado'), N'Imagenes\Ejercicios\33\d040c697f2684f8098f8255790e910ad.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl concentrado'), N'Imagenes\Ejercicios\34\ad580af555884e36b37730477e7218a5.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl concentrado'), N'Imagenes\Ejercicios\34\de8b21f27dfb46938ac5a7872555f20d.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl concentrado'), N'Imagenes\Ejercicios\34\d56df006905b4fdf93dd6df66d962a52.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en polea baja'), N'Imagenes\Ejercicios\35\1e659e4ed3d1438c9be2bebc23ec5c0a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en polea baja'), N'Imagenes\Ejercicios\35\61c44ba5cce948b28bb3afdb6b61de93.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en polea baja'), N'Imagenes\Ejercicios\35\a6717fa0295a402e866c196fc20ba9a7.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco Scott'), N'Imagenes\Ejercicios\36\73c345ee743648b9a4cdb5ff6f8e0ba6.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco Scott'), N'Imagenes\Ejercicios\36\96b01413ef7347d194885e9ba9d11f51.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco Scott'), N'Imagenes\Ejercicios\36\d3fbf87f11d14dafbc63ee56d400cc8a.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea'), N'Imagenes\Ejercicios\37\0ed79d02dad1414685486b2a434bb354.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea'), N'Imagenes\Ejercicios\37\92d4649effc74f4180045c86906f08c6.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea'), N'Imagenes\Ejercicios\37\d9f982fc170e4214aed6896dbed7dde4.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances'), N'Imagenes\Ejercicios\38\02910fd1a76d4e48aa369d9663de107f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances'), N'Imagenes\Ejercicios\38\5bac8e54da824d869fae5faea4c4739b.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances'), N'Imagenes\Ejercicios\38\99c94885d0c34f16ae90aa7ff459d152.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps'), N'Imagenes\Ejercicios\39\677cc07ed7b64012bfaf0ae24685b95a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps'), N'Imagenes\Ejercicios\39\f3a3f5d151964c98ab8e00df20c0208a.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps'), N'Imagenes\Ejercicios\39\3c7f81957a554a3b8795fcea46f17eee.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en banco'), N'Imagenes\Ejercicios\40\a05bcc5929474dbaa1ea72965e1ffc8d.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en banco'), N'Imagenes\Ejercicios\40\eeeaaacd936e4535889ef45b1f4b3f61.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en banco'), N'Imagenes\Ejercicios\40\fa38787b743e4c2782907137d6bcaf96.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps sobre la cabeza'), N'Imagenes\Ejercicios\41\57e9f9bb0fff4335bc6cd071c82dbd40.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps sobre la cabeza'), N'Imagenes\Ejercicios\41\38ec16385e284a1e852f50b0a40f73fb.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps sobre la cabeza'), N'Imagenes\Ejercicios\41\d186654606014f87999e2a63acb1fdeb.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca agarre cerrado'), N'Imagenes\Ejercicios\42\b523a181f43d4e59a86674ceb1f34419.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca agarre cerrado'), N'Imagenes\Ejercicios\42\f358815f2d4e464aa51b6208f4c8b670.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca agarre cerrado'), N'Imagenes\Ejercicios\42\163c5e0ba0f5454baadf5d0cf1fedf2d.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de antebrazo'), N'Imagenes\Ejercicios\43\f3929d42a87d41669badde5769351873.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra'), N'Imagenes\Ejercicios\44\e2af9cae689d4f1f9ec43be0a4335d63.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra'), N'Imagenes\Ejercicios\44\ea6c80631f47459f8c37ff66e2e2b91f.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra'), N'Imagenes\Ejercicios\44\a886f7ffbb64451095c03523500c4ce8.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas'), N'Imagenes\Ejercicios\45\8d4cefb357314eba8ffb6e3b9d5e48ea.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas'), N'Imagenes\Ejercicios\45\3773641f4dbf46b896ff44cf6b60e7dd.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas'), N'Imagenes\Ejercicios\45\bfec493744b548588325cacdc11f3d33.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano'), N'Imagenes\Ejercicios\46\e57fb227bdbb488d8a700fd7ecadb63c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano'), N'Imagenes\Ejercicios\46\67e4bcad45094366bccdc9ce82a581ea.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano'), N'Imagenes\Ejercicios\46\e87c9cb7339346efa43b1473a65e8039.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps'), N'Imagenes\Ejercicios\47\0f3404f0fb6a4bc98b760b7ecf8ab51c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps'), N'Imagenes\Ejercicios\47\8f4a44ddcd7c4664b309cfbb38cb2947.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps'), N'Imagenes\Ejercicios\47\f43831b87a464d7f9e44a72bf3cd0dff.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral'), N'Imagenes\Ejercicios\48\8297f4d73a0b4a6d89cb368b4491314c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral'), N'Imagenes\Ejercicios\48\6da58a693aed423c9bf24f7e2848ddb1.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl femoral'), N'Imagenes\Ejercicios\48\13211114998144dea9c16ec03d1fce1c.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust'), N'Imagenes\Ejercicios\49\ce0fa5b69ba4498bbf6cbfc8a423aefe.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust'), N'Imagenes\Ejercicios\49\27630f4de4384e52a3b62debd1c2a559.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust'), N'Imagenes\Ejercicios\49\b6df2fd8eb3c4a47bb288727246419a6.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas'), N'Imagenes\Ejercicios\50\a6c3c6bbc6dc405da77052792474aa27.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas'), N'Imagenes\Ejercicios\50\8e20e726d2dc491b98747aa61dfc6300.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas'), N'Imagenes\Ejercicios\50\e17050a6dbf34521b586bfd1661a3fac.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones'), N'Imagenes\Ejercicios\51\27bfaf23150a45118f23cf3b3a739f61.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones'), N'Imagenes\Ejercicios\51\d60aeb87a8814a25b10fd6605d3fdf67.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones'), N'Imagenes\Ejercicios\51\d76ad4cb817b4658a974e68460dad0a8.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara'), N'Imagenes\Ejercicios\52\63f528afeb0f49c6be9a524a4d3784eb.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara'), N'Imagenes\Ejercicios\52\5148c278db9c41c38bbc8f480d8958c6.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara'), N'Imagenes\Ejercicios\52\6b44e7213bd94013bef5ed221720877f.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Abductores en maquina'), N'Imagenes\Ejercicios\53\6ca343660b3543598cd18bf6fe37aea1.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Abductores en maquina'), N'Imagenes\Ejercicios\53\c800c72f3aa44a8ba323d5bd4047a86c.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Abductores en maquina'), N'Imagenes\Ejercicios\53\8fed62367ef244fba331ab764a44eb8a.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla frontal'), N'Imagenes\Ejercicios\54\668507c737284e4fbaa04df562412ff9.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla frontal'), N'Imagenes\Ejercicios\54\5968b620c5cd4234984ef6ba3f228681.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla frontal'), N'Imagenes\Ejercicios\54\c76a7e7d32764a4cb429b2b36adb1b14.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla hack'), N'Imagenes\Ejercicios\55\fdbf34c8b75940e0abf303e9b71d66b5.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla hack'), N'Imagenes\Ejercicios\55\4fd1211d31a34d9d90e779c16fe1d444.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla hack'), N'Imagenes\Ejercicios\55\3fc019c7b2314c3c9070357e4e655bb2.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla goblet'), N'Imagenes\Ejercicios\56\ffb513c895b6430b80ba22cc6cad422e.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla goblet'), N'Imagenes\Ejercicios\56\ecd0849e15e344619faa4f965ceed5f3.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla goblet'), N'Imagenes\Ejercicios\56\9238ea035d5b4deba04b6e3e8349eb7f.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto sumo'), N'Imagenes\Ejercicios\57\4364ca3c3af640edbadd52d209f0ce1f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto sumo'), N'Imagenes\Ejercicios\57\067a590530434ca582364c430c0fa6fd.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto sumo'), N'Imagenes\Ejercicios\57\336e1fbd7ed84f54b6f531a480102db1.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aductores en maquina'), N'Imagenes\Ejercicios\58\35e17897995d4a70817d1ecf6cb6e987.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aductores en maquina'), N'Imagenes\Ejercicios\58\8179abc3f8cf4469a94cc15ba8104ecd.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aductores en maquina'), N'Imagenes\Ejercicios\58\40b051beb56e4f4499727635f6f616a8.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Subida al cajon'), N'Imagenes\Ejercicios\59\09e5d0543b044d34bb758a2a22e15a26.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Subida al cajon'), N'Imagenes\Ejercicios\59\eae8b541499943ecbafd6c3fc5536087.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Puente de gluteos'), N'Imagenes\Ejercicios\60\0c60ab3ce4044f8cbf298218154512f2.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Puente de gluteos'), N'Imagenes\Ejercicios\60\15717101ae6f4db3a7055e0f1751e13d.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Puente de gluteos'), N'Imagenes\Ejercicios\60\8a068a67485649e1b6bc69d75e2d3607.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de gluteo en polea'), N'Imagenes\Ejercicios\61\01e3c97a00dc49959791a3d722907d3a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de gluteo en polea'), N'Imagenes\Ejercicios\61\6fe422c47a264f9ca85f887a99d50543.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de gluteo en polea'), N'Imagenes\Ejercicios\61\ed1dcaa0204545e787d5ada84f2e89a9.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones sentado'), N'Imagenes\Ejercicios\62\aff5c3bafece440b8c4f1e742b437fb5.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones sentado'), N'Imagenes\Ejercicios\62\04df4d92b27442fa8bde2cb6667d609d.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones sentado'), N'Imagenes\Ejercicios\62\abbf0de723d14ce1a4090de9941e3fae.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal'), N'Imagenes\Ejercicios\63\c0695827a8bb4df3bf244797f2a925b1.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal'), N'Imagenes\Ejercicios\64\2bea747e6a374e45a533e256c1f2688d.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal'), N'Imagenes\Ejercicios\64\3b0e81d7b6014027b1ca2a87c1fa2163.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch abdominal'), N'Imagenes\Ejercicios\64\e2752cb137a04312807648b2a645fe21.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores'), N'Imagenes\Ejercicios\65\1401831e11334ed0a26eec0f198693a4.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores'), N'Imagenes\Ejercicios\65\2af02f79fe3147f4a33af9db53c8ce04.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores'), N'Imagenes\Ejercicios\65\bd03e4e4f11543ebb6a3fb11fed44cd7.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado'), N'Imagenes\Ejercicios\66\6f877ac54dd74b95bc96d21456af4ebf.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado'), N'Imagenes\Ejercicios\66\fc417ed979e44efaa021a6d5e410e936.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado'), N'Imagenes\Ejercicios\66\01b3344b9b104d358144026cc1c21032.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Rueda abdominal'), N'Imagenes\Ejercicios\67\705df799b28243e2ae55d72ab3a9fc78.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Giro ruso'), N'Imagenes\Ejercicios\68\faab029a28d44601a8f466dd4e9a2929.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Giro ruso'), N'Imagenes\Ejercicios\68\3b4bf10039694b3c820babc6cc8c3feb.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Giro ruso'), N'Imagenes\Ejercicios\68\552632a205c04281b6dfba24eb8d6771.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha lateral'), N'Imagenes\Ejercicios\69\716e5e48b36a4667b6e0cae4bcd5a4b0.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch en polea'), N'Imagenes\Ejercicios\70\ec039a8000f54cd9826f658ae58378f8.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Crunch en polea'), N'Imagenes\Ejercicios\70\83abb110b1d44cde92c4431e2cf684f9.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Bicho muerto'), N'Imagenes\Ejercicios\71\dc53c60fb737498facf3a33cee3c5d5b.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Bicho muerto'), N'Imagenes\Ejercicios\71\5a5a88c4ff924bbf952aa20731dfe48d.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Bicho muerto'), N'Imagenes\Ejercicios\71\a1c637d6637f4bb1bfbcdf6c0f672312.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cinta de correr'), N'Imagenes\Ejercicios\72\ff69c5b54d004df189a4ef1855b35dc0.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Bicicleta fija'), N'Imagenes\Ejercicios\73\853e93dffa484db0b8a19059ad796ed6.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Eliptica'), N'Imagenes\Ejercicios\74\f00a8e379b4b4f439476c7d34482f375.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Eliptica'), N'Imagenes\Ejercicios\74\0aa5f1e1af2844b499c7459c3102170e.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Eliptica'), N'Imagenes\Ejercicios\74\26ea688f332d49b8b4aa029884a6f66a.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo ergometro'), N'Imagenes\Ejercicios\75\ceb2e2ff4e1e47fd99e42a008d00fcd9.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo ergometro'), N'Imagenes\Ejercicios\75\c4e53791a08946b3b38da864344dffab.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo ergometro'), N'Imagenes\Ejercicios\75\9adea8f6d40a49d787c3f610964a5521.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladora'), N'Imagenes\Ejercicios\76\6ad161ff365e4e3d95c66a94ff230ab7.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladora'), N'Imagenes\Ejercicios\76\14494a9b87a94b099364bd572f8a97c9.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladora'), N'Imagenes\Ejercicios\76\702b1e281f314aafa5928f5d699bd8e1.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Salto con soga'), N'Imagenes\Ejercicios\77\47ff881cc00c427faee2cdfc4e27195e.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Salto con soga'), N'Imagenes\Ejercicios\77\12a5428a126b41f8aa963787a98c8af7.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Salto con soga'), N'Imagenes\Ejercicios\77\840e40a39aa8406c93d97c8f4bcd45d1.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Burpees'), N'Imagenes\Ejercicios\78\d5a3ed964f554bcba3c77cbe8dd51f04.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Burpees'), N'Imagenes\Ejercicios\78\7defdeaa94984fab97e5b34c912b14d2.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Burpees'), N'Imagenes\Ejercicios\78\cffd9496b59d49868d3037ac0ba0404b.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Salto al cajon'), N'Imagenes\Ejercicios\79\832da073532b453d87230587afa97ba3.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Swing con kettlebell'), N'Imagenes\Ejercicios\80\16fb350d131b4f5db1b2c69fabff77e8.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Swing con kettlebell'), N'Imagenes\Ejercicios\80\2154e7888a024e18bad365332e529427.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Swing con kettlebell'), N'Imagenes\Ejercicios\80\dcd16089ccc142e4a3e9c4eb4ff5e27d.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sogas de batalla'), N'Imagenes\Ejercicios\81\b6a61ae0116f4cba945a7790131ba289.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sogas de batalla'), N'Imagenes\Ejercicios\81\f3bf7c6bbf48489daf1ba0cd8b6fe7bc.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sogas de batalla'), N'Imagenes\Ejercicios\81\8614e347ebe14c72af8d4576d7a0bce4.jpg', 3),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Caminata del granjero'), N'Imagenes\Ejercicios\82\0f6279ceb9e94b1cb63e064a72b440fc.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Caminata del granjero'), N'Imagenes\Ejercicios\82\fdcbec6bbb62494790209da8f69523a8.jpg', 2)
;

/* Rutinas */
INSERT INTO Rutina (Nombre, Descripcion, FechaCreacion, FechaInicio, FechaFin, Estado, IdEntrenador)
VALUES
    (N'Comienzo 1', N'Adaptacion general para iniciar el entrenamiento.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Hipertrofia 1', N'Volumen moderado para desarrollar masa muscular.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Hipertrofia 2', N'Progresion de hipertrofia con mayor volumen.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Fuerza', N'Mejora de fuerza con ejercicios compuestos.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Powerlifting', N'Sentadilla, press banca y peso muerto.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Cardio', N'Entrenamiento cardiovascular y acondicionamiento.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Pecho con barra', N'Trabajo de pectorales con press de banca.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Pecho inclinado', N'Trabajo inclinado con mancuernas.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Aperturas de pecho', N'Aislamiento de pectorales con mancuernas.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Fondos de tren superior', N'Fondos con peso corporal.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Espalda en polea', N'Jalones para el trabajo de dorsales.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Remo de fuerza', N'Remo con barra y tecnica controlada.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Espalda controlada', N'Remo sentado en polea.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Estabilidad de hombros', N'Trabajo posterior del hombro con face pull.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Press de hombros', N'Press militar para hombros y triceps.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Hombros laterales', N'Elevaciones laterales con carga moderada.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Biceps con barra', N'Curl de biceps con barra.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Brazos con mancuernas', N'Curl martillo para brazos y antebrazos.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Triceps en polea', N'Extensiones de triceps con polea.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Sentadilla inicial', N'Practica de sentadilla con barra.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Piernas en prensa', N'Trabajo de cuadriceps y gluteos en prensa.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Cadena posterior', N'Peso muerto rumano para cadena posterior.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Cuadriceps en maquina', N'Extension de cuadriceps en maquina.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Isquiotibiales', N'Flexion de rodilla con curl femoral.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Gluteos en banco', N'Extension de cadera con hip thrust.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Piernas unilaterales', N'Zancadas para trabajo unilateral.', DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker'));

/* Nueve rutinas modelo: objetivo por nivel. Las rutinas anteriores quedan libres. */
SET IDENTITY_INSERT Rutina ON;
INSERT INTO Rutina (IdRutina, Nombre, Descripcion, FechaCreacion, FechaInicio, FechaFin, Estado, Objetivo, Nivel, IdEntrenador)
VALUES
    (2001, N'Fuerza – Principiante', N'Ejercicios compuestos con pocas repeticiones y descansos amplios.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Fuerza', N'Principiante', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2002, N'Fuerza – Intermedio', N'Ejercicios compuestos con pocas repeticiones y descansos amplios.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Fuerza', N'Intermedio', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2003, N'Fuerza – Avanzado', N'Ejercicios compuestos con pocas repeticiones y descansos amplios.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Fuerza', N'Avanzado', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2004, N'Hipertrofia – Principiante', N'Volumen progresivo de 8 a 12 repeticiones.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Hipertrofia', N'Principiante', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2005, N'Hipertrofia – Intermedio', N'Volumen progresivo de 8 a 12 repeticiones.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Hipertrofia', N'Intermedio', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2006, N'Hipertrofia – Avanzado', N'Volumen progresivo de 8 a 12 repeticiones.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Hipertrofia', N'Avanzado', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2007, N'Acondicionamiento – Principiante', N'Circuitos de cuerpo completo y trabajo cardiovascular.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Acondicionamiento', N'Principiante', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2008, N'Acondicionamiento – Intermedio', N'Circuitos de cuerpo completo y trabajo cardiovascular.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Acondicionamiento', N'Intermedio', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (2009, N'Acondicionamiento – Avanzado', N'Circuitos de cuerpo completo y trabajo cardiovascular.', CAST(GETDATE() AS date), NULL, NULL, 1, N'Acondicionamiento', N'Avanzado', (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker'));
SET IDENTITY_INSERT Rutina OFF;

/* Socios */
INSERT INTO Socio (DNI, Nombre, Apellido, FechaNacimiento, FechaAlta, Peso, Altura, FotoRuta, Sexo, Estado)
VALUES
    (N'38514072', N'Marco', N'Rossi', '19900412', DATEADD(DAY, 30, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 82.50, 1.78, NULL, N'M', 1),
    (N'39862145', N'Giulia', N'Lombardi', '19950723', DATEADD(DAY, 27, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 58.00, 1.64, NULL, N'F', 1),
    (N'33419587', N'Luca', N'Bianchi', '19871102', DATEADD(DAY, 24, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 88.00, 1.82, NULL, N'M', 1),
    (N'37590264', N'Francesca', N'Moretti', '19930215', DATEADD(DAY, 21, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 61.50, 1.67, NULL, N'F', 1),
    (N'31047826', N'Giovanni', N'Romano', '19840930', DATEADD(DAY, 18, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 90.20, 1.80, NULL, N'M', 1),
    (N'41286093', N'Chiara', N'Barbieri', '19980508', DATEADD(DAY, 15, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 55.30, 1.60, NULL, N'F', 1),
    (N'36158402', N'Alessandro', N'Colombo', '19911219', DATEADD(DAY, 12, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 77.80, 1.75, NULL, N'M', 1),
    (N'42671358', N'Sofia', N'Fontana', '20000327', DATEADD(DAY, 9, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 52.40, 1.62, NULL, N'F', 1),
    (N'40135729', N'Matteo', N'Ricci', '19960805', DATEADD(DAY, 6, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 74.60, 1.76, NULL, N'M', 1),
    (N'34728016', N'Martina', N'Santoro', '19891014', DATEADD(DAY, 3, DATEADD(MONTH, -2, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 63.00, 1.69, NULL, N'F', 1),
    (N'29864103', N'Francesco', N'Marino', '19820121', DATEADD(DAY, 30, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 95.40, 1.84, NULL, N'M', 1),
    (N'40592871', N'Alessia', N'Mariani', '19970611', DATEADD(DAY, 27, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 57.90, 1.63, NULL, N'F', 1),
    (N'44306758', N'Lorenzo', N'Greco', '20020228', DATEADD(DAY, 24, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 70.10, 1.79, NULL, N'M', 1),
    (N'32150947', N'Elena', N'Rinaldi', '19861203', DATEADD(DAY, 21, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 66.20, 1.70, NULL, N'F', 1),
    (N'38047615', N'Andrea', N'Bruno', '19940417', DATEADD(DAY, 18, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 81.00, 1.77, NULL, N'M', 1),
    (N'41938260', N'Valentina', N'Caruso', '19990909', DATEADD(DAY, 15, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 54.70, 1.58, NULL, N'F', 1),
    (N'33875104', N'Davide', N'Gallo', '19880726', DATEADD(DAY, 12, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 86.30, 1.81, NULL, N'M', 1),
    (N'43219587', N'Sara', N'Ferrara', '20010130', DATEADD(DAY, 9, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 59.60, 1.66, NULL, N'F', 1),
    (N'37014896', N'Simone', N'Conti', '19920522', DATEADD(DAY, 6, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 79.50, 1.74, NULL, N'M', 1),
    (N'35486120', N'Beatrice', N'Galli', '19901118', DATEADD(DAY, 3, DATEADD(MONTH, -3, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 62.80, 1.68, NULL, N'F', 1),
    (N'31605279', N'Stefano', N'De Luca', '19850306', DATEADD(DAY, 29, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 92.70, 1.83, NULL, N'M', 1),
    (N'45128063', N'Aurora', N'Martini', '20030814', DATEADD(DAY, 26, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 51.20, 1.61, NULL, N'F', 1),
    (N'40871532', N'Riccardo', N'Mancini', '19971001', DATEADD(DAY, 23, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 73.40, 1.73, NULL, N'M', 1),
    (N'38396741', N'Giorgia', N'Leone', '19940625', DATEADD(DAY, 20, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 60.30, 1.65, NULL, N'F', 1),
    (N'27543190', N'Paolo', N'Costa', '19790209', DATEADD(DAY, 17, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 88.90, 1.76, NULL, N'M', 1),
    (N'36402857', N'Federica', N'Longo', '19910429', DATEADD(DAY, 14, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 64.50, 1.71, NULL, N'F', 1),
    (N'46015328', N'Federico', N'Giordano', '20041212', DATEADD(DAY, 11, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 68.00, 1.80, NULL, N'M', 1),
    (N'39750614', N'Camilla', N'Gentile', '19960319', DATEADD(DAY, 8, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 56.10, 1.59, NULL, N'F', 1),
    (N'30268471', N'Antonio', N'Rizzo', '19830707', DATEADD(DAY, 5, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 97.20, 1.79, NULL, N'M', 0),
    (N'41307529', N'Ilaria', N'Martinelli', '19980916', DATEADD(DAY, 2, DATEADD(MONTH, -4, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 58.80, 1.66, NULL, N'F', 0);

/* RutinaEjercicio */

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* RutinaEjercicio */
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

/* Planes */
INSERT INTO [Plan] (Nombre, Descripcion, Precio, Estado)
VALUES
    (N'Normal', N'Acceso al gimnasio.', 28000, 1),
    (N'Premium', N'Acceso al gimnasio con seguimiento de un entrenador.', 35000, 1);

/* Secuencias de lunes a viernes de las nueve rutinas modelo. */
INSERT INTO RutinaEjercicio (Series, Repeticiones, Peso, Descanso, Orden, DiaSemana, Estado, IdRutina, IdEjercicio)
VALUES
    (3, 6, 25.00, 180, 1, 1, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra')),
    (3, 6, 25.00, 180, 2, 1, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano')),
    (3, 6, 25.00, 180, 1, 2, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca')),
    (3, 6, 25.00, 180, 2, 2, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra')),
    (3, 6, 25.00, 180, 1, 3, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional')),
    (3, 6, NULL, 180, 2, 3, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal')),
    (3, 6, 25.00, 180, 1, 4, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar')),
    (3, 6, NULL, 180, 2, 4, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas')),
    (3, 6, 25.00, 180, 1, 5, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas')),
    (3, 6, 25.00, 180, 2, 5, 1, 2001, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea')),
    (4, 5, 40.00, 150, 1, 1, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra')),
    (4, 5, 40.00, 150, 2, 1, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano')),
    (4, 5, 40.00, 150, 1, 2, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca')),
    (4, 5, 40.00, 150, 2, 2, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra')),
    (4, 5, 40.00, 150, 1, 3, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional')),
    (4, 5, NULL, 150, 2, 3, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal')),
    (4, 5, 40.00, 150, 1, 4, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar')),
    (4, 5, NULL, 150, 2, 4, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas')),
    (4, 5, 40.00, 150, 1, 5, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas')),
    (4, 5, 40.00, 150, 2, 5, 1, 2002, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea')),
    (5, 4, 60.00, 120, 1, 1, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra')),
    (5, 4, 60.00, 120, 2, 1, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano')),
    (5, 4, 60.00, 120, 1, 2, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca')),
    (5, 4, 60.00, 120, 2, 2, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra')),
    (5, 4, 60.00, 120, 1, 3, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional')),
    (5, 4, NULL, 120, 2, 3, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal')),
    (5, 4, 60.00, 120, 1, 4, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar')),
    (5, 4, NULL, 120, 2, 4, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Dominadas')),
    (5, 4, 60.00, 120, 1, 5, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas')),
    (5, 4, 60.00, 120, 2, 5, 1, 2003, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea')),
    (3, 10, 12.00, 120, 1, 1, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca')),
    (3, 10, 4.80, 120, 2, 1, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas')),
    (3, 10, 12.00, 120, 1, 2, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho')),
    (3, 10, 12.00, 120, 2, 2, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea')),
    (3, 10, 12.00, 120, 1, 3, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra')),
    (3, 10, 12.00, 120, 2, 3, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas')),
    (3, 10, 12.00, 120, 1, 4, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros con mancuernas')),
    (3, 10, 4.80, 120, 2, 4, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales')),
    (3, 10, 4.80, 120, 1, 5, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra')),
    (3, 10, 4.80, 120, 2, 5, 1, 2004, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea')),
    (4, 10, 20.00, 90, 1, 1, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca')),
    (4, 10, 8.00, 90, 2, 1, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas')),
    (4, 10, 20.00, 90, 1, 2, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho')),
    (4, 10, 20.00, 90, 2, 2, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea')),
    (4, 10, 20.00, 90, 1, 3, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra')),
    (4, 10, 20.00, 90, 2, 3, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas')),
    (4, 10, 20.00, 90, 1, 4, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros con mancuernas')),
    (4, 10, 8.00, 90, 2, 4, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales')),
    (4, 10, 8.00, 90, 1, 5, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra')),
    (4, 10, 8.00, 90, 2, 5, 1, 2005, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea')),
    (5, 12, 28.00, 75, 1, 1, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca')),
    (5, 12, 11.20, 75, 2, 1, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Aperturas con mancuernas')),
    (5, 12, 28.00, 75, 1, 2, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho')),
    (5, 12, 28.00, 75, 2, 2, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea')),
    (5, 12, 28.00, 75, 1, 3, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra')),
    (5, 12, 28.00, 75, 2, 3, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas')),
    (5, 12, 28.00, 75, 1, 4, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros con mancuernas')),
    (5, 12, 11.20, 75, 2, 4, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales')),
    (5, 12, 11.20, 75, 1, 5, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra')),
    (5, 12, 11.20, 75, 2, 5, 1, 2006, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea')),
    (1, 10, NULL, 90, 1, 1, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cinta de correr')),
    (2, 15, 3.20, 90, 2, 1, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla goblet')),
    (1, 10, NULL, 90, 1, 2, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Bicicleta fija')),
    (2, 15, NULL, 90, 2, 2, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores')),
    (1, 10, NULL, 90, 1, 3, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo ergometro')),
    (2, 15, NULL, 90, 2, 3, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas')),
    (1, 10, NULL, 90, 1, 4, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Eliptica')),
    (2, 15, 3.20, 90, 2, 4, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Swing con kettlebell')),
    (1, 10, NULL, 90, 1, 5, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cinta de correr')),
    (2, 15, NULL, 90, 2, 5, 1, 2007, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal')),
    (1, 15, NULL, 60, 1, 1, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cinta de correr')),
    (3, 20, 4.80, 60, 2, 1, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla goblet')),
    (1, 15, NULL, 60, 1, 2, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Bicicleta fija')),
    (3, 20, NULL, 60, 2, 2, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores')),
    (1, 15, NULL, 60, 1, 3, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo ergometro')),
    (3, 20, NULL, 60, 2, 3, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas')),
    (1, 15, NULL, 60, 1, 4, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Eliptica')),
    (3, 20, 4.80, 60, 2, 4, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Swing con kettlebell')),
    (1, 15, NULL, 60, 1, 5, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cinta de correr')),
    (3, 20, NULL, 60, 2, 5, 1, 2008, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal')),
    (1, 20, NULL, 45, 1, 1, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cinta de correr')),
    (4, 25, 6.40, 45, 2, 1, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla goblet')),
    (1, 20, NULL, 45, 1, 2, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Bicicleta fija')),
    (4, 25, NULL, 45, 2, 2, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores')),
    (1, 20, NULL, 45, 1, 3, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo ergometro')),
    (4, 25, NULL, 45, 2, 3, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas')),
    (1, 20, NULL, 45, 1, 4, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Eliptica')),
    (4, 25, 6.40, 45, 2, 4, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Swing con kettlebell')),
    (1, 20, NULL, 45, 1, 5, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Cinta de correr')),
    (4, 25, NULL, 45, 2, 5, 1, 2009, (SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal'));

/* Membresias */
INSERT INTO Membresia (FechaInicio, FechaVencimiento, Estado, IdPlan, IdSocio, IdUsuarioSistema, IdRutina)
VALUES
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'38514072'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'39862145'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'33419587'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'37590264'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'31047826'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'41286093'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'36158402'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'42671358'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'40135729'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'34728016'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'29864103'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'40592871'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'44306758'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'32150947'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'38047615'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'41938260'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'33875104'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'43219587'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'37014896'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'35486120'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'31605279'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'45128063'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'40871532'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'38396741'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio')),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'27543190'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'36402857'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'46015328'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'39750614'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 0, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'30268471'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 0, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'41307529'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL);

/* MercadoPago */
INSERT INTO MercadoPago (ExternalReference, StatusDetail, FechaAprobacion)
VALUES
    (N'CUOTA-SEP26-38514072', N'Pago acreditado', DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))),
    (N'CUOTA-SEP26-33419587', N'Pago acreditado', DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))),
    (N'CUOTA-SEP26-31047826', N'Pago acreditado', DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))),
    (N'CUOTA-SEP26-36158402', N'Pago acreditado', DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))),
    (N'CUOTA-SEP26-40135729', N'Pago acreditado', DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))),
    (N'CUOTA-SEP26-33875104', N'Pago acreditado', DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))),
    (N'CUOTA-SEP26-37014896', N'Pago acreditado', DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))));

/* MetodoPago */
INSERT INTO MetodoPago (Estado, Observaciones, IdNroPagoMP, IdPagoEfectivo)
VALUES
    (1, N'Mercado Pago - Rossi Marco - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-38514072'), NULL),
    (1, N'Efectivo - Lombardi Giulia - septiembre', NULL, 1),
    (1, N'Mercado Pago - Bianchi Luca - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-33419587'), NULL),
    (1, N'Efectivo - Moretti Francesca - septiembre', NULL, 1),
    (1, N'Mercado Pago - Romano Giovanni - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-31047826'), NULL),
    (1, N'Efectivo - Barbieri Chiara - septiembre', NULL, 1),
    (1, N'Mercado Pago - Colombo Alessandro - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-36158402'), NULL),
    (1, N'Efectivo - Fontana Sofia - septiembre', NULL, 1),
    (1, N'Mercado Pago - Ricci Matteo - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-40135729'), NULL),
    (1, N'Efectivo - Santoro Martina - septiembre', NULL, 1),
    (1, N'Efectivo - Caruso Valentina - septiembre', NULL, 1),
    (1, N'Mercado Pago - Gallo Davide - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-33875104'), NULL),
    (1, N'Efectivo - Ferrara Sara - septiembre', NULL, 1),
    (1, N'Mercado Pago - Conti Simone - septiembre', (SELECT IdNroPagoMP FROM MercadoPago WHERE ExternalReference = N'CUOTA-SEP26-37014896'), NULL),
    (1, N'Efectivo - Galli Beatrice - septiembre', NULL, 1);

/* Pagos */
INSERT INTO Pago (Fecha, Descripcion, Importe, Estado, IdMetodoPago, IdUsuarioRegistro)
VALUES
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 38514072', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Rossi Marco - septiembre'), 3),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 39862145', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Lombardi Giulia - septiembre'), 1),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 33419587', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Bianchi Luca - septiembre'), 2),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 37590264', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Moretti Francesca - septiembre'), 3),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 31047826', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Romano Giovanni - septiembre'), 1),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 41286093', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Barbieri Chiara - septiembre'), 2),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 36158402', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Colombo Alessandro - septiembre'), 3),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 42671358', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Fontana Sofia - septiembre'), 1),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 40135729', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Ricci Matteo - septiembre'), 2),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 34728016', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Santoro Martina - septiembre'), 3),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 41938260', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Caruso Valentina - septiembre'), 1),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 33875104', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Gallo Davide - septiembre'), 2),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 43219587', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Ferrara Sara - septiembre'), 3),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 37014896', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Conti Simone - septiembre'), 1),
    (DATEADD(DAY, 1, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), N'Cuota septiembre - 35486120', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Galli Beatrice - septiembre'), 2);

/* Cuotas */
INSERT INTO CuotaMembresia (FechaDesde, FechaHasta, Importe, EstadoPago, IdRegistroPago, IdMembresia)
VALUES
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 38514072'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38514072') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 39862145'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39862145') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 33419587'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'33419587') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 37590264'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37590264') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 31047826'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31047826') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 41286093'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41286093') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 36158402'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36158402') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 42671358'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'42671358') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 40135729'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40135729') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 34728016'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34728016') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'29864103') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40592871') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'44306758') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'32150947') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38047615') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 41938260'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41938260') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 33875104'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'33875104') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 43219587'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'43219587') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 37014896'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37014896') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 35486120'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'35486120') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31605279') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'45128063') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40871532') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38396741') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'27543190') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36402857') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'46015328') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39750614') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'30268471') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))))),
    (DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), DATEADD(DAY, 29, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date)))), 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41307529') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))));

/* Pago conserva la cuota histórica; la cuota conserva el pago vigente. */
UPDATE p
SET IdCuotaMembresia = c.IdCuotaMembresia
FROM Pago AS p
INNER JOIN CuotaMembresia AS c ON c.IdRegistroPago = p.IdRegistroPago
WHERE p.IdCuotaMembresia IS NULL;

-- Cada cuota conserva la tarifa vigente de su plan para el mes del servicio.
;WITH PeriodosTarifa AS (
    SELECT c.IdCuotaMembresia, pl.Precio,
        81 - DATEDIFF(MONTH, c.FechaDesde, DATEADD(DAY,1-DAY(CAST(GETDATE() AS date)),CAST(GETDATE() AS date))) AS AntiguedadMeses
    FROM dbo.CuotaMembresia c
    INNER JOIN dbo.Membresia m ON m.IdMembresia = c.IdMembresia
    INNER JOIN dbo.[Plan] pl ON pl.IdPlan = m.IdPlan
), EtapasTarifa AS (
    SELECT *, CASE
        WHEN AntiguedadMeses >= 81 THEN 31
        WHEN AntiguedadMeses < 36 THEN FLOOR(CASE WHEN AntiguedadMeses < 0 THEN 0 ELSE AntiguedadMeses END / 3.0)
        WHEN AntiguedadMeses < 60 THEN 12 + FLOOR((AntiguedadMeses - 36) / 2.0)
        ELSE 24 + FLOOR((AntiguedadMeses - 60) / 3.0)
    END AS Etapa
    FROM PeriodosTarifa
)
UPDATE c SET Importe = ROUND((t.Precio * POWER(CAST(1.097 AS decimal(10,6)), t.Etapa - 31)) / 500.0, 0) * 500
FROM dbo.CuotaMembresia c INNER JOIN EtapasTarifa t ON t.IdCuotaMembresia = c.IdCuotaMembresia;
UPDATE p SET Importe = c.Importe
FROM dbo.Pago p INNER JOIN dbo.CuotaMembresia c ON c.IdCuotaMembresia = p.IdCuotaMembresia;
DELETE FROM dbo.Pago WHERE IdCuotaMembresia IS NULL;
/* MembresiaEntrenador */
INSERT INTO MembresiaEntrenador (Estado, IdMembresia, IdEntrenador)
VALUES
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38514072') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39862145') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'33419587') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37590264') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31047826') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41286093') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36158402') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'42671358') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40135729') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34728016') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'29864103') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40592871') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'44306758') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'32150947') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38047615') AND FechaInicio = DATEADD(DAY, 0, DATEADD(MONTH, -1, DATEADD(DAY, 1 - DAY(CAST(GETDATE() AS date)), CAST(GETDATE() AS date))))), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker'));
GO
