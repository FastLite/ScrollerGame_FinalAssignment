using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this script to your main player spacecraft.
/// </summary>
public class PlayerControllerFat : MonoBehaviour
{
    /// <summary>
    /// Tweak in the inspector depending upon whether the movement  is too slow or fast 
    /// </summary>
    public float movementSpeed = 0.5f;

    /// <summary>
    /// This is the point from which our missile will spawn
    /// </summary>
    public Transform bulletSpawnPt;

    /// <summary>
    /// This is our prefab that we will spawn when we press the 'Fire1'  (defined under Unity's Input as 'left control' key)
    /// </summary>
    public GameObject bulletPrefab;

    /// <summary>
    /// the force multiplier. controls how fast the missile will travel
    /// </summary>
    public float missileForce=20;

    void Update()
    {

        float translationX = Input.GetAxis("Horizontal") * movementSpeed*Time.deltaTime;

        //move the spaceship in the X axis only..
        transform.Translate(translationX, 0, 0);


        if(Input.GetKeyUp(KeyCode.Space))
        {
            //Instantiate the missile and make it the child of the missileSpawnPt so that we can get its spawn position from it
            GameObject go = Instantiate(bulletPrefab, bulletSpawnPt);
            //Notice the use of 'Rigidbody2D' for 2d games.. We use 'Rigidbody' for 3d games.
            //add force so that the missile can travel forward. we  propel the missile in the forward direction of the spaceship
            go.GetComponent<Rigidbody2D>().AddForce( transform.up* missileForce);

            Destroy(go, 1.5f); //destroy the missile after 3 seconds (if it didn't already get destroyed upon hitting a collider)

        }


    }


}
