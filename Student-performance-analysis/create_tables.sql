CREATE TABLE Students(
	student_id SERIAL   PRIMARY KEY,
	name VARCHAR(50),
	email VARCHAR(100),
	year INT
);

CREATE TABLE Courses(
	course_id SERIAL PRIMARY KEY,
	course_name VARCHAR(50),
	teacher VARCHAR(50)
);

CREATE TABLE Enrollments(
	enrollment_id SERIAL PRIMARY KEY,
	student_id INT,
	FOREIGN KEY (student_id) REFERENCES Students(student_id),
	course_id INT,
	FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);

CREATE TABLE Grades(
	grade_id SERIAL PRIMARY KEY,
	student_id INT,
	FOREIGN KEY (student_id) REFERENCES Students(student_id),
	course_id INT,
	FOREIGN KEY (course_id) REFERENCES Courses(course_id),
	grade INT
);

	
)