import streamlit as st
import pandas as pd


st.title=("Students Dashboard")

df=pd.read_csv("students.csv")

st.dataframe(df)

col1, col2 = st.columns(2)

course = st.selectbox("Selecteaza cursul",["All"]+list(df["course"].unique()))

if course == "All":
    filtered_df = df
else:
    filtered_df = df[df["course"] == course]

st.dataframe(filtered_df)

suma_note = filtered_df["grade"].sum()
media_note=filtered_df["grade"].mean()

with col1:
    st.write("Media notelor:",media_note)
with col2:
    st.write("Suma notelor:",suma_note)


medie_per_student = filtered_df.groupby("student_name")["grade"].mean()
st.subheader("Medie Student")
st.bar_chart(medie_per_student)

medie_per_student = filtered_df.groupby("student_name")["grade"].mean().sort_values(ascending=False)[0:1]
st.write("Studentul cu media cea mai mare",medie_per_student)