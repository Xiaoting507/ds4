USE LeonardoLiang;
GO
IF OBJECT_ID('SP_ValidarLogin', 'P') IS NOT NULL
    DROP PROCEDURE SP_ValidarLogin;
GO
CREATE PROCEDURE SP_ValidarLogin
    @Email NVARCHAR(150),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        UsuarioId,
        Email,
        NombreCompleto,
        Rol,
        Activo
    FROM LL_Usuarios
    WHERE Email = @Email 
        AND PasswordHash = @PasswordHash 
        AND Activo = 1;
    -- Actualizar último acceso
    IF @@ROWCOUNT > 0
    BEGIN
        UPDATE LL_Usuarios
        SET UltimoAcceso = GETDATE()
        WHERE Email = @Email;
    END
END
GO
IF OBJECT_ID('SP_RegistrarUsuario', 'P') IS NOT NULL
    DROP PROCEDURE SP_RegistrarUsuario;
GO
CREATE PROCEDURE SP_RegistrarUsuario
    @Email NVARCHAR(150),
    @PasswordHash NVARCHAR(255),
    @NombreCompleto NVARCHAR(200),
    @UsuarioId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    -- Verificar si email ya existe
    IF EXISTS (SELECT 1 FROM LL_Usuarios WHERE Email = @Email)
    BEGIN
        SET @UsuarioId = -1;
        RETURN;
    END
    INSERT INTO LL_Usuarios (Email, PasswordHash, NombreCompleto, Rol, Activo, FechaRegistro)
    VALUES (@Email, @PasswordHash, @NombreCompleto, 'Miembro', 1, GETDATE());
    SET @UsuarioId = SCOPE_IDENTITY();
