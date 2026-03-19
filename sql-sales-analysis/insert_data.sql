INSERT INTO Customers(name,city)
VALUES
('Andreea','Brasov'),
('Maria','Cluj'),
('Ion','Bucuresti'),
('Elena','Iasi'),
('Vlad','Timisoara');

INSERT INTO Products(product_name,category,price)
VALUES
('laptop','Electronics',3500),
('Mouse','Accessories',100),
('Keyboard','Accessories',200),
('Monitor','Electronics',900),
('Headphones','Audio',250);

INSERT INTO Orders(customer_id,order_date)
VALUES
(1,'2026-01-10'),
(2,'2026-01-12'),
(1,'2026-01-15'),
(3,'2026-01-20');

INSERT INTO Order_Items(order_id,product_id,quantity)
VALUES
(1,1,1),
(1,2,2),
(2,3,1),
(2,5,1),
(3,4,1),
(3,2,1),
(4,1,1),
(4,5,2);
