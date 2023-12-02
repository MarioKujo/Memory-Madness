using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRenderer : MonoBehaviour
{
    [SerializeField]
    Sprite[] prefabCard; // Array de sprites que contendr� las im�genes de las cartas
    SpriteRenderer sr; // Componente SpriteRenderer de la carta
    public static Sprite[] pairs = new Sprite[8]; // Array est�tico que contendr� las parejas de cartas
    public static List<int> numberPairs = new List<int>(); // Lista que contendr� los n�meros de las im�genes de las cartas
    public static int[] instantiatedPairs = new int[8]; // Array que contendr� el n�mero de veces que se ha instanciado una pareja de cartas
    int id; // N�mero de identificaci�n de la carta
    public bool isFlipped = false;
    public static bool[] completePairs = new bool[8]; // Array est�tico que indica si se han completado las parejas de cartas
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); // Se obtiene el componente SpriteRenderer de la carta
        // Si la lista numberPairs tiene menos elementos que el array de parejas de cartas,
        // se rellena la lista con n�meros aleatorios que corresponden a las im�genes de las cartas
        if (numberPairs.Count < prefabCard.Length)
        {
            int i = 0;
            while (numberPairs.Count < pairs.Length && i < pairs.Length)
            {
                int rng = Random.Range(0, prefabCard.Length);
                if (!numberPairs.Contains(rng))
                {
                    numberPairs.Add(rng);
                    pairs[i] = prefabCard[rng];
                    i++;
                }
            }
        }
        // Se elige un n�mero aleatorio que correspondr� a una pareja de cartas
        int ran = Random.Range(0, pairs.Length);
        // Si ya se han instanciado dos cartas de esa pareja, se vuelve a elegir un n�mero aleatorio
        while (instantiatedPairs[ran] == 2 && !AllPairsAreInstantiatedTwice())
        {
            ran = Random.Range(0, pairs.Length);
        }
        // Se asigna el n�mero de identificaci�n de la carta
        id = ran;
        // Se asigna el sprite correspondiente a la carta
        sr.sprite = pairs[ran];
        // Se aumenta el contador de instanciaci�n de esa pareja de cartas
        instantiatedPairs[ran]++;
    }
    //Funci�n que devuelve si todas las cartas ya han sido instanciadas 2 veces
    bool AllPairsAreInstantiatedTwice()
    {
        foreach (int i in instantiatedPairs)
        {
            if (i < 2)
            {
                return false;
            }
        }
        return true;
    }
    // Funci�n que devuelve el n�mero de identificaci�n de la carta
    public int GetId()
    {
        return id;
    }
}
