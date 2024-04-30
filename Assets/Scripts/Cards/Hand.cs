using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardScript;
using TMPro;

public class Hand : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handTransform;
    public float fanSpread = 5f;
    public float cardSpacing = 1.5f;
    public List<GameObject> cardsInHand = new List<GameObject>();

    void SetSortingLayerRecursiveForChildren(Transform parent, string layerName)
    {
        // Verifica se o GameObject pai tem um SpriteRenderer
        SpriteRenderer spriteRenderer = parent.GetComponent<SpriteRenderer>();
        MeshRenderer meshRenderer = parent.GetComponent<MeshRenderer>();
        if (spriteRenderer != null)
        {
            // Define a Sorting Layer do SpriteRenderer
            spriteRenderer.sortingLayerName = layerName;
        }
        if (meshRenderer != null){
            meshRenderer.sortingLayerName = layerName;
        }

        // Percorre todos os filhos do GameObject pai recursivamente
        foreach (Transform child in parent)
        {
            // Chama esta função novamente para cada filho
            SetSortingLayerRecursiveForChildren(child, layerName);
        }
    }

    public void UpdateHandVisual()
    {
        int cardCount = cardsInHand.Count;
        for (int i = 0; i < cardCount; i++)
        {
            float rotationAngle = (-fanSpread * (i - (cardCount - 1) / 2f));
            cardsInHand[i].transform.localRotation = Quaternion.Euler(0f, 0f, rotationAngle);

            float horizontalOffset = i * cardSpacing;
            cardsInHand[i].transform.localPosition = new Vector3(horizontalOffset, 0f, 0f);

            int cardNumber = i+1;
            SetSortingLayerRecursiveForChildren(cardsInHand[i].transform, "Card " + cardNumber.ToString());
        }
    }

    public void AddCardToHand()
    {
        GameObject newCard = Instantiate(cardPrefab, handTransform.position, Quaternion.identity, handTransform);
        cardsInHand.Add(newCard);
        UpdateHandVisual();
    }

    void Start()
    {
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
        AddCardToHand();
    }

    void Update()
    {
        UpdateHandVisual();
    }
}
