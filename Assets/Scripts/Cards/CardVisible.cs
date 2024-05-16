using UnityEngine;

public class CardVisible : MonoBehaviour
{
    private void OnMouseUp()
    {
        Hand handManager = FindObjectOfType<Hand>();
        bool anyoneVisible = false;
        for (int i = 0; i < handManager.cardsInHand.Count; i++)
        {
            CardMovimentation dragCard = handManager.cardsInHand[i].GetComponent<CardMovimentation>();
            if (dragCard.wasVisible)
            {
                dragCard.wasVisible = false;
                anyoneVisible = true;
                Destroy(dragCard.objectCardVisible);
            }
        }

        if (anyoneVisible)
        {
            handManager.cardVisibleBG.SetActive(false);
        }
    }
}
