USE NeptunoDB;
GO

/* =========================================================
   PRODUCTOS
   ========================================================= */

-- 1. LISTAR PRODUCTOS
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
        p.Descontinuado
    FROM Productos p
    INNER JOIN Proveedores pr
        ON p.ProveedorID = pr.ProveedorID
    INNER JOIN Categorias c
        ON p.CategoriaID = c.CategoriaID
    ORDER BY p.ProductoID;
END;
GO


-- 2. OBTENER PRODUCTO POR ID
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Obtener
    @ProductoID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        ProductoID,
        NombreProducto,
        ProveedorID,
        CategoriaID,
        CantidadPorUnidad,
        PrecioUnidad,
        UnidadesEnExistencia,
        UnidadesEnPedido,
        NivelDeReorden,
        Descontinuado
    FROM Productos
    WHERE ProductoID = @ProductoID;
END;
GO


-- 3. INSERTAR PRODUCTO
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Insertar
    @NombreProducto NVARCHAR(40),
    @ProveedorID INT,
    @CategoriaID INT,
    @CantidadPorUnidad NVARCHAR(20),
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
END;
GO


-- 4. ACTUALIZAR PRODUCTO
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Actualizar
    @ProductoID INT,
    @NombreProducto NVARCHAR(40),
    @ProveedorID INT,
    @CategoriaID INT,
    @CantidadPorUnidad NVARCHAR(20),
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


-- 5. ELIMINAR PRODUCTO
CREATE OR ALTER PROCEDURE dbo.sp_Productos_Eliminar
    @ProductoID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Productos
    WHERE ProductoID = @ProductoID;
END;
GO


/* =========================================================
   CATEGORÍAS
   ========================================================= */

-- 6. LISTAR CATEGORÍAS
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Listar
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


-- 7. OBTENER CATEGORÍA
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Obtener
    @CategoriaID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        CategoriaID,
        NombreCategoria,
        Descripcion
    FROM Categorias
    WHERE CategoriaID = @CategoriaID;
END;
GO


-- 8. INSERTAR CATEGORÍA
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Insertar
    @NombreCategoria NVARCHAR(15),
    @Descripcion NVARCHAR(MAX)
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
END;
GO


-- 9. ACTUALIZAR CATEGORÍA
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Actualizar
    @CategoriaID INT,
    @NombreCategoria NVARCHAR(15),
    @Descripcion NVARCHAR(MAX)
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


-- 10. ELIMINAR CATEGORÍA
CREATE OR ALTER PROCEDURE dbo.sp_Categorias_Eliminar
    @CategoriaID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Categorias
    WHERE CategoriaID = @CategoriaID;
END;
GO


/* =========================================================
   PROVEEDORES
   ========================================================= */

-- 11. LISTAR PROVEEDORES
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
        Fax
    FROM Proveedores
    ORDER BY ProveedorID;
END;
GO


-- 12. OBTENER PROVEEDOR
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Obtener
    @ProveedorID INT
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
    WHERE ProveedorID = @ProveedorID;
END;
GO


-- 13. INSERTAR PROVEEDOR
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Insertar
    @CompaniaNombre NVARCHAR(40),
    @NombreContacto NVARCHAR(30),
    @CargoContacto NVARCHAR(30),
    @Direccion NVARCHAR(60),
    @Ciudad NVARCHAR(15),
    @CodigoPostal NVARCHAR(10),
    @Pais NVARCHAR(15),
    @Telefono NVARCHAR(24),
    @Fax NVARCHAR(24)
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
END;
GO


-- 14. ACTUALIZAR PROVEEDOR
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Actualizar
    @ProveedorID INT,
    @CompaniaNombre NVARCHAR(40),
    @NombreContacto NVARCHAR(30),
    @CargoContacto NVARCHAR(30),
    @Direccion NVARCHAR(60),
    @Ciudad NVARCHAR(15),
    @CodigoPostal NVARCHAR(10),
    @Pais NVARCHAR(15),
    @Telefono NVARCHAR(24),
    @Fax NVARCHAR(24)
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


-- 15. ELIMINAR PROVEEDOR
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Eliminar
    @ProveedorID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Proveedores
    WHERE ProveedorID = @ProveedorID;
END;
GO


