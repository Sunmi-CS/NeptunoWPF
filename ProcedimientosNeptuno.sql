USE NeptunoDB;
GO

/* ============================================================
   PRODUCTOS
   ============================================================ */

-- LISTAR PRODUCTOS ACTIVOS
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.ProductoID,
        p.NombreProducto,
        p.ProveedorID,
        pr.CompaniaNombre AS Proveedor,
        p.CategoriaID,
        c.NombreCategoria AS Categoria,
        p.CantidadPorUnidad,
        p.PrecioUnidad,
        p.UnidadesEnExistencia,
        p.UnidadesEnPedido,
        p.NivelDeReorden,
        p.Descontinuado,
        p.Activo
    FROM dbo.Productos p
    LEFT JOIN dbo.Proveedores pr
        ON p.ProveedorID = pr.ProveedorID
    LEFT JOIN dbo.Categorias c
        ON p.CategoriaID = c.CategoriaID
    WHERE p.Activo = 1
    ORDER BY p.ProductoID;
END;
GO


-- INSERTAR PRODUCTO
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Insertar
    @NombreProducto       NVARCHAR(60),
    @ProveedorID          INT = NULL,
    @CategoriaID          INT = NULL,
    @CantidadPorUnidad    NVARCHAR(30) = NULL,
    @PrecioUnidad         DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido     SMALLINT,
    @NivelDeReorden       SMALLINT,
    @Descontinuado        BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Productos
    (
        NombreProducto,
        ProveedorID,
        CategoriaID,
        CantidadPorUnidad,
        PrecioUnidad,
        UnidadesEnExistencia,
        UnidadesEnPedido,
        NivelDeReorden,
        Descontinuado,
        Activo
    )
    VALUES
    (
        @NombreProducto,
        @ProveedorID,
        @CategoriaID,
        @CantidadPorUnidad,
        @PrecioUnidad,
        @UnidadesEnExistencia,
        @UnidadesEnPedido,
        @NivelDeReorden,
        @Descontinuado,
        1
    );
END;
GO


-- ACTUALIZAR PRODUCTO
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Actualizar
    @ProductoID           INT,
    @NombreProducto       NVARCHAR(60),
    @ProveedorID          INT = NULL,
    @CategoriaID          INT = NULL,
    @CantidadPorUnidad    NVARCHAR(30) = NULL,
    @PrecioUnidad         DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido     SMALLINT,
    @NivelDeReorden       SMALLINT,
    @Descontinuado        BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Productos
    SET
        NombreProducto = @NombreProducto,
        ProveedorID = @ProveedorID,
        CategoriaID = @CategoriaID,
        CantidadPorUnidad = @CantidadPorUnidad,
        PrecioUnidad = @PrecioUnidad,
        UnidadesEnExistencia = @UnidadesEnExistencia,
        UnidadesEnPedido = @UnidadesEnPedido,
        NivelDeReorden = @NivelDeReorden,
        Descontinuado = @Descontinuado
    WHERE ProductoID = @ProductoID
      AND Activo = 1;
END;
GO


-- ELIMINACIÓN LÓGICA DE PRODUCTO
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Eliminar
    @ProductoID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Productos
    SET Activo = 0
    WHERE ProductoID = @ProductoID
      AND Activo = 1;
END;
GO


/* ============================================================
   CATEGORÍAS
   ============================================================ */

-- LISTAR CATEGORÍAS ACTIVAS
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CategoriaID,
        NombreCategoria,
        Descripcion,
        Activo
    FROM dbo.Categorias
    WHERE Activo = 1
    ORDER BY CategoriaID;
END;
GO


-- INSERTAR CATEGORÍA
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Insertar
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Categorias
    (
        NombreCategoria,
        Descripcion,
        Activo
    )
    VALUES
    (
        @NombreCategoria,
        @Descripcion,
        1
    );
END;
GO


-- ACTUALIZAR CATEGORÍA
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Actualizar
    @CategoriaID INT,
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Categorias
    SET
        NombreCategoria = @NombreCategoria,
        Descripcion = @Descripcion
    WHERE CategoriaID = @CategoriaID
      AND Activo = 1;
END;
GO


-- ELIMINACIÓN LÓGICA DE CATEGORÍA
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Eliminar
    @CategoriaID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Categorias
    SET Activo = 0
    WHERE CategoriaID = @CategoriaID
      AND Activo = 1;
