using UnityEngine;
using CardScript;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card cardData;
    public GameObject cardPrefab;
    public Sprite cardImage;
    public TMP_Text nameText;
    public TMP_Text healthText;
    public TMP_Text damageText;

    public TMP_Text manaText;

    public TMP_Text descriptionText;


    // void Start()
    // {
    //     UpdateCardDisplay();
    // }

    public void UpdateCardDisplay()
    {
        nameText.text = cardData.nameCard;

        Allie alliePrefab = cardPrefab.GetComponent<Allie>();
        if (!(alliePrefab == null))
        {
            healthText.text = alliePrefab.life.ToString();
            manaText.text = alliePrefab.manaCost.ToString();
            damageText.text = alliePrefab.damage.ToString();
        }

        descriptionText.text = cardData.descriptionCard;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = cardData.spriteCard;

        if (cardData.cardType == Card.CardType.Speel)
        {
            healthText.text = null;
            damageText.text = null;
        }
        else if (cardData.cardType == Card.CardType.Artefact)
        {
            damageText.text = null;
        }



    }

}
