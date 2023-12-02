using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField]
    GameObject canvasPause;  // Men� de pausa
    [SerializeField]
    GameObject canvasSure;   // Men� que te pregunta "Seguro? S�/No"
    public static bool hasWon;
    string sceneName = "";   // Para introducir el nombre de la escena

    void Start()
    {
        hasWon = false;
        canvasPause.SetActive(false);  // Inicializa el canvas para que sea invisible
        canvasSure.SetActive(false);   // Inicializa el canvas para que sea invisible
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))  // Si se pulsa la tecla escape, se activa el men� de pausa y se para todo el juego
        {
            Pause();  // Funci�n para pausar
        }
        if (hasWon)
        {
            SceneManager.LoadScene("Summary");  // Si el jugador ha ganado, carga la escena "Summary"
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;  // Se detiene el tiempo
        if (sceneName == "Reset")
        {
            Continue();
        }
        else
        {
            canvasPause.SetActive(true);   // Muestra el canvas de pausa
            canvasSure.SetActive(false);   // Esconde el canvas de "Seguro?"
        }
    }

    public void Continue()
    {
        canvasPause.SetActive(false);   // Esconde el canvas de pausa
        canvasSure.SetActive(false);    // Esconde el canvas de "Seguro?"
        sceneName = "";
        Time.timeScale = 1;   // Vuelve a correr el tiempo
    }

    public void Sure(string name)
    {
        Time.timeScale = 0;   // Detiene el tiempo
        sceneName = name;     // Introduce el nombre de la escena
                              // (si pulsas el bot�n "Men� principal" es "MainMenu"
                              // y si le das a "Salir" cierra el programa)
        canvasSure.SetActive(true);   // Muestra el canvas de "Seguro?"
        canvasPause.SetActive(false); // Esconde el canvas de pausa
    }

    /* Los he tenido que hacer separados porque el "Seguro" se lo aplico a los botones "Men� principal" y "Salir",
       y el CargarEscena a los botones "S�" y "No" */
    public void LoadScene()
    {
        Time.timeScale = 1;
        if (sceneName == "Reset")
        {
            CardRenderer.numberPairs.Clear();  // Limpia el diccionario de pares de cartas
            for (int i = 0; i < CardRenderer.completePairs.Length; i++)
            {
                CardRenderer.completePairs[i] = false;  // Reinicia la lista de pares completados
                CardRenderer.pairs[i] = null;           // Reinicia la lista de pares
                CardRenderer.instantiatedPairs[i] = 0;  // Reinicia la lista de pares instanciados
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Recarga la escena actual
        }
        if (sceneName == "MainMenu")
        {
            SceneManager.LoadScene("Title");//Si recibe "MainMenu", vuelve al t�tulo
        }
        if (sceneName == "Quit")
        {
            Close();//Si recibe "Quit", se sale
        }
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Reinicia la escena activa actualmente
    }

    public void Close()
    {
        Application.Quit(); // cierra la aplicaci�n
    }
}