using UnityEngine;

public class CardMovimentation : MonoBehaviour
{
    private bool isDragging = false;
    private bool wasClicked = false;
    public GameObject objectCardVisible;
    public bool wasVisible = false;
    private float clickTime;
    private float clickDurationThreshold = 0.35f;
    private Vector3 offset;
    private Hand handManager;
    public Vector3 enlargedScale = new Vector3(1.5f, 1.5f, 1.5f);


    private void Start()
    {
        handManager = FindObjectOfType<Hand>();

        if (!handManager.cardVisibleBG)
        {
            GameObject backgroundBGPrefab = Resources.Load<GameObject>("CardVisibleBG");

            GameObject backgroundBG = Instantiate(backgroundBGPrefab);
            handManager.SetSortingLayerRecursiveForChildren(backgroundBG.transform, "CardVisibleBG");
            handManager.cardVisibleBG = backgroundBG;
            handManager.cardVisibleBG.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        wasClicked = true;
        clickTime = Time.time;
    }

    private void OnMouseDrag()
    {
        bool anyoneVisible = false;
        for (int i = 0; i < handManager.cardsInHand.Count; i++)
        {
            CardMovimentation dragCard = handManager.cardsInHand[i].GetComponent<CardMovimentation>();
            if (dragCard.wasVisible)
            {
                anyoneVisible = true;
            }
        }
        if (isDragging && !anyoneVisible)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }

    }

    private void OnMouseUp()
    {
        isDragging = false;

        if (Time.time - clickTime > clickDurationThreshold)
        {
            wasClicked = false;
        }

        if (wasClicked)
        {
            bool anyoneVisible = false;
            for (int i = 0; i < handManager.cardsInHand.Count; i++)
            {
                CardMovimentation dragCard = handManager.cardsInHand[i].GetComponent<CardMovimentation>();
                if (dragCard.wasVisible)
                {
                    anyoneVisible = true;
                }
            }
            if (!anyoneVisible)
            {
                objectCardVisible = Instantiate(gameObject, Vector3.zero, Quaternion.identity);
                objectCardVisible.transform.localScale = enlargedScale;
                objectCardVisible.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 10));
                Vector3 newPosition = new Vector3(objectCardVisible.transform.position.x, objectCardVisible.transform.position.y, -1);
                objectCardVisible.transform.position = newPosition;
                handManager.SetSortingLayerRecursiveForChildren(objectCardVisible.transform, "CardVisible");
                for (int i = 0; i < handManager.cardsInHand.Count; i++)
                {
                    CardMovimentation dragCard = handManager.cardsInHand[i].GetComponent<CardMovimentation>();
                    dragCard.wasVisible = true;
                }

                handManager.cardVisibleBG.SetActive(true);
            }
            else
            {
                for (int i = 0; i < handManager.cardsInHand.Count; i++)
                {
                    CardMovimentation dragCard = handManager.cardsInHand[i].GetComponent<CardMovimentation>();
                    Destroy(dragCard.objectCardVisible);
                    dragCard.wasVisible = false;
                }
                handManager.cardVisibleBG.SetActive(false);
            }
        }
        else
        {
            Hand handManager = FindObjectOfType<Hand>();

            GameObject[] limitEvokeObjects = GameObject.FindGameObjectsWithTag("LimitEvoke");
            Debug.Log(limitEvokeObjects[0].transform.position.y);
            Debug.Log(transform.position.y);
            if (limitEvokeObjects.Length > 0)
            {
                Transform limitEvokeTransform = limitEvokeObjects[0].transform;

                if (limitEvokeTransform.position.y <= transform.position.y)
                {
                    int index = handManager.cardsInHand.IndexOf(gameObject);
                    if (index != -1)
                    {
                        handManager.cardsInHand.RemoveAt(index);
                    }

                    Destroy(gameObject);
                }
            }
        }

        handManager.UpdateHandVisual();
        wasClicked = false;
    }
}