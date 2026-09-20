/* 20/09/2026. Migración aditiva para SysGymDB existente.
   Ejercicio admite cero, una o varias imágenes. No modifica registros existentes.
   No ejecutar SysGymDB.sql completo sobre una base con datos. */
USE SysGymDB;
GO
SET XACT_ABORT ON;
BEGIN TRY
    BEGIN TRANSACTION;
    IF OBJECT_ID(N'dbo.Ejercicio', N'U') IS NULL
        RAISERROR('No existe dbo.Ejercicio.', 16, 1);

    IF OBJECT_ID(N'dbo.EjercicioImagen', N'U') IS NULL
    BEGIN
        CREATE TABLE dbo.EjercicioImagen (
            IdEjercicioImagen INT IDENTITY(1,1) PRIMARY KEY,
            IdEjercicio INT NOT NULL,
            RutaRelativa NVARCHAR(260) NOT NULL,
            Orden INT NOT NULL,
            CONSTRAINT CK_EjercicioImagen_Orden CHECK (Orden > 0),
            CONSTRAINT UQ_EjercicioImagen_Ruta UNIQUE (RutaRelativa),
            CONSTRAINT FK_EjercicioImagen_Ejercicio
                FOREIGN KEY (IdEjercicio) REFERENCES dbo.Ejercicio(IdEjercicio)
        );
    END
    ELSE
        PRINT 'EjercicioImagen ya existe: no se modifica. Verificar su esquema antes de usarla.';
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    DECLARE @mensaje NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR('%s', 16, 1, @mensaje);
END CATCH;
GO
