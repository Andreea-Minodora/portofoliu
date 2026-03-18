
# Online Store SQL Analysis

Acesta este **primul meu proiect SQL**, realizat pentru a exersa conceptele de bază ale bazelor de date relaționale și ale limbajului SQL folosind **PostgreSQL**.

Proiectul simulează o bază de date simplă pentru un **magazin online**, care permite gestionarea clienților, produselor, comenzilor și produselor din fiecare comandă.

---

## Scopul proiectului

Scopul acestui proiect a fost să învăț și să aplic concepte fundamentale SQL, precum:

* crearea tabelelor
* definirea relațiilor între tabele
* inserarea datelor
* realizarea interogărilor pentru analiză

---

## Structura bazei de date

Proiectul conține 4 tabele principale:

### Clienti

Conține informații despre clienți.

* id_client
* nume
* email
* oras

### Produse

Conține lista produselor disponibile în magazin.

* id_produs
* nume_produs
* categorie
* pret

### Comenzi

Conține comenzile realizate de clienți.

* id_comanda
* data_comanda
* id_client

### Produse_Comenzi

Tabel intermediar care leagă comenzile de produsele comandate.

* id
* id_comanda
* id_produs
* cantitate

---

## Exemple de interogări realizate

În cadrul proiectului am realizat mai multe analize SQL:

* afișarea produselor din fiecare comandă
* calcularea valorii fiecărei linii din comandă
* calcularea valorii totale a fiecărei comenzi
* identificarea clientului care a cheltuit cel mai mult
* calcularea venitului total al magazinului
* identificarea produselor cele mai vândute
* afișarea **Top 3 produse după vânzări**

---

## Tehnologii utilizate

* PostgreSQL
* SQL

---

## Structura proiectului

```
sql-online-store-analysis
│
├── create_tables.sql
├── insert_data.sql
├── queries.sql
└── README.md
```

---

## Ce am învățat

Prin acest proiect am învățat să folosesc:

* `CREATE TABLE`
* `FOREIGN KEY`
* `INSERT`
* `JOIN`
* `SUM`
* `GROUP BY`
* `ORDER BY`
* `LIMIT`

Acest proiect reprezintă **primul meu pas în învățarea SQL și a analizei datelor folosind baze de date relaționale**.

---

## Autor

Andreea Minodora
