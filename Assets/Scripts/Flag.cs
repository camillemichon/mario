using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CountDown.run = false;
        Debug.Log("Congrats ! You won !");
    }
}
