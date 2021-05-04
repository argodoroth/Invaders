using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to control the moving background
public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;
    void Start()
    {
        //Gets the material from the object
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, scrollSpeed);
    }

    void Update()
    {
        //Tells the material to be offset independent of framerate
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
}
