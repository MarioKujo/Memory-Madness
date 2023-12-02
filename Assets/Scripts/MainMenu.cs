using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script para manejar el menú principal del juego
public class MainMenu : MonoBehaviour
{
    //Referencias a los objetos del canvas para el título del menú y la confirmación de salida
    [SerializeField]
    GameObject canvasTitle;
    [SerializeField]
    GameObject canvasSure;

    //Se llama al inicio del script
    private void Start()
    {
        //Se activa el canvas del título y se desactiva el canvas de la confirmación
        canvasTitle.SetActive(true);
        canvasSure.SetActive(false);
    }

    //Método para cargar una escena específica del juego
    public void CargarNivel(string SceneName)
    {
        SceneManager.LoadScene(SceneName);//Carga la escena que se introduzca en unity
    }

    //Método para mostrar el canvas de confirmación de salida
    public void Quit()
    {
        canvasTitle.SetActive(false);
        canvasSure.SetActive(true);
    }

    //Método para salir del juego
    public void Salir()
    {
        Application.Quit();
    }

    //Método para volver al canvas del título después de cancelar la salida del juego
    public void Volver()
    {
        canvasSure.SetActive(false);
        canvasTitle.SetActive(true);
    }
}
