using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownBird : Bird
{
    [SerializeField]
    private float _bodyScaleRadius = 2;

    private bool _hasPuffUp = false;
    public override void PuffUp()
    {
        if (!_hasPuffUp)
        {
            transform.localScale += transform.localScale * _bodyScaleRadius;
            _hasPuffUp = true;
            StartCoroutine(DestroyAfter(0.5f));

        }
    }

}
