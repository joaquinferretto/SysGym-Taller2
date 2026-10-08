USE SysGymDB;
GO
-- Actualización aditiva. No reconstruye ni inventa fechas históricas.
IF COL_LENGTH('dbo.Socio', 'FechaAlta') IS NULL
    ALTER TABLE dbo.Socio ADD FechaAlta DATETIME2 NULL;
GO