END;
GO


/* ============================================================
   PROVEEDORES
   ============================================================ */

-- LISTAR PROVEEDORES ACTIVOS
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ProveedorID,
        CompaniaNombre,
        NombreContacto,
        CargoContacto,
        Direccion,
        Ciudad,
        CodigoPostal,
        Pais,
        Telefono,
        Fax,
        Activo
    FROM dbo.Proveedores
    WHERE Activo = 1
    ORDER BY ProveedorID;
END;
GO


-- BUSCAR PROVEEDORES POR CONTACTO Y/O CIUDAD
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Buscar
    @NombreContacto NVARCHAR(40) = NULL,
    @Ciudad NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ProveedorID,
        CompaniaNombre,
        NombreContacto,
        CargoContacto,
        Direccion,
        Ciudad,
        CodigoPostal,
        Pais,
        Telefono,
        Fax,
        Activo
    FROM dbo.Proveedores
    WHERE Activo = 1
      AND (
            @NombreContacto IS NULL
            OR @NombreContacto = ''
            OR NombreContacto LIKE '%' + @NombreContacto + '%'
          )
      AND (
            @Ciudad IS NULL
            OR @Ciudad = ''
            OR Ciudad LIKE '%' + @Ciudad + '%'
          )
    ORDER BY ProveedorID;
END;
GO


-- INSERTAR PROVEEDOR
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Insertar
    @CompaniaNombre NVARCHAR(60),
    @NombreContacto NVARCHAR(40) = NULL,
    @CargoContacto NVARCHAR(40) = NULL,
    @Direccion NVARCHAR(80) = NULL,
    @Ciudad NVARCHAR(30) = NULL,
    @CodigoPostal NVARCHAR(10) = NULL,
    @Pais NVARCHAR(30) = NULL,
    @Telefono NVARCHAR(24) = NULL,
    @Fax NVARCHAR(24) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Proveedores
    (
        CompaniaNombre,
        NombreContacto,
        CargoContacto,
        Direccion,
        Ciudad,
        CodigoPostal,
        Pais,
        Telefono,
        Fax,
        Activo
    )
    VALUES
    (
        @CompaniaNombre,
        @NombreContacto,
        @CargoContacto,
        @Direccion,
        @Ciudad,
        @CodigoPostal,
        @Pais,
        @Telefono,
        @Fax,
        1
    );
END;
GO


-- ACTUALIZAR PROVEEDOR
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Actualizar
    @ProveedorID INT,
    @CompaniaNombre NVARCHAR(60),
    @NombreContacto NVARCHAR(40) = NULL,
    @CargoContacto NVARCHAR(40) = NULL,
    @Direccion NVARCHAR(80) = NULL,
    @Ciudad NVARCHAR(30) = NULL,
    @CodigoPostal NVARCHAR(10) = NULL,
    @Pais NVARCHAR(30) = NULL,
    @Telefono NVARCHAR(24) = NULL,
    @Fax NVARCHAR(24) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Proveedores
    SET
        CompaniaNombre = @CompaniaNombre,
        NombreContacto = @NombreContacto,
        CargoContacto = @CargoContacto,
        Direccion = @Direccion,
        Ciudad = @Ciudad,
        CodigoPostal = @CodigoPostal,
        Pais = @Pais,
        Telefono = @Telefono,
        Fax = @Fax
    WHERE ProveedorID = @ProveedorID
      AND Activo = 1;
END;
GO


-- ELIMINACIÓN LÓGICA DE PROVEEDOR
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Eliminar
    @ProveedorID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Proveedores
    SET Activo = 0
    WHERE ProveedorID = @ProveedorID
      AND Activo = 1;
END;
GO


/* ============================================================
   PEDIDOS
   ============================================================ */

-- LISTAR PEDIDOS ACTIVOS
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.PedidoID,
        p.ClienteID,
        c.Empresa AS Cliente,
        p.EmpleadoID,
        CONCAT(e.Nombre, ' ', e.Apellidos) AS Empleado,
        p.FechaPedido,
        p.FechaRequerida,
        p.FechaEnvio,
        p.TransportistaID,
        t.CompaniaNombre AS Transportista,
        p.Destinatario,
        p.CiudadDestino,
        p.PaisDestino,
        p.Activo
    FROM dbo.Pedidos p
    LEFT JOIN dbo.Clientes c
        ON p.ClienteID = c.ClienteID
    LEFT JOIN dbo.Empleados e
        ON p.EmpleadoID = e.EmpleadoID
    LEFT JOIN dbo.Transportistas t
        ON p.TransportistaID = t.TransportistaID
    WHERE p.Activo = 1
    ORDER BY p.PedidoID;
