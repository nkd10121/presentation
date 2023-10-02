using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class dispTime : MonoBehaviour
{
    void Start()
    {

        gameObject.GetComponent<UnityEngine.UI.Text>().text = Timer.endMinuts.ToString("00") + ":" + Timer.endSeconds.ToString("00");

        Debug.Log(Timer.endSeconds);
    }
}
