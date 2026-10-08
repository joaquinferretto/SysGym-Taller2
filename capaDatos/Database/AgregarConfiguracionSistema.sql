SET XACT_ABORT ON;
BEGIN TRANSACTION;
IF OBJECT_ID(N'dbo.ConfiguracionSistema', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.ConfiguracionSistema (
        IdConfiguracion INT NOT NULL CONSTRAINT PK_ConfiguracionSistema PRIMARY KEY,
        MaxCuotasVencidasPermitidas INT NOT NULL CONSTRAINT DF_Configuracion_MaxVencidas DEFAULT 2,
        MaxMesesAnticipacionCuotas INT NOT NULL CONSTRAINT DF_Configuracion_MaxAnticipacion DEFAULT 1,
        DiasAvisoVencimiento INT NOT NULL CONSTRAINT DF_Configuracion_DiasAviso DEFAULT 7,
        CONSTRAINT CK_Configuracion_Unica CHECK (IdConfiguracion = 1),
        CONSTRAINT CK_Configuracion_MaxVencidas CHECK (MaxCuotasVencidasPermitidas BETWEEN 1 AND 120),
        CONSTRAINT CK_Configuracion_MaxAnticipacion CHECK (MaxMesesAnticipacionCuotas BETWEEN 0 AND 120),
        CONSTRAINT CK_Configuracion_DiasAviso CHECK (DiasAvisoVencimiento BETWEEN 0 AND 365)
    );
END;
IF NOT EXISTS (SELECT 1 FROM dbo.ConfiguracionSistema WHERE IdConfiguracion = 1)
    INSERT INTO dbo.ConfiguracionSistema (IdConfiguracion, MaxCuotasVencidasPermitidas, MaxMesesAnticipacionCuotas, DiasAvisoVencimiento)
    VALUES (1, 2, 1, 7);
COMMIT TRANSACTION;
