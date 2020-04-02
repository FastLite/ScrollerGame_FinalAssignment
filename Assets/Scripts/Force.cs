using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public bool isForceAdded = false;
    public float force = 10;
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space) && !isForceAdded)
            rb2d.AddForce(transform.up * force);
        isForceAdded = true;

    }


}

