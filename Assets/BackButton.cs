using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audioSource;
    bool isChange = false;
    int stay = 0;
    string scene = "";

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isChange == true)
        {
            stay++;
            if (stay > 150)
            {
                SceneManager.LoadScene(scene);
            }
        }
    }

    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
        audioSource.PlayOneShot(sound);
        isChange = true;
        scene = "TitleScene";
    }

    public void OnClick1()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
        audioSource.PlayOneShot(sound);
        isChange = true;
        scene = "SampleScene";
    }
}
