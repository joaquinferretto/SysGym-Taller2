/* SysGym — 23 de octubre de 2026. Vincula cada pago con su cuota histórica sin alterar el pago vigente. */
SET XACT_ABORT ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET ARITHABORT ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET NUMERIC_ROUNDABORT OFF;
BEGIN TRANSACTION;

IF COL_LENGTH('dbo.Pago', 'IdCuotaMembresia') IS NULL
    ALTER TABLE dbo.Pago ADD IdCuotaMembresia INT NULL;

/* SQL dinámico acotado: la columna puede haberse creado en este mismo lote. */
EXEC sys.sp_executesql N'
    UPDATE p
    SET IdCuotaMembresia = c.IdCuotaMembresia
    FROM dbo.Pago AS p
    INNER JOIN dbo.CuotaMembresia AS c ON c.IdRegistroPago = p.IdRegistroPago
    WHERE p.IdCuotaMembresia IS NULL;';

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Pago_CuotaMembresia' AND parent_object_id = OBJECT_ID('dbo.Pago'))
    EXEC sys.sp_executesql N'ALTER TABLE dbo.Pago WITH CHECK ADD CONSTRAINT FK_Pago_CuotaMembresia
        FOREIGN KEY (IdCuotaMembresia) REFERENCES dbo.CuotaMembresia(IdCuotaMembresia);';

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pago_IdCuotaMembresia' AND object_id = OBJECT_ID('dbo.Pago'))
    EXEC sys.sp_executesql N'CREATE INDEX IX_Pago_IdCuotaMembresia ON dbo.Pago(IdCuotaMembresia)
        WHERE IdCuotaMembresia IS NOT NULL;';

COMMIT TRANSACTION;
