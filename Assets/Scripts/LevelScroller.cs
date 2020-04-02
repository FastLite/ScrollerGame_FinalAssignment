using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScroller : MonoBehaviour
{
    public float scrollSpeed = -50;
    void Start()
    {
        
    }

  
    void Update()
    {
        float translationY = scrollSpeed * Time.deltaTime;
        transform.Translate(0, translationY, 0);
    }
}
