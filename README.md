# MoneyXChange by Eduardo Rodríguez Patiño
Mi proyecto realizado para la postulación a Belatrix.

## Explicación
La solución esta compuesta basicamente por 2 proyectos:

1. Client: el cliente web que se comunica con nuestra API y permite gestionar el tipo de cambio.
2. API: nuestra capa API que contiene toda la lógica backend para satisfacer a los clientes.

## Tareas realizadas
1. Creación de componente Vue para gestionar el tipo de cambio.
2. Sistema de cache a través de localStorage para persistir la información cada 10 minutos antes de volver a consultar a nuestro endpoint.
3. Autenticación de usuario a través de ASP.NET Identity Core y Json Web Token para acceder a nuestra API.
3. Creación de pruebas unitarias en la parte back-end.

## Tecnologías
1. VueJs
2. ASP.NET Core 3.1
3. ASP.NET Identity Core
4. Entity Framework Core

## ¿Cómo levantar el proyecto?

### 1. Cambiar el puerto
Al proyecto Belatrix.Api para que apunte al puerto 50000. Ej: http://localhost:50000

### 2. Actualizar la cadena de conexión
Por la de su servidor para poder ejecutar las migraciones. Esta se encuentra en Belatrix.Api/appsettings.json

### 3. Ejecutar las migraciones pendientes
1. Seleccionar el proyecto Belatrix.Api como StartUp Project.
2. Desd ela consola del NuGet seleccionar el proyecto Belatrix.Persistence como principal
3. Ejecutar el comando update-database.

### 4. Insertar el usuario por defecto
Ejecutar el siguiente script en su base de datos de SQL Server

```INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Name], [LastName], [RegisteredAt]) VALUES (N'6309c8dd-d889-4b9b-8b13-d4565986e650', N'demo@belatrix.com', N'DEMO@BELATRIX.COM', N'demo@belatrix.com', N'DEMO@BELATRIX.COM', 0, N'AQAAAAEAACcQAAAAEHvxp9SKR4jvsFv7s7qP7J3r9kHc+9qWKob7qCz6FhOQNBVFqq/rKO22UzKlCMx9JA==', N'3JBTBLEFHTLRGEQ6I55X5YXDIHKYSIPH', N'cb0f2f0a-2d90-4072-9b34-2e1368a8bddc', NULL, 0, 0, NULL, 1, 0, N'Eduardo', N'Rodríguez Patiño', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))```

El usuario es:
* Usuer: demo@belatrix.com
* Password: 123456

### 5. Múltiples StartUp Projects
Configurar al solución para que levante Belatrix.Api y Belatrix.Client como proyectos principales y probar.

## Capturas de pantalla
![](https://kodoti.blob.core.windows.net/belatrix/belatrix-moneyxchange-a.jpg)

![](https://kodoti.blob.core.windows.net/belatrix/belatrix-moneyxchange-b.png)

![](https://kodoti.blob.core.windows.net/belatrix/belatrix-moneyxchange-c.png)

![](https://kodoti.blob.core.windows.net/belatrix/belatrix-moneyxchange-d.png)