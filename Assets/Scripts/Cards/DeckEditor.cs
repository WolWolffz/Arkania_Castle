using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(Deck))]


    public class DeckEditor : Editor
    {
        public override void OnInspectorGUI(){
            DrawDefaultInspector();

            Deck deckManager = (Deck)target;
            if (GUILayout.Button("Draw New Card")){
                Hand handManager = FindObjectOfType<Hand>();
                if (handManager != null){
                    deckManager.DrawCard(handManager);
                }
            }
        }    
    }
#endif
