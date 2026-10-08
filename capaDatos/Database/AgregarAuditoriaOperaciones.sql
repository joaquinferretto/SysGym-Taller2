/* SysGym — 8 de octubre de 2026. Ejecutar en la base existente, sin borrar ni reconstruir datos. */
SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF COL_LENGTH('dbo.Pago', 'IdUsuarioRegistro') IS NULL
    ALTER TABLE dbo.Pago ADD IdUsuarioRegistro INT NULL;
IF COL_LENGTH('dbo.UsuarioSistema', 'IdUsuarioCreador') IS NULL
    ALTER TABLE dbo.UsuarioSistema ADD IdUsuarioCreador INT NULL;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Pago_UsuarioRegistro')
    ALTER TABLE dbo.Pago ADD CONSTRAINT FK_Pago_UsuarioRegistro
        FOREIGN KEY (IdUsuarioRegistro) REFERENCES dbo.UsuarioSistema(IdUsuarioSistema);
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_UsuarioSistema_Creador')
    ALTER TABLE dbo.UsuarioSistema ADD CONSTRAINT FK_UsuarioSistema_Creador
        FOREIGN KEY (IdUsuarioCreador) REFERENCES dbo.UsuarioSistema(IdUsuarioSistema);

IF OBJECT_ID('dbo.AuditoriaOperacion', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.AuditoriaOperacion (
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
            FOREIGN KEY (IdUsuario) REFERENCES dbo.UsuarioSistema(IdUsuarioSistema)
    );
    CREATE INDEX IX_AuditoriaOperacion_FechaHora
        ON dbo.AuditoriaOperacion (FechaHora DESC, IdAuditoria DESC);
END;

COMMIT TRANSACTION;
