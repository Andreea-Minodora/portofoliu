import pandas as pd

df=pd.read_csv("students.csv")
print(df)

medie_per_student = df.groupby("student_name")["grade"].mean()
print("media fiecarui student:",medie_per_student)

medie_per_student = df.groupby("student_name")["grade"].mean().sort_values(ascending=False)[0:1]
print("Studentul cu cea mai mare medie",medie_per_student)

medie_note_by_curs =  df.groupby("course")["grade"].mean()
print("media notelor pentru fiecare curs",medie_note_by_curs)


medie_per_student = df.groupby("student_name")["grade"].mean()
media_peste_8=medie_per_student[medie_per_student>8]
print("Studentii cu media peste 8:",media_peste_8)

nr_cursuri= df.groupby("student_name")["course"].nunique()
print("numarul de cursuri:",nr_cursuri)

suma_note = df["grade"].sum()

print("Suma tuturor notelor",suma_note)

media_note=df["grade"].mean()
print("Media notelor:",media_note)


medie_per_student = df.groupby("student_name")["grade"].mean().sort_values(ascending=True)[0:1]
print("Studentul cu cea mai mica medie",medie_per_student)


medie_note_by_curs =  df.groupby("course")["grade"].mean().sort_values(ascending=False)[0:1]
print("Cursul cu media cea mai mare",medie_note_by_curs)


filtru=df[df["grade"]>9]
print("Notele mai mari ca 9",filtru)

media_notelor_mate=df[df["course"] == "Math"]["grade"].mean()
print("Media notelor la matematica",media_notelor_mate)


nr_studenti_dif=df.groupby("course")["student_name"].nunique()
print(nr_studenti_dif)