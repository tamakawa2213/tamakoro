using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearTimeText : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        timerText = obj.GetComponent<TextMeshProUGUI>();
        timerText.text = "ClearTime " + Global.minute.ToString("00") + ":" + ((int)Global.seconds).ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
