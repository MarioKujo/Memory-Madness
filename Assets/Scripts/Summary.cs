using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Summary : MonoBehaviour
{
    [SerializeField]
    GameObject canvasSure; // Referencia al canvas que pregunta si estás seguro
    [SerializeField]
    GameObject canvasWin; // Referencia al canvas que muestra el resumen de la partida
    [SerializeField]
    TextMeshProUGUI scoreText; // Referencia al texto que muestra la puntuación
    [SerializeField]
    TextMeshProUGUI movesText; // Referencia al texto que muestra el número de movimientos
    string sceneName = ""; // Nombre de la escena (se inicializa a una cadena vacía)

    void Start()
    {
        // Actualiza los textos del resumen de la partida con la puntuación y número de movimientos
        scoreText.text = "Score: " + GameManager.score;
        movesText.text = "Moves: " + GameManager.numberOfMoves;

        // Muestra el canvas del resumen de la partida y oculta el canvas de "Seguro?"
        canvasWin.SetActive(true);
        canvasSure.SetActive(false);
    }

    // Método que se llama cuando se pulsa el botón "No"
    public void GoBack()
    {
        // Oculta el canvas de "Seguro?"
        canvasSure.SetActive(false);

        // Muestra el canvas del resumen de la partida
        canvasWin.SetActive(true);
    }

    // Método que se llama cuando se pulsa el botón "Menú principal" o "Salir"
    public void Sure(string name)
    {
        // Guarda el nombre de la escena ("MainMenu" o "Quit")
        sceneName = name;

        // Oculta el canvas del resumen de la partida
        canvasWin.SetActive(false);

        // Muestra el canvas de "Seguro?"
        canvasSure.SetActive(true);
    }

    // Método que se llama cuando se pulsa el botón "Jugar de nuevo"
    public void PlayAgain()
    {
        CardRenderer.numberPairs.Clear();  // Limpia el diccionario de pares de cartas
        for (int i = 0; i < CardRenderer.completePairs.Length; i++)
        {
            CardRenderer.completePairs[i] = false;  // Reinicia la lista de pares completados
            CardRenderer.pairs[i] = null;           // Reinicia la lista de pares
            CardRenderer.instantiatedPairs[i] = 0;  // Reinicia la lista de pares instanciados
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recarga la escena actual
        // Carga la escena del nivel de nuevo
        SceneManager.LoadScene("Level");
    }

    // Método que se llama cuando se pulsa el botón "Sí"
    public void LoadScene()
    {
        // Si se pulsó el botón "Menú principal"
        if (sceneName == "MainMenu")
        {
            // Reinicia la lista de parejas de cartas
            CardRenderer.numberPairs.Clear();

            // Reinicia las listas que guardan las parejas de cartas completadas
            for (int i = 0; i < CardRenderer.completePairs.Length; i++)
            {
                CardRenderer.completePairs[i] = false;
                CardRenderer.pairs[i] = null;
                CardRenderer.instantiatedPairs[i] = 0;
            }

            // Carga la escena del título
            SceneManager.LoadScene("Title");
        }

        // Si se pulsó el botón "Salir"
        if (sceneName == "Quit")
        {
            // Cierra la aplicación
            Application.Quit();
        }
    }
}
