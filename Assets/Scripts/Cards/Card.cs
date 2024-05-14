
using UnityEngine;

namespace CardScript
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class Card : ScriptableObject
    {
        public CardType cardType;
        public string nameCard;
        public int manaCost;
        public int health;
        public int damage;

        public GameObject prefab;

        public string descriptionCard;

        public CardEffect effect;

        public Sprite spriteCard;

        public enum CardEffect
        {
            NoEffect,
            Healer,
            Damage
        }
        public enum CardType
        {
            Troop,
            Artefact,
            Speel
        }
    }
}