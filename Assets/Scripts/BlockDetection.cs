using UnityEngine;

public class BlockDetection : MonoBehaviour
{
    public Camera camera;
    public GameObject bankGO;
    
    private RaycastHit hit;
    private Bank bank;

    private void Awake()
    {
        bank = bankGO.GetComponent<Bank>();
    }

    void Update()
    {
        // check when the user press the left click
        if (Input.GetMouseButtonDown(0))
        {
            // get the position of the mouse on the screen
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50))
            {
                switch (hit.transform.tag)
                {
                    case "Brick":
                        Destroy(hit.transform.gameObject);
                        break;
                    case "Interrogation":
                        bank.AddCoins(1);
                        break;
                    default:
                        Debug.Log(hit.transform.tag);
                        break;
                }
            }
        }
    }
}