END;
GO


-- INSERTAR PEDIDO
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Insertar
    @ClienteID INT = NULL,
    @EmpleadoID INT = NULL,
    @FechaPedido DATE,
    @FechaRequerida DATE = NULL,
    @FechaEnvio DATE = NULL,
    @TransportistaID INT = NULL,
    @Destinatario NVARCHAR(60) = NULL,
    @CiudadDestino NVARCHAR(30) = NULL,
    @PaisDestino NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Pedidos
    (
        ClienteID,
        EmpleadoID,
        FechaPedido,
        FechaRequerida,
        FechaEnvio,
        TransportistaID,
        Destinatario,
        CiudadDestino,
        PaisDestino,
        Activo
    )
    VALUES
    (
        @ClienteID,
        @EmpleadoID,
        @FechaPedido,
        @FechaRequerida,
        @FechaEnvio,
        @TransportistaID,
        @Destinatario,
        @CiudadDestino,
        @PaisDestino,
        1
    );
END;
GO


-- ACTUALIZAR PEDIDO
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Actualizar
    @PedidoID INT,
    @ClienteID INT = NULL,
    @EmpleadoID INT = NULL,
    @FechaPedido DATE,
    @FechaRequerida DATE = NULL,
    @FechaEnvio DATE = NULL,
    @TransportistaID INT = NULL,
    @Destinatario NVARCHAR(60) = NULL,
    @CiudadDestino NVARCHAR(30) = NULL,
    @PaisDestino NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Pedidos
    SET
        ClienteID = @ClienteID,
        EmpleadoID = @EmpleadoID,
        FechaPedido = @FechaPedido,
        FechaRequerida = @FechaRequerida,
        FechaEnvio = @FechaEnvio,
        TransportistaID = @TransportistaID,
        Destinatario = @Destinatario,
        CiudadDestino = @CiudadDestino,
        PaisDestino = @PaisDestino
    WHERE PedidoID = @PedidoID
      AND Activo = 1;
END;
GO


-- ELIMINACIÓN LÓGICA DE PEDIDO
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Eliminar
    @PedidoID INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Pedidos
    SET Activo = 0
    WHERE PedidoID = @PedidoID
      AND Activo = 1;
END;
GO


/* ============================================================
   REPORTE DE DETALLE DE PEDIDOS POR FECHAS
   ============================================================ */

CREATE OR ALTER PROCEDURE dbo.sp_DetallePedidos_PorFechas
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        dp.PedidoID,
        dp.ProductoID,
        p.FechaPedido,
        pr.NombreProducto,
        dp.PrecioUnidad,
        dp.Cantidad,
        dp.Descuento,
        (
            dp.PrecioUnidad
            * dp.Cantidad
            * (1 - dp.Descuento)
        ) AS Subtotal
    FROM dbo.DetallePedidos dp
    INNER JOIN dbo.Pedidos p
        ON dp.PedidoID = p.PedidoID
    INNER JOIN dbo.Productos pr
        ON dp.ProductoID = pr.ProductoID
    WHERE p.FechaPedido >= @FechaInicio
      AND p.FechaPedido <= @FechaFin
      AND p.Activo = 1
    ORDER BY p.FechaPedido, dp.PedidoID;
END;
GO

EXEC dbo.sp_Productos_Listar;

EXEC dbo.sp_Categorias_Listar;

EXEC dbo.sp_Proveedores_Listar;

EXEC dbo.sp_Proveedores_Buscar
    @NombreContacto = N'Ana',
    @Ciudad = N'Lima';
EXEC dbo.sp_Pedidos_Listar;

EXEC dbo.sp_DetallePedidos_PorFechas
    @FechaInicio = '2026-08-12',
    @FechaFin = '2026-08-20';


EXEC dbo.sp_Productos_Eliminar
    @ProductoID = 1;

SELECT *
FROM dbo.Productos
WHERE ProductoID = 1;

EXEC dbo.sp_Productos_Listar;