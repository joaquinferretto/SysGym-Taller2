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
    Salario DECIMAL(18,2) NOT NULL DEFAULT 0,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(500) NOT NULL,
    FotoRuta NVARCHAR(260) NULL,
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

/* Índices de consultas habituales */

CREATE INDEX IX_Membresia_IdSocio
    ON Membresia(IdSocio);

/* Un pago no puede asociarse a dos cuotas, pero las cuotas pendientes pueden tener NULL */

CREATE UNIQUE INDEX UX_CuotaMembresia_IdRegistroPago
    ON CuotaMembresia(IdRegistroPago)
    WHERE IdRegistroPago IS NOT NULL;
GO

/* Datos iniciales. Ultima actualizacion: 22 de septiembre de 2026.
   Solo sentencias INSERT: sin ciclos, variables, procedimientos ni funciones propias.
   Ejecutar una sola vez sobre la base recien creada. Las claves foraneas se resuelven
   con subconsultas simples por su clave natural (Username, DNI, Nombre). */

INSERT INTO Rol (Descripcion, Estado)
VALUES
    (N'Administrador', 1),
    (N'Recepcionista', 1),
    (N'Entrenador', 1);

/* Usuarios del sistema: SanMartin (administrador original) y tres usuarios de origen aleman,
   uno por rol. Klaus, Greta y Lukas usan la clave de prueba Prueba123! (hash Argon2id). Sus fotos estan en capaDatos/Imagenes/Usuarios.
   Nunca usar estas claves en produccion. */
