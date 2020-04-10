using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SHOOT_TYPE { PLAYER_SHOOT,ENEMY_SHOOT}
public class ShootScript : MonoBehaviour
{
    public SHOOT_TYPE shootType;

    public int damage;

    public float launchForce = 1;

    public GameObject explosionEffectPrefab;
    public GameObject hitEffectPrefab;

    GameManager gMgr;

    private void Awake()
    {
        gMgr = GameObject.FindObjectOfType<GameManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            if (shootType==SHOOT_TYPE.PLAYER_SHOOT && collision.gameObject.tag == "Enemy")
            {


                InstantiateHitEffect();



                Enemy enemyRef = collision.gameObject.GetComponent<Enemy>();

                if (enemyRef.IsEnemyKilled(gMgr.damage))
                {
                    GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
                    
                    Destroy(explosion, 2);
                    Destroy(collision.gameObject);

                    enemyRef.SpawnPickup();

                    GameObject.FindObjectOfType<GameManager>().OnEnemyDestroy();
                }

            }
            else if (shootType == SHOOT_TYPE.ENEMY_SHOOT && collision.gameObject.tag == "Player")
            {
                gMgr.ChangePlayersHealth(GameObject.FindObjectOfType<Enemy>().shootDamage * -1);

                InstantiateHitEffect();
            }

        }
    }

    public void InstantiateHitEffect()
    {
        GameObject HitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
        Destroy(HitEffect, 1);
    }



}
