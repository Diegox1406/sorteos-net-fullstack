-- Paso 1: Crear la base de datos
IF DB_ID('sorteo') IS NOT NULL 
BEGIN
    ALTER DATABASE sorteo SET SINGLE_USER WITH ROLLBACK IMMEDIATE; 
    DROP DATABASE sorteo; 
END
GO

CREATE DATABASE sorteo; 
GO

USE sorteo; 
GO

-- Paso 2: Crear tabla 'boleto'
CREATE TABLE boleto (
  idboleto BIGINT PRIMARY KEY IDENTITY(1,1),
  boleto VARCHAR(7) NOT NULL DEFAULT '00001',
  fechaApartado DATETIME NULL,
  fechaCompra DATETIME NULL,
  comprado BIGINT NOT NULL DEFAULT 0,
  created_at DATETIME NULL,
  updated_at DATETIME NULL
);
GO

-- Paso 3: Crear tabla 'users'
CREATE TABLE users (
  id BIGINT PRIMARY KEY IDENTITY(1,1),
  name VARCHAR(255) NULL,
  email VARCHAR(255) NOT NULL,
  username VARCHAR(255) NOT NULL,
  password VARCHAR(255) NOT NULL,
  created_at DATETIME NULL,
  updated_at DATETIME NULL,

  CONSTRAINT UQ_users_email UNIQUE(email),
  CONSTRAINT UQ_users_username UNIQUE(username)
);
GO

-- Paso 4: Crear tabla 'participante'
CREATE TABLE participante (
  idparticipante BIGINT PRIMARY KEY IDENTITY(1,1),
  nombre VARCHAR(255) NOT NULL,
  telefono VARCHAR(255) NOT NULL,
  created_at DATETIME NULL,
  updated_at DATETIME NULL,
  user_id BIGINT NULL,
  boleto_id BIGINT NULL,

  CONSTRAINT FK_participante_user FOREIGN KEY (user_id) REFERENCES users(id),
  CONSTRAINT FK_participante_boleto FOREIGN KEY (boleto_id) REFERENCES boleto(idboleto)
);
GO

-- Paso 5: Crear tabla 'apartados'
CREATE TABLE apartados (
  idapartados BIGINT PRIMARY KEY IDENTITY(1,1),
  boleto INT NOT NULL,
  fechaapartados DATETIME NOT NULL,
  boleto_id BIGINT NULL,
  created_at DATETIME NULL,
  updated_at DATETIME NULL,

  CONSTRAINT FK_apartados_boleto FOREIGN KEY (boleto_id) REFERENCES boleto(idboleto)
);
GO

-- Insertar usuario administrador
INSERT INTO users (name, email, username, password, created_at, updated_at)
VALUES 
('Admin', 'admin@gmail.com', 'admin', 'hola1234', GETDATE(), GETDATE());
GO

-- Insertar 30 boletos sin apartar
DECLARE @i INT = 1;
WHILE @i <= 30
BEGIN
    INSERT INTO boleto (boleto, fechaApartado, fechaCompra, comprado, created_at, updated_at)
    VALUES (
        RIGHT('00000' + CAST(@i AS VARCHAR), 5),
        NULL,
        NULL,
        0,
        GETDATE(),
        GETDATE()
    );
    SET @i += 1;
END
GO

-- Insertar 3 participantes asociados a los primeros 3 boletos
INSERT INTO participante (nombre, telefono, user_id, boleto_id, created_at, updated_at)
VALUES
('Carlos Gómez', '555-1234567', 1, 1, GETDATE(), GETDATE()),
('María López', '555-9876543', 1, 2, GETDATE(), GETDATE()),
('Ana Pérez', '555-5555555', 1, 3, GETDATE(), GETDATE());
GO

-- NOTA: No se insertan registros en 'apartados' para dejar todos los boletos libres.