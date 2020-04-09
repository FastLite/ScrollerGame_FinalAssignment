using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControllerFat : MonoBehaviour
{
    public int maximumHealth = 150;

    public int bulletDamage = 50;

    public float bulletForce = 0.075f;

    public float movementSpeed = 0.5f;

    public Transform bulletSpawnPt;

    public GameObject bulletPrefab;

    
    void Update()
    {

        float translationX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        float translationY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;


        transform.Translate(translationX, 0, 0);
        transform.Translate(0, translationY, 0);


        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("player should shoot here");

            GameObject go = Instantiate(bulletPrefab, bulletSpawnPt);

            go.transform.parent = null;

            go.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);

        }


    }
    private void LateUpdate()
    {
        bulletSpawnPt.transform.parent.rotation = Quaternion.identity;
    }


}
