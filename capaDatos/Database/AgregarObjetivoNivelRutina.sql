/* SysGym — 23 de octubre de 2026. Clasifica las rutinas modelo sin alterar las libres existentes. */
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
BEGIN TRANSACTION;

IF COL_LENGTH('dbo.Rutina', 'Objetivo') IS NULL
    ALTER TABLE dbo.Rutina ADD Objetivo NVARCHAR(30) NULL;

IF COL_LENGTH('dbo.Rutina', 'Nivel') IS NULL
    ALTER TABLE dbo.Rutina ADD Nivel NVARCHAR(20) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Rutina_Objetivo' AND parent_object_id = OBJECT_ID('dbo.Rutina'))
    EXEC sys.sp_executesql N'ALTER TABLE dbo.Rutina WITH CHECK ADD CONSTRAINT CK_Rutina_Objetivo CHECK (Objetivo IS NULL OR Objetivo IN (N''Fuerza'', N''Hipertrofia'', N''Acondicionamiento''));';

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Rutina_Nivel' AND parent_object_id = OBJECT_ID('dbo.Rutina'))
    EXEC sys.sp_executesql N'ALTER TABLE dbo.Rutina WITH CHECK ADD CONSTRAINT CK_Rutina_Nivel CHECK (Nivel IS NULL OR Nivel IN (N''Principiante'', N''Intermedio'', N''Avanzado''));';

IF NOT EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Rutina_ModeloPar' AND parent_object_id = OBJECT_ID('dbo.Rutina'))
    EXEC sys.sp_executesql N'ALTER TABLE dbo.Rutina WITH CHECK ADD CONSTRAINT CK_Rutina_ModeloPar CHECK ((Objetivo IS NULL AND Nivel IS NULL) OR (Objetivo IS NOT NULL AND Nivel IS NOT NULL));';

COMMIT TRANSACTION;