INSERT INTO UsuarioSistema (Nombre, Apellido, DNI, Telefono, FechaNacimiento, Salario, Username, Password, FotoRuta, Sexo, Estado, IdRol)
VALUES
    (N'San', N'Martin', N'30000001', NULL, NULL, 0, N'SanMartin', N'ARGON2ID:19:65536:3:2:fk55JlSsiyw0cDMfppSqSg==:+OMMPicxIZ0iPl0t7h0mNrg59Ysq3F5HKt/gNrYPuqU=', NULL, NULL, 1, 1),
    (N'Klaus', N'Schneider', N'28451736', N'1147218365', '19800612', 950000, N'klaus.schneider', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', N'Imagenes\Usuarios\7e665f53c5f54ac1ba1192da2609e7b1.jpg', N'M', 1, 1),
    (N'Greta', N'Hoffmann', N'33617290', N'1158340172', '19880923', 720000, N'greta.hoffmann', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', N'Imagenes\Usuarios\1b5f13461f324ba59a3f927204fa637a.jpg', N'F', 1, 2),
    (N'Lukas', N'Becker', N'36072814', N'1163927458', '19920304', 810000, N'lukas.becker', N'ARGON2ID:19:65536:3:2:UzjfnbF3n+md5BTKCQfiIA==:jJLuZEShyp0cWykdXKDVbXM4xpdItsG98tfSyzCx0sU=', N'Imagenes\Usuarios\31c5235ceec7401cb94ab63107735658.jpg', N'M', 1, 3);

INSERT INTO PagoEfectivo (Estado)
VALUES (1);

INSERT INTO MercadoPago (ExternalReference)
VALUES (N'SYSGYM_MANUAL');

INSERT INTO MetodoPago (Estado, Observaciones, IdNroPagoMP, IdPagoEfectivo)
VALUES
    (1, N'Mercado Pago', 1, NULL),
    (1, N'Pago en efectivo', NULL, 1);

/* Ejercicios: 82 ejercicios del catalogo, agrupados por zona. Incluye cardio y acondicionamiento. */
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

/* Imagenes base de ejercicios: los archivos estan en capaDatos/Imagenes/Ejercicios/{id}
   y el proyecto los copia a Datos\Imagenes al compilar. SQL solo guarda la ruta relativa. */
INSERT INTO EjercicioImagen (IdEjercicio, RutaRelativa, Orden)
VALUES
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca'), N'Imagenes\Ejercicios\1\a64931fc2f4646a8a6e49d562d24ac6c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con mancuernas'), N'Imagenes\Ejercicios\2\e54c109a17ca4ccc86d7928667cd1801.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en paralelas'), N'Imagenes\Ejercicios\4\dbc3d3befc8b461cbdc330895516b352.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press plano con mancuernas'), N'Imagenes\Ejercicios\5\6b032a3b84fd462c8bd34e1fd4bdfa3c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press declinado con barra'), N'Imagenes\Ejercicios\7\7199498d90d84faaa1dd16881390f2e7.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press inclinado con barra'), N'Imagenes\Ejercicios\8\243a1be452a44da48c8c114e3656eed2.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Flexiones de brazos'), N'Imagenes\Ejercicios\10\1d9dbbc2360746d98c678c2efe5caeb9.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon al pecho'), N'Imagenes\Ejercicios\11\9e9a7406e3504e10bdec2d51e8c1b449.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con barra'), N'Imagenes\Ejercicios\12\8f7405ed759a454bb75af75fa013b8a1.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo sentado en polea'), N'Imagenes\Ejercicios\13\bdf86d7884994b71aa0611bee415fa0f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Face pull'), N'Imagenes\Ejercicios\14\12cfdbb22ca04a078f0be31a8f727975.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo con mancuerna'), N'Imagenes\Ejercicios\16\62b3320e720642dd91fff78601a5fd0b.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Pullover en polea'), N'Imagenes\Ejercicios\17\7d4d0faaa7d441eb91c06684a53b56ea.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto convencional'), N'Imagenes\Ejercicios\18\24a5edca910247a099b98c05bcb86760.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo en T'), N'Imagenes\Ejercicios\19\535de7f10e564f92a28b1fd9eb439a6d.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Jalon con agarre cerrado'), N'Imagenes\Ejercicios\20\ca09243c3789487aa844256eb1971a78.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hiperextensiones lumbares'), N'Imagenes\Ejercicios\21\365e87ea9d8648368c2e090a1a2cc03f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Encogimientos para trapecio'), N'Imagenes\Ejercicios\22\28ccaf4a83994660aced69aff36ca1f8.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press militar'), N'Imagenes\Ejercicios\23\e9a40a3e091f490fa21db383a70f2849.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones laterales'), N'Imagenes\Ejercicios\24\f136a728733b44d4a3dc1aa71434c54f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press Arnold'), N'Imagenes\Ejercicios\25\8daa3e411ded4c489cf3cfd9736a14fc.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevaciones frontales'), N'Imagenes\Ejercicios\26\7f0e8083eb764169aaf2fd40c8b2049c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Pajaro con mancuernas'), N'Imagenes\Ejercicios\27\07a380fcc5ec4fdfb7320a0c4df9cb4b.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros con mancuernas'), N'Imagenes\Ejercicios\28\898e9213b5454bc5999d7cdb061ecc2d.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de hombros en maquina'), N'Imagenes\Ejercicios\29\239c73013e7946719223743b4facfcb8.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo al menton'), N'Imagenes\Ejercicios\30\b8190b98714d4897a7f5202522218989.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra'), N'Imagenes\Ejercicios\31\5351ffc368114920a4b4a5c8b7bbd079.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de biceps con barra'), N'Imagenes\Ejercicios\31\9a98ff3b559c4e499283f1d3fb7b271f.jpg', 2),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl martillo'), N'Imagenes\Ejercicios\32\8577d4dcc40d46c38c4af60273eba86a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl en banco Scott'), N'Imagenes\Ejercicios\36\e801c2b6742c45c38cb221ebf4b7dba9.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps en polea'), N'Imagenes\Ejercicios\37\44cb0c6a4dab4c3997f78cdfb3556e91.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press frances'), N'Imagenes\Ejercicios\38\86b0230ff7bf423baa69fb172ecfbcd1.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de triceps'), N'Imagenes\Ejercicios\39\dd45d1d8c7524597b14d913c1a3f8207.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Fondos en banco'), N'Imagenes\Ejercicios\40\0f705fc9f64b4fdc95312dc048995c89.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de triceps sobre la cabeza'), N'Imagenes\Ejercicios\41\6d1cee73ced741c3b23b044b2d643ff2.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Press de banca agarre cerrado'), N'Imagenes\Ejercicios\42\6e83b0400dc64be4805bc0f9f755847d.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Curl de antebrazo'), N'Imagenes\Ejercicios\43\be1adc7cb2394178bd2b7d26cfffb82c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla con barra'), N'Imagenes\Ejercicios\44\1fe87990a7ba4340801b9175ca85dcf4.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Prensa de piernas'), N'Imagenes\Ejercicios\45\3e0f486a6f314ad084784bcf3513d25c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto rumano'), N'Imagenes\Ejercicios\46\df0b72cf9ea84f3ebf3eaf15cae1549a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Extension de cuadriceps'), N'Imagenes\Ejercicios\47\f56e05de8bba488e81a8b5a778bfead0.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Hip thrust'), N'Imagenes\Ejercicios\49\6138bf2024eb407ba39b3ad0629a6406.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Zancadas'), N'Imagenes\Ejercicios\50\70e6829bd4594a1aa39193021d4b5984.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones'), N'Imagenes\Ejercicios\51\aeec142624ac4c0b98008714d8a69888.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla bulgara'), N'Imagenes\Ejercicios\52\2dce8c7a8dff4e0e887dff842715c2a1.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla frontal'), N'Imagenes\Ejercicios\54\2ec35379bac947a5a2d7fef4e50446cd.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla hack'), N'Imagenes\Ejercicios\55\14c7f8fc4cea4b8ea77440f8f800df2a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sentadilla goblet'), N'Imagenes\Ejercicios\56\11ecfafb9aed4dd9aae14493a302f214.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Peso muerto sumo'), N'Imagenes\Ejercicios\57\df37b32090b34dc2b0d6119d374db98b.png', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Subida al cajon'), N'Imagenes\Ejercicios\59\397cc1728a054fadb653dfc70f6f122f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Puente de gluteos'), N'Imagenes\Ejercicios\60\cdaa2a81d24e410a83805a3ff682d05f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Patada de gluteo en polea'), N'Imagenes\Ejercicios\61\214fdfd3b7c14b2da58f7635457aff24.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de talones sentado'), N'Imagenes\Ejercicios\62\7cf9d7ac42b24f1d96dd7d8c3093ab8f.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha abdominal'), N'Imagenes\Ejercicios\63\c0695827a8bb4df3bf244797f2a925b1.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladores'), N'Imagenes\Ejercicios\65\301183f276614f7fb622a9002cea028b.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Elevacion de piernas colgado'), N'Imagenes\Ejercicios\66\f969c3dc878a4aac8ec100db6a599c51.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Rueda abdominal'), N'Imagenes\Ejercicios\67\00191f001e3f49e48670fd99ed90c18c.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Giro ruso'), N'Imagenes\Ejercicios\68\cd1614901ae248d1a367f88f4adbca98.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Plancha lateral'), N'Imagenes\Ejercicios\69\716e5e48b36a4667b6e0cae4bcd5a4b0.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Eliptica'), N'Imagenes\Ejercicios\74\7726b3e480554c999bd924a9169d160a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Remo ergometro'), N'Imagenes\Ejercicios\75\dfef50bfccd844d5a044e7e921c979a1.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Escaladora'), N'Imagenes\Ejercicios\76\8b04dc64a54a4dbcbfb64bb27164bc3a.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Salto con soga'), N'Imagenes\Ejercicios\77\290e525866054827af338dd9c0a3e2c7.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Salto al cajon'), N'Imagenes\Ejercicios\79\832da073532b453d87230587afa97ba3.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Swing con kettlebell'), N'Imagenes\Ejercicios\80\d835134a31f54dc3b514c6b5ea85f312.jpg', 1),
    ((SELECT IdEjercicio FROM Ejercicio WHERE Nombre = N'Sogas de batalla'), N'Imagenes\Ejercicios\81\edb44dc6ae0b4da1bda1449db30d5c61.jpg', 1);

