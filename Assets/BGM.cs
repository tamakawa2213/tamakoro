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
        //��ʑJ�ڂ��Ă��I�u�W�F�N�g�����Ȃ��悤�ɂ���
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