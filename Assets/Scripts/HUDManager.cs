// Este script se encarga de actualizar el marcador y el número de movimientos en la interfaz de usuario

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
[SerializeField]
TextMeshProUGUI scoreText; // TextMeshProUGUI para mostrar el marcador de la partida
[SerializeField]
TextMeshProUGUI numberOfMoves; // TextMeshProUGUI para mostrar el número de movimientos
// Actualiza el marcador y el número de movimientos en la interfaz de usuario
private void Update()
{
    scoreText.text = "Score: " + GameManager.score;
    numberOfMoves.text = "Moves: " + GameManager.numberOfMoves;
}
}





