using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardScript;
public class Deck : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    private int currentIndex = 0;

    int limitCards = 3;

    GameObject hand;
    int cardsInHand;


    void Start(){
    Card[] cards = Resources.LoadAll<Card>("Cards");
    allCards.AddRange(cards);
    hand = GameObject.FindGameObjectWithTag("HandPosition");
    cardsInHand = hand.transform.childCount;
}   

    public void CallFillHand(){
        cardsInHand = hand.transform.childCount;
        InvokeRepeating("FillHand", 0f, 0.25f);
    }
    public void FillHand()
    {
        if (cardsInHand < limitCards)
        {
            DrawCard();
            limitCards -= 1;
        }
        else
        {
            limitCards = 3;

            CancelInvoke("FillHand");
        }

    }

public void DrawCard()
    {
        Hand handManager = FindObjectOfType<Hand>();
        if (allCards.Count == 0)
        {
            return;
        }

        int randomIndex = Random.Range(0, allCards.Count);
        
        Card nextCard = allCards[randomIndex];
        
        handManager.AddCardToHand(nextCard);
        
        currentIndex = randomIndex;
    }

}
