using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerControllerFat : MonoBehaviour
{
    public float FireRate = 1.0f;

    public float LastFire;

    public int maximumHealth = 150;

    public int bulletDamage = 50;

    public float bulletForce = 0.075f;

    public float movementSpeed = 0.5f;

    public Transform bulletSpawnPt;
    public GameObject fireEffectPrefab;
    public GameObject bulletPrefab;
    public AudioSource sourceOfAudio;

    public AudioClip shootSound;


    private void Start()
    {
        sourceOfAudio = gameObject.GetComponent<AudioSource>();
        sourceOfAudio.Stop();
    }

    void Update()
    {
       
        float translationX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        float translationY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;


        transform.Translate(translationX, 0, 0);
        transform.Translate(0, translationY, 0);

        

        if (Input.GetKey (KeyCode.Space) && LastFire + FireRate <= Time.time)
        {
            sourceOfAudio.PlayOneShot(shootSound);
            Debug.Log("player should shoot here");

            GameObject go = Instantiate(bulletPrefab, bulletSpawnPt);
            Instantiate(fireEffectPrefab, bulletSpawnPt);
            go.transform.parent = null;

            go.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletForce);
            LastFire = Time.time;
        }


    }
    private void LateUpdate()
    {
        bulletSpawnPt.transform.parent.rotation = Quaternion.identity;
    }


}
