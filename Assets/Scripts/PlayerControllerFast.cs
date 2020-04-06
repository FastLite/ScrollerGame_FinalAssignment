using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControllerFast : MonoBehaviour
{

    public float movementSpeed = 0.5f;

    public Transform bulletSpawnPt1;

    public Transform bulletSpawnPt2;

    public GameObject bulletPrefab;

    public float bulletForce = 50;

    public float bulletDamage = 5;





    void Update()
    {

        float translationX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        float translationY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;


        transform.Translate(translationX, 0, 0);
        transform.Translate(0, translationY, 0);


        if (Input.GetKeyUp(KeyCode.Space))
        {

            GameObject go2 = Instantiate(bulletPrefab, bulletSpawnPt1);
            GameObject go1 = Instantiate(bulletPrefab, bulletSpawnPt2);

            go1.transform.parent = null;
            go2.transform.parent = null;



            go1.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);
            go2.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);



        }


    }

   

}
