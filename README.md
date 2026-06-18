# 🌸 Botica Imperial

Sistema de gestión para una botica imperial inspirado en el universo de *Los diarios de la boticaria*.
Este proyecto es una evolución completa del ejercicio realizado durante el primer curso de **1º de DAW**, reconstruido con una arquitectura más sólida, nuevas tecnologías y funcionalidades avanzadas.

---

## 📖 Introducción

En el Palacio Imperial, la botica se ha convertido en un punto clave para resolver emergencias médicas, estudiar posibles envenenamientos y controlar sustancias peligrosas.

Jinshi, encargado de supervisar el orden administrativo del palacio, ha detectado que el antiguo sistema ya no es suficiente. La cantidad de sustancias, casos médicos e investigaciones ha aumentado, por lo que Maomao necesita una aplicación más robusta, organizada y preparada para trabajar con diferentes sistemas de persistencia.

El objetivo de este proyecto es desarrollar una aplicación web básica que permita gestionar sustancias alquímicas, casos médicos, tratamientos, sospechas de envenenamiento e informes, aplicando buenas prácticas de programación aprendidas durante el curso.

---

## 🎯 Objetivos del proyecto

Este proyecto busca aplicar y reforzar conocimientos de:

* Desarrollo web con Blazor
* Programación orientada a objetos
* Arquitectura por capas
* Acceso a datos con **ADO.NET**, **Dapper** y **Entity Framework Core**
* Persistencia configurable mediante `appsettings.json`
* Base de datos **SQLite**
* Importación y exportación de datos
* Validación de entidades
* Gestión de errores personalizados
* Sistema de caché **LRU**
* Borrado físico y lógico
* Logging en consola y ficheros
* Testing completo del sistema

---

## 🌿 Gestión de sustancias alquímicas

En la botica existen tres tipos principales de sustancias:

* **Medicinas**
* **Venenos**
* **Afrodisíacos**

Todas las sustancias comparten una serie de datos comunes, pero cada tipo posee atributos específicos según su función dentro del sistema.

---

### 🌱 Atributos comunes de una sustancia

Cada sustancia contiene:

* Identificador único
* Nombre
* Descripción
* Precio
* Disponibilidad
* Nivel de peligro
* Fecha de creación
* Fecha de última modificación
* Estado de borrado lógico

---

### 📦 Disponibilidad

La disponibilidad indica lo difícil que es encontrar una sustancia dentro del Palacio Imperial.

```text
Común
Rara
Muy rara
```

---

### ⚠️ Nivel de peligro

El nivel de peligro permite clasificar el riesgo de cada sustancia.

```text
Nulo
Bajo
Medio
Alto
Crítico
```

---

## 💊 Medicinas

Las medicinas son sustancias destinadas a tratar enfermedades, aliviar síntomas o estabilizar a un paciente.

### Atributos específicos

* Síntomas
* Dosis recomendada
* Efectos secundarios
* Tiempo de efecto

---

## ☠️ Venenos

Los venenos son sustancias capaces de provocar intoxicaciones, malestar, pérdida de conciencia o incluso la muerte.

### Atributos específicos

* Vía de administración
* Tiempo de aparición de síntomas
* Antídoto solo si es conocido
* Grado de toxicidad
* Probabilidad de supervivencia

### Vías de administración

```text
Oral
Contacto
Inhalación
Desconocida
```

---

## 💘 Afrodisíacos

Los afrodisíacos son sustancias que alteran el ánimo, la energía o el deseo. Aunque no siempre son letales, pueden provocar riesgos importantes si se usan de forma indebida.

### Atributos específicos

* Intensidad del efecto
* Duración
* Contraindicaciones
* Riesgos por uso excesivo


---

## 📜 Gestión de casos médicos

Los casos médicos representan investigaciones abiertas dentro del Palacio Imperial.
Pueden estar relacionados con enfermedades comunes, intoxicaciones, errores de medicación o sospechas de envenenamiento.

---

### 🧾 Atributos de un caso médico

Cada caso contiene:

* Identificador único
* Nombre o título del caso
* Síntomas observados
* Fecha de inicio
* Gravedad
* Causa sospechada
* Sustancias sospechosas
* Tratamientos aplicados
* Estado del caso

---

### 🚨 Gravedad del caso

```text
Nula
Leve
Moderada
Grave
Crítica
```

---

### 🔎 Causa sospechada

```text
Enfermedad
Veneno
Reacción adversa
Desconocida
```

---

### 📌 Estado del caso

```text
Abierto
En investigación
Resuelto
Archivado
```

---

## 🔗 Relación entre casos y sustancias

Un caso médico puede estar relacionado con varias sustancias.

El sistema permite:

* Asociar sustancias sospechosas a un caso
* Registrar medicinas utilizadas como tratamiento
* Consultar casos donde intervino un veneno
* Consultar casos donde una medicina fue efectiva
* Investigar sustancias repetidas en varios casos

---

## 🔒 Reglas de negocio

Para que el sistema sea más realista, se incorporan reglas de negocio que controlan el comportamiento de la aplicación.

### 🌿 Reglas sobre sustancias

* No pueden existir dos sustancias activas con el mismo nombre.
* El precio aproximado de una sustancia no puede ser negativo.
* Las sustancias borradas lógicamente no aparecerán en los listados normales.
* No se puede eliminar físicamente una sustancia asociada a un caso médico activo.

---

### 📜 Reglas sobre casos médicos

* No se puede registrar una fecha de inicio posterior a la fecha actual.
* Si la causa sospechada es veneno, debe existir al menos una sustancia de tipo veneno asociada.
* Si una medicina resuelve un caso, debe estar registrada previamente como tratamiento aplicado.
* Solo se permite borrar físicamente un caso si ya está archivado o resuelto.

