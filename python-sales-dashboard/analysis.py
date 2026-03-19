import pandas as pd
import matplotlib.pyplot as plt

df=pd.read_csv("data.csv")
#df=dataFrame(adica tabel)
print(df)


df["revenue"]=df["price"] * df["quantity"]

total_revenue=df["revenue"].sum()

print("total revenue:", total_revenue)

top_products =df.groupby("product_name")["quantity"].sum().sort_values(ascending=False)

print(top_products)

top_products.plot(kind="bar",title="top products")
plt.savefig("plots/top+products.png")
plt.clf()


top_clients =df.groupby("customer_name")["revenue"].sum().sort_values(ascending=False)

print(top_clients)

sales_by_category = df.groupby("category")["revenue"].sum().sort_values(ascending=False)

print(sales_by_category)
sales_by_category.plot(kind="bar",title="sales by category")
plt.savefig("plots/sales_by_category.png")
plt.clf()

average_order= df.groupby("order_id")["revenue"].sum().mean()

print("Average order Value:", average_order)
