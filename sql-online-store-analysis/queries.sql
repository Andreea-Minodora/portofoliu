-- 1. Afisarea tuturor clientilor
SELECT * 
FROM Clienti;

-- 2. Afisarea tuturor produselor
SELECT * 
FROM Produse;

-- 3. Afisarea tuturor comenzilor
SELECT * 
FROM Comenzi;

-- 4. Afisarea produselor din fiecare comanda
SELECT Clienti.nume,
       Comenzi.id_comanda,
       Produse.nume_produs,
       Produse_Comenzi.cantitate,
       Produse.pret,
       Comenzi.data_comanda
FROM Clienti
JOIN Comenzi
ON Clienti.id_client = Comenzi.id_client
JOIN Produse_Comenzi
ON Comenzi.id_comanda = Produse_Comenzi.id_comanda
JOIN Produse
ON Produse_Comenzi.id_produs = Produse.id_produs;

-- 5. Calcularea valorii fiecarei linii din comanda
SELECT Clienti.nume,
       Comenzi.id_comanda,
       Produse.nume_produs,
       Produse_Comenzi.cantitate,
       Produse.pret,
       Produse.pret * Produse_Comenzi.cantitate AS total,
       Comenzi.data_comanda
FROM Clienti
JOIN Comenzi
ON Clienti.id_client = Comenzi.id_client
JOIN Produse_Comenzi
ON Comenzi.id_comanda = Produse_Comenzi.id_comanda
JOIN Produse
ON Produse_Comenzi.id_produs = Produse.id_produs;

-- 6. Valoarea totala a fiecarei comenzi
SELECT Clienti.nume,
       Comenzi.id_comanda,
       SUM(Produse.pret * Produse_Comenzi.cantitate) AS total_comanda
FROM Clienti
JOIN Comenzi
ON Clienti.id_client = Comenzi.id_client
JOIN Produse_Comenzi
ON Comenzi.id_comanda = Produse_Comenzi.id_comanda
JOIN Produse
ON Produse_Comenzi.id_produs = Produse.id_produs
GROUP BY Clienti.nume, Comenzi.id_comanda
ORDER BY Comenzi.id_comanda;

-- 7. Clientul care a cheltuit cel mai mult
SELECT Clienti.nume,
       SUM(Produse.pret * Produse_Comenzi.cantitate) AS total_cheltuit
FROM Clienti
JOIN Comenzi
ON Clienti.id_client = Comenzi.id_client
JOIN Produse_Comenzi
ON Comenzi.id_comanda = Produse_Comenzi.id_comanda
JOIN Produse
ON Produse_Comenzi.id_produs = Produse.id_produs
GROUP BY Clienti.nume
ORDER BY total_cheltuit DESC;

-- 8. Venitul total al magazinului
SELECT SUM(Produse.pret * Produse_Comenzi.cantitate) AS venit_total
FROM Produse
JOIN Produse_Comenzi
ON Produse.id_produs = Produse_Comenzi.id_produs;

-- 9. Cantitatea vanduta pentru fiecare produs
SELECT Produse.nume_produs,
       SUM(Produse_Comenzi.cantitate) AS total_vandut
FROM Produse
JOIN Produse_Comenzi
ON Produse.id_produs = Produse_Comenzi.id_produs
GROUP BY Produse.nume_produs
ORDER BY total_vandut DESC;

-- 10. Top 3 produse dupa vanzari
SELECT Produse.nume_produs,
       SUM(Produse_Comenzi.cantitate) AS total_vandut
FROM Produse
JOIN Produse_Comenzi
ON Produse.id_produs = Produse_Comenzi.id_produs
GROUP BY Produse.nume_produs
ORDER BY total_vandut DESC
LIMIT 3;