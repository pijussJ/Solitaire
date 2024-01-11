using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public static string[] type = new string[] { "C", "H", "D", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    public List<string> deck;
    public void StartGame()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        // test
        foreach (string card in deck)
        {
            print(card);
        }
        Deal();
    }
    private void Start()
    {
        StartGame();
    }
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string t in type)
        {
            foreach (string v in values)
            {
                newDeck.Add(t + v);
            }
        }
        return newDeck;
    }

    // Kadangi nezinojau kaip shufflint kortas radau Fisher-Yates algoritma, kaip tai padaryti. 
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void Deal()
    {
        // offsetas tam, kad patikrint ar visos kortos neatsirastu vienoje vietoje
        float yOffset = 0;
        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab,new Vector3(transform.position.x,transform.position.y - yOffset, transform.position.z), Quaternion.identity);
            newCard.name = card;
            newCard.GetComponent<CanSelect>().faceUp = true;

            yOffset = yOffset + 0.4f;
        }
    }
}
