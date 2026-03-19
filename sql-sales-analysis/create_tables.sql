CREATE TABLE Customers(
	customer_id SERIAL PRIMARY KEY,
	name VARCHAR(50),
	email VARCHAR (100),
	city VARCHAR(50)
);

CREATE TABLE Products(
	product_id SERIAL PRIMARY KEY,
	product_name VARCHAR(50),
	category VARCHAR(50),
	price NUMERIC
);

CREATE TABLE Orders(
	order_id SERIAL PRIMARY KEY,
	customer_id INT,
	FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
	order_date DATE
);

CREATE TABLE Order_Items(
	order_item_id SERIAL PRIMARY KEY,
	order_id INT,
	FOREIGN KEY (order_id) REFERENCES Orders(order_id),
	product_id INT,
	FOREIGN KEY (product_id) REFERENCES Products(product_id),
	quantity INT
	
);