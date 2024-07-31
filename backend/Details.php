<?php 
header('Content-Type: application/json');
include "../backend/db.php";

$id = (int)$_POST['id'];
$stmt = $db->prepare("SELECT nom, prenom, age FROM utilisateur WHERE ID = ?");
$stmt->execute([$id]);
$result = $stmt->fetch(PD0:: FETCH_ASSOC);

echo json_encode([
    'success'=>$result
    ]);
?>