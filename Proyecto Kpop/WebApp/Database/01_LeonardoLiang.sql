IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'LeonardoLiang')
BEGIN
    CREATE DATABASE LeonardoLiang;
END
GO
USE LeonardoLiang;
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LL_Usuarios')
BEGIN
    CREATE TABLE LL_Usuarios (
        UsuarioId INT PRIMARY KEY IDENTITY(1,1),
        Email NVARCHAR(150) UNIQUE NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        NombreCompleto NVARCHAR(200) NOT NULL,
        Rol NVARCHAR(50) NOT NULL DEFAULT 'Miembro', -- Admin, Moderador, Miembro
        Activo BIT NOT NULL DEFAULT 1,
        FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
        UltimoAcceso DATETIME NULL,
        CONSTRAINT CK_Usuario_Rol CHECK (Rol IN ('Admin', 'Moderador', 'Miembro'))
    );
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LL_Agencias')
BEGIN
    CREATE TABLE LL_Agencias (
        AgenciaId INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(200) NOT NULL,
        Pais NVARCHAR(100) NOT NULL,
        Fundacion INT NULL,
        Sitio NVARCHAR(500) NULL,
        LogoURL NVARCHAR(500) NULL,
        Descripcion NVARCHAR(MAX) NULL,
        Activa BIT NOT NULL DEFAULT 1,
        FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
    );
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LL_Idols')
BEGIN
    CREATE TABLE LL_Idols (
        IdolId INT PRIMARY KEY IDENTITY(1,1),
        NombreArtistico NVARCHAR(200) NOT NULL,
        NombreReal NVARCHAR(200) NULL,
        TipoIdol NVARCHAR(50) NOT NULL, -- Grupo, Solista
        AgenciaId INT NULL,
        FechaDebut DATE NULL,
        Genero NVARCHAR(50) NULL, -- Masculino, Femenino, Mixto
        Fandom NVARCHAR(150) NULL, -- Nombre del fandom (ARMY, BLINK, etc.)
        Pais NVARCHAR(100) NULL,
        Biografia NVARCHAR(MAX) NULL,
        FotoURL NVARCHAR(500) NULL,
        Activo BIT NOT NULL DEFAULT 1,
        FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Idol_Agencia FOREIGN KEY (AgenciaId) REFERENCES LL_Agencias(AgenciaId),
        CONSTRAINT CK_Idol_Tipo CHECK (TipoIdol IN ('Grupo', 'Solista')),
        CONSTRAINT CK_Idol_Genero CHECK (Genero IN ('Masculino', 'Femenino', 'Mixto', 'Otro'))
    );
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LL_Miembros')
BEGIN
    CREATE TABLE LL_Miembros (
        MiembroId INT PRIMARY KEY IDENTITY(1,1),
        UsuarioId INT NULL, -- Puede estar vinculado a un usuario
        Nombre NVARCHAR(150) NOT NULL,
        Apellido NVARCHAR(150) NOT NULL,
        Email NVARCHAR(150) UNIQUE NULL,
        Telefono NVARCHAR(50) NULL,
        RolFanclub NVARCHAR(100) NULL, -- Presidente, Vice, Tesorero, etc.
        IdolFavoritoId INT NULL,
        FechaIngreso DATE NOT NULL DEFAULT CAST(GETDATE() AS DATE),
        Biografia NVARCHAR(500) NULL,
        Activo BIT NOT NULL DEFAULT 1,
        FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Miembro_Usuario FOREIGN KEY (UsuarioId) REFERENCES LL_Usuarios(UsuarioId),
        CONSTRAINT FK_Miembro_Idol FOREIGN KEY (IdolFavoritoId) REFERENCES LL_Idols(IdolId)
    );
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LL_Eventos')
BEGIN
    CREATE TABLE LL_Eventos (
        EventoId INT PRIMARY KEY IDENTITY(1,1),
        Titulo NVARCHAR(300) NOT NULL,
        Descripcion NVARCHAR(MAX) NULL,
        IdolId INT NULL, -- Idol relacionado con el evento
        TipoEvento NVARCHAR(100) NULL, -- Meet & Greet, Concierto, Reunión, Cup Sleeve, etc.
        FechaEvento DATE NOT NULL,
        HoraInicio TIME NOT NULL,
        HoraFin TIME NULL,
        Zona NVARCHAR(200) NOT NULL, -- Ciudad, barrio, región
        Direccion NVARCHAR(500) NOT NULL,
        DireccionDetallada NVARCHAR(MAX) NULL,
        Capacidad INT NOT NULL DEFAULT 0,
        PuestosDisponibles INT NOT NULL DEFAULT 0,
        RequiereReserva BIT NOT NULL DEFAULT 1,
        Estado NVARCHAR(50) NOT NULL DEFAULT 'Programado', -- Programado, EnCurso, Finalizado, Cancelado
        ImagenURL NVARCHAR(500) NULL,
        CreadoPorUsuarioId INT NULL,
        FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
        UltimaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),
        CONSTRAINT FK_Evento_Idol FOREIGN KEY (IdolId) REFERENCES LL_Idols(IdolId),
        CONSTRAINT FK_Evento_Usuario FOREIGN KEY (CreadoPorUsuarioId) REFERENCES LL_Usuarios(UsuarioId),
        CONSTRAINT CK_Evento_Estado CHECK (Estado IN ('Programado', 'EnCurso', 'Finalizado', 'Cancelado')),
        CONSTRAINT CK_Evento_Capacidad CHECK (PuestosDisponibles <= Capacidad)
    );
