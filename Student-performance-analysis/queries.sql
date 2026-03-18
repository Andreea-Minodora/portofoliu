-- 1. Media fiecărui student
SELECT Students.name, AVG(grade) AS average_grade
FROM Students
JOIN Grades
ON Students.student_id = Grades.student_id
GROUP BY Students.name
ORDER BY average_grade DESC;

-- 2. Studentul cu cea mai mare medie
SELECT Students.name, AVG(grade) AS average_grade
FROM Students
JOIN Grades
ON Students.student_id = Grades.student_id
GROUP BY Students.name
ORDER BY average_grade DESC
LIMIT 1;

-- 3. Media pe fiecare curs
SELECT Courses.course_name, AVG(grade) AS average_grade
FROM Courses
JOIN Grades
ON Courses.course_id = Grades.course_id
GROUP BY Courses.course_name
ORDER BY average_grade DESC;

-- 4. Numărul de studenți pe fiecare curs
SELECT Courses.course_name, COUNT(Enrollments.student_id) AS total_students
FROM Courses
JOIN Enrollments
ON Courses.course_id = Enrollments.course_id
GROUP BY Courses.course_name;

-- 5. Nota maximă pe fiecare curs
SELECT Courses.course_name, MAX(grade) AS nota_maxima
FROM Courses
JOIN Grades
ON Courses.course_id = Grades.course_id
GROUP BY Courses.course_name;

-- 6. Cursurile cu media peste 8
SELECT Courses.course_name, AVG(grade) AS media_note
FROM Courses
JOIN Grades
ON Courses.course_id = Grades.course_id
GROUP BY Courses.course_name
HAVING AVG(grade) > 8;

-- 7. Top 3 studenți după medie
SELECT Students.name, AVG(grade) AS average_grade
FROM Students
JOIN Grades
ON Students.student_id = Grades.student_id
GROUP BY Students.name
ORDER BY average_grade DESC
LIMIT 3;

-- 8. Studenți fără note
SELECT Students.name
FROM Students
LEFT JOIN Grades 
ON Students.student_id = Grades.student_id
WHERE Grades.student_id IS NULL;

-- 9. Studentul cu nota maximă la fiecare curs
SELECT Courses.course_name,
       Students.name,
       Grades.grade
FROM Grades
JOIN Students
ON Grades.student_id = Students.student_id
JOIN Courses
ON Grades.course_id = Courses.course_id
JOIN (
    SELECT course_id, MAX(grade) AS max_grade
    FROM Grades
    GROUP BY course_id
) MaxGrades
ON Grades.course_id = MaxGrades.course_id
AND Grades.grade = MaxGrades.max_grade;