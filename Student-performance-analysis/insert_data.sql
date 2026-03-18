INSERT INTO Students(name,email,year)
VALUES
('Andreea','andreea@gmail.com',2),
('Maria','maria@gmail.com',1),
('Ion','ion@gmail.com',3),
('Elena','elena@gmail.com',2),
('Vlad','vlad@gmail.com',1);

INSERT INTO Courses(course_name,teacher)
VALUES
('Databases','Prof. Popescu'),
('Programming','Prof. Ionescu'),
('Mathematics','Prof. Georgescu');

INSERT INTO Enrollments(student_id,course_id)
VALUES
(1,1),
(1,2),
(2,2),
(2,3),
(3,1),
(3,3),
(4,1),
(5,2);


INSERT INTO Grades(student_id,course_id,grade)
VALUES
(1,1,10),
(1,2,9),
(2,2,8),
(2,3,7),
(3,1,9),
(3,3,10),
(4,1,8),
(5,2,9);
SELECT * FROM Students;
SELECT * FROM Courses;
SELECT * FROM Enrollments;
SELECT * FROM Grades;
