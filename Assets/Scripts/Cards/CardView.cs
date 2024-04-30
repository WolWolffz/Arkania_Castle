using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardScript;
using UnityEngine.UI;
using TMPro;

public class CardView : MonoBehaviour
{
    public Card cardData;
    public GameObject cardPrefab;

    public Texture2D cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;

    public TMP_Text manaText;

    public TMP_Text descriptionText;

    void Start()
    {
        UpdateCardDisplay();
    }

    public void UpdateCardDisplay()
    {
        nameText.text = cardData.nameCard;
        healthText.text = cardData.health.ToString();
        manaText.text = cardData.manaCost.ToString();
        damageText.text = cardData.damage.ToString();
        descriptionText.text = cardData.descriptionCard;
        if (cardData.cardType == Card.CardType.Troop)
        {
            cardImage.GetComponent<SpriteRenderer>().sprite = cardData.spriteCard.GetComponent<SpriteRenderer>().sprite;
        }
        else if (cardData.cardType == Card.CardType.Artefact)
        {

        }
        else if (cardData.cardType == Card.CardType.Speel)
        {

        }



    }

}