/* Rutinas: 6 generales y 20 plantillas; todas creadas por el entrenador Lukas Becker. */
INSERT INTO Rutina (Nombre, Descripcion, FechaCreacion, FechaInicio, FechaFin, Estado, IdEntrenador)
VALUES
    (N'Comienzo 1', N'Adaptacion general para iniciar el entrenamiento.', '20260901', NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Hipertrofia 1', N'Volumen moderado para desarrollar masa muscular.', '20260901', NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Hipertrofia 2', N'Progresion de hipertrofia con mayor volumen.', '20260901', NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Fuerza', N'Mejora de fuerza con ejercicios compuestos.', '20260901', NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Powerlifting', N'Sentadilla, press banca y peso muerto.', '20260901', NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Cardio', N'Entrenamiento cardiovascular y acondicionamiento.', '20260901', NULL, NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Pecho con barra', N'Trabajo de pectorales con press de banca.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Pecho inclinado', N'Trabajo inclinado con mancuernas.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Aperturas de pecho', N'Aislamiento de pectorales con mancuernas.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Fondos de tren superior', N'Fondos con peso corporal.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Espalda en polea', N'Jalones para el trabajo de dorsales.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Remo de fuerza', N'Remo con barra y tecnica controlada.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Espalda controlada', N'Remo sentado en polea.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Estabilidad de hombros', N'Trabajo posterior del hombro con face pull.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Press de hombros', N'Press militar para hombros y triceps.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Hombros laterales', N'Elevaciones laterales con carga moderada.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Biceps con barra', N'Curl de biceps con barra.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Brazos con mancuernas', N'Curl martillo para brazos y antebrazos.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Triceps en polea', N'Extensiones de triceps con polea.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Sentadilla inicial', N'Practica de sentadilla con barra.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Piernas en prensa', N'Trabajo de cuadriceps y gluteos en prensa.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Cadena posterior', N'Peso muerto rumano para cadena posterior.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Cuadriceps en maquina', N'Extension de cuadriceps en maquina.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Isquiotibiales', N'Flexion de rodilla con curl femoral.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Gluteos en banco', N'Extension de cadera con hip thrust.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (N'Piernas unilaterales', N'Zancadas para trabajo unilateral.', '20260901', '20260901', NULL, 1, (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker'));

