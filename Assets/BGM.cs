using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BGM : MonoBehaviour
{
    bool isStart = true;
    // Use this for initialization
    void Start()
    {
        //画面遷移してもオブジェクトが壊れないようにする
        DontDestroyOnLoad(this);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "TitleScene")
        {
            isStart = false;
        }

        if (SceneManager.GetActiveScene().name == "TitleScene" && isStart == false)
        {
            Destroy(this.gameObject);
        }
    }
}