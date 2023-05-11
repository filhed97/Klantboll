using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShirtColor : MonoBehaviour
{
    public Texture shirtTexture;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = shirtTexture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
