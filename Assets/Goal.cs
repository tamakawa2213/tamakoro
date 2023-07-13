using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Color startColor;
    public Color targetColor;
    public float changeSpeed = 1f;

    private Renderer objectRenderer;
    private float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {


        //�F
        ColorUpdata();
    }

    void ColorUpdata()
    {
        t += changeSpeed * Time.deltaTime;
        objectRenderer.material.color = Color.Lerp(startColor, targetColor, t);

        if (t >= 1f)
        {
            // �F�̕ύX������������AstartColor��targetColor�����ւ��邱�ƂŎ��̕ύX���������܂�
            Color tempColor = startColor;
            startColor = targetColor;
            targetColor = tempColor;
            t = 0f;
        }
    }

}
