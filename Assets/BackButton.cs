using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
        SceneManager.LoadScene("TitleScene");
    }

    public void OnClick1()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
        SceneManager.LoadScene("SampleScene");
    }
}
