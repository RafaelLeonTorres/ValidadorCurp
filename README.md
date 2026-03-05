# 🧾 Validador CURP

Aplicación Fullstack para validar una CURP contra:

* Nombres
* Apellido Paterno
* Apellido Materno
* Fecha de nacimiento
* Sexo
* Estado
* Nacionalidad

El sistema compara la CURP contra los datos proporcionados y devuelve una lista de inconsistencias o confirma que es válida.

---

# 🏗️ Tecnologías

## Backend

* .NET 8
* ASP.NET Core Web API
* Swagger

## Frontend

* Angular 17+
* Angular Material
* TypeScript

---

# 📂 Estructura del Proyecto

```
ValidadorCurp/
│
├── CurpValidatorAPI/         → API .NET
└── CurpAngularApp/           → Aplicación Angular
```

---

# ⚙️ Requisitos

* .NET 8 SDK
* Node.js 18+
* Angular CLI

Verificar versiones:

```bash
dotnet --version
node -v
npm -v
```

---

# 🚀 Cómo ejecutar el Backend (.NET API)

1️⃣ Entrar a la carpeta del API:

```bash
cd CurpValidator
```

2️⃣ Restaurar dependencias:

```bash
dotnet restore
```

3️⃣ Ejecutar la aplicación:

```bash
dotnet run
```

La API normalmente corre en:

```
https://localhost:7190
```

Swagger estará disponible en:

```
https://localhost:7190/swagger
```

⚠️ IMPORTANTE: Verificar el puerto que muestra la consola.

---

# 🌐 Cómo ejecutar el Frontend (Angular)

1️⃣ Entrar a la carpeta Angular:

```bash
cd CurpAngularApp
```

2️⃣ Instalar dependencias:

```bash
npm install
```

3️⃣ Ejecutar la aplicación:

```bash
npm start
```

La aplicación estará disponible en:

```
http://localhost:4200
```

---

# 🔗 Configuración Importante: URL del API

En Angular verificar el archivo del servicio:

```
src/app/services/curpValidar.service.ts
```

Debe tener algo como:

```ts
private urlBase = 'https://localhost:7190/api/Curp';
```

⚠️ Si el puerto del backend cambia, actualizar esta URL.

---

# 📡 Endpoint del Backend

```
POST /api/Curp/validar
```

Ejemplo de JSON enviado:

```json
{
  "curp": "SABC921215HMCLRR09",
  "nombres": "Carlos",
  "apellidoPaterno": "Salgado",
  "apellidoMaterno": "Briseño",
  "fechaNacimiento": "1992-12-15T00:00:00.000Z",
  "sexo": 1,
  "esMexicano": true
}
```

Respuesta:

* `[]` → CURP válida
* `["Error 1", "Error 2"]` → Lista de inconsistencias

---

# 🛑 Problemas comunes

## Error CORS

Si Angular no puede conectarse al backend:

Verificar en `Program.cs` que esté habilitado CORS:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

app.UseCors("AllowAll");
```

---

# 🧪 Flujo de uso

1. Ejecutar API
2. Ejecutar Angular
3. Abrir http://localhost:4200
4. Capturar datos
5. Presionar Validar
6. Ver resultado

---

# 📌 Notas

* El frontend convierte la fecha al formato ISO antes de enviarla.
* El backend valida la CURP comparando estructura y datos personales.
* El sistema no persiste información en base de datos.

---

# 👨‍💻 Autor

Rafael León Torres
