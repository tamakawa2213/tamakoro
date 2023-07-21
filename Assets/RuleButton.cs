using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RuleButton : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audioSource;
    bool isChange = false;
    int stay = 0;

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
            if(stay > 150)
            {
                SceneManager.LoadScene("Rule");
            }
        }
    }


    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
        audioSource.PlayOneShot(sound);
        isChange = true;
    }

    
}