END
GO
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LL_Reservas')
BEGIN
    CREATE TABLE LL_Reservas (
        ReservaId INT PRIMARY KEY IDENTITY(1,1),
        EventoId INT NOT NULL,
        MiembroId INT NULL,
        UsuarioId INT NULL, -- Usuario que hizo la reserva
        NombreReserva NVARCHAR(200) NOT NULL,
        EmailContacto NVARCHAR(150) NOT NULL,
        TelefonoContacto NVARCHAR(50) NULL,
        NumeroPersonas INT NOT NULL DEFAULT 1,
        Confirmada BIT NOT NULL DEFAULT 0,
        Asistio BIT NULL, -- NULL = no determinado, 0 = no asistió, 1 = sí asistió
        FechaReserva DATETIME NOT NULL DEFAULT GETDATE(),
        FechaCancelacion DATETIME NULL,
        Notas NVARCHAR(500) NULL,
        CONSTRAINT FK_Reserva_Evento FOREIGN KEY (EventoId) REFERENCES LL_Eventos(EventoId) ON DELETE CASCADE,
        CONSTRAINT FK_Reserva_Miembro FOREIGN KEY (MiembroId) REFERENCES LL_Miembros(MiembroId),
        CONSTRAINT FK_Reserva_Usuario FOREIGN KEY (UsuarioId) REFERENCES LL_Usuarios(UsuarioId),
        CONSTRAINT CK_Reserva_Personas CHECK (NumeroPersonas > 0 AND NumeroPersonas <= 10)
    );
END
GO
CREATE NONCLUSTERED INDEX IX_Eventos_Fecha 
ON LL_Eventos(FechaEvento DESC);
GO
CREATE NONCLUSTERED INDEX IX_Eventos_Zona 
ON LL_Eventos(Zona);
GO
CREATE NONCLUSTERED INDEX IX_Idols_Nombre 
ON LL_Idols(NombreArtistico);
GO
CREATE NONCLUSTERED INDEX IX_Miembros_Activo 
ON LL_Miembros(Activo, FechaIngreso DESC);
GO
CREATE NONCLUSTERED INDEX IX_Reservas_Evento 
ON LL_Reservas(EventoId, Confirmada);
GO
IF OBJECT_ID('VW_ProximosEventos', 'V') IS NOT NULL
    DROP VIEW VW_ProximosEventos;