/* Socios: 30 socios de origen italiano (15 mujeres y 15 hombres). Rizzo y Martinelli estan de baja para probar la reactivacion. */
INSERT INTO Socio (DNI, Nombre, Apellido, FechaNacimiento, Peso, Altura, FotoRuta, Sexo, Estado)
VALUES
    (N'38514072', N'Marco', N'Rossi', '19900412', 82.50, 1.78, NULL, N'M', 1),
    (N'39862145', N'Giulia', N'Lombardi', '19950723', 58.00, 1.64, NULL, N'F', 1),
    (N'33419587', N'Luca', N'Bianchi', '19871102', 88.00, 1.82, NULL, N'M', 1),
    (N'37590264', N'Francesca', N'Moretti', '19930215', 61.50, 1.67, NULL, N'F', 1),
    (N'31047826', N'Giovanni', N'Romano', '19840930', 90.20, 1.80, NULL, N'M', 1),
    (N'41286093', N'Chiara', N'Barbieri', '19980508', 55.30, 1.60, NULL, N'F', 1),
    (N'36158402', N'Alessandro', N'Colombo', '19911219', 77.80, 1.75, NULL, N'M', 1),
    (N'42671358', N'Sofia', N'Fontana', '20000327', 52.40, 1.62, NULL, N'F', 1),
    (N'40135729', N'Matteo', N'Ricci', '19960805', 74.60, 1.76, NULL, N'M', 1),
    (N'34728016', N'Martina', N'Santoro', '19891014', 63.00, 1.69, NULL, N'F', 1),
    (N'29864103', N'Francesco', N'Marino', '19820121', 95.40, 1.84, NULL, N'M', 1),
    (N'40592871', N'Alessia', N'Mariani', '19970611', 57.90, 1.63, NULL, N'F', 1),
    (N'44306758', N'Lorenzo', N'Greco', '20020228', 70.10, 1.79, NULL, N'M', 1),
    (N'32150947', N'Elena', N'Rinaldi', '19861203', 66.20, 1.70, NULL, N'F', 1),
    (N'38047615', N'Andrea', N'Bruno', '19940417', 81.00, 1.77, NULL, N'M', 1),
    (N'41938260', N'Valentina', N'Caruso', '19990909', 54.70, 1.58, NULL, N'F', 1),
    (N'33875104', N'Davide', N'Gallo', '19880726', 86.30, 1.81, NULL, N'M', 1),
    (N'43219587', N'Sara', N'Ferrara', '20010130', 59.60, 1.66, NULL, N'F', 1),
    (N'37014896', N'Simone', N'Conti', '19920522', 79.50, 1.74, NULL, N'M', 1),
    (N'35486120', N'Beatrice', N'Galli', '19901118', 62.80, 1.68, NULL, N'F', 1),
    (N'31605279', N'Stefano', N'De Luca', '19850306', 92.70, 1.83, NULL, N'M', 1),
    (N'45128063', N'Aurora', N'Martini', '20030814', 51.20, 1.61, NULL, N'F', 1),
    (N'40871532', N'Riccardo', N'Mancini', '19971001', 73.40, 1.73, NULL, N'M', 1),
    (N'38396741', N'Giorgia', N'Leone', '19940625', 60.30, 1.65, NULL, N'F', 1),
    (N'27543190', N'Paolo', N'Costa', '19790209', 88.90, 1.76, NULL, N'M', 1),
    (N'36402857', N'Federica', N'Longo', '19910429', 64.50, 1.71, NULL, N'F', 1),
    (N'46015328', N'Federico', N'Giordano', '20041212', 68.00, 1.80, NULL, N'M', 1),
    (N'39750614', N'Camilla', N'Gentile', '19960319', 56.10, 1.59, NULL, N'F', 1),
    (N'30268471', N'Antonio', N'Rizzo', '19830707', 97.20, 1.79, NULL, N'M', 0),
    (N'41307529', N'Ilaria', N'Martinelli', '19980916', 58.80, 1.66, NULL, N'F', 0);

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

