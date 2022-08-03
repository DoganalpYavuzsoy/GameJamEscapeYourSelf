using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance;


    [SerializeField] private int gridLenght = 20;
    [SerializeField] private int gridHeight = 20;
    [SerializeField] private float spaceBetween = 3;

    [SerializeField] private List<GameObject> buildings;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void CreateGrid()
    {
        for (int i = 0; i < gridLenght; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Instantiate(buildings[0], new Vector3(i * spaceBetween, 0, j * spaceBetween), Quaternion.identity);
            }
        }
    }
}