GO
CREATE VIEW VW_ProximosEventos AS
SELECT 
    e.EventoId,
    e.Titulo,
    e.Descripcion,
    e.TipoEvento,
    e.FechaEvento,
    e.HoraInicio,
    e.HoraFin,
    e.Zona,
    e.Direccion,
    e.Capacidad,
    e.PuestosDisponibles,
    e.Estado,
    i.NombreArtistico AS Idol,
    i.Fandom,
    COUNT(r.ReservaId) AS TotalReservas,
    SUM(CASE WHEN r.Confirmada = 1 THEN 1 ELSE 0 END) AS ReservasConfirmadas
FROM LL_Eventos e
LEFT JOIN LL_Idols i ON e.IdolId = i.IdolId
LEFT JOIN LL_Reservas r ON e.EventoId = r.EventoId AND r.FechaCancelacion IS NULL
WHERE e.FechaEvento >= CAST(GETDATE() AS DATE)
    AND e.Estado IN ('Programado', 'EnCurso')
GROUP BY 
    e.EventoId, e.Titulo, e.Descripcion, e.TipoEvento, e.FechaEvento, 
    e.HoraInicio, e.HoraFin, e.Zona, e.Direccion, e.Capacidad, 
    e.PuestosDisponibles, e.Estado, i.NombreArtistico, i.Fandom;
GO
IF OBJECT_ID('VW_EstadisticasMiembros', 'V') IS NOT NULL
    DROP VIEW VW_EstadisticasMiembros;
GO
CREATE VIEW VW_EstadisticasMiembros AS
SELECT 
    m.MiembroId,
    m.Nombre + ' ' + m.Apellido AS NombreCompleto,
    m.Email,
    m.RolFanclub,
    i.NombreArtistico AS IdolFavorito,
    m.FechaIngreso,
    COUNT(DISTINCT r.EventoId) AS EventosReservados,
    SUM(CASE WHEN r.Asistio = 1 THEN 1 ELSE 0 END) AS EventosAsistidos,
    m.Activo
FROM LL_Miembros m
LEFT JOIN LL_Idols i ON m.IdolFavoritoId = i.IdolId
LEFT JOIN LL_Reservas r ON m.MiembroId = r.MiembroId
GROUP BY 
    m.MiembroId, m.Nombre, m.Apellido, m.Email, m.RolFanclub, 
    i.NombreArtistico, m.FechaIngreso, m.Activo;
GO
IF OBJECT_ID('VW_EventosPopulares', 'V') IS NOT NULL
    DROP VIEW VW_EventosPopulares;
GO
CREATE VIEW VW_EventosPopulares AS
SELECT TOP 10
    e.EventoId,
    e.Titulo,
    e.FechaEvento,
    e.Zona,
    i.NombreArtistico AS Idol,
    COUNT(r.ReservaId) AS TotalReservas,
    e.Capacidad,
    CAST((COUNT(r.ReservaId) * 100.0 / NULLIF(e.Capacidad, 0)) AS DECIMAL(5,2)) AS PorcentajeOcupacion
FROM LL_Eventos e
LEFT JOIN LL_Idols i ON e.IdolId = i.IdolId
LEFT JOIN LL_Reservas r ON e.EventoId = r.EventoId AND r.FechaCancelacion IS NULL
GROUP BY e.EventoId, e.Titulo, e.FechaEvento, e.Zona, i.NombreArtistico, e.Capacidad
ORDER BY COUNT(r.ReservaId) DESC;
GO
IF OBJECT_ID('SP_ReservarEvento', 'P') IS NOT NULL
    DROP PROCEDURE SP_ReservarEvento;
