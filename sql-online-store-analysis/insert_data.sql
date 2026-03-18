INSERT INTO Clienti (id_client, nume, email, oras)
VALUES
(1, 'Andreea', 'andreea@gmail.com', 'Brasov'),
(2, 'Maria', 'maria@gmail.com', 'Cluj'),
(3, 'Ion', 'ion@gmail.com', 'Bucuresti'),
(4, 'Elena', 'elena@gmail.com', 'Iasi');

INSERT INTO Produse (nume_produs, categorie, pret)
VALUES
('Laptop', 'Electronice', 3500.99),
('Mouse', 'Accesorii', 89.50),
('Tastatura', 'Accesorii', 150.00),
('Monitor', 'Electronice', 899.99),
('Casti', 'Audio', 199.90);

INSERT INTO Comenzi (data_comanda, id_client)
VALUES
('2026-02-12', 1),
('2026-02-23', 1),
('2026-02-18', 2),
('2026-02-19', 3);

INSERT INTO Produse_Comenzi (id_comanda, id_produs, cantitate)
VALUES
(1, 1, 1),
(1, 2, 2),
(2, 3, 1),
(2, 5, 1),
(3, 4, 1),
(3, 2, 1),
(4, 1, 1),
(4, 5, 2);