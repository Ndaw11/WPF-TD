<?php
header('Content-Type: application/json');
include "../backend/db.php";

$id = (int) $_POST['id'];
$stmt = $db->prepare("DELETE FROM utilisateur WHERE ID = ?");
$result = $stmt->execute([$id]);

echo json_encode([
    'id' => $id,
    'success'=>$result
    ]);
?>