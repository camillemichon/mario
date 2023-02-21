using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdown;
    public float time = 366;
    
    public static bool run = true;
    

    private void Update()
    {
        if (run)
        {
            time -= Time.deltaTime;
            countdown.text = "TIME\n" + (int) time;
            if (time < 0)
            {
                run = false;
                Debug.Log("OVER !");
            }
        }
    }
}
