create database ParqueosDB;

USE [ParqueosDB]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarifa]') AND type in (N'U'))
ALTER TABLE [dbo].[Tarifa] DROP CONSTRAINT IF EXISTS [CK_Tarifa_Monto]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Parqueo]') AND type in (N'U'))
ALTER TABLE [dbo].[Parqueo] DROP CONSTRAINT IF EXISTS [CK_Parqueo_Capacidad]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
ALTER TABLE [dbo].[Factura] DROP CONSTRAINT IF EXISTS [CK_Factura_Monto]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[Vehiculo] DROP CONSTRAINT IF EXISTS [FK_Vehiculo_TipoVehiculo]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[Vehiculo] DROP CONSTRAINT IF EXISTS [FK_Vehiculo_Cliente]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarifa]') AND type in (N'U'))
ALTER TABLE [dbo].[Tarifa] DROP CONSTRAINT IF EXISTS [FK_Tarifa_TipoVehiculo]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IngresoVehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[IngresoVehiculo] DROP CONSTRAINT IF EXISTS [FK_Ingreso_Vehiculo]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IngresoVehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[IngresoVehiculo] DROP CONSTRAINT IF EXISTS [FK_Ingreso_Espacio]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
ALTER TABLE [dbo].[Factura] DROP CONSTRAINT IF EXISTS [FK_Factura_Ingreso]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EspacioParqueo]') AND type in (N'U'))
ALTER TABLE [dbo].[EspacioParqueo] DROP CONSTRAINT IF EXISTS [FK_Espacio_Parqueo]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[Vehiculo] DROP CONSTRAINT IF EXISTS [DF__Vehiculo__Creado__5AEE82B9]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Vehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[Vehiculo] DROP CONSTRAINT IF EXISTS [DF__Vehiculo__Activo__59FA5E80]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoVehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[TipoVehiculo] DROP CONSTRAINT IF EXISTS [DF__TipoVehic__Cread__5165187F]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TipoVehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[TipoVehiculo] DROP CONSTRAINT IF EXISTS [DF__TipoVehic__Activ__5070F446]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarifa]') AND type in (N'U'))
ALTER TABLE [dbo].[Tarifa] DROP CONSTRAINT IF EXISTS [DF__Tarifa__CreadoEn__6754599E]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tarifa]') AND type in (N'U'))
ALTER TABLE [dbo].[Tarifa] DROP CONSTRAINT IF EXISTS [DF__Tarifa__Activo__66603565]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Parqueo]') AND type in (N'U'))
ALTER TABLE [dbo].[Parqueo] DROP CONSTRAINT IF EXISTS [DF__Parqueo__CreadoE__4BAC3F29]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Parqueo]') AND type in (N'U'))
ALTER TABLE [dbo].[Parqueo] DROP CONSTRAINT IF EXISTS [DF__Parqueo__Activo__4AB81AF0]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[IngresoVehiculo]') AND type in (N'U'))
ALTER TABLE [dbo].[IngresoVehiculo] DROP CONSTRAINT IF EXISTS [DF__IngresoVe__Cread__6C190EBB]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
ALTER TABLE [dbo].[Factura] DROP CONSTRAINT IF EXISTS [DF__Factura__CreadoE__70DDC3D8]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EspacioParqueo]') AND type in (N'U'))
ALTER TABLE [dbo].[EspacioParqueo] DROP CONSTRAINT IF EXISTS [DF__EspacioPa__Cread__628FA481]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EspacioParqueo]') AND type in (N'U'))
ALTER TABLE [dbo].[EspacioParqueo] DROP CONSTRAINT IF EXISTS [DF__EspacioPa__Activ__619B8048]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EspacioParqueo]') AND type in (N'U'))
ALTER TABLE [dbo].[EspacioParqueo] DROP CONSTRAINT IF EXISTS [DF__EspacioPa__Dispo__60A75C0F]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
ALTER TABLE [dbo].[Cliente] DROP CONSTRAINT IF EXISTS [DF__Cliente__CreadoE__5629CD9C]
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
ALTER TABLE [dbo].[Cliente] DROP CONSTRAINT IF EXISTS [DF__Cliente__Activo__5535A963]
GO
/****** Objeto: Table [dbo].[Vehiculo] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[Vehiculo]
GO
/****** Objeto: Table [dbo].[TipoVehiculo] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[TipoVehiculo]
GO
/****** Objeto: Table [dbo].[Tarifa] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[Tarifa]
GO
/****** Objeto: Table [dbo].[Parqueo] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[Parqueo]
GO
/****** Objeto: Table [dbo].[IngresoVehiculo] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[IngresoVehiculo]
GO
/****** Objeto: Table [dbo].[Factura] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[Factura]
GO
/****** Objeto: Table [dbo].[EspacioParqueo] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[EspacioParqueo]
GO
/****** Objeto: Table [dbo].[Cliente] Fecha de script: 29/7/2026 11:32:54 ******/
DROP TABLE IF EXISTS [dbo].[Cliente]
GO
/****** Objeto: Table [dbo].[Cliente] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
[ClienteId] [int] IDENTITY(1,1) NOT NULL,
[Nombre] [nvarchar](100) NOT NULL,
[Apellidos] [nvarchar](150) NOT NULL,
[Cedula] [nvarchar](25) NOT NULL,
[Telefono] [nvarchar](25) NULL,
[Correo] [nvarchar](254) NULL,
[Activo] [bit] NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED
(
[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Cliente_Cedula] UNIQUE NONCLUSTERED
(
[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[EspacioParqueo] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EspacioParqueo](
[EspacioId] [int] IDENTITY(1,1) NOT NULL,
[ParqueoId] [int] NOT NULL,
[NumeroEspacio] [nvarchar](20) NOT NULL,
[Disponible] [bit] NOT NULL,
[Activo] [bit] NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_EspacioParqueo] PRIMARY KEY CLUSTERED
(
[EspacioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Espacio] UNIQUE NONCLUSTERED
(
[ParqueoId] ASC,
[NumeroEspacio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Factura] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
[FacturaId] [int] IDENTITY(1,1) NOT NULL,
[IngresoId] [int] NOT NULL,
[FechaFactura] [datetime2](3) NOT NULL,
[HorasCobradas] [decimal](10, 2) NOT NULL,
[MontoTotal] [decimal](18, 2) NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED
(
[FacturaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[IngresoVehiculo] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IngresoVehiculo](
[IngresoId] [int] IDENTITY(1,1) NOT NULL,
[VehiculoId] [int] NOT NULL,
[EspacioId] [int] NOT NULL,
[FechaIngreso] [datetime2](3) NOT NULL,
[FechaSalida] [datetime2](3) NULL,
[Estado] [nvarchar](20) NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_IngresoVehiculo] PRIMARY KEY CLUSTERED
(
[IngresoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Parqueo] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parqueo](
[ParqueoId] [int] IDENTITY(1,1) NOT NULL,
[NombreParqueo] [nvarchar](150) NOT NULL,
[Direccion] [nvarchar](250) NOT NULL,
[Telefono] [nvarchar](25) NULL,
[CapacidadTotal] [int] NOT NULL,
[Activo] [bit] NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_Parqueo] PRIMARY KEY CLUSTERED
(
[ParqueoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Tarifa] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarifa](
[TarifaId] [int] IDENTITY(1,1) NOT NULL,
[TipoVehiculoId] [int] NOT NULL,
[Descripcion] [nvarchar](100) NOT NULL,
[MontoHora] [decimal](18, 2) NOT NULL,
[Activo] [bit] NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_Tarifa] PRIMARY KEY CLUSTERED
(
[TarifaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[TipoVehiculo] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoVehiculo](
[TipoVehiculoId] [int] IDENTITY(1,1) NOT NULL,
[Descripcion] [nvarchar](100) NOT NULL,
[Activo] [bit] NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_TipoVehiculo] PRIMARY KEY CLUSTERED
([TipoVehiculoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_TipoVehiculo] UNIQUE NONCLUSTERED
(
[Descripcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Objeto: Table [dbo].[Vehiculo] Fecha de script: 29/7/2026 11:32:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculo](
[VehiculoId] [int] IDENTITY(1,1) NOT NULL,
[ClienteId] [int] NOT NULL,
[TipoVehiculoId] [int] NOT NULL,
[Placa] [nvarchar](20) NOT NULL,
[Marca] [nvarchar](100) NOT NULL,
[Modelo] [nvarchar](100) NULL,
[Color] [nvarchar](50) NULL,
[Activo] [bit] NOT NULL,
[CreadoEn] [datetime2](3) NOT NULL,
[CreadoPor] [nvarchar](50) NULL,
[ActualizadoEn] [datetime2](3) NULL,
[ActualizadoPor] [nvarchar](50) NULL,
[RowVer] [timestamp] NOT NULL,
CONSTRAINT [PK_Vehiculo] PRIMARY KEY CLUSTERED
([VehiculoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Vehiculo_Placa] UNIQUE NONCLUSTERED
(
[Placa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente] ADD DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Cliente] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[EspacioParqueo] ADD DEFAULT ((1)) FOR [Disponible]
GO
ALTER TABLE [dbo].[EspacioParqueo] ADD DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[EspacioParqueo] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[Factura] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[IngresoVehiculo] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[Parqueo] ADD DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Parqueo] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[Tarifa] ADD DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Tarifa] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[TipoVehiculo] ADD DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[TipoVehiculo] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[Vehiculo] ADD DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Vehiculo] ADD DEFAULT (sysutcdatetime()) FOR [CreadoEn]
GO
ALTER TABLE [dbo].[EspacioParqueo] WITH CHECK ADD CONSTRAINT [FK_Espacio_Parqueo] FOREIGN KEY([ParqueoId])
REFERENCES [dbo].[Parqueo] ([ParqueoId])
GO
ALTER TABLE [dbo].[EspacioParqueo] CHECK CONSTRAINT [FK_Espacio_Parqueo]
GO
ALTER TABLE [dbo].[Factura] WITH CHECK ADD CONSTRAINT [FK_Factura_Ingreso] FOREIGN KEY([IngresoId])
REFERENCES [dbo].[IngresoVehiculo] ([IngresoId])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Ingreso]
GO
ALTER TABLE [dbo].[IngresoVehiculo] WITH CHECK ADD CONSTRAINT [FK_Ingreso_Espacio] FOREIGN KEY([EspacioId])
REFERENCES [dbo].[EspacioParqueo] ([EspacioId])
GO
ALTER TABLE [dbo].[IngresoVehiculo] CHECK CONSTRAINT [FK_Ingreso_Espacio]
GO
ALTER TABLE [dbo].[IngresoVehiculo] WITH CHECK ADD CONSTRAINT [FK_Ingreso_Vehiculo] FOREIGN KEY([VehiculoId])
REFERENCES [dbo].[Vehiculo] ([VehiculoId])
GO
ALTER TABLE [dbo].[IngresoVehiculo] CHECK CONSTRAINT [FK_Ingreso_Vehiculo]
GO
ALTER TABLE [dbo].[Tarifa] WITH CHECK ADD CONSTRAINT [FK_Tarifa_TipoVehiculo] FOREIGN KEY([TipoVehiculoId])
REFERENCES [dbo].[TipoVehiculo] ([TipoVehiculoId])
GO
ALTER TABLE [dbo].[Tarifa] CHECK CONSTRAINT [FK_Tarifa_TipoVehiculo]
GO
ALTER TABLE [dbo].[Vehiculo] WITH CHECK ADD CONSTRAINT [FK_Vehiculo_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [FK_Vehiculo_Cliente]
GO
ALTER TABLE [dbo].[Vehiculo] WITH CHECK ADD CONSTRAINT [FK_Vehiculo_TipoVehiculo] FOREIGN KEY([TipoVehiculoId])
REFERENCES [dbo].[TipoVehiculo] ([TipoVehiculoId])
GO
ALTER TABLE [dbo].[Vehiculo] CHECK CONSTRAINT [FK_Vehiculo_TipoVehiculo]
GO
ALTER TABLE [dbo].[Factura] WITH CHECK ADD CONSTRAINT [CK_Factura_Monto] CHECK (([MontoTotal]>=(0)))
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [CK_Factura_Monto]
GO
ALTER TABLE [dbo].[Parqueo] WITH CHECK ADD CONSTRAINT [CK_Parqueo_Capacidad] CHECK (([CapacidadTotal]>(0)))
GO
ALTER TABLE [dbo].[Parqueo] CHECK CONSTRAINT [CK_Parqueo_Capacidad]
GO
ALTER TABLE [dbo].[Tarifa] WITH CHECK ADD CONSTRAINT [CK_Tarifa_Monto] CHECK (([MontoHora]>(0)))
GO
ALTER TABLE [dbo].[Tarifa] CHECK CONSTRAINT [CK_Tarifa_Monto]
GO

USE ParqueosDB;
GO

SELECT 
    TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;

SELECT * FROM Cliente;
SELECT * FROM EspacioParqueo;
SELECT * FROM IngresoVehiculo;
select * from Parqueo;
select * from Tarifa;
select * from TipoVehiculo;
select * from Vehiculo;

USE ParqueosDB;
GO

SELECT DB_NAME() AS BaseDatosActual;

