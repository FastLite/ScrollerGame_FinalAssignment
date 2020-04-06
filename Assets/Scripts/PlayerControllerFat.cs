using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControllerFat : MonoBehaviour
{

    public float movementSpeed = 0.5f;

    public Transform bulletSpawnPt;

    public GameObject bulletPrefab;

    public float bulletForce = 20;

    void Update()
    {

        float translationX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        float translationY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;


        transform.Translate(translationX, 0, 0);
        transform.Translate(0, translationY, 0);


        if (Input.GetKeyUp(KeyCode.Space))
        {

            GameObject go = Instantiate(bulletPrefab, bulletSpawnPt);

            go.transform.parent = null;

            go.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);

        }


    }

    
}
