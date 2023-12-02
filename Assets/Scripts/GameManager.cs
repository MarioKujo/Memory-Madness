using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Button button;

    public static CardRenderer card1;
    Animator acard1;
    float anim1Length;
    public static CardRenderer card2;
    Animator acard2;
    float anim2Length;

    bool CoroutineInProgress = false;

    public static int score;
    public static int numberOfMoves;
    int contador;

    // Awake se llama antes de que se active el objeto, una sola vez
    private void Awake()
    {
        // Inicializar variables
        card1 = null;
        card2 = null;
        score = 0;
        numberOfMoves = 0;
        button.interactable = true; // El botón del menú está activo
    }

    // Update se llama una vez por cada frame
    private void Update()
    {
        // Si se han volteado 2 cartas, comprobar si son iguales o no
        if (CardAnimator.flip == 2)
        {
            // Contador es una variable auxiliar que permite contar una sola vez por cada par de cartas volteadas
            if (contador <= 0)
            {
                numberOfMoves++; // Incrementar el número de movimientos
                contador++; // Incrementar contador para evitar que se cuente varias veces
            }

            // Obtener el Animator de las dos cartas
            acard1 = card1.GetComponentInParent<Animator>();
            anim1Length = acard1.GetCurrentAnimatorStateInfo(0).length;
            acard2 = card2.GetComponentInParent<Animator>();
            anim2Length = acard2.GetCurrentAnimatorStateInfo(0).length;

            // Si las dos cartas son iguales, aumentar la puntuación y comprobar si se ha ganado
            if (card1.GetId() == card2.GetId())
            {
                score++;
                CardAnimator.flip = 0;
                card1 = null;
                card2 = null;

                // Si se ha ganado, desactivar el botón del menú y esperar a que termine la animación de la última carta para mostrar la pantalla de victoria
                if (score == 8)
                {
                    button.interactable = false;

                    if (anim1Length >= anim2Length)
                    {
                        CoroutineInProgress = true;
                        StartCoroutine(waitWin(anim1Length));
                    }
                    else
                    {
                        StartCoroutine(waitWin(anim2Length));
                        CoroutineInProgress = true;
                    }
                }
                // Si no se ha ganado, reiniciar contador para que se pueda contar otro par de cartas
                else
                {
                    contador = 0;
                }
            }
            // Si las dos cartas no son iguales, volver a voltearlas después de un tiempo
            else
            {
                if (!CoroutineInProgress)
                {
                    if (anim1Length >= anim2Length)
                    {
                        CoroutineInProgress = true;
                        StartCoroutine(Unflip(anim1Length));
                    }
                    else
                    {
                        StartCoroutine(Unflip(anim2Length));
                        CoroutineInProgress = true;
                    }
                }
            }
        }
    }

    // Esperar un tiempo antes de mostrar la pantalla de victoria
    IEnumerator waitWin(float duration)
    {
        yield return new WaitForSeconds(duration);
        CanvasController.hasWon = true;
    }

    // Volver a voltear las dos cartas que no son iguales
    IEnumerator Unflip(float duration)
    {
        yield return new WaitForSeconds(duration); // Espera la duración de la animación antes de desvoltear las cartas
        acard1.SetTrigger("Unflip"); // Desvoltea la carta 1
        acard2.SetTrigger("Unflip"); // Desvoltea la carta 2
        anim1Length = acard1.GetCurrentAnimatorStateInfo(0).length; // Obtiene la duración de la animación de la carta 1
        anim2Length = acard2.GetCurrentAnimatorStateInfo(0).length; // Obtiene la duración de la animación de la carta 2
        if (anim1Length >= anim2Length) // Si la duración de la animación de la carta 1 es mayor o igual a la de la carta 2
        {
            yield return new WaitForSeconds(duration); // Espera la duración de la animación de la carta 1 antes de continuar
        }
        else // Si la duración de la animación de la carta 2 es mayor a la de la carta 1
        {
            yield return new WaitForSeconds(duration); // Espera la duración de la animación de la carta 2 antes de continuar
        }
        if (card1 != null) // Si la carta 1 no es nula
        {
            card1.isFlipped = false; // Desmarca la carta 1 como volteada
        }
        if (card2 != null) // Si la carta 2 no es nula
        {
            card2.isFlipped = false; // Desmarca la carta 2 como volteada
        }
        card1 = null; // Marca la carta 1 como nula
        card2 = null; // Marca la carta 2 como nula
        CardAnimator.flip = 0; // Marca la variable de flip como 0 para indicar que no hay cartas volteadas
        CoroutineInProgress = false; // Marca la variable de coroutineInProgress como falsa para indicar que la coroutine ha terminado
        contador = 0; // Reinicia el contador de cartas volteadas
    }
}