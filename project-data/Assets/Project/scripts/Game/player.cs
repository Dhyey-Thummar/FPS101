using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [Header("Visual")]
    public Camera playerCamera;
    [Header("Gameplay")]
    public int initial_ammo = 12;
    private int ammo;
    public int Ammo { get { return ammo; } }

    public int initial_health = 100;
    private int health;
    public int Health { get { return health; } }

    public float knockbackforce = 10f;
    private bool isHurt;
    public float hurtDuration = 0.5f;

    void Start()
    {
        ammo = initial_ammo;
        health = initial_health;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0)
            {
                ammo--;
           
            GameObject bulletObject = ObjectPooler.Instance.GetBullet(true);
            
            bulletObject.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 2 + playerCamera.transform.right * 1 + (playerCamera.transform.up * (-0.45f));
            bulletObject.transform.forward = playerCamera.transform.forward;
            }
        }

        if(health < 0)
        {
            SceneManager.LoadScene("Menu");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    void OnTriggerEnter(Collider othercollider)
    {
        if (othercollider.GetComponent<AmmoCrate>() != null)
        {
            AmmoCrate ammoCrate = othercollider.GetComponent<AmmoCrate>();
            ammo += ammoCrate.ammo/2;
            Destroy(ammoCrate.gameObject);
        }
        if(isHurt == false)
        {
            GameObject hazard = null;
            if (othercollider.GetComponent<Enemy>() != null)
            {
                Enemy enemy = othercollider.GetComponent<Enemy>();
                hazard = enemy.gameObject;
                health -= enemy.damage; 
            }
            else if (othercollider.GetComponent<Bullet>() != null)
            {
                Bullet bullet = othercollider.GetComponent<Bullet>();
                if (bullet.ShotByPlayer == false)
                {
                    health -= bullet.damage;
                    bullet.gameObject.SetActive(false);
                }
            }
            if (hazard != null)
            {
                isHurt = true;
                Vector3 hurtdirection = (transform.position - hazard.transform.position).normalized;
                Vector3 knockbackdirection = (hurtdirection + Vector3.up*0.4f).normalized;

                GetComponent<Force_receiver>().AddForce(knockbackdirection, knockbackforce);
                StartCoroutine(HurtRoutine());
            }
        }
    }   


    IEnumerator HurtRoutine()
    {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }
}
