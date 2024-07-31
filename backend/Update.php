<?php
header ('Content-Type: application/json');
include "../backend/db.php";
$id = $_POST['id'];
$nom = $_POST['nom'];
$prenom = $_POST['prenom'];
$age = (int) $_POST['age'];

$stmt = $db->prepare("UPDATE utilisateur SET nom = ?, prenom = ?, age = ? WHERE id = ?");
$result = $stmt->execute([$nom, $prenom, $age, $id]);
echo json_encode([
    'success'=>$result
    ]);
?>