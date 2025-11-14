-- Mostrar boletos activos (comprados = 1)
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_MostrarBoletosComprados') 
DROP PROCEDURE SP_MostrarBoletosComprados  
GO
CREATE PROC SP_MostrarBoletosComprados
AS
BEGIN
    SELECT * FROM boleto WHERE comprado = 1;
END
GO

-- Mostrar todos los boletos
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_MostrarBoletos') 
DROP PROCEDURE SP_MostrarBoletos  
GO
CREATE PROC SP_MostrarBoletos
AS
BEGIN
    SELECT * FROM boleto;
END
GO

-- Insertar boleto
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_RegistrarBoleto') 
DROP PROCEDURE SP_RegistrarBoleto  
GO
CREATE PROC SP_RegistrarBoleto
    @boleto VARCHAR(7),
    @fechaApartado DATETIME = NULL,
    @fechaCompra DATETIME = NULL,
    @comprado BIGINT = 0
AS
BEGIN
    BEGIN TRAN SP_RegistrarBoleto
    BEGIN TRY
        INSERT INTO boleto (boleto, fechaApartado, fechaCompra, comprado, created_at, updated_at)
        VALUES (@boleto, @fechaApartado, @fechaCompra, @comprado, GETDATE(), GETDATE());
        COMMIT TRAN SP_RegistrarBoleto
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_RegistrarBoleto
    END CATCH
END
GO

-- Buscar boleto por id
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_BuscarBoletoPorId') 
DROP PROCEDURE SP_BuscarBoletoPorId  
GO
CREATE PROC SP_BuscarBoletoPorId
    @idboleto BIGINT
AS
BEGIN
    SELECT * FROM boleto WHERE idboleto = @idboleto;
END
GO

-- Actualizar boleto
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_ActualizarBoleto') 
DROP PROCEDURE SP_ActualizarBoleto  
GO
CREATE PROC SP_ActualizarBoleto
    @idboleto BIGINT,
    @boleto VARCHAR(7),
    @fechaApartado DATETIME = NULL,
    @fechaCompra DATETIME = NULL,
    @comprado BIGINT
AS
BEGIN
    BEGIN TRAN SP_ActualizarBoleto
    BEGIN TRY
        UPDATE boleto
        SET boleto = @boleto,
            fechaApartado = @fechaApartado,
            fechaCompra = @fechaCompra,
            comprado = @comprado,
            updated_at = GETDATE()
        WHERE idboleto = @idboleto;
        COMMIT TRAN SP_ActualizarBoleto
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_ActualizarBoleto
    END CATCH
END
GO

-- Eliminar boleto (marcar comprado = 0)
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_EliminarBoleto') 
DROP PROCEDURE SP_EliminarBoleto  
GO
CREATE PROC SP_EliminarBoleto
    @idboleto BIGINT
AS
BEGIN
    BEGIN TRAN SP_EliminarBoleto
    BEGIN TRY
        UPDATE boleto SET comprado = 0, updated_at = GETDATE() WHERE idboleto = @idboleto;
        COMMIT TRAN SP_EliminarBoleto
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_EliminarBoleto
    END CATCH
END
GO

-- Habilitar boleto (marcar comprado = 1)
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_HabilitarBoleto') 
DROP PROCEDURE SP_HabilitarBoleto  
GO
CREATE PROC SP_HabilitarBoleto
    @idboleto BIGINT
AS
BEGIN
    BEGIN TRAN SP_HabilitarBoleto
    BEGIN TRY
        UPDATE boleto SET comprado = 1, updated_at = GETDATE() WHERE idboleto = @idboleto;
        COMMIT TRAN SP_HabilitarBoleto
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_HabilitarBoleto
    END CATCH
END
GO






-- Mostrar usuarios
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_MostrarUsers') 
DROP PROCEDURE SP_MostrarUsers  
GO
CREATE PROC SP_MostrarUsers
AS
BEGIN
    SELECT * FROM users;
END
GO

