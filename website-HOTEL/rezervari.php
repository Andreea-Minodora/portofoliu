<?php
// Setează afișarea erorilor pentru debugging
error_reporting(E_ALL);
ini_set('display_errors', 0);  // Nu afișa erorile pe pagină
ini_set('log_errors', 1);  // Activează logarea erorilor
ini_set('error_log', '/path/to/your/php-error.log');  // Setează calea fișierului de log

include 'db.php'; // Include fișierul de conexiune la baza de date

if (!$conn) {
    die("Eroare la conectarea la baza de date: " . mysqli_connect_error());
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    // Preia datele din formular
    $nume = isset($_POST['nume']) ? $_POST['nume'] : '';
    $email = isset($_POST['email']) ? $_POST['email'] : '';
    $nr_pers = isset($_POST['nr_pers']) ? $_POST['nr_pers'] : '';
    $data_rezerv = isset($_POST['data_rezerv']) ? $_POST['data_rezerv'] : '';
    $ora_rezerv = isset($_POST['ora_rezerv']) ? $_POST['ora_rezerv'] : '';
    $comentarii = isset($_POST['comentarii']) ? $_POST['comentarii'] : '';

    // Setează intervalul orar pentru rezervări (de exemplu 9:00 - 18:00)
    $interval_start = "09:00";
    $interval_end = "22:00";

    // Verifică dacă ora rezervării se află în intervalul dorit
    if ($ora_rezerv < $interval_start || $ora_rezerv > $interval_end) {
        // Dacă ora nu este în interval, afișează un mesaj de eroare
        echo "<p>Rezervările se pot face doar între orele $interval_start și $interval_end.</p>";
        exit();
    }

    // Pregătește interogarea pentru inserare în baza de date
    $stmt = $conn->prepare("INSERT INTO RezervariRestaurant (nume, email, nr_pers, data_rezerv, ora_rezerv, comentarii) VALUES (?, ?, ?, ?, ?, ?)");
    if (!$stmt) {
        die("Eroare la pregătirea interogării: " . $conn->error);
    }

    // Leagă parametrii interogării
    $stmt->bind_param("ssisss", $nume, $email, $nr_pers, $data_rezerv, $ora_rezerv, $comentarii);

    // Execută interogarea
    if ($stmt->execute()) {
        // Închide conexiunea la baza de date
        $stmt->close();
        $conn->close();
    
        // Afișează pagina de succes
        echo 
        '<!DOCTYPE html>
        <html lang="ro">
        <head>
            <meta charset="UTF-8">
            <meta name="viewport" content="width=device-width, initial-scale=1.0">
            <title>Rezervare Confirmată</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    background-image: url(background.jpg);
                    background-size: cover;
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    height: 100vh;
                    margin: 0;
                }
                .container {
                    background: white;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    text-align: center;
                    max-width: 400px;
                    width: 100%;
                }
                h2 {
                    color: #28a745;
                }
                p {
                    font-size: 16px;
                    color: #333;
                }
                .btn {
                    display: inline-block;
                    margin-top: 15px;
                    padding: 10px 20px;
                    background-color:rgb(109, 109, 112);
                    color: white;
                    text-decoration: none;
                    border-radius: 5px;
                    font-size: 16px;
                }
                .btn:hover {
                    background-color:rgb(85, 136, 187);
                }
            </style>
        </head>
        <body>
    
        <div class="container">
            <h2>Rezervare Confirmată!</h2>
            <p>Rezervarea ta a fost înregistrată cu succes.</p>
            <p>Te așteptăm cu drag!</p>
            <a href="site.html" class="btn">Mergi la pagina principală</a>
        </div>
    
        </body>
        </html>';
        exit(); // Oprește execuția scriptului
    } else {
        die("Eroare la inserarea rezervării: " . $stmt->error);
    }
}
