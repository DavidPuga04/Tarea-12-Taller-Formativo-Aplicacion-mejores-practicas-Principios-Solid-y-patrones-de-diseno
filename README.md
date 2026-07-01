# Design Patterns – Taller Formativo: Mejores Prácticas, Principios SOLID y Patrones de Diseño

Aplicación web de gestión de vehículos desarrollada como taller formativo para practicar la aplicación de **principios SOLID** y **patrones de diseño** sobre una base de código heredada (legacy) que presentaba fallos y malas prácticas. El objetivo del ejercicio es refactorizar y extender la funcionalidad sin romper el diseño existente, aplicando el patrón correcto a cada problema.

El repositorio contiene dos implementaciones del mismo dominio (gestión de vehículos):

1. **`DesignPatterns/`** – Aplicación **ASP.NET Core MVC (.NET 8, C#)**, el proyecto principal del taller.
2. **`headapps/app/`** – Migración parcial del "core" a una aplicación web moderna con **Next.js 16 + React 19 + TypeScript**, simulando la migración de un equipo full-stack a un stack más actual.

---

## 📋 ¿Qué hace la aplicación?

La aplicación permite:

- Visualizar el listado de vehículos registrados (marca, modelo, color, año, etc.).
- Agregar vehículos predefinidos mediante botones de acción rápida:
  - **Add Mustang** (Ford Mustang)
  - **Add Explorer** (Ford Explorer)
  - **Add Escape** (Ford Escape)
- Encender el motor (**Start Engine**), apagarlo (**Stop Engine**) y cargar combustible (**Add Gas**) de un vehículo, validando reglas de negocio (por ejemplo, no se puede encender un motor sin combustible ni encenderlo dos veces).
- Persistir los vehículos en memoria (sin necesidad de una base de datos real), ya que el esquema de base de datos del equipo de datos aún no estaba disponible al momento del desarrollo.

---

## 🧩 Principios SOLID aplicados

| Principio | Dónde se aplica | Cómo se aplica |
|---|---|---|
| **S – Single Responsibility** | `MyVehiclesRepository`, `CarBuilder`, `MemoryCollection`, cada `*Factory` | Cada clase tiene una única responsabilidad: el repositorio solo persiste/consulta vehículos, el builder solo construye instancias de `Car`, cada factory solo sabe crear un modelo específico de vehículo. |
| **O – Open/Closed** | `CarFactory` (clase abstracta) y sus implementaciones (`MustangFactory`, `ExplorerFactory`, `FordEscapeFactory`) | Para soportar un nuevo modelo de vehículo (ej. un nuevo Ford), se **agrega** una nueva clase Factory sin modificar el código existente de las fábricas actuales ni del controlador. |
| **L – Liskov Substitution** | `Vehicle` (clase base abstracta) → `Car`, `Motocycle` | Cualquier subclase de `Vehicle` puede usarse donde se espera un `Vehicle`/`IVehicle` sin alterar el comportamiento esperado (p. ej. `Tires`, `StartEngine`, `AddGas`). |
| **I – Interface Segregation** | `IVehicle`, `IVehicleRepository` | Las interfaces exponen únicamente los métodos relevantes para su contrato (comportamiento del vehículo por un lado, persistencia por otro), evitando forzar implementaciones innecesarias. |
| **D – Dependency Inversion** | `HomeController` depende de `IVehicleRepository`, no de una implementación concreta; la inyección se resuelve vía `ServicesConfiguration` (Inyección de Dependencias de ASP.NET Core) | El controlador y las clases de alto nivel dependen de abstracciones (interfaces), no de implementaciones concretas como `MyVehiclesRepository` o `DBVehicleRepository`, permitiendo intercambiar el repositorio en memoria por uno de base de datos sin tocar el controlador. |

---

## 🎨 Patrones de diseño aplicados

### 1. Repository Pattern
- **Interfaz:** `Repositories/IVehicleRepository.cs`
- **Implementaciones:** `MyVehiclesRepository` (persistencia en memoria) y `DBVehicleRepository` (esqueleto preparado para conectarse a una base de datos real).
- **Propósito:** Desacoplar la lógica de acceso a datos del resto de la aplicación. Permite que el equipo pruebe la funcionalidad completa **sin depender de que el esquema de base de datos esté listo**, y en el futuro solo se debe cambiar el registro de la inyección de dependencias (`ServicesConfiguration`) para apuntar a `DBVehicleRepository` una vez esté implementado.

### 2. Singleton Pattern
- **Clase:** `Infraestructure/Singleton/MemoryCollection.cs`
- **Propósito:** Mantener una única colección de vehículos compartida en memoria durante todo el ciclo de vida de la aplicación, simulando una fuente de datos persistente mientras no existe una base de datos real.

### 3. Builder Pattern
- **Clase:** `ModelBuilders/CarBuilder.cs`
- **Propósito:** Construir instancias de `Car` de forma fluida (`SetColor().SetBrand().SetModel().Build()`), centralizando la asignación de **propiedades por defecto** (como el año actual, calculado dinámicamente con `DateTime.Now.Year`). Este diseño minimiza el impacto de futuros cambios: si el equipo de negocio solicita agregar las 20 propiedades adicionales previstas para el siguiente sprint, solo será necesario añadir los nuevos `Set...()` y su valor por defecto dentro del builder, sin modificar las factories ni el controlador.

### 4. Factory Method Pattern
- **Clase abstracta:** `FactoryMethods/CarFactory.cs`
- **Implementaciones:** `MustangFactory`, `ExplorerFactory`, `FordEscapeFactory`
- **Propósito:** Encapsular la creación de cada modelo de vehículo específico (delegando la construcción real al `CarBuilder`). Ante la previsión de que el negocio agregará más modelos en el futuro, este patrón permite **añadir nuevas fábricas** sin modificar el código existente (cumpliendo también el principio Open/Closed).

### 5. Dependency Injection (Composition Root)
- **Clase:** `Infraestructure/DependencyInjection/ServicesConfiguration.cs`
- **Propósito:** Centralizar el registro de servicios e interfaces (`IVehicleRepository` → `MyVehiclesRepository`) en un único punto, facilitando el cambio de implementación (por ejemplo, hacia `DBVehicleRepository`) sin tocar el resto de la aplicación.

### 6. Herencia y Polimorfismo (Template para tipos de vehículo)
- **Clases:** `Vehicle` (abstracta) → `Car`, `Motocycle`
- **Propósito:** Definir comportamiento común (encender/apagar motor, cargar gasolina, validar combustible) en la clase base `Vehicle`, mientras cada subtipo especializa propiedades particulares (por ejemplo, el número de llantas: 4 para `Car`, 2 para `Motocycle`).

---

## 🏗️ Estructura del proyecto
```bash
Udla-Workshop-Design-Patterns-master/
├── DesignPatterns/                     # Aplicación principal ASP.NET Core MVC (.NET 8)
│   ├── Controllers/
│   │   └── HomeController.cs           # Endpoints: Index, AddMustang, AddExplorer, AddEscape, StartEngine, AddGas, StopEngine
│   ├── FactoryMethods/                 # Factory Method (CarFactory y fábricas concretas)
│   ├── Infraestructure/
│   │   ├── DependencyInjection/        # Registro de servicios (DIP)
│   │   └── Singleton/                  # MemoryCollection (Singleton)
│   ├── ModelBuilders/                  # CarBuilder (Builder)
│   ├── Models/                         # Vehicle, Car, Motocycle, IVehicle, HomeViewModel
│   ├── Repositories/                   # IVehicleRepository y sus implementaciones (Repository)
│   ├── Views/                          # Vistas Razor (Home/Index, Privacy, Error, Layout)
│   ├── Program.cs / Startup.cs
│   ├── Dockerfile
│   └── DesignPatterns.csproj
├── headapps/app/                       # Migración a Next.js 16 + React 19 + TypeScript
│   ├── src/actions/                    # Server Actions (add-explorer.ts)
│   ├── src/components/                 # ActionBar, VehicleList, componentes UI
│   ├── src/lib/database/               # MemoryDatabase (Singleton en TS)
│   ├── src/lib/services/               # VehicleService (Repository/Service Layer en TS)
│   └── src/lib/types/                  # Tipos de dominio (Vehicle)
├── docker-compose.yml
├── docker-compose.override.yml
└── Instrucciones.md                    # Enunciado original del taller
```
---

## ⚙️ Requisitos previos

### Para la aplicación ASP.NET Core (`DesignPatterns/`)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- (Opcional) [Docker](https://www.docker.com/) si se prefiere ejecutar en contenedor

### Para la aplicación Next.js (`headapps/app/`)
- [Node.js 18+](https://nodejs.org/)
- [pnpm](https://pnpm.io/) (el proyecto incluye `pnpm-lock.yaml`)

---

## 🚀 Cómo ejecutar el proyecto

### Opción 1: Aplicación ASP.NET Core MVC (proyecto principal)

1. Clonar el repositorio:

https://github.com/DavidPuga04/Tarea-12-Taller-Formativo-Aplicacion-mejores-practicas-Principios-Solid-y-patrones-de-diseno.git

2. Ubicarse en la carpeta del proyecto:
```bash
   cd DesignPatterns
```
3. Restaurar dependencias:
```bash
   dotnet restore
```
4. Ejecutar la aplicación:
```bash
   dotnet run
```
5. Abrir el navegador en la URL indicada en consola (por defecto algo como `https://localhost:5001` o `http://localhost:5000`).

#### Con Docker

Desde la raíz del repositorio:
```bash
docker compose up --build
```
La aplicación quedará disponible en `http://localhost:5101` (según `docker-compose.override.yml`).

### Opción 2: Aplicación Next.js (migración moderna)

1. Ubicarse en la carpeta del proyecto:
```bash
   cd headapps/app
```
2. Instalar dependencias:
```bash
   pnpm install
```
3. Ejecutar en modo desarrollo:
```bash
   pnpm dev
```
4. Abrir el navegador en `http://localhost:3000`.

---

## 🌐 Despliegue

Esta aplicación se encuentra desplegada en Render

🔗 Link del deploy: https://minicore-front-z1bv.onrender.com

---

## 🎥 Video explicativo

Video en YouTube explicando el desarrollo, los principios SOLID y los patrones de diseño aplicados en el proyecto

🔗 Link del video: https://youtu.be/_ZfVo8-V5hQ

---

## 👤 Autor

*David Puga* /
david.puga@udla.edu.ec