/* Planes: Normal y Premium */
INSERT INTO [Plan] (Nombre, Descripcion, Precio, Estado)
VALUES
    (N'Normal', N'Acceso al gimnasio.', 15000, 1),
    (N'Premium', N'Acceso al gimnasio con seguimiento de un entrenador.', 25000, 1);

/* Membresias: 30 (15 Premium y 15 Normal), registradas por la recepcionista Greta Hoffmann.
   Los socios 1 a 24 tienen rutina asignada (Membresia.IdRutina); los ultimos 6 no, para probar Asignar/Crear personalizada.
   Las dos membresias de los socios dados de baja quedan inactivas. */
INSERT INTO Membresia (FechaInicio, FechaVencimiento, Estado, IdPlan, IdSocio, IdUsuarioSistema, IdRutina)
VALUES
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'38514072'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho con barra')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'39862145'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Pecho inclinado')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'33419587'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Aperturas de pecho')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'37590264'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fondos de tren superior')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'31047826'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda en polea')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'41286093'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Remo de fuerza')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'36158402'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Espalda controlada')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'42671358'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Estabilidad de hombros')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'40135729'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Press de hombros')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'34728016'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hombros laterales')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'29864103'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Biceps con barra')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'40592871'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Brazos con mancuernas')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'44306758'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Triceps en polea')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'32150947'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Sentadilla inicial')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Premium'), (SELECT IdSocio FROM Socio WHERE DNI = N'38047615'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas en prensa')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'41938260'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cadena posterior')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'33875104'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cuadriceps en maquina')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'43219587'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Isquiotibiales')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'37014896'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Gluteos en banco')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'35486120'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Piernas unilaterales')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'31605279'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Comienzo 1')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'45128063'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Hipertrofia 1')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'40871532'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Fuerza')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'38396741'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), (SELECT IdRutina FROM Rutina WHERE Nombre = N'Cardio')),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'27543190'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'36402857'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'46015328'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    ('20260901', '20260930', 1, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'39750614'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    ('20260901', '20260930', 0, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'30268471'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL),
    ('20260901', '20260930', 0, (SELECT IdPlan FROM [Plan] WHERE Nombre = N'Normal'), (SELECT IdSocio FROM Socio WHERE DNI = N'41307529'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'greta.hoffmann'), NULL);

/* MercadoPago: referencias ficticias de los cobros acreditados por Mercado Pago; no son cobros reales. */
INSERT INTO MercadoPago (ExternalReference, StatusDetail, FechaAprobacion)
VALUES
    (N'CUOTA-SEP26-38514072', N'Pago acreditado', '20260902'),
    (N'CUOTA-SEP26-33419587', N'Pago acreditado', '20260902'),
    (N'CUOTA-SEP26-31047826', N'Pago acreditado', '20260902'),
    (N'CUOTA-SEP26-36158402', N'Pago acreditado', '20260902'),
    (N'CUOTA-SEP26-40135729', N'Pago acreditado', '20260902'),
    (N'CUOTA-SEP26-33875104', N'Pago acreditado', '20260902'),
    (N'CUOTA-SEP26-37014896', N'Pago acreditado', '20260902');

/* MetodoPago: un metodo por cobro (Mercado Pago o efectivo en pesos). */
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

/* Pagos: 15 cobros aprobados de la cuota de septiembre. */
INSERT INTO Pago (Fecha, Descripcion, Importe, Estado, IdMetodoPago)
VALUES
    ('20260902', N'Cuota septiembre - 38514072', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Rossi Marco - septiembre')),
    ('20260902', N'Cuota septiembre - 39862145', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Lombardi Giulia - septiembre')),
    ('20260902', N'Cuota septiembre - 33419587', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Bianchi Luca - septiembre')),
    ('20260902', N'Cuota septiembre - 37590264', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Moretti Francesca - septiembre')),
    ('20260902', N'Cuota septiembre - 31047826', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Romano Giovanni - septiembre')),
    ('20260902', N'Cuota septiembre - 41286093', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Barbieri Chiara - septiembre')),
    ('20260902', N'Cuota septiembre - 36158402', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Colombo Alessandro - septiembre')),
    ('20260902', N'Cuota septiembre - 42671358', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Fontana Sofia - septiembre')),
    ('20260902', N'Cuota septiembre - 40135729', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Ricci Matteo - septiembre')),
    ('20260902', N'Cuota septiembre - 34728016', 25000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Santoro Martina - septiembre')),
    ('20260902', N'Cuota septiembre - 41938260', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Caruso Valentina - septiembre')),
    ('20260902', N'Cuota septiembre - 33875104', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Gallo Davide - septiembre')),
    ('20260902', N'Cuota septiembre - 43219587', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Ferrara Sara - septiembre')),
    ('20260902', N'Cuota septiembre - 37014896', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Mercado Pago - Conti Simone - septiembre')),
    ('20260902', N'Cuota septiembre - 35486120', 15000, N'Aprobado', (SELECT IdMetodoPago FROM MetodoPago WHERE Observaciones = N'Efectivo - Galli Beatrice - septiembre'));

/* Cuotas de septiembre: 15 pagadas (con su pago) y 15 pendientes (sin pago asociado). */
INSERT INTO CuotaMembresia (FechaDesde, FechaHasta, Importe, EstadoPago, IdRegistroPago, IdMembresia)
VALUES
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 38514072'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38514072') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 39862145'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39862145') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 33419587'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'33419587') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 37590264'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37590264') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 31047826'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31047826') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 41286093'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41286093') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 36158402'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36158402') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 42671358'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'42671358') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 40135729'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40135729') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 34728016'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34728016') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'29864103') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40592871') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'44306758') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'32150947') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 25000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38047615') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 41938260'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41938260') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 33875104'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'33875104') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 43219587'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'43219587') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 37014896'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37014896') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pagada', (SELECT IdRegistroPago FROM Pago WHERE Descripcion = N'Cuota septiembre - 35486120'), (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'35486120') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31605279') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'45128063') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40871532') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38396741') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'27543190') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36402857') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'46015328') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39750614') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'30268471') AND FechaInicio = '20260901')),
    ('20260901', '20260930', 15000, N'Pendiente', NULL, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41307529') AND FechaInicio = '20260901'));

/* Asignacion de entrenador: Lukas Becker acompana a las 15 membresias Premium. */
INSERT INTO MembresiaEntrenador (Estado, IdMembresia, IdEntrenador)
VALUES
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38514072') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'39862145') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'33419587') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'37590264') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'31047826') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'41286093') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'36158402') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'42671358') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40135729') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'34728016') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'29864103') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'40592871') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'44306758') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'32150947') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker')),
    (1, (SELECT IdMembresia FROM Membresia WHERE IdSocio = (SELECT IdSocio FROM Socio WHERE DNI = N'38047615') AND FechaInicio = '20260901'), (SELECT IdUsuarioSistema FROM UsuarioSistema WHERE Username = N'lukas.becker'));
GO
