-- SysGym - Migración de rutinas por socio a plantillas reutilizables.
-- Ejecutar una sola vez sobre una base creada con la versión anterior.
-- La migración conserva las rutinas existentes y las asigna a la membresía
-- más reciente de cada socio que ya tenía una rutina.

USE SysGymDB;
GO

IF OBJECT_ID(N'dbo.RutinaAsignacion', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.RutinaAsignacion (
        IdRutinaAsignacion INT IDENTITY(1,1) PRIMARY KEY,
        FechaAsignacion DATETIME2 NOT NULL,
        FechaFin DATETIME2 NULL,
        Estado BIT NOT NULL DEFAULT 1,
        IdRutina INT NOT NULL,
        IdMembresia INT NOT NULL,

        CONSTRAINT CK_RutinaAsignacion_Fechas
            CHECK (FechaFin IS NULL OR FechaFin >= FechaAsignacion),
        CONSTRAINT FK_RutinaAsignacion_Rutina
            FOREIGN KEY (IdRutina) REFERENCES dbo.Rutina(IdRutina),
        CONSTRAINT FK_RutinaAsignacion_Membresia
            FOREIGN KEY (IdMembresia) REFERENCES dbo.Membresia(IdMembresia)
    );
END;
GO

-- Convertir cada rutina anterior en una asignación histórica.
IF COL_LENGTH(N'dbo.Rutina', N'IdSocio') IS NOT NULL
BEGIN
    INSERT INTO dbo.RutinaAsignacion
        (FechaAsignacion, FechaFin, Estado, IdRutina, IdMembresia)
    SELECT
        COALESCE(r.FechaInicio, r.FechaCreacion, SYSDATETIME()),
        r.FechaFin,
        CASE WHEN r.Estado = 1 AND m.Estado = 1 THEN 1 ELSE 0 END,
        r.IdRutina,
        m.IdMembresia
    FROM dbo.Rutina r
    CROSS APPLY
    (
        SELECT TOP (1) m1.IdMembresia, m1.Estado
        FROM dbo.Membresia m1
        WHERE m1.IdSocio = r.IdSocio
        ORDER BY m1.FechaInicio DESC, m1.IdMembresia DESC
    ) m
    WHERE NOT EXISTS
    (
        SELECT 1
        FROM dbo.RutinaAsignacion ra
        WHERE ra.IdRutina = r.IdRutina
    );

    -- La rutina deja de guardar un socio propietario: ahora es una plantilla.
    IF EXISTS
    (
        SELECT 1
        FROM sys.foreign_keys
        WHERE name = N'FK_Rutina_Socio'
          AND parent_object_id = OBJECT_ID(N'dbo.Rutina')
    )
    BEGIN
        ALTER TABLE dbo.Rutina DROP CONSTRAINT FK_Rutina_Socio;
    END;

    ALTER TABLE dbo.Rutina DROP COLUMN IdSocio;
END;
GO

IF NOT EXISTS
(
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_RutinaAsignacion_IdMembresia'
      AND object_id = OBJECT_ID(N'dbo.RutinaAsignacion')
)
BEGIN
    CREATE INDEX IX_RutinaAsignacion_IdMembresia
        ON dbo.RutinaAsignacion(IdMembresia);
END;

IF NOT EXISTS
(
    SELECT 1 FROM sys.indexes
    WHERE name = N'IX_RutinaAsignacion_Activas'
      AND object_id = OBJECT_ID(N'dbo.RutinaAsignacion')
)
BEGIN
    CREATE INDEX IX_RutinaAsignacion_Activas
        ON dbo.RutinaAsignacion(IdRutina, Estado);
END;
GO

-- Desde este punto, el flujo correcto es:
-- 1) Crear una rutina general (por ejemplo: Hipertrofia 1).
-- 2) Agregarle ejercicios y parámetros.
-- 3) Asignarla a una o muchas membresías mediante RutinaAsignacion.
