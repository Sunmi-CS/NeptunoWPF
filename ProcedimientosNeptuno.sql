/* ============================================================
   CRUD de Productos  
   ============================================================ */

-- Listar productos
USE NeptunoDB;
GO

CREATE OR ALTER PROCEDURE sp_Productos_Listar
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
        p.Descontinuado
    FROM Productos p
    LEFT JOIN Proveedores pr
        ON p.ProveedorID = pr.ProveedorID
    LEFT JOIN Categorias c
        ON p.CategoriaID = c.CategoriaID
    ORDER BY p.ProductoID;
END;
GO

-- Insertar producto
CREATE OR ALTER PROCEDURE sp_Productos_Insertar
    @NombreProducto NVARCHAR(60),
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL,
    @CantidadPorUnidad NVARCHAR(30) = NULL,
    @PrecioUnidad DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido SMALLINT,
    @NivelDeReorden SMALLINT,
    @Descontinuado BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Productos
    (
        NombreProducto,
        ProveedorID,
        CategoriaID,
        CantidadPorUnidad,
        PrecioUnidad,
        UnidadesEnExistencia,
        UnidadesEnPedido,
        NivelDeReorden,
        Descontinuado
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
        @Descontinuado
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ProductoID;
END;
GO

-- Actualizar producto
CREATE OR ALTER PROCEDURE sp_Productos_Actualizar
    @ProductoID INT,
    @NombreProducto NVARCHAR(60),
    @ProveedorID INT = NULL,
    @CategoriaID INT = NULL,
    @CantidadPorUnidad NVARCHAR(30) = NULL,
    @PrecioUnidad DECIMAL(10,2),
    @UnidadesEnExistencia SMALLINT,
    @UnidadesEnPedido SMALLINT,
    @NivelDeReorden SMALLINT,
    @Descontinuado BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Productos
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
    WHERE ProductoID = @ProductoID;
END;
GO

-- Eliminar producto
CREATE OR ALTER PROCEDURE sp_Productos_Eliminar
    @ProductoID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM DetallePedidos
        WHERE ProductoID = @ProductoID
    )
    BEGIN
        THROW 50001, 'No se puede eliminar el producto porque tiene pedidos asociados.', 1;
    END;

    DELETE FROM Productos
    WHERE ProductoID = @ProductoID;
END;
GO

/* ============================================================
   CRUD de Categorías  
   ============================================================ */

   -- Listar categorías
CREATE OR ALTER PROCEDURE sp_Categorias_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CategoriaID,
        NombreCategoria,
        Descripcion
    FROM Categorias
    ORDER BY CategoriaID;
END;
GO

-- Insertar categoría
CREATE OR ALTER PROCEDURE sp_Categorias_Insertar
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Categorias
    (
        NombreCategoria,
        Descripcion
    )
    VALUES
    (
        @NombreCategoria,
        @Descripcion
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS CategoriaID;
END;
GO

-- Actualizar categoría
CREATE OR ALTER PROCEDURE sp_Categorias_Actualizar
    @CategoriaID INT,
    @NombreCategoria NVARCHAR(30),
    @Descripcion NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Categorias
    SET
        NombreCategoria = @NombreCategoria,
        Descripcion = @Descripcion
    WHERE CategoriaID = @CategoriaID;
END;
GO

-- Eliminar categoría
CREATE OR ALTER PROCEDURE sp_Categorias_Eliminar
    @CategoriaID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Productos
        WHERE CategoriaID = @CategoriaID
    )
    BEGIN
        THROW 50002, 'No se puede eliminar la categoría porque tiene productos asociados.', 1;
    END;

    DELETE FROM Categorias
    WHERE CategoriaID = @CategoriaID;
END;
GO

/* ============================================================
   CRUD de Proveedores  
   ============================================================ */
-- Listar proveedores
CREATE OR ALTER PROCEDURE sp_Proveedores_Listar
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
        Fax
    FROM Proveedores
    ORDER BY ProveedorID;
END;
GO

