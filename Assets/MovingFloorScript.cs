using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingFloorScript : MonoBehaviour
{
    public Vector3 iniPos;
    public Vector3 targetPos;
    public float spped;
    private Sequence sequence;

    // Start is called before the first frame update
    void Start()
    {

        iniPos = transform.position;
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(targetPos, spped).SetEase(Ease.OutQuad))
                .Append(transform.DOLocalMove(iniPos, spped).SetEase(Ease.OutQuad))
                .SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        sequence.Kill();
    }

}
