CREATE TABLE Clienti (
    id_client INT PRIMARY KEY,
    nume VARCHAR(50),
    email VARCHAR(100),
    oras VARCHAR(50)
);

CREATE TABLE Produse (
    id_produs SERIAL PRIMARY KEY,
    nume_produs VARCHAR(50),
    categorie VARCHAR(50),
    pret NUMERIC
);

CREATE TABLE Comenzi (
    id_comanda SERIAL PRIMARY KEY,
    data_comanda DATE,
    id_client INT,
    FOREIGN KEY (id_client) REFERENCES Clienti(id_client)
);

CREATE TABLE Produse_Comenzi (
    id SERIAL PRIMARY KEY,
    id_comanda INT,
    FOREIGN KEY (id_comanda) REFERENCES Comenzi(id_comanda),
    id_produs INT,
    FOREIGN KEY (id_produs) REFERENCES Produse(id_produs),
    cantitate INT
);