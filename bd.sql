
-- Tabla Cliente
CREATE TABLE Cliente (
   Identificacion INT identity(1,1) PRIMARY KEY,
   Nombres VARCHAR(50) NOT NULL,
   Apellidos VARCHAR(50) NOT NULL,
   Direccion VARCHAR(100),
   Telefono VARCHAR(15)
);

-- Tabla Mesero
CREATE TABLE Mesero (
   IdMesero INT identity(1,1) PRIMARY KEY,
   Nombres VARCHAR(50) NOT NULL,
   Apellidos VARCHAR(50) NOT NULL,
   Edad INT,
   Antiguedad DATE NOT NULL
);

-- Tabla Mesa
CREATE TABLE Mesa (
   NroMesa INT identity(1,1) PRIMARY KEY,
   Nombre VARCHAR(50) NOT NULL,
   Reservada bit DEFAULT 0,
   Puestos INT NOT NULL
);

-- Tabla Supervisor
CREATE TABLE Supervisor (
   IdSupervisor INT identity(1,1) PRIMARY KEY,
   Nombres VARCHAR(50) NOT NULL,
   Apellidos VARCHAR(50) NOT NULL,
   Edad INT,
   Antiguedad DATE NOT NULL
);

-- Tabla Factura
CREATE TABLE Factura (
   NroFactura INT identity(1,1) PRIMARY KEY,
   IdCliente INT NOT NULL,
   NroMesa INT NOT NULL,
   IdMesero INT NOT NULL,
   Fecha DATE NOT NULL,
   FOREIGN KEY (IdCliente) REFERENCES Cliente(Identificacion) ON DELETE CASCADE,
   FOREIGN KEY (NroMesa) REFERENCES Mesa(NroMesa) ON DELETE CASCADE,
   FOREIGN KEY (IdMesero) REFERENCES Mesero(IdMesero) ON DELETE CASCADE
);

-- Tabla DetalleFactura
CREATE TABLE DetalleFactura (
   IdDetalleFactura INT identity(1,1) PRIMARY KEY,
   NroFactura INT NOT NULL,
   IdSupervisor INT NOT NULL,
   Plato VARCHAR(100) NOT NULL,
   Valor DECIMAL(10, 2) NOT NULL,
   FOREIGN KEY (NroFactura) REFERENCES Factura(NroFactura) ON DELETE CASCADE,
   FOREIGN KEY (IdSupervisor) REFERENCES Supervisor(IdSupervisor) ON DELETE CASCADE
);

-- Insertar datos de prueba para la tabla Cliente
INSERT INTO Cliente (Nombres, Apellidos, Direccion, Telefono)
VALUES 
('Juan', 'P�rez', 'Calle 123', '3001234567'),
('Mar�a', 'G�mez', 'Avenida 45', '3109876543'),
('Carlos', 'Mart�nez', 'Carrera 89', '3155678999');

-- Insertar datos de prueba para la tabla Mesero
INSERT INTO Mesero (Nombres, Apellidos, Edad, Antiguedad)
VALUES 
('Luis', 'Ram�rez', 25, '2020-05-15'),
('Ana', 'Fern�ndez', 30, '2018-08-20'),
('Pedro', 'L�pez', 22, '2023-01-10');

-- Insertar datos de prueba para la tabla Mesa
INSERT INTO Mesa (Nombre, Reservada, Puestos)
VALUES 
('Mesa VIP', 1, 4),
('Mesa Familiar', 0, 6),
('Mesa Terraza', 0, 2);

-- Insertar datos de prueba para la tabla Supervisor
INSERT INTO Supervisor (Nombres, Apellidos, Edad, Antiguedad)
VALUES 
('Sof�a', 'Castro', 35, '2015-09-12'),
('David', 'Moreno', 40, '2010-04-03'),
('Clara', 'Jim�nez', 28, '2019-07-01');

-- Insertar datos de prueba para la tabla Factura
INSERT INTO Factura (IdCliente, NroMesa, IdMesero, Fecha)
VALUES 
(1, 1, 2, '2023-11-15'),
(2, 2, 1, '2023-11-16'),
(3, 3, 3, '2023-11-17');

-- Insertar datos de prueba para la tabla DetalleFactura
INSERT INTO DetalleFactura (NroFactura, IdSupervisor, Plato, Valor)
VALUES 
(1, 1, 'Plato Ejecutivo', 15000.00),
(1, 2, 'Bandeja Paisa', 20000.00),
(2, 2, 'Hamburguesa', 12000.00),
(2, 3, 'Ensalada C�sar', 10000.00),
(3, 1, 'Pizza Familiar', 30000.00);

-- select * from DetalleFactura
-- select * from Factura