---

## 🧩 Funcionalidades principales

### 🌿 Gestión de sustancias

El sistema permite:

* Crear sustancias
* Modificar sustancias
* Consultar sustancias
* Eliminar sustancias
* Restaurar sustancias borradas lógicamente
* Filtrar por tipo de sustancia
* Filtrar por disponibilidad
* Filtrar por nivel de peligro
* Consultar sustancias raras
* Consultar sustancias peligrosas
* Buscar sustancias por nombre
* Ver detalle completo de una sustancia

---

### 📜 Gestión de casos médicos

El sistema permite:

* Crear nuevos casos médicos
* Modificar casos existentes
* Registrar síntomas
* Asociar sustancias sospechosas
* Registrar tratamientos aplicados
* Resolver casos
* Archivar casos
* Filtrar casos por gravedad
* Filtrar casos por estado
* Consultar casos resueltos
* Consultar casos abiertos
* Ver historial de tratamientos aplicados

---

## 🖥️ Interfaz web con Blazor

La nueva versión incorpora una interfaz web básica desarrollada con Blazor.

### Pantallas previstas

* Página de inicio
* Listado de sustancias
* Formulario de creación y edición de sustancias
* Detalle de sustancia
* Listado de casos médicos
* Formulario de creación y edición de casos
* Detalle de caso médico
* Pantalla de informes
* Pantalla de importación y exportación
* Pantalla de configuración básica

---

## 🗄️ Persistencia de datos

El sistema trabaja con una base de datos **SQLite** y permite cambiar el tipo de repositorio desde el archivo `appsettings.json`.

Se implementan tres formas de acceso a datos:

* **ADO.NET**
* **Dapper**
* **Entity Framework Core**

---

### Tipos de repositorio permitidos

```text
Memoria
AdoNet
Dapper
EntityFramework
```

---

## 🗑️ Borrado físico y lógico

El sistema soporta dos tipos de eliminación.

### Borrado lógico

El registro no desaparece de la base de datos, sino que queda marcado como eliminado.

```text
IsDeleted = true
```

Este tipo de borrado permite recuperar información y mantener el historial.

### Borrado físico

El registro se elimina definitivamente de la base de datos.

Este borrado solo se permite cuando no rompe ninguna regla de negocio.

---

## 📤 Importación y exportación de datos

El sistema permite importar y exportar la información de sustancias y casos médicos en varios formatos.

### Formatos soportados

* JSON
* CSV
* XML
* Binario secuencial

---

### Datos exportables

* Sustancias
* Medicinas
* Venenos
* Afrodisíacos
* Casos médicos
* Informes generados

---

## ⚡ Sistema de caché LRU

Para mejorar el rendimiento, el proyecto incorpora una caché **LRU**.

LRU significa **Least Recently Used**, es decir, el sistema elimina de la caché los elementos menos utilizados recientemente cuando se supera el límite máximo configurado.

---

## 🧯 Sistema de errores personalizado

El proyecto incorpora un sistema de errores propio para controlar excepciones de forma clara y organizada.

### Tipos de errores previstos

* Error de validación
* Error de base de datos
* Error de importación
* Error de exportación
* Error de configuración
* Error de entidad no encontrada

---

## 📝 Logging

El sistema registra información importante en consola y en ficheros.

### Eventos registrados

* Inicio de la aplicación
* Creación de sustancias
* Modificación de sustancias
* Eliminación lógica o física
* Creación de casos médicos
* Resolución de casos
* Errores de validación
* Errores de base de datos
* Importaciones realizadas
* Exportaciones realizadas
* Cambios de repositorio desde configuración

---

## 📊 Informes

El sistema genera informes útiles para la administración de la botica.

### Informes principales

* Veneno más peligroso
* Sustancias más caras
* Sustancias con mayor nivel de peligro
* Casos médicos resueltos
* Casos médicos abiertos
* Casos donde la causa sospechada sea veneno
* Medicina más utilizada en tratamientos
* Afrodisíaco con mayor intensidad
* Porcentaje de casos resueltos
* Casos críticos pendientes
* Sustancias raras o muy raras
* Casos con más de una sustancia sospechosa

---

## 🧪 Testing

El proyecto incluye pruebas para comprobar el correcto funcionamiento de las partes principales del sistema.

### Pruebas previstas

* Tests de entidades
* Tests de validadores
* Tests de reglas de negocio
* Tests de servicios
* Tests de repositorios
* Tests de importación
* Tests de exportación
* Tests de caché LRU
* Tests de borrado lógico
* Tests de borrado físico
* Tests de errores personalizados
---


## 📌 Mejoras respecto a la primera versión

La primera versión del proyecto se centraba principalmente en la gestión de sustancias y casos médicos.

Esta nueva versión añade:

* Interfaz web con Blazor
* Tres sistemas de acceso a datos
* Configuración dinámica desde `appsettings.json`
* Base de datos SQLite
* Importación y exportación en varios formatos
* Borrado lógico y físico
* Caché LRU
* Reglas de negocio
* Logger en consola y ficheros
* Sistema de errores personalizado
* Testing completo
* Arquitectura más limpia y mantenible

---

## 🌙 Inspiración

Este proyecto está inspirado en la ambientación de *Los diarios de la boticaria*, especialmente en la investigación de sustancias, venenos, síntomas y casos médicos dentro de un entorno imperial.

La finalidad del proyecto es educativa y busca aplicar conocimientos de programación, bases de datos, arquitectura y testing en un contexto narrativo original y atractivo.

---

## 👩‍💻 Autora

Proyecto desarrollado por la alumna de 1º de DAW **Lucía Fuertes Cruz** como evolución de un ejercicio realizado durante el primer curso.

---
