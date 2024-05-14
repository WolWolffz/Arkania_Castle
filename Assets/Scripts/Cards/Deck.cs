using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardScript;
public class Deck : MonoBehaviour
{
    public List<Card> allCards = new List<Card>();
    private int currentIndex = 0;


    void Start(){
    Card[] cards = Resources.LoadAll<Card>("Cards");
    allCards.AddRange(cards);
    // Hand handManager = FindObjectOfType<Hand>();
    // for (int i = 0; i < 4; i++ ){
    //     DrawCard(handManager);
    // }
}
    public void DrawCard(Hand handManager){
        if (allCards.Count == 0){
            return;
        }
        Card nextCard = allCards[currentIndex];
        handManager.AddCardToHand(nextCard);
        currentIndex = (currentIndex+1) % allCards.Count; 

    }

}