-- Insertar usuario
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_RegistrarUser') 
DROP PROCEDURE SP_RegistrarUser  
GO
CREATE PROC SP_RegistrarUser
    @name VARCHAR(255) = NULL,
    @email VARCHAR(255),
    @username VARCHAR(255),
    @password VARCHAR(255)
AS
BEGIN
    BEGIN TRAN SP_RegistrarUser
    BEGIN TRY
        INSERT INTO users (name, email, username, password, created_at, updated_at)
        VALUES (@name, @email, @username, @password, GETDATE(), GETDATE());
        COMMIT TRAN SP_RegistrarUser
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_RegistrarUser
    END CATCH
END
GO

-- Buscar usuario por id
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_BuscarUserPorId') 
DROP PROCEDURE SP_BuscarUserPorId  
GO
CREATE PROC SP_BuscarUserPorId
    @id BIGINT
AS
BEGIN
    SELECT * FROM users WHERE id = @id;
END
GO

-- Actualizar usuario
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_ActualizarUser') 
DROP PROCEDURE SP_ActualizarUser  
GO
CREATE PROC SP_ActualizarUser
    @id BIGINT,
    @name VARCHAR(255) = NULL,
    @email VARCHAR(255),
    @username VARCHAR(255),
    @password VARCHAR(255)
AS
BEGIN
    BEGIN TRAN SP_ActualizarUser
    BEGIN TRY
        UPDATE users
        SET name = @name,
            email = @email,
            username = @username,
            password = @password,
            updated_at = GETDATE()
        WHERE id = @id;
        COMMIT TRAN SP_ActualizarUser
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_ActualizarUser
    END CATCH
END
GO

-- Eliminar usuario (opcional: podrías hacer un borrado lógico o físico; aquí un borrado físico)
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_EliminarUser') 
DROP PROCEDURE SP_EliminarUser  
GO
CREATE PROC SP_EliminarUser
    @id BIGINT
AS
BEGIN
    BEGIN TRAN SP_EliminarUser
    BEGIN TRY
        DELETE FROM users WHERE id = @id;
        COMMIT TRAN SP_EliminarUser
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_EliminarUser
    END CATCH
END
GO



-- Mostrar participantes
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_MostrarParticipante') 
DROP PROCEDURE SP_MostrarParticipante  
GO
CREATE PROC SP_MostrarParticipante
AS
BEGIN
    SELECT * FROM participante;
END
GO

-- Insertar participante
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_RegistrarParticipante') 
DROP PROCEDURE SP_RegistrarParticipante  
GO
CREATE PROC SP_RegistrarParticipante
    @nombre VARCHAR(255),
    @telefono VARCHAR(255),
    @user_id BIGINT = NULL,
    @boleto_id BIGINT = NULL
AS
BEGIN
    BEGIN TRAN SP_RegistrarParticipante
    BEGIN TRY
        INSERT INTO participante (nombre, telefono, user_id, boleto_id, created_at, updated_at)
        VALUES (@nombre, @telefono, @user_id, @boleto_id, GETDATE(), GETDATE());
        COMMIT TRAN SP_RegistrarParticipante
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_RegistrarParticipante
    END CATCH
END
GO

-- Buscar participante por id
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_BuscarParticipantePorId') 
DROP PROCEDURE SP_BuscarParticipantePorId  
GO
CREATE PROC SP_BuscarParticipantePorId
    @idparticipante BIGINT
AS
BEGIN
    SELECT * FROM participante WHERE idparticipante = @idparticipante;
END
GO

-- Actualizar participante
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_ActualizarParticipante') 
DROP PROCEDURE SP_ActualizarParticipante  
GO
CREATE PROC SP_ActualizarParticipante
    @idparticipante BIGINT,
    @nombre VARCHAR(255),
    @telefono VARCHAR(255),
    @user_id BIGINT = NULL,
    @boleto_id BIGINT = NULL
AS
BEGIN
    BEGIN TRAN SP_ActualizarParticipante
    BEGIN TRY
        UPDATE participante
        SET nombre = @nombre,
            telefono = @telefono,
            user_id = @user_id,
            boleto_id = @boleto_id,
            updated_at = GETDATE()
        WHERE idparticipante = @idparticipante;
        COMMIT TRAN SP_ActualizarParticipante
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_ActualizarParticipante
    END CATCH