-- 16. BUSCAR PROVEEDORES
CREATE OR ALTER PROCEDURE dbo.sp_Proveedores_Buscar
    @NombreContacto NVARCHAR(30) = NULL,
    @Ciudad NVARCHAR(15) = NULL
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
        (
            @NombreContacto IS NULL
            OR @NombreContacto = ''
            OR NombreContacto LIKE '%' + @NombreContacto + '%'
        )
        AND
        (
            @Ciudad IS NULL
            OR @Ciudad = ''
            OR Ciudad LIKE '%' + @Ciudad + '%'
        )
    ORDER BY ProveedorID;
END;
GO


/* =========================================================
   PEDIDOS
   ========================================================= */

-- 17. LISTAR PEDIDOS
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        p.PedidoID,
        p.ClienteID,
        c.Empresa AS Cliente,
        p.EmpleadoID,
        e.Nombre + ' ' + e.Apellidos AS Empleado,
        p.FechaPedido,
        p.FechaRequerida,
        p.FechaEnvio,
        p.TransportistaID,
        t.CompaniaNombre AS Transportista,
        p.Destinatario,
        p.CiudadDestino,
        p.PaisDestino
    FROM Pedidos p
    INNER JOIN Clientes c
        ON p.ClienteID = c.ClienteID
    INNER JOIN Empleados e
        ON p.EmpleadoID = e.EmpleadoID
    INNER JOIN Transportistas t
        ON p.TransportistaID = t.TransportistaID
    ORDER BY p.PedidoID;
END;
GO


-- 18. OBTENER PEDIDO
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Obtener
    @PedidoID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        PedidoID,
        ClienteID,
        EmpleadoID,
        FechaPedido,
        FechaRequerida,
        FechaEnvio,
        TransportistaID,
        Destinatario,
        CiudadDestino,
        PaisDestino
    FROM Pedidos
    WHERE PedidoID = @PedidoID;
END;
GO


-- 19. INSERTAR PEDIDO
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Insertar
    @ClienteID NVARCHAR(5),
    @EmpleadoID INT,
    @FechaPedido DATETIME,
    @FechaRequerida DATETIME,
    @FechaEnvio DATETIME = NULL,
    @TransportistaID INT,
    @Destinatario NVARCHAR(40),
    @CiudadDestino NVARCHAR(15),
    @PaisDestino NVARCHAR(15)
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
END;
GO


-- 20. ACTUALIZAR PEDIDO
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Actualizar
    @PedidoID INT,
    @ClienteID NVARCHAR(5),
    @EmpleadoID INT,
    @FechaPedido DATETIME,
    @FechaRequerida DATETIME,
    @FechaEnvio DATETIME = NULL,
    @TransportistaID INT,
    @Destinatario NVARCHAR(40),
    @CiudadDestino NVARCHAR(15),
    @PaisDestino NVARCHAR(15)
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


-- 21. ELIMINAR PEDIDO
CREATE OR ALTER PROCEDURE dbo.sp_Pedidos_Eliminar
    @PedidoID INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY

        BEGIN TRANSACTION;

        -- Primero eliminamos los detalles relacionados
        DELETE FROM DetallePedidos
        WHERE PedidoID = @PedidoID;

        -- Luego eliminamos el pedido
        DELETE FROM Pedidos
        WHERE PedidoID = @PedidoID;

        COMMIT TRANSACTION;

    END TRY
    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;

    END CATCH
END;
GO


/* =========================================================
   REPORTE DE DETALLES DE PEDIDOS POR FECHA
   ========================================================= */

-- 22. DETALLES DE PEDIDOS POR INTERVALO DE FECHAS
CREATE OR ALTER PROCEDURE dbo.sp_DetallePedidos_FiltrarPorFecha
    @FechaInicio DATETIME,
    @FechaFin DATETIME
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
        (
            dp.PrecioUnidad * dp.Cantidad
        ) * (1 - dp.Descuento) AS Subtotal
    FROM DetallePedidos dp
    INNER JOIN Pedidos p
        ON dp.PedidoID = p.PedidoID
    INNER JOIN Productos pr
        ON dp.ProductoID = pr.ProductoID
    WHERE p.FechaPedido BETWEEN @FechaInicio AND @FechaFin
    ORDER BY
        p.FechaPedido,
        dp.PedidoID;
END;
GO