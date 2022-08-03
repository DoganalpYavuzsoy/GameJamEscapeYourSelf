using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dest : MonoBehaviour
{
    public GameObject destroyedVersion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
