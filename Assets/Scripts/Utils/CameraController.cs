using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public new Camera camera;
    // public float camx,camy;
    public float upperBound, lowerBound;

    private GameManager gameManager;
    private Vector3 dragOrigin;
    private bool sliding = false;
    private Vector3 slidePos;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        gameManager.cameraController = this;

        if (camera == null)
        {
            camera = Camera.main;
        }

        upperBound = gameManager.level.transform.localScale.y / 2 - gameManager.level.transform.localScale.y / 3;
        lowerBound = camera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliding)
            Slide();

        if (Input.GetMouseButtonDown(0))
            dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Hand handManager = FindObjectOfType<Hand>();
            bool anyoneDragging = false;
            for (int i = 0; i < handManager.cardsInHand.Count; i++)
            {
                CardMovimentation dragCard = handManager.cardsInHand[i].GetComponent<CardMovimentation>();
                if (dragCard.isDragging)
                {
                    anyoneDragging = true;
                }
            }
            if (!anyoneDragging)
            {
                float difference = dragOrigin.y - camera.ScreenToWorldPoint(Input.mousePosition).y;
                var pos = camera.transform.position;
                pos.y += difference;

                if (pos.y < upperBound && pos.y > lowerBound)
                    camera.transform.position = pos;
            }
        }
    }

    public void SlideTo(Vector3 pos){
        slidePos = pos;
        sliding = true;
    }

    public void SlideToGameTurnPos(){
        slidePos = camera.transform.position;

        switch (gameManager.gameTurn)
            {
                case "PLAYER":
                    slidePos.y = lowerBound;
                    break;

                case "ENEMY":
                    slidePos.y = upperBound;
                    break;

                // case "BATTLE":
                //     slidePos.y = upperBound - lowerBound;
                //     break;
            }
            
        Debug.Log(slidePos);
        sliding = true;
    }

    void Slide(){
        Vector3 cameraCurrentPos = camera.transform.position;

        camera.transform.position = Vector3.Lerp(cameraCurrentPos, slidePos, 0.1f);

        bool onBound = cameraCurrentPos.y < upperBound - 1f && cameraCurrentPos.y > lowerBound + 1f;
        bool onPosition = cameraCurrentPos.y >= slidePos.y - 1f && cameraCurrentPos.y <= slidePos.y + 1f;

        if (!onBound || onPosition)
            sliding = false;
    }
}
