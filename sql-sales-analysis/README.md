# Sales Data SQL Analysis

Acesta este al treilea meu proiect SQL, realizat folosind **PostgreSQL**, și are ca scop analiza datelor de vânzări pentru un magazin online.

Proiectul modelează o bază de date relațională și include mai multe interogări SQL pentru analiza performanței vânzărilor.

---

## Scopul proiectului

Scopul acestui proiect a fost să exersez concepte SQL utilizate în analiza datelor, precum:

* relații între tabele
* agregări (`SUM`, `AVG`)
* analiză de tip business (venituri, clienți, produse)
* utilizarea subinterogărilor (subqueries)

---

## Structura bazei de date

Proiectul conține 4 tabele principale:

### Customers

Informații despre clienți.

* `customer_id`
* `name`
* `email`
* `city`

---

### Products

Informații despre produsele disponibile.

* `product_id`
* `product_name`
* `category`
* `price`

---

### Orders

Comenzile plasate de clienți.

* `order_id`
* `customer_id`
* `order_date`

---

### Order_Items

Tabel intermediar care leagă comenzile de produse și cantități.

* `order_item_id`
* `order_id`
* `product_id`
* `quantity`

---

## Analize SQL realizate

În cadrul acestui proiect am realizat următoarele analize:

* calcularea venitului total al magazinului
* vânzări totale pentru fiecare produs
* identificarea clienților care au cheltuit cel mai mult
* analiza vânzărilor pe categorii de produse
* calcularea valorii fiecărei comenzi
* calcularea valorii medii a comenzilor (folosind subquery)

---

## Structura proiectului

```id="n3mqte"
sql-sales-analysis
│
├── create_tables.sql
├── insert_data.sql
├── queries.sql
└── README.md
```

---

## Tehnologii utilizate

* SQL
* PostgreSQL

---

## Ce am învățat

Prin acest proiect am învățat să folosesc:

* `CREATE TABLE`
* `PRIMARY KEY`
* `FOREIGN KEY`
* `JOIN`
* `SUM`
* `AVG`
* `GROUP BY`
* `ORDER BY`
* `LIMIT`
* subqueries

Acest proiect face parte din portofoliul meu SQL și demonstrează abilitățile mele de analiză a datelor folosind baze de date relaționale.

---

## Autor

**Andreea Minodora**

