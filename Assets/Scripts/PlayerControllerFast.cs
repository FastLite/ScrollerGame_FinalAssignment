using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControllerFast : MonoBehaviour
{
    public float FireRate = 1.0f;
 
 
    public float LastFire;

    public int maximumHealth = 80;

    public int bulletDamage = 5;

    public float bulletForce = 0.2f;

    public float movementSpeed = 1.5f;

    public Transform bulletSpawnPt1;

    public Transform bulletSpawnPt2;

    public AudioClip shootSound;

    public GameObject fireEffectPrefab;

    public GameObject bulletPrefab;
    void Update()
    {

        float translationX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        float translationY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        

        transform.Translate(translationX, 0, 0);
        transform.Translate(0, translationY, 0);


        if (Input.GetKey(KeyCode.Space) && LastFire + FireRate <= Time.time)
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);

            GameObject go2 = Instantiate(bulletPrefab, bulletSpawnPt1);
            GameObject go1 = Instantiate(bulletPrefab, bulletSpawnPt2);
            Instantiate(fireEffectPrefab, bulletSpawnPt1);
            Instantiate(fireEffectPrefab, bulletSpawnPt2);

            go1.transform.parent = null;
            go2.transform.parent = null;



            go1.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);
            go2.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);

            LastFire = Time.time;

        }


    }

   

}
