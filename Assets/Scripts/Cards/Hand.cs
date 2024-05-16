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
        GameObject deck = GameObject.FindGameObjectWithTag("Deck");
        GameObject hand = GameObject.FindGameObjectWithTag("HandPosition");

        if (deck != null && hand != null)
        {
            // Instancia a carta na posição do deck
            GameObject newCard = Instantiate(cardPrefab, deck.transform.position, Quaternion.identity, handTransform);
            cardsInHand.Add(newCard);

            // Inicia a movimentação da carta para a posição da mão
            StartCoroutine(MoveCardToHand(newCard, hand.transform.position, cardData));
        }
    }

    // Função para mover a carta gradualmente para a posição da mão
    private System.Collections.IEnumerator MoveCardToHand(GameObject card, Vector3 handPosition, Card cardData)
    {
        float elapsedTime = 0;
        float moveDuration = 1.0f; // Duração do movimento (em segundos)

        Vector3 startingPosition = card.transform.position;

        while (elapsedTime < moveDuration)
        {
            // Calcula a posição intermediária usando Lerp
            card.transform.position = Vector3.Lerp(startingPosition, handPosition, elapsedTime / moveDuration);

            // Atualiza o tempo decorrido
            elapsedTime += Time.deltaTime;

            // Aguarda o próximo quadro
            yield return null;
        }

        // Garante que a carta esteja na posição final
        card.transform.position = handPosition;
        card.GetComponent<CardDisplay>().cardData = cardData;
        card.GetComponent<CardDisplay>().cardPrefab = cardData.prefab;
        card.GetComponent<CardDisplay>().UpdateCardDisplay();
        // card.GetComponent<SpriteRenderer>().sprite = cardData.spriteCard;

        // Atualiza a visualização da mão após a carta estar na posição correta
        UpdateHandVisual();
    }
}
