using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public List<GameObject> buildingParts;
    public Ease ease;


    void Start()
    {
        transform.DOScaleY(1f, Random.Range(0.5f, 1f)).SetEase(ease);
    }
}
