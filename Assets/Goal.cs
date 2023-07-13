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


        //色
        ColorUpdata();
    }

    void ColorUpdata()
    {
        t += changeSpeed * Time.deltaTime;
        objectRenderer.material.color = Color.Lerp(startColor, targetColor, t);

        if (t >= 1f)
        {
            // 色の変更が完了したら、startColorとtargetColorを入れ替えることで次の変更を準備します
            Color tempColor = startColor;
            startColor = targetColor;
            targetColor = tempColor;
            t = 0f;
        }
    }

}