GO
CREATE PROCEDURE SP_ReservarEvento
    @EventoId INT,
    @MiembroId INT = NULL,
    @UsuarioId INT = NULL,
    @NombreReserva NVARCHAR(200),
    @EmailContacto NVARCHAR(150),
    @TelefonoContacto NVARCHAR(50) = NULL,
    @NumeroPersonas INT = 1,
    @ReservaId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @PuestosDisponibles INT;
    -- Verificar puestos disponibles
    SELECT @PuestosDisponibles = PuestosDisponibles 
    FROM LL_Eventos 
    WHERE EventoId = @EventoId;
    IF @PuestosDisponibles < @NumeroPersonas
    BEGIN
        RAISERROR('No hay suficientes puestos disponibles', 16, 1);
        RETURN;
    END
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Crear reserva
        INSERT INTO LL_Reservas (
            EventoId, MiembroId, UsuarioId, NombreReserva, 
            EmailContacto, TelefonoContacto, NumeroPersonas, Confirmada
        )
        VALUES (
            @EventoId, @MiembroId, @UsuarioId, @NombreReserva, 
            @EmailContacto, @TelefonoContacto, @NumeroPersonas, 1
        );
        SET @ReservaId = SCOPE_IDENTITY();
        -- Actualizar puestos disponibles
        UPDATE LL_Eventos
        SET PuestosDisponibles = PuestosDisponibles - @NumeroPersonas,
            UltimaActualizacion = GETDATE()
        WHERE EventoId = @EventoId;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
IF OBJECT_ID('SP_CancelarReserva', 'P') IS NOT NULL
    DROP PROCEDURE SP_CancelarReserva;
