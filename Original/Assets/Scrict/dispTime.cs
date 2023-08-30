using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dispTime : MonoBehaviour
{
    void Update()
    {
        if(Timer.endSeconds != Timer.endOldSeconds)
        {
            Timer.endTimerText.text = Timer.endMinuts.ToString("00") + ":" + Timer.endSeconds.ToString("00");
        }
    }
}
