using UnityEngine;

public class MoveCam : MonoBehaviour
{
    void Update()
    {
        if (!CountDown.run)
            return;

        float input = Input.GetAxis("Horizontal");
        transform.position += new Vector3(input, 0, 0) * 0.03f;

        if (transform.position.x > 208)
            CountDown.run = false;
    }
}