GO
CREATE PROCEDURE SP_CancelarReserva
    @ReservaId INT
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @EventoId INT;
    DECLARE @NumeroPersonas INT;
    -- Obtener datos de la reserva
    SELECT @EventoId = EventoId, @NumeroPersonas = NumeroPersonas
    FROM LL_Reservas
    WHERE ReservaId = @ReservaId AND FechaCancelacion IS NULL;
    IF @EventoId IS NULL
    BEGIN
        RAISERROR('Reserva no encontrada o ya cancelada', 16, 1);
        RETURN;
    END
    BEGIN TRANSACTION;
    BEGIN TRY
        -- Marcar como cancelada
        UPDATE LL_Reservas
        SET FechaCancelacion = GETDATE()
        WHERE ReservaId = @ReservaId;
        -- Liberar puestos
        UPDATE LL_Eventos
        SET PuestosDisponibles = PuestosDisponibles + @NumeroPersonas,
            UltimaActualizacion = GETDATE()
        WHERE EventoId = @EventoId;
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END
GO
INSERT INTO LL_Usuarios (Email, PasswordHash, NombreCompleto, Rol) VALUES
('admin@fanclub.com', 'HASH_PASSWORD_ADMIN', 'Administrador Sistema', 'Admin'),
('leonardo@fanclub.com', 'HASH_PASSWORD_LEO', 'Leonardo Liang', 'Admin'),
('moderador@fanclub.com', 'HASH_PASSWORD_MOD', 'María Moderadora', 'Moderador'),
('miembro1@fanclub.com', 'HASH_PASSWORD_MEM1', 'Carlos Miembro', 'Miembro'),
('miembro2@fanclub.com', 'HASH_PASSWORD_MEM2', 'Ana Participante', 'Miembro');
GO
INSERT INTO LL_Agencias (Nombre, Pais, Fundacion, Sitio) VALUES
('HYBE Corporation', 'Corea del Sur', 2005, 'https://hybecorp.com'),
('SM Entertainment', 'Corea del Sur', 1995, 'https://www.smtown.com'),
('YG Entertainment', 'Corea del Sur', 1996, 'https://www.ygfamily.com'),
('JYP Entertainment', 'Corea del Sur', 1997, 'https://www.jype.com'),
('Sony Music Entertainment Japan', 'Japón', 1968, 'https://www.sonymusic.co.jp');
GO
INSERT INTO LL_Idols (NombreArtistico, TipoIdol, AgenciaId, FechaDebut, Genero, Fandom, Pais) VALUES
('BTS', 'Grupo', 1, '2013-06-13', 'Masculino', 'ARMY', 'Corea del Sur'),
('BLACKPINK', 'Grupo', 3, '2016-08-08', 'Femenino', 'BLINK', 'Corea del Sur'),
('TWICE', 'Grupo', 4, '2015-10-20', 'Femenino', 'ONCE', 'Corea del Sur'),
('Stray Kids', 'Grupo', 4, '2018-03-25', 'Masculino', 'STAY', 'Corea del Sur'),
('NewJeans', 'Grupo', 1, '2022-08-01', 'Femenino', 'Bunnies', 'Corea del Sur'),
('IU', 'Solista', NULL, '2008-09-18', 'Femenino', 'UAENA', 'Corea del Sur');
GO
INSERT INTO LL_Miembros (UsuarioId, Nombre, Apellido, Email, RolFanclub, IdolFavoritoId, FechaIngreso) VALUES
(2, 'Leonardo', 'Liang', 'leonardo@fanclub.com', 'Presidente', 1, '2023-01-15'),
(3, 'María', 'González', 'moderador@fanclub.com', 'Vice-Presidente', 2, '2023-02-01'),
(4, 'Carlos', 'Rodríguez', 'miembro1@fanclub.com', 'Tesorero', 3, '2023-03-10'),
(5, 'Ana', 'Martínez', 'miembro2@fanclub.com', 'Secretaria', 4, '2023-04-05'),
(NULL, 'José', 'Hernández', 'jose@email.com', 'Miembro', 5, '2023-05-20');
GO
INSERT INTO LL_Eventos (
    Titulo, Descripcion, IdolId, TipoEvento, FechaEvento, HoraInicio, HoraFin,
    Zona, Direccion, Capacidad, PuestosDisponibles, CreadoPorUsuarioId
) VALUES
(
    'BTS Cup Sleeve Event', 
    'Evento especial con cup sleeves de BTS y rifas de photocards',
    1, 'Cup Sleeve', '2025-01-15', '14:00', '18:00',
    'Ciudad de Panamá - Casco Viejo', 'Café Central, Calle 5ta, Plaza Herrera',
    50, 50, 2
),
(
    'BLACKPINK Dance Cover Meetup',
    'Reunión para practicar covers de BLACKPINK y tomar fotos',
    2, 'Reunión', '2025-01-20', '16:00', '19:00',
    'Ciudad de Panamá - Albrook', 'Albrook Mall, Food Court 2do piso',
    30, 30, 2
),
(
    'TWICE Birthday Celebration',
    'Celebración del cumpleaños de Nayeon con decoraciones y torta',
    3, 'Celebración', '2025-01-25', '15:00', '20:00',
    'Ciudad de Panamá - Costa del Este', 'Town Center, Área de eventos',
    40, 40, 3
),
(
    'NewJeans Listening Party',
    'Escuchar el nuevo comeback de NewJeans juntos',
    5, 'Escucha', '2025-02-01', '18:00', '21:00',
    'Ciudad de Panamá - Calle Uruguay', 'K-Café, 2do piso',
    25, 25, 2
),
(
    'Multi-Fandom Game Night',
    'Noche de juegos K-pop con trivia y premios',
    NULL, 'Reunión', '2025-02-10', '17:00', '22:00',
    'Ciudad de Panamá - El Cangrejo', 'Community Center Via España',
    60, 60, 3
);
GO
DECLARE @Reserva1 INT, @Reserva2 INT, @Reserva3 INT;
EXEC SP_ReservarEvento 
    @EventoId = 1, @MiembroId = 1, @UsuarioId = 2,
    @NombreReserva = 'Leonardo Liang', @EmailContacto = 'leonardo@fanclub.com',
    @NumeroPersonas = 2, @ReservaId = @Reserva1 OUTPUT;
EXEC SP_ReservarEvento 
    @EventoId = 1, @MiembroId = 2, @UsuarioId = 3,
    @NombreReserva = 'María González', @EmailContacto = 'moderador@fanclub.com',
    @NumeroPersonas = 1, @ReservaId = @Reserva2 OUTPUT;
EXEC SP_ReservarEvento 
    @EventoId = 2, @MiembroId = 3, @UsuarioId = 4,
    @NombreReserva = 'Carlos Rodríguez', @EmailContacto = 'miembro1@fanclub.com',
    @NumeroPersonas = 3, @ReservaId = @Reserva3 OUTPUT;
GO
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME;
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS ORDER BY TABLE_NAME;
SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' ORDER BY ROUTINE_NAME;

