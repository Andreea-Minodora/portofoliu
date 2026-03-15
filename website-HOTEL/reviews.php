<?php
// Conectare la baza de date
$servername = "localhost";
$username = "root";
$password = "";
$dbname = "hotelrezervari";

$conn = new mysqli($servername, $username, $password, $dbname);

// Verifică dacă conexiunea a reușit
if ($conn->connect_error) {
    die("Eroare la conectarea la baza de date: " . $conn->connect_error);
}

// Preia recenziile din baza de date
$sql = "SELECT nota, comentariu, data_postare FROM reviews ORDER BY data_postare DESC";
$result = $conn->query($sql);
?>

<!DOCTYPE html>
<html lang="ro">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Recenzii Hotel</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-image: url(background.jpg);
            background-size: cover;
            display: flex;
            justify-content: center;
            align-items: flex-start;
            flex-direction: column;
            margin: 0;
            height: 100vh;
            padding: 20px;
        }

        h2, h3 {
            color: grey;

            text-align: center;
        }

        .content-wrapper {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            width: 100%;
            max-width: 800px; /* Limitează lățimea pentru un aspect mai curat */
        }

        .form-container, .review-container {
            width: 100%;
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            margin-bottom: 20px;
        }

        .form-container {
            margin-top: 30px;
        }

        .review {
            border-bottom: 1px solid #ddd;
            padding: 10px;
        }

        .review:last-child {
            border-bottom: none;
        }

        .rating {
            color: #f39c12;
            font-size: 18px;
        }

        .date {
            font-size: 12px;
            color: gray;
        }

        form {
            display: flex;
            flex-direction: column;
            align-items: flex-start;
        }

        input, textarea, select, button {
            width: 100%;
            padding: 10px;
            margin: 10px 0;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        button {
            background-color: #28a745;
            color: white;
            font-size: 16px;
        }

        button:hover {
            background-color: #218838;
        }

        body {
    font-family: Arial, sans-serif;
    background-color: #f8f9fa;
    display: flex;
    justify-content: center;
    align-items: center;  /* Adăugat pentru a centra pe verticală */
    flex-direction: column;
    margin: 0;
    height: 100vh;
    padding: 20px;
}

.content-wrapper {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center; /* Adăugat pentru a centra pe orizontală */
    width: 100%;
    max-width: 800px; /* Limitează lățimea pentru un aspect mai curat */
}

.form-container, .review-container {
    width: 100%;
    background: white;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    margin-bottom: 20px;
}

.form-container {
    margin-top: 30px;
}

.review {
    border-bottom: 1px solid #ddd;
    padding: 10px;
}

.review:last-child {
    border-bottom: none;
}

.rating {
    color: #f39c12;
    font-size: 18px;
}

.date {
    font-size: 12px;
    color: gray;
}

form {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}

input, textarea, select, button {
    width: 100%;
    padding: 10px;
    margin: 10px 0;
    border: 1px solid #ccc;
    border-radius: 5px;
}

button {
    background-color: #28a745;
    color: white;
    font-size: 16px;
}

button:hover {
    background-color: #218838;
}

textarea {
    resize: vertical;
}

.review-container {
    margin-top: 20px;
    justify-content: center;
    align-items: center;
}

.review p {
    margin: 5px 0;
}

.button-home {
    position: absolute;
    top: 20px;
    right: 20px;
    background-color:rgb(66, 67, 68);
    color: white;
    font-size: 16px;
    padding: 10px 20px;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    text-decoration: none;
}

.button-home:hover {
    background-color: #0056b3;
}

    </style>
</head>
<body>

<div class="content-wrapper">
    <h2>Recenziile Clienților</h2>

    <!-- Formularul pentru adăugarea recenziilor -->
    <div class="form-container">
        <h3>Adaugă o recenzie:</h3>
        <form action="submit_review.php" method="POST">
            <label for="rating">Nota:</label>
            <select name="rating" required>
            <option value="5">⭐️⭐️⭐️⭐️⭐️ - Excelent</option>
            <option value="4">⭐️⭐️⭐️⭐️ - Foarte bun</option>
            <option value="3">⭐️⭐️⭐️ - Bun</option>
            <option value="2">⭐️⭐️ - Acceptabil</option>
            <option value="1">⭐️ - Slab</option>
            </select>
            
            <label for="comment">Comentariu:</label>
            <textarea name="comment" rows="4" required></textarea>
            
            <button type="submit">Trimite</button>
        </form>
    </div>

    <!-- Secțiunea pentru recenzii -->
    <div class="review-container">
        <h3>Recenzii Adăugate:</h3>
        <?php
        if ($result->num_rows > 0) {
            while ($row = $result->fetch_assoc()) {
                // Formatează data
                $formattedDate = date("d-m-Y H:i:s", strtotime($row["data_postare"]));
                echo '<div class="review">';
                echo '<p class="rating">Nota: ' . str_repeat("⭐", $row["nota"]) . '</p>';
                echo '<p>"' . htmlspecialchars($row["comentariu"]) . '"</p>';
                echo '<p class="date">Publicat pe: ' . $formattedDate . '</p>';
                echo '</div>';
            }
        } else {
            echo "<p>Nu există recenzii încă.</p>";
        }
        ?>
    </div>
</div>

<?php
// Închide conexiunea la baza de date
$conn->close();
?>


<a href="site.html" class="button-home">Pagina Principală</a>

</body>
</html>
