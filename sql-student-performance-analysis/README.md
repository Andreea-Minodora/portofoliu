
# Student Performance SQL Analysis

Acesta este **al doilea meu proiect SQL** și continuă procesul meu de învățare a bazelor de date relaționale și a analizelor SQL folosind **PostgreSQL**.
Proiectul analizează **performanța academică a studenților**, folosind o bază de date simplă care conține studenți, cursuri, înscrieri la cursuri și note.

---

## Scopul proiectului

Scopul acestui proiect a fost să exersez și să aplic concepte SQL precum:

* relații între tabele
* agregări (`AVG`, `COUNT`, `MAX`)
* filtrare după agregare (`HAVING`)
* `JOIN` și `LEFT JOIN`
* subinterogări (subqueries)
* analize de tip **top performeri**

---

## Structura bazei de date

Baza de date conține 4 tabele principale:

### Students

Informații despre studenți.

* `student_id`
* `name`
* `email`
* `year`

---

### Courses

Lista cursurilor disponibile.

* `course_id`
* `course_name`
* `teacher`

---

### Enrollments

Tabel intermediar care arată la ce cursuri sunt înscriși studenții.

* `enrollment_id`
* `student_id`
* `course_id`

---

### Grades

Notele obținute de studenți la cursuri.

* `grade_id`
* `student_id`
* `course_id`
* `grade`

---

## Analize SQL realizate

În acest proiect am realizat mai multe interogări pentru analiza performanței studenților:

* media fiecărui student
* studentul cu cea mai mare medie
* media notelor pe fiecare curs
* numărul de studenți înscriși la fiecare curs
* nota maximă pe fiecare curs
* cursurile cu media peste 8
* top 3 studenți după medie
* studenți fără note (`LEFT JOIN`)
* studentul cu nota maximă la fiecare curs (subquery)

---

## Structura proiectului

```
sql-student-performance-analysis
│
├── create_tables.sql
├── insert_data.sql
├── queries.sql
└── README.md
```

---

## Tehnologii utilizate

* PostgreSQL
* SQL

---

## Ce am învățat

Prin acest proiect am învățat să folosesc:

* `CREATE TABLE`
* `PRIMARY KEY`
* `FOREIGN KEY`
* `JOIN`
* `LEFT JOIN`
* `AVG`
* `COUNT`
* `MAX`
* `GROUP BY`
* `HAVING`
* `ORDER BY`
* `LIMIT`
* subqueries

Acest proiect face parte din **portofoliul meu de învățare SQL** și reprezintă un pas important în dezvoltarea abilităților mele de analiză a datelor folosind baze de date relaționale.

---

## Autor

**Andreea Minodora**