-- Insertar proveedor
CREATE OR ALTER PROCEDURE sp_Proveedores_Insertar
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

    INSERT INTO Proveedores
    (
        CompaniaNombre,
        NombreContacto,
        CargoContacto,
        Direccion,
        Ciudad,
        CodigoPostal,
        Pais,
        Telefono,
        Fax
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
        @Fax
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS ProveedorID;
END;
GO

-- Actualizar proveedor
CREATE OR ALTER PROCEDURE sp_Proveedores_Actualizar
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

    UPDATE Proveedores
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
    WHERE ProveedorID = @ProveedorID;
END;
GO

-- Eliminar proveedor
CREATE OR ALTER PROCEDURE sp_Proveedores_Eliminar
    @ProveedorID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM Productos
        WHERE ProveedorID = @ProveedorID
    )
    BEGIN
        THROW 50003, 'No se puede eliminar el proveedor porque tiene productos asociados.', 1;
    END;

    DELETE FROM Proveedores
    WHERE ProveedorID = @ProveedorID;
END;
GO


-- Listado de proveedores por nombreContacto y ciudad

CREATE OR ALTER PROCEDURE sp_Proveedores_Buscar
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
        Fax
    FROM Proveedores
    WHERE
        (@NombreContacto IS NULL
         OR NombreContacto LIKE '%' + @NombreContacto + '%')
    AND
        (@Ciudad IS NULL
         OR Ciudad LIKE '%' + @Ciudad + '%')
    ORDER BY CompaniaNombre;
END;
GO


/* ============================================================
   CRUD de Pedidos  
   ============================================================ */
-- Listar pedidos
CREATE OR ALTER PROCEDURE sp_Pedidos_Listar
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
        p.PaisDestino
    FROM Pedidos p
    LEFT JOIN Clientes c
        ON p.ClienteID = c.ClienteID
    LEFT JOIN Empleados e
        ON p.EmpleadoID = e.EmpleadoID
    LEFT JOIN Transportistas t
        ON p.TransportistaID = t.TransportistaID
    ORDER BY p.PedidoID;
END;
GO

-- Insertar pedido
CREATE OR ALTER PROCEDURE sp_Pedidos_Insertar
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

    INSERT INTO Pedidos
    (
        ClienteID,
        EmpleadoID,
        FechaPedido,
        FechaRequerida,
        FechaEnvio,
        TransportistaID,
        Destinatario,
        CiudadDestino,
        PaisDestino
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
        @PaisDestino
    );

    SELECT CAST(SCOPE_IDENTITY() AS INT) AS PedidoID;
END;
GO

-- Actualizar pedido
CREATE OR ALTER PROCEDURE sp_Pedidos_Actualizar
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

    UPDATE Pedidos
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
    WHERE PedidoID = @PedidoID;
END;
GO

-- Eliminar pedido
CREATE OR ALTER PROCEDURE sp_Pedidos_Eliminar
    @PedidoID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM DetallePedidos
        WHERE PedidoID = @PedidoID
    )
    BEGIN
        THROW 50004, 'No se puede eliminar el pedido porque tiene detalles asociados.', 1;
    END;

    DELETE FROM Pedidos
    WHERE PedidoID = @PedidoID;
END;
GO

-- Reporte de detalles de pedidos por fechas
CREATE OR ALTER PROCEDURE sp_DetallePedidos_PorFechas
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        dp.PedidoID,
        p.FechaPedido,
        dp.ProductoID,
        pr.NombreProducto,
        dp.PrecioUnidad,
        dp.Cantidad,
        dp.Descuento,
        (dp.PrecioUnidad * dp.Cantidad * (1 - dp.Descuento)) AS Subtotal
    FROM DetallePedidos dp
    INNER JOIN Pedidos p
        ON dp.PedidoID = p.PedidoID
    INNER JOIN Productos pr
        ON dp.ProductoID = pr.ProductoID
    WHERE p.FechaPedido BETWEEN @FechaInicio AND @FechaFin
    ORDER BY p.FechaPedido, dp.PedidoID;
END;
GO