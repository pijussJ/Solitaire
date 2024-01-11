using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private CanSelect canSelect;
    private AI ai;

    private void Start()
    {
        List<string> deck = AI.GenerateDeck();
        ai = FindObjectOfType<AI>();

        int i = 0;
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                cardFace = ai.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        canSelect = GetComponent<CanSelect>();

    }
    private void Update()
    {
        if (canSelect.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
            transform.localScale = new Vector3(0.36f, 0.3892f, 0.3698f);
        }
        else
        {
            spriteRenderer.sprite = cardBack;
            transform.localScale = new Vector3(1.08f, 1.1678f, 1.1f);
        }
    }



}
