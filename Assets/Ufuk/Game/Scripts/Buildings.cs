using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public GameObject baseVisual;
    public GameObject destructVisual;
    public List<GameObject> buildingParts;
    public Ease ease;

    private void Awake()
    {
        destructVisual.SetActive(false);
    }

    void Start()
    {
        baseVisual.transform.DOScaleY(Random.Range(0.5f, 0.8f), Random.Range(0.2f, 1.5f)).SetEase(ease);
    }
}
