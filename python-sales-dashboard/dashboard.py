import streamlit as st
import pandas as pd

st.title("Sales Dashboard")

df = pd.read_csv("data.csv")
df["revenue"] = df["price"] * df["quantity"]

category = st.selectbox("Select Category",["All"]+ list(df["category"].unique()))

if category =="All":
    filtered_df = df
else:
    filtered_df=df[df["category"]== category]

total_revenue = filtered_df["revenue"].sum()
top_products = filtered_df.groupby("product_name")["quantity"].sum().sort_values(ascending=False)
average_order=filtered_df.groupby("order_id")["revenue"].sum().mean()
top_customers=filtered_df.groupby("customer_name")["revenue"].sum().sort_values(ascending=False)

st.write("Filtered Dataset:")
st.dataframe(filtered_df)

st.write("Total Revenue:", total_revenue)
st.write("Average Order:",average_order)
st.write("Top Customers:",top_customers)
st.subheader("Top Products")
st.bar_chart(top_products)
st.subheader("Top Customers")
st.bar_chart(top_customers)

total_orders=filtered_df["order_id"].nunique()
st.write("total orders:",total_orders)