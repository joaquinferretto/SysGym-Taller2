/*
   Migración para una SysGymDB creada con el esquema anterior.
   No elimina datos ni recrea la base.

   Ejecutar este archivo una sola vez sobre SysGymDB existente.
   Después ejecutar en SysGymDB la sección "Catálogo inicial..."
   de SysGymDB.sql.
*/

USE SysGymDB;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    /* Fotos y sexo opcionales; los registros existentes conservan valores nulos. */
    IF COL_LENGTH(N'dbo.UsuarioSistema', N'Foto') IS NULL
        ALTER TABLE dbo.UsuarioSistema ADD Foto VARBINARY(MAX) NULL;
    IF COL_LENGTH(N'dbo.UsuarioSistema', N'Sexo') IS NULL
        ALTER TABLE dbo.UsuarioSistema ADD Sexo CHAR(1) NULL;
    IF COL_LENGTH(N'dbo.Socio', N'Foto') IS NULL
        ALTER TABLE dbo.Socio ADD Foto VARBINARY(MAX) NULL;
    IF COL_LENGTH(N'dbo.Socio', N'Sexo') IS NULL
        ALTER TABLE dbo.Socio ADD Sexo CHAR(1) NULL;

    /* Los usuarios del sistema son empleados y deben registrar su salario mensual. */
    IF COL_LENGTH(N'dbo.UsuarioSistema', N'Salario') IS NULL
    BEGIN
        ALTER TABLE dbo.UsuarioSistema
            ADD Salario DECIMAL(18,2) NOT NULL
                CONSTRAINT DF_UsuarioSistema_Salario DEFAULT 0 WITH VALUES;
    END;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.check_constraints
        WHERE name = N'CK_UsuarioSistema_Salario'
          AND parent_object_id = OBJECT_ID(N'dbo.UsuarioSistema')
    )
    BEGIN
        EXEC(N'ALTER TABLE dbo.UsuarioSistema WITH CHECK
            ADD CONSTRAINT CK_UsuarioSistema_Salario CHECK (Salario >= 0);');
    END;

    /* Las rutinas actuales son plantillas generales, no pertenecen a un socio. */
    IF COL_LENGTH(N'dbo.Rutina', N'IdSocio') IS NOT NULL
    BEGIN
        ALTER TABLE dbo.Rutina
            ALTER COLUMN IdSocio INT NULL;
    END;

    /* Relación de una plantilla general con una membresía concreta. */
    IF OBJECT_ID(N'dbo.RutinaAsignacion', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.RutinaAsignacion
        (
            IdRutinaAsignacion INT IDENTITY(1,1) NOT NULL
                CONSTRAINT PK_RutinaAsignacion PRIMARY KEY,
            FechaAsignacion DATETIME2 NOT NULL,
            FechaFin DATETIME2 NULL,
            Estado BIT NOT NULL CONSTRAINT DF_RutinaAsignacion_Estado DEFAULT 1,
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

    /* Catalogo por plan. La carga inicial conserva la rutina base y las
       rutinas ya asignadas a sus membresias; no se repite al volver a migrar. */
    IF OBJECT_ID(N'dbo.PlanRutina', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.PlanRutina
        (
            IdPlan INT NOT NULL,
            IdRutina INT NOT NULL,
            CONSTRAINT PK_PlanRutina PRIMARY KEY (IdPlan, IdRutina),
            CONSTRAINT FK_PlanRutina_Plan FOREIGN KEY (IdPlan) REFERENCES dbo.[Plan](IdPlan),
            CONSTRAINT FK_PlanRutina_Rutina FOREIGN KEY (IdRutina) REFERENCES dbo.Rutina(IdRutina)
        );

        INSERT INTO dbo.PlanRutina (IdPlan, IdRutina)
        SELECT IdPlan, IdRutina FROM dbo.[Plan]
        UNION
        SELECT m.IdPlan, a.IdRutina
        FROM dbo.RutinaAsignacion a
        INNER JOIN dbo.Membresia m ON m.IdMembresia = a.IdMembresia
        WHERE a.Estado = 1;
    END;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO

SELECT N'Migración completada. Ya podés ejecutar la sección de catálogo de SysGymDB.sql.' AS Resultado;
