-- SysGym - Carga inicial de ejercicios y plantillas generales de rutina.
-- NOTA: esta carga ya está integrada en capaDatos/Database/SysGymDB.sql,
-- por lo que no hace falta ejecutar este archivo al crear una base nueva.
-- Este archivo es SQL aunque conserve la extension .md.
-- Todo el texto explicativo esta comentado para poder ejecutarlo completo.
--
-- Las plantillas son reutilizables: no pertenecen a un socio.
-- Si @DniSocio tiene una membresia activa, tambien se asignan a ese socio.
-- Cambiar @UsernameEntrenador y @DniSocio antes de ejecutar si corresponde.

USE SysGymDB;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @DniSocio NVARCHAR(20) = N'44213011';
DECLARE @UsernameEntrenador NVARCHAR(50) = N'entrenador';
DECLARE @IdSocio INT;
DECLARE @IdEntrenador INT;
DECLARE @IdMembresia INT;

SELECT @IdSocio = s.IdSocio
FROM Socio AS s
WHERE s.DNI = @DniSocio AND s.Estado = 1;

SELECT @IdEntrenador = u.IdUsuarioSistema
FROM UsuarioSistema AS u
INNER JOIN Rol AS r ON r.IdRol = u.IdRol
WHERE u.Username = @UsernameEntrenador
  AND u.Estado = 1
  AND r.Estado = 1
  AND r.Descripcion = N'Entrenador';

IF @IdEntrenador IS NULL
    THROW 50002, 'No se encontro un entrenador activo con el usuario indicado.', 1;

SELECT TOP (1) @IdMembresia = m.IdMembresia
FROM Membresia AS m
INNER JOIN [Plan] AS p ON p.IdPlan = m.IdPlan
WHERE m.IdSocio = @IdSocio AND m.Estado = 1
  AND p.Estado = 1 AND p.IncluyeRutinaPersonal = 1
ORDER BY m.FechaInicio DESC, m.IdMembresia DESC;

BEGIN TRY
    BEGIN TRANSACTION;

    -- Ejercicios disponibles para las rutinas.
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

    UPDATE e
       SET e.Descripcion = x.Descripcion, e.Estado = 1
    FROM Ejercicio AS e
    INNER JOIN @Ejercicios AS x ON x.Nombre = e.Nombre;

    INSERT INTO Ejercicio (Nombre, Descripcion, Estado)
    SELECT x.Nombre, x.Descripcion, 1
    FROM @Ejercicios AS x
    WHERE NOT EXISTS (SELECT 1 FROM Ejercicio AS e WHERE e.Nombre = x.Nombre);

    -- Plantillas generales reutilizables.
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

    -- Ejercicios base de cada plantilla. El peso queda NULL para personalizarlo.
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

    -- Asignacion opcional de todas las plantillas a la membresia activa indicada.
    IF @IdMembresia IS NOT NULL
    BEGIN
        INSERT INTO RutinaAsignacion
            (FechaAsignacion, FechaFin, Estado, IdRutina, IdMembresia)
        SELECT SYSDATETIME(), NULL, 1, r.IdRutina, @IdMembresia
        FROM @Rutinas AS x
        INNER JOIN Rutina AS r ON r.Nombre = x.Nombre AND r.IdEntrenador = @IdEntrenador
        WHERE NOT EXISTS
        (
            SELECT 1 FROM RutinaAsignacion AS ra
            WHERE ra.IdRutina = r.IdRutina AND ra.IdMembresia = @IdMembresia AND ra.Estado = 1
        );
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;

-- Resumen: una fila por plantilla con su cantidad de socios asignados.
SELECT r.IdRutina, r.Nombre, r.Descripcion,
       COUNT(CASE WHEN ra.Estado = 1 THEN 1 END) AS SociosAsignados
FROM Rutina AS r
LEFT JOIN RutinaAsignacion AS ra ON ra.IdRutina = r.IdRutina
WHERE r.IdEntrenador = @IdEntrenador
  AND r.Nombre IN (N'Comienzo 1', N'Hipertrofia 1', N'Hipertrofia 2', N'Fuerza', N'Powerlifting', N'Cardio')
GROUP BY r.IdRutina, r.Nombre, r.Descripcion
ORDER BY r.Nombre;

-- El script no crea socios, usuarios, membresias ni planes.
-- Esos registros deben existir previamente.
