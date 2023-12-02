using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRenderer : MonoBehaviour
{
    [SerializeField]
    Sprite[] prefabCard; // Array de sprites que contendrá las imágenes de las cartas
    SpriteRenderer sr; // Componente SpriteRenderer de la carta
    public static Sprite[] pairs = new Sprite[8]; // Array estático que contendrá las parejas de cartas
    public static List<int> numberPairs = new List<int>(); // Lista que contendrá los números de las imágenes de las cartas
    public static int[] instantiatedPairs = new int[8]; // Array que contendrá el número de veces que se ha instanciado una pareja de cartas
    int id; // Número de identificación de la carta
    public bool isFlipped = false;
    public static bool[] completePairs = new bool[8]; // Array estático que indica si se han completado las parejas de cartas
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>(); // Se obtiene el componente SpriteRenderer de la carta
        // Si la lista numberPairs tiene menos elementos que el array de parejas de cartas,
        // se rellena la lista con números aleatorios que corresponden a las imágenes de las cartas
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
        // Se elige un número aleatorio que correspondrá a una pareja de cartas
        int ran = Random.Range(0, pairs.Length);
        // Si ya se han instanciado dos cartas de esa pareja, se vuelve a elegir un número aleatorio
        while (instantiatedPairs[ran] == 2 && !AllPairsAreInstantiatedTwice())
        {
            ran = Random.Range(0, pairs.Length);
        }
        // Se asigna el número de identificación de la carta
        id = ran;
        // Se asigna el sprite correspondiente a la carta
        sr.sprite = pairs[ran];
        // Se aumenta el contador de instanciación de esa pareja de cartas
        instantiatedPairs[ran]++;
    }
    //Función que devuelve si todas las cartas ya han sido instanciadas 2 veces
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
    // Función que devuelve el número de identificación de la carta
    public int GetId()
    {
        return id;
    }
}
