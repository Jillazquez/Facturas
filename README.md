# Generador de Facturas en PDF

Este proyecto es una aplicación de Windows Forms en C# que permite generar facturas en formato PDF con un logo de la empresa y una tabla de detalles de productos utilizando iTextSharp.

## Requisitos

- Visual Studio 2019 o superior
- .NET Framework 4.7.2 o superior
- Paquete NuGet iTextSharp

## Instalación

1. Clona el repositorio en tu máquina local.
2. Abre el proyecto en Visual Studio.
3. Instala el paquete iTextSharp desde NuGet ejecutando el siguiente comando en la Consola del Administrador de Paquetes:

## Funcionalidades

### Agregar Productos

La aplicación permite agregar productos a una lista, ingresando el nombre del producto, el precio unitario y la cantidad. Al agregar un producto, se calcula automáticamente el total (precio unitario * cantidad) y se muestra en una tabla.

### Guardar en CSV

Puedes guardar la lista de productos en un archivo CSV. El archivo CSV se guarda en la carpeta `Documentos` de tu usuario y contiene las columnas: Nombre, Precio Unitario, Cantidad, y Total.

### Generar PDF

La aplicación genera un documento PDF que incluye:

- Un logo de la empresa en la parte superior.
- Un título que indica que es una factura.
- Una tabla que contiene los productos seleccionados de la lista, con las columnas: Nombre, Precio Unitario, Cantidad, y Total.

## Uso

### 1. Agregar Productos

1. Ingresa el nombre del producto en el campo "Nombre".
2. Ingresa el precio unitario en el campo "Precio Unitario".
3. Ingresa la cantidad en el campo "Cantidad".
4. Haz clic en el botón "Agregar Producto" para añadir el producto a la lista.
5. El producto se mostrará en la tabla con el cálculo del total.

![Agregar Producto](/images/app.PNG)

### 2. Guardar Productos en CSV

1. Una vez que hayas agregado los productos, haz clic en el botón "Guardar CSV".
2. La aplicación guardará la lista de productos en un archivo CSV ubicado en la carpeta `Documentos` de tu usuario.
3. Verás un mensaje de confirmación indicando que el archivo CSV se ha guardado correctamente.


### 3. Generar PDF

1. Selecciona los productos que deseas incluir en la factura desde la lista.
2. Haz clic en el botón "Generar PDF".
3. La aplicación generará un documento PDF que incluye:
- El logo de la empresa en la parte superior.
- Un título centrado que dice "Factura".
- Una tabla con los productos seleccionados, mostrando las columnas de Nombre, Precio Unitario, Cantidad y Total.
4. El PDF se guardará en la carpeta `Documentos` de tu usuario y verás un mensaje de confirmación indicando que el PDF se ha generado correctamente.

![Generar PDF](/images/factura.PNG)

## Notas

- **Ruta del Logo**: Asegúrate de que la ruta del logo en el código sea correcta. Si la imagen del logo no se encuentra en la ruta especificada, no se agregará al PDF. La imagen se tiene que llamar icono.png
- **Formato del PDF**: Puedes personalizar el formato del PDF (tamaños de fuente, estilos de tabla, etc.) según tus necesidades.
- **Selección de Productos**: Solo los productos seleccionados en la lista serán incluidos en el PDF generado.

## Contribuciones

Las contribuciones son bienvenidas. Si tienes sugerencias, mejoras o encuentras algún error, no dudes en hacer un fork del proyecto y enviar un pull request.

## Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.
