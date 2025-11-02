CREATE TABLE Status (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    DisplayName NVARCHAR(100) NOT NULL
);

----------------

CREATE TABLE [User]
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

----------------
CREATE TABLE Task (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    StatusId INT NOT NULL,
    CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    DueDate DATETIME2 NULL,
    CompletedDate DATETIME2 NULL,
    IsDeleted BIT NOT NULL DEFAULT 0
    CONSTRAINT FK_Task_Status FOREIGN KEY (StatusId) REFERENCES Status(Id)
);

-----------------
INSERT INTO Status (Name, DisplayName)
VALUES
('NotStarted', 'Not Started'),
('Ongoing', 'On Going'),
('Completed', 'Completed'),
('Blocked', 'Blocked');

------------------
INSERT INTO [User] (Email, Password) VALUES
('admin@gmail.com', 'admin123'),
('user1@gmail.com', 'password1'),
('user2@gmail.com', 'password2'),
('user3@gmail.com', 'password3'),
('user4@gmail.com', 'password4');

-----------------
INSERT INTO Task (Title, Description, CreatedDate, DueDate, CompletedDate, StatusId)
VALUES
('Design Database Schema', 'Design and finalize the DB schema for the project', GETDATE(), DATEADD(DAY, 3, GETDATE()), NULL, 2), -- On Going
('Create API Endpoints', 'Develop CRUD endpoints for Task module', GETDATE(), DATEADD(DAY, 7, GETDATE()), NULL, 1), -- Not Started
('Frontend Integration', 'Integrate API with Angular frontend', GETDATE(), DATEADD(DAY, 10, GETDATE()), NULL, 4), -- Blocked
('Testing and QA', 'Perform integration testing and bug fixes', DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -4, GETDATE()), 3), -- Completed
('Exercise and Workout', 'Read 2 chapters of the Angular development book.', GETDATE(), DATEADD(DAY, 3, GETDATE()), NULL, 2), -- On Going
('Household Chores', 'Prepare the monthly sales report for management review.', GETDATE(), DATEADD(DAY, 7, GETDATE()), NULL, 1), -- Not Started
('Filing and Documentation', 'Coordinate with vendors for the upcoming product launch event.', GETDATE(), DATEADD(DAY, 10, GETDATE()), NULL, 4), -- Blocked
('Online Courses and Tutorials', 'Study chapters 5-8 for the upcoming history exam.', DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -4, GETDATE()), 3); -- Completed
