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
    public List<string>[] bottoms;
    public List<string>[] tops;

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();


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
        SolitaireSort();
        Deal();
    }
    private void Start()
    {
        List<string> bottoms = new List<string> { "bottom0", "bottom1", "bottom2", "bottom3", "bottom4", "bottom5", "bottom6" };
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
        for (int i = 0; i < 7; i++)
        { 


        // offsetas tam, kad patikrint ar visos kortos neatsirastu vienoje vietoje
        float yOffset = 0;
            foreach (string card in bottoms[i])
            {
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z), Quaternion.identity, bottomPos[i].transform);
                newCard.name = card;
                if (card == bottoms[i][bottoms[i].Count-1])
                {
                newCard.GetComponent<CanSelect>().faceUp = true;
                }
                yOffset = yOffset + 0.4f;
            }
        }
    }
    // funkcija kad isdalinti kortas
    void SolitaireSort()
    {
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
