using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour
{
    // Prefab de la carta
    [SerializeField]
    GameObject prefabCard;

    // Número de filas y columnas de cartas que se van a crear
    [SerializeField]
    int filas;
    [SerializeField]
    int columnas;

    // Separación horizontal y vertical entre cartas
    [SerializeField]
    float offset_x;
    [SerializeField]
    float offset_y;

    private void Awake()
    {
        // Bucle anidado para crear las cartas en filas y columnas
        for (int i = 0; i < columnas; i++)
        {
            for (int j = 0; j < filas; j++)
            {
                // Se instancia una nueva carta a partir del prefab
                GameObject clon = Instantiate(prefabCard);

                // Se le da una posición en función de la separación entre cartas y la posición del objeto que tiene el script
                clon.transform.position = transform.position + new Vector3(i * offset_x, -j * offset_y, 0);
            }
        }
    }
}
