using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AI : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject[] bottomPos;
    public GameObject[] topPos;

    public static string[] type = new string[] { "C", "H", "D", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<List<string>> bottoms; // Changed to List<List<string>> for individual bottom lists
    public List<string> tops;

    public List<string> deck;

    private void Start()
    {
        bottoms = new List<List<string>> { new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>(), new List<string>() };
        StartGame();
    }

    public void StartGame()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        // test
        foreach (string card in deck)
        {
            print(card);
        }
        SolitaireSort();
        Deal();
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

    // Fisher-Yates shuffle algorithm
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
        for (int i = 0; i < 7; i++)
        {
            // offsetas tam, kad patikrint ar visos kortos neatsirastu vienoje vietoje
            float yOffset = 0;
            foreach (string card in bottoms[i])
            {
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z), Quaternion.identity, bottomPos[i].transform);
                
                newCard.name = card;
                if (card == bottoms[i].Last<string>())
                {
                    newCard.GetComponent<CanSelect>().faceUp = true;
                }
                yOffset += 0.4f;
            }
        }
    }

    void SolitaireSort()
    {
        print(bottoms);
        print(deck);

        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }
}