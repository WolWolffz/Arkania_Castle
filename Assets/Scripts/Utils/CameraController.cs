using UnityEngine;

public class CameraController : MonoBehaviour
{
    public new Camera camera;
    // public float camx,camy;
    public float upperBound, lowerBound;

    private GameManager gameManager;
    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
        if (camera == null)
        {
            camera = Camera.main;
        }

        upperBound = gameManager.level.transform.localScale.y / 2 - gameManager.level.transform.localScale.y / 3 + 1f;
        lowerBound = camera.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // camx = camera.transform.position.x;
        // camy = camera.transform.position.y;
        // if(Input.GetKey(KeyCode.UpArrow) && camy < UpperBound){
        //     Vector3 v3 = new Vector3(camx,+0.05f);
        //     camera.transform.Translate(v3);
        // }else if(Input.GetKey(KeyCode.DownArrow) && camy > LowerBound){
        //     Vector3 v3 = new Vector3(camx,-0.05f);
        //     camera.transform.Translate(v3);
        // }

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
}
