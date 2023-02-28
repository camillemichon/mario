using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CountDown.run = false;
        Debug.Log("You lost !");
    }
}
