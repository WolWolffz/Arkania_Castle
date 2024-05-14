using System.Collections.Generic;
using UnityEngine;
using CardScript;

public class Hand : MonoBehaviour
{
    public Deck deckManager;
    public GameObject cardPrefab;
    public Transform handTransform;
    public float fanSpread = 5f;
    public float cardSpacing = 1.5f;

    public float verticalSpacing = 0.2f;
    public GameObject cardVisibleBG;
    public List<GameObject> cardsInHand = new List<GameObject>();

    public void SetSortingLayerRecursiveForChildren(Transform parent, string layerName)
    {
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        MeshRenderer meshRenderer = parent.GetComponent<MeshRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = layerName;
        }
        if (meshRenderer != null)
        {
            meshRenderer.sortingLayerName = layerName;
        }

        foreach (Transform child in parent)
        {
            SetSortingLayerRecursiveForChildren(child, layerName);
        }
    }

    public void UpdateHandVisual()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            cardsInHand[0].transform.localPosition = new Vector3(0f, 0f, 0f);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (-fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float normalizePosition = (2f * i / (cardCount - 1) - 1f);
            float horizontalOffset = (cardSpacing * (i - (cardCount - 1) / 2f));
            float verticalOffset = verticalSpacing * (1 - normalizePosition * normalizePosition);

            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0f);
            int cardNumber = i + 1;
            SetSortingLayerRecursiveForChildren(cardsInHand[i].transform, "Card " + cardNumber.ToString());
        }
    }

    public void AddCardToHand(Card cardData)
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);

        newCard.GetComponent<CardDisplay>().cardData = cardData;
        UpdateHandVisual();
    }
}
