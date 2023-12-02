using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimator : MonoBehaviour
{
    public static int flip; // Contador de volteos de carta estático
    Animator mechanim; // Referencia al animator del objeto
    CardRenderer card; // Referencia al CardRenderer del objeto

    private void Awake()
    {
        flip = 0; // Inicializa el contador de volteos a 0
        card = GetComponentInChildren<CardRenderer>(); // Obtiene el CardRenderer del objeto
        mechanim = GetComponent<Animator>(); // Obtiene el Animator del objeto
    }

    private void OnMouseDown()
    {
        if (Time.timeScale == 1) // Si el tiempo no está pausado
        {
            if (flip < 2) // Si no se han volteado dos cartas ya
            {
                flip++; // Incrementa el contador de volteos
                mechanim.SetTrigger("Flip"); // Activa la animación "Flip" en el Animator
                StartCoroutine(Flip(mechanim.GetCurrentAnimatorStateInfo(0).length)); // Espera a que termine la animación antes de activar la carta
                if (GameManager.card1 == null) // Si no hay una carta volteada
                {
                    GameManager.card1 = card; // Establece la carta como la primera volteada
                }
                else if (GameManager.card2 == null && GameManager.card1 != card) // Si ya hay una carta volteada y no es la misma que se acaba de voltear
                {
                    GameManager.card2 = card; // Establece la carta como la segunda volteada
                }
            }
        }
    }

    IEnumerator Flip(float duration)
    {
        yield return new WaitForSeconds(duration); // Espera a que termine la animación
        mechanim.ResetTrigger("Flip"); // Reinicia el trigger de la animación
        card.isFlipped = true; // Activa la variable que indica que la carta está volteada
    }
}