END
GO

-- Eliminar participante (borrado físico)
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_EliminarParticipante') 
DROP PROCEDURE SP_EliminarParticipante  
GO
CREATE PROC SP_EliminarParticipante
    @idparticipante BIGINT
AS
BEGIN
    BEGIN TRAN SP_EliminarParticipante
    BEGIN TRY
        DELETE FROM participante WHERE idparticipante = @idparticipante;
        COMMIT TRAN SP_EliminarParticipante
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_EliminarParticipante
    END CATCH
END
GO



-- Mostrar apartados
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_MostrarApartados') 
DROP PROCEDURE SP_MostrarApartados  
GO
CREATE PROC SP_MostrarApartados
AS
BEGIN
    SELECT * FROM apartados;
END
GO

-- Eliminar el procedimiento si existe
IF EXISTS (SELECT * FROM sys.procedures WHERE NAME = 'SP_RegistrarApartado')
    DROP PROCEDURE SP_RegistrarApartado;
GO

-- Insertar Apartados
CREATE PROCEDURE SP_RegistrarApartado
    @boleto INT,
    @fechaapartados DATETIME,
    @boleto_id BIGINT,
    @resultado INT OUTPUT  -- <--- NUEVO parámetro de salida
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRAN;

    BEGIN TRY
        -- Verificar si ya existe un apartado para ese boleto_id
        IF EXISTS (SELECT 1 FROM apartados WHERE boleto_id = @boleto_id)
        BEGIN
            SET @resultado = -1;  -- boleto ya apartado
            ROLLBACK TRAN;
            RETURN;
        END

        -- Insertar en tabla 'apartados'
        INSERT INTO apartados (boleto, fechaapartados, boleto_id, created_at, updated_at)
        VALUES (@boleto, @fechaapartados, @boleto_id, GETDATE(), GETDATE());

        -- Actualizar tabla 'boleto'
        UPDATE boleto
        SET fechaApartado = @fechaapartados,
            updated_at = GETDATE()
        WHERE idboleto = @boleto_id;

        COMMIT TRAN;
        SET @resultado = 1;  -- éxito
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN;
        SET @resultado = 0;  -- error general
    END CATCH
END
GO

-- Buscar apartado por id
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_BuscarApartadoPorId') 
DROP PROCEDURE SP_BuscarApartadoPorId  
GO
CREATE PROC SP_BuscarApartadoPorId
    @idapartados BIGINT
AS
BEGIN
    SELECT * FROM apartados WHERE idapartados = @idapartados;
END
GO

-- Actualizar apartado
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_ActualizarApartado') 
DROP PROCEDURE SP_ActualizarApartado  
GO
CREATE PROC SP_ActualizarApartado
    @idapartados BIGINT,
    @boleto INT,
    @fechaapartados DATETIME,
    @boleto_id BIGINT = NULL
AS
BEGIN
    BEGIN TRAN SP_ActualizarApartado
    BEGIN TRY
        UPDATE apartados
        SET boleto = @boleto,
            fechaapartados = @fechaapartados,
            boleto_id = @boleto_id,
            updated_at = GETDATE()
        WHERE idapartados = @idapartados;
        COMMIT TRAN SP_ActualizarApartado
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_ActualizarApartado
    END CATCH
END
GO

-- Eliminar apartado (borrado físico)
IF EXISTS(SELECT * FROM sys.procedures WHERE NAME='SP_EliminarApartado') 
DROP PROCEDURE SP_EliminarApartado  
GO
CREATE PROC SP_EliminarApartado
    @idapartados BIGINT
AS
BEGIN
    BEGIN TRAN SP_EliminarApartado
    BEGIN TRY
        DELETE FROM apartados WHERE idapartados = @idapartados;
        COMMIT TRAN SP_EliminarApartado
    END TRY
    BEGIN CATCH
        ROLLBACK TRAN SP_EliminarApartado
    END CATCH
END
GO



