using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startabutton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
        SceneManager.LoadScene("SampleScene");
    }
}
