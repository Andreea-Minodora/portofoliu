
SELECT SUM(price*quantity)
FROM Products
JOIN Order_Items
ON Products.product_id=Order_Items.product_id


SELECT Products.product_name, SUM(price*quantity) AS VANZARI
FROM Products
JOIN Order_Items
ON Products.product_id=Order_Items.product_id
GROUP BY Products.product_name
ORDER BY VANZARI DESC

SELECT Customers.name, SUM(price*quantity) AS TOTAL
FROM Customers
JOIN Orders
ON Customers.customer_id=Orders.customer_id
JOIN Order_Items
ON Order_Items.order_id=Orders.order_id
JOIN Products
ON Order_Items.product_id=Products.product_id
GROUP BY Customers.name
ORDER BY TOTAL DESC;


SELECT Products.category, SUM(price*quantity) AS TOTAL
FROM Products
JOIN Order_Items
ON Products.product_id=Order_Items.product_id
GROUP BY Products.category
ORDER BY TOTAL DESC;


SELECT AVG(TOTAL) AS MEDIA_VALORII_COMENZILOR
FROM(
	SELECT Orders.order_id, SUM(price*quantity) AS TOTAL
	FROM Orders
	JOIN Order_Items
	ON Orders.order_id=Order_Items.order_id
	JOIN Products
	ON Order_Items.product_id=Products.product_id
	GROUP BY Orders.order_id
	)