END
GO
IF OBJECT_ID('SP_ObtenerEventos', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerEventos;
GO
CREATE PROCEDURE SP_ObtenerEventos
    @SoloProximos BIT = 0,
    @Zona NVARCHAR(200) = NULL,
    @IdolId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        e.EventoId,
        e.Titulo,
        e.Descripcion,
        e.IdolId,
        e.TipoEvento,
        e.FechaEvento,
        e.HoraInicio,
        e.HoraFin,
        e.Zona,
        e.Direccion,
        e.DireccionDetallada,
        e.Capacidad,
        e.PuestosDisponibles,
        e.RequiereReserva,
        e.Estado,
        e.ImagenURL,
        e.FechaCreacion,
        i.NombreArtistico AS IdolNombre,
        i.Fandom AS IdolFandom,
        COUNT(r.ReservaId) AS TotalReservas
    FROM LL_Eventos e
    LEFT JOIN LL_Idols i ON e.IdolId = i.IdolId
    LEFT JOIN LL_Reservas r ON e.EventoId = r.EventoId AND r.FechaCancelacion IS NULL
    WHERE 
        (@SoloProximos = 0 OR (e.FechaEvento >= CAST(GETDATE() AS DATE) AND e.Estado = 'Programado'))
        AND (@Zona IS NULL OR e.Zona LIKE '%' + @Zona + '%')
        AND (@IdolId IS NULL OR e.IdolId = @IdolId)
    GROUP BY 
        e.EventoId, e.Titulo, e.Descripcion, e.IdolId, e.TipoEvento, 
        e.FechaEvento, e.HoraInicio, e.HoraFin, e.Zona, e.Direccion, 
        e.DireccionDetallada, e.Capacidad, e.PuestosDisponibles, 
        e.RequiereReserva, e.Estado, e.ImagenURL, e.FechaCreacion,
        i.NombreArtistico, i.Fandom
    ORDER BY e.FechaEvento, e.HoraInicio;
END
GO
IF OBJECT_ID('SP_ObtenerEventoPorId', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerEventoPorId;
GO
CREATE PROCEDURE SP_ObtenerEventoPorId
    @EventoId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        e.*,
        i.NombreArtistico AS IdolNombre,
        i.Fandom AS IdolFandom,
        u.NombreCompleto AS CreadoPorNombre
    FROM LL_Eventos e
    LEFT JOIN LL_Idols i ON e.IdolId = i.IdolId
    LEFT JOIN LL_Usuarios u ON e.CreadoPorUsuarioId = u.UsuarioId
    WHERE e.EventoId = @EventoId;
    -- Reservas del evento
    SELECT 
        r.*,
        m.Nombre + ' ' + m.Apellido AS MiembroNombre
    FROM LL_Reservas r
    LEFT JOIN LL_Miembros m ON r.MiembroId = m.MiembroId
    WHERE r.EventoId = @EventoId 
        AND r.FechaCancelacion IS NULL
    ORDER BY r.FechaReserva DESC;
END
GO
IF OBJECT_ID('SP_CrearEvento', 'P') IS NOT NULL
    DROP PROCEDURE SP_CrearEvento;
GO
CREATE PROCEDURE SP_CrearEvento
    @Titulo NVARCHAR(300),
    @Descripcion NVARCHAR(MAX),
    @IdolId INT,
    @TipoEvento NVARCHAR(100),
    @FechaEvento DATE,
    @HoraInicio TIME,
    @HoraFin TIME,
    @Zona NVARCHAR(200),
    @Direccion NVARCHAR(500),
    @DireccionDetallada NVARCHAR(MAX),
    @Capacidad INT,
    @ImagenURL NVARCHAR(500),
    @CreadoPorUsuarioId INT,
    @EventoId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LL_Eventos (
        Titulo, Descripcion, IdolId, TipoEvento, FechaEvento, 
        HoraInicio, HoraFin, Zona, Direccion, DireccionDetallada,
        Capacidad, PuestosDisponibles, RequiereReserva, Estado,
        ImagenURL, CreadoPorUsuarioId, FechaCreacion, UltimaActualizacion
    )
    VALUES (
        @Titulo, @Descripcion, @IdolId, @TipoEvento, @FechaEvento,
        @HoraInicio, @HoraFin, @Zona, @Direccion, @DireccionDetallada,
        @Capacidad, @Capacidad, 1, 'Programado',
        @ImagenURL, @CreadoPorUsuarioId, GETDATE(), GETDATE()
    );
    SET @EventoId = SCOPE_IDENTITY();
END
GO
IF OBJECT_ID('SP_ActualizarEvento', 'P') IS NOT NULL
    DROP PROCEDURE SP_ActualizarEvento;
GO
CREATE PROCEDURE SP_ActualizarEvento
    @EventoId INT,
    @Titulo NVARCHAR(300),
    @Descripcion NVARCHAR(MAX),
    @IdolId INT,
    @TipoEvento NVARCHAR(100),
    @FechaEvento DATE,
    @HoraInicio TIME,
    @HoraFin TIME,
    @Zona NVARCHAR(200),
    @Direccion NVARCHAR(500),
    @DireccionDetallada NVARCHAR(MAX),
    @Estado NVARCHAR(50),
    @ImagenURL NVARCHAR(500)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE LL_Eventos
    SET 
        Titulo = @Titulo,
        Descripcion = @Descripcion,
        IdolId = @IdolId,
        TipoEvento = @TipoEvento,
        FechaEvento = @FechaEvento,
        HoraInicio = @HoraInicio,
        HoraFin = @HoraFin,
        Zona = @Zona,
        Direccion = @Direccion,
        DireccionDetallada = @DireccionDetallada,
        Estado = @Estado,
        ImagenURL = @ImagenURL,
        UltimaActualizacion = GETDATE()
    WHERE EventoId = @EventoId;
END
GO
IF OBJECT_ID('SP_EliminarEvento', 'P') IS NOT NULL
    DROP PROCEDURE SP_EliminarEvento;
GO
CREATE PROCEDURE SP_EliminarEvento
    @EventoId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM LL_Eventos WHERE EventoId = @EventoId;
END
GO
IF OBJECT_ID('SP_ObtenerIdols', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerIdols;
GO
CREATE PROCEDURE SP_ObtenerIdols
    @SoloActivos BIT = 1,
    @TipoIdol NVARCHAR(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        i.*,
        a.Nombre AS AgenciaNombre,
        COUNT(DISTINCT e.EventoId) AS TotalEventos,
        COUNT(DISTINCT m.MiembroId) AS TotalFans
    FROM LL_Idols i
    LEFT JOIN LL_Agencias a ON i.AgenciaId = a.AgenciaId
    LEFT JOIN LL_Eventos e ON i.IdolId = e.IdolId
    LEFT JOIN LL_Miembros m ON i.IdolId = m.IdolFavoritoId AND m.Activo = 1
    WHERE 
        (@SoloActivos = 0 OR i.Activo = 1)
        AND (@TipoIdol IS NULL OR i.TipoIdol = @TipoIdol)
    GROUP BY 
        i.IdolId, i.NombreArtistico, i.NombreReal, i.TipoIdol, i.AgenciaId,
        i.FechaDebut, i.Genero, i.Fandom, i.Pais, i.Biografia, i.FotoURL,
        i.Activo, i.FechaRegistro, a.Nombre
    ORDER BY i.NombreArtistico;
END
GO
IF OBJECT_ID('SP_ObtenerIdolPorId', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerIdolPorId;
GO
CREATE PROCEDURE SP_ObtenerIdolPorId
    @IdolId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        i.*,
        a.Nombre AS AgenciaNombre
    FROM LL_Idols i
    LEFT JOIN LL_Agencias a ON i.AgenciaId = a.AgenciaId
    WHERE i.IdolId = @IdolId;
END
GO
IF OBJECT_ID('SP_CrearIdol', 'P') IS NOT NULL
    DROP PROCEDURE SP_CrearIdol;
GO
CREATE PROCEDURE SP_CrearIdol
    @NombreArtistico NVARCHAR(200),
    @NombreReal NVARCHAR(200),
    @TipoIdol NVARCHAR(50),
    @AgenciaId INT,
    @FechaDebut DATE,
    @Genero NVARCHAR(50),
    @Fandom NVARCHAR(150),
    @Pais NVARCHAR(100),
    @Biografia NVARCHAR(MAX),
    @FotoURL NVARCHAR(500),
    @IdolId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LL_Idols (
        NombreArtistico, NombreReal, TipoIdol, AgenciaId, FechaDebut,
        Genero, Fandom, Pais, Biografia, FotoURL, Activo, FechaRegistro
    )
    VALUES (
        @NombreArtistico, @NombreReal, @TipoIdol, @AgenciaId, @FechaDebut,
        @Genero, @Fandom, @Pais, @Biografia, @FotoURL, 1, GETDATE()
    );
    SET @IdolId = SCOPE_IDENTITY();
END
GO
IF OBJECT_ID('SP_ActualizarIdol', 'P') IS NOT NULL
    DROP PROCEDURE SP_ActualizarIdol;
GO
CREATE PROCEDURE SP_ActualizarIdol
    @IdolId INT,
    @NombreArtistico NVARCHAR(200),
    @NombreReal NVARCHAR(200),
    @TipoIdol NVARCHAR(50),
    @AgenciaId INT,
    @FechaDebut DATE,
    @Genero NVARCHAR(50),
    @Fandom NVARCHAR(150),
    @Pais NVARCHAR(100),
    @Biografia NVARCHAR(MAX),
    @FotoURL NVARCHAR(500),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE LL_Idols
    SET 
        NombreArtistico = @NombreArtistico,
        NombreReal = @NombreReal,
        TipoIdol = @TipoIdol,
        AgenciaId = @AgenciaId,
        FechaDebut = @FechaDebut,
        Genero = @Genero,
        Fandom = @Fandom,
        Pais = @Pais,
        Biografia = @Biografia,
        FotoURL = @FotoURL,
        Activo = @Activo
    WHERE IdolId = @IdolId;
END
GO
IF OBJECT_ID('SP_EliminarIdol', 'P') IS NOT NULL
    DROP PROCEDURE SP_EliminarIdol;
GO
CREATE PROCEDURE SP_EliminarIdol
    @IdolId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM LL_Idols WHERE IdolId = @IdolId;
END
GO
IF OBJECT_ID('SP_ObtenerMiembros', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerMiembros;
GO
CREATE PROCEDURE SP_ObtenerMiembros
    @SoloActivos BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        m.*,
        i.NombreArtistico AS IdolFavoritoNombre,
        u.Email AS UsuarioEmail,
        COUNT(r.ReservaId) AS TotalReservas
    FROM LL_Miembros m
    LEFT JOIN LL_Idols i ON m.IdolFavoritoId = i.IdolId
    LEFT JOIN LL_Usuarios u ON m.UsuarioId = u.UsuarioId
    LEFT JOIN LL_Reservas r ON m.MiembroId = r.MiembroId AND r.FechaCancelacion IS NULL
    WHERE (@SoloActivos = 0 OR m.Activo = 1)
    GROUP BY 
        m.MiembroId, m.UsuarioId, m.Nombre, m.Apellido, m.Email, m.Telefono,
        m.RolFanclub, m.IdolFavoritoId, m.FechaIngreso, m.Biografia,
        m.Activo, m.FechaRegistro, i.NombreArtistico, u.Email
    ORDER BY m.Apellido, m.Nombre;
END
GO
IF OBJECT_ID('SP_CrearMiembro', 'P') IS NOT NULL
    DROP PROCEDURE SP_CrearMiembro;
GO
CREATE PROCEDURE SP_CrearMiembro
    @UsuarioId INT,
    @Nombre NVARCHAR(150),
    @Apellido NVARCHAR(150),
    @Email NVARCHAR(150),
    @Telefono NVARCHAR(50),
    @RolFanclub NVARCHAR(100),
    @IdolFavoritoId INT,
    @FechaIngreso DATE,
    @Biografia NVARCHAR(500),
    @MiembroId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO LL_Miembros (
        UsuarioId, Nombre, Apellido, Email, Telefono, RolFanclub,
        IdolFavoritoId, FechaIngreso, Biografia, Activo, FechaRegistro
    )
    VALUES (
        @UsuarioId, @Nombre, @Apellido, @Email, @Telefono, @RolFanclub,
        @IdolFavoritoId, @FechaIngreso, @Biografia, 1, GETDATE()
    );
    SET @MiembroId = SCOPE_IDENTITY();
END
GO
IF OBJECT_ID('SP_ActualizarMiembro', 'P') IS NOT NULL
    DROP PROCEDURE SP_ActualizarMiembro;
GO
CREATE PROCEDURE SP_ActualizarMiembro
    @MiembroId INT,
    @Nombre NVARCHAR(150),
    @Apellido NVARCHAR(150),
    @Email NVARCHAR(150),
    @Telefono NVARCHAR(50),
    @RolFanclub NVARCHAR(100),
    @IdolFavoritoId INT,
    @Biografia NVARCHAR(500),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE LL_Miembros
    SET 
        Nombre = @Nombre,
        Apellido = @Apellido,
        Email = @Email,
        Telefono = @Telefono,
        RolFanclub = @RolFanclub,
        IdolFavoritoId = @IdolFavoritoId,
        Biografia = @Biografia,
        Activo = @Activo
    WHERE MiembroId = @MiembroId;
END
GO
IF OBJECT_ID('SP_ObtenerEstadisticasDashboard', 'P') IS NOT NULL
    DROP PROCEDURE SP_ObtenerEstadisticasDashboard;
GO
CREATE PROCEDURE SP_ObtenerEstadisticasDashboard
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        (SELECT COUNT(*) FROM LL_Idols WHERE Activo = 1) AS TotalIdols,
        (SELECT COUNT(*) FROM LL_Miembros WHERE Activo = 1) AS TotalMiembros,
        (SELECT COUNT(*) FROM LL_Eventos WHERE Estado != 'Cancelado') AS TotalEventos,
        (SELECT COUNT(*) FROM LL_Eventos WHERE FechaEvento >= CAST(GETDATE() AS DATE) AND Estado = 'Programado') AS ProximosEventos,
        (SELECT COUNT(*) FROM LL_Reservas WHERE FechaCancelacion IS NULL) AS TotalReservas;
END
GO

