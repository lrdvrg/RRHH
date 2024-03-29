CREATE DATABASE RECURSOSHUMANOS
DROP DATABASE RECURSOSHUMANOS
USE RECURSOSHUMANOS

CREATE TABLE EMPLEADO(
ID_EMPLEADO INT IDENTITY,
NOMBRE VARCHAR(50),
APELLIDO VARCHAR(50),
TEL�FONO VARCHAR(50),
DEPARTAMENTO VARCHAR(50),
CARGO VARCHAR(50),
FECHA_INGRESO DATE,
SALARIO INT,
ESTATUS VARCHAR(50),  /*(ACTIVO/INACTIVO)*/ 
CONSTRAINT PK_IDE PRIMARY KEY (ID_EMPLEADO)
)

CREATE TABLE DEPARTAMENTO(
ID_DEPARTAMENTO INT IDENTITY,
NOMBRE VARCHAR(50),
ENCARGADO VARCHAR(50),
CONSTRAINT PK_IDD PRIMARY KEY (ID_DEPARTAMENTO)
)

CREATE TABLE CARGO(
ID_CARGO INT IDENTITY,
NOMBRE_EMPLEADO VARCHAR(50),
CARGO VARCHAR(50),
CONSTRAINT PK_IDCA PRIMARY KEY (ID_CARGO)
)

CREATE TABLE NOMINA(
ID_NOMINA INT IDENTITY,
A�O DATE,				
MES DATE,				
MONTO_TOTAL INT,
CONSTRAINT PK_IDN PRIMARY KEY (ID_NOMINA)
)

CREATE TABLE SALIDA_EMPLEADO(
ID_SALIDA_EMPLEADO INT IDENTITY, 
NOMBRE_EMPLEADO VARCHAR(50),      /*(LA SALIDA DE UN EMPLEADO, ES INACTIVARLO.)*/ 
TIPO_SALIDA VARCHAR(50),		  /*(RENUNCIA, DESPIDO, DESAHUCIO)*/ 
MOTIVO VARCHAR(100),
FECHA_SALIDA VARCHAR(50),
CONSTRAINT PK_IDN PRIMARY KEY (ID_SALIDA_EMPLEADO)
)

CREATE TABLE VACACIONES(
ID_VACACIONES INT IDENTITY,
NOMBRE_EMPLEADO VARCHAR(50),
DESDE VARCHAR(50),             
HASTA VARCHAR(50),			   
CORRESPONDIENTE VARCHAR(50),   
COMENTARIO VARCHAR(100), 
CONSTRAINT PK_IDV PRIMARY KEY (ID_VACACIONES)
)

CREATE TABLE PERMISO(
ID_PERMISO INT IDENTITY,
NOMBRE_EMPLEADO VARCHAR(50),
DESDE VARCHAR(50),             
HASTA VARCHAR(50),           
COMENTARIOS VARCHAR(100),
CONSTRAINT PK_IDP PRIMARY KEY (ID_PERMISO)
)

CREATE TABLE LICENCIA(
ID_LICECIA INT IDENTITY,
NOMBRE_EMPLEADO VARCHAR(50),
DESDE VARCHAR(50),           
HASTA VARCHAR(50),            
MOTIVO VARCHAR(50),
COMENTARIOS VARCHAR(100),
)