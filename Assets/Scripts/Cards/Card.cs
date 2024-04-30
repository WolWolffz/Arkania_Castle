using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardScript{
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public CardType cardType;
    public string nameCard;
    public int manaCost;
    public int health;
    public int damage;

    public string descriptionCard;

    public CardEffect effect;

    public GameObject spriteCard;

    public enum CardEffect{
        NoEffect,
        Healer,
        Damage
    }
    public enum CardType{
        Troop,
        Artefact,
        Speel
    }
}
}