/*
    Datos de prueba para 4VGym
    Inserta 10 registros en ActivityTypes, Instructors y Activities.
    Tambien relaciona las actividades con sus monitores.

    IMPORTANTE:
    - Ejecutar sobre la base de datos 4vGym.
    - Elimina registros existentes.
*/

USE [4vGym];
GO

SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

DELETE FROM ActivityInstructor;
DELETE FROM Activities;
DELETE FROM Instructors;
DELETE FROM ActivityTypes;

/* 1. Tipos de actividad */
SET IDENTITY_INSERT dbo.ActivityTypes ON;

INSERT INTO dbo.ActivityTypes (Id, Name, NumberMonitors)
VALUES
    (1, 'Yoga', 1),
    (2, 'Pilates', 1),
    (3, 'Spinning', 2),
    (4, 'Cross Training', 2),
    (5, 'Zumba', 1),
    (6, 'Natacion', 2),
    (7, 'Boxeo', 2),
    (8, 'Body Pump', 2),
    (9, 'Estiramientos', 1),
    (10, 'Entrenamiento funcional', 2);

SET IDENTITY_INSERT dbo.ActivityTypes OFF;

/* 2. Monitores */
SET IDENTITY_INSERT dbo.Instructors ON;

INSERT INTO dbo.Instructors (Id, Name, Email, PhotoUrl, Phone)
VALUES
    (1, 'Miguel Goyena', 'miguel.goyena@4vgym.com', 'https://example.com/monitores/miguel.jpg', '600100001'),
    (2, 'Maria Martin', 'maria.martin@4vgym.com', 'https://example.com/monitores/maria.jpg', '600100002'),
    (3, 'Iban Sarria', 'iban.sarria@4vgym.com', 'https://example.com/monitores/iban.jpg', '600100003'),
    (4, 'Laura Garcia', 'laura.garcia@4vgym.com', 'https://example.com/monitores/laura.jpg', '600100004'),
    (5, 'Carlos Ruiz', 'carlos.ruiz@4vgym.com', 'https://example.com/monitores/carlos.jpg', '600100005'),
    (6, 'Ana Lopez', 'ana.lopez@4vgym.com', 'https://example.com/monitores/ana.jpg', '600100006'),
    (7, 'David Perez', 'david.perez@4vgym.com', 'https://example.com/monitores/david.jpg', '600100007'),
    (8, 'Elena Sanchez', 'elena.sanchez@4vgym.com', 'https://example.com/monitores/elena.jpg', '600100008'),
    (9, 'Javier Moreno', 'javier.moreno@4vgym.com', 'https://example.com/monitores/javier.jpg', '600100009'),
    (10, 'Sara Fernandez', 'sara.fernandez@4vgym.com', 'https://example.com/monitores/sara.jpg', '600100010');

SET IDENTITY_INSERT dbo.Instructors OFF;

/* 3. Actividades
   InstructorsId almacena la lista de identificadores como JSON.
   Las fechas se establecen en el futuro para cumplir la validacion del modelo.
*/
SET IDENTITY_INSERT dbo.Activities ON;

INSERT INTO dbo.Activities
    (Id, ActivityTypeId, MaxParticipants, DateStart, DateEnd)
VALUES
    (1, 1, 15, '2026-12-01T09:00:00', '2026-12-01T10:00:00'),
    (2, 2, 18, '2026-12-02T10:00:00', '2026-12-02T11:00:00'),
    (3, 3, 20, '2026-12-03T17:00:00', '2026-12-03T18:00:00'),
    (4, 4, 20, '2026-12-04T18:00:00', '2026-12-04T19:00:00'),
    (5, 5, 25, '2026-12-05T11:00:00', '2026-12-05T12:00:00'),
    (6, 6, 15, '2026-12-06T09:00:00', '2026-12-06T10:30:00'),
    (7, 7, 16, '2026-12-07T17:00:00', '2026-12-07T18:00:00'),
    (8, 8, 22, '2026-12-08T18:00:00', '2026-12-08T19:00:00'),
    (9, 9, 12, '2026-12-09T08:00:00', '2026-12-09T09:00:00'),
    (10, 10, 20, '2026-12-10T19:00:00', '2026-12-10T20:00:00');

SET IDENTITY_INSERT dbo.Activities OFF;

/* 4. Relacion muchos a muchos entre actividades y monitores.
   EF Core crea esta tabla de union a partir de las propiedades de navegacion.
   Si ya existe, se insertan las relaciones en ella.
*/
IF OBJECT_ID(N'dbo.ActivityInstructor', N'U') IS NOT NULL
BEGIN
    INSERT INTO dbo.ActivityInstructor (ActivitiesId, InstructorsId)
    VALUES
        (1, 1),
        (2, 2),
        (3, 3), (3, 4),
        (4, 5), (4, 6),
        (5, 7),
        (6, 8), (6, 9),
        (7, 1), (7, 10),
        (8, 2), (8, 3),
        (9, 4),
        (10, 5), (10, 6);
END;

COMMIT TRANSACTION;
GO

/* Comprobacion: deben aparecer 10 filas en cada tabla principal. */
SELECT COUNT(*) AS TotalActivityTypes FROM dbo.ActivityTypes;
SELECT COUNT(*) AS TotalInstructors FROM dbo.Instructors;
SELECT COUNT(*) AS TotalActivities FROM dbo.Activities;
GO