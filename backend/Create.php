<?php
header ('Content-Type: application/json');
include "../backend/db.php";
$nom = $_POST['nom'];
$prenom = $_POST['prenom'];
$age = (int) $_POST['age'];
$stmt = $db->prepare("INSERT INTO utilisateur (nom, prenom, age) VALUES (?, ?, ?)");
$result = $stmt->execute( [$nom, $prenom, $age]);
echo json_encode([
'success'=>$result
]);
?>