using System.Collections;
using System.Collections.Generic;
using RPGM.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.Audio;

public class PlayerShooting : MonoBehaviour
{
    static public GameObject projectile;
    static public GameObject shockwave;
    static public GameObject player;
    static public AudioClip missileSound;
    
    static public float projectileCD;
    static public float shockwaveCD;
    static public float projectileSpeed;
    static public float offset;
    static public float projectileLifespan;
    
    private float projectileCooldown;
    private float basicAttackCooldown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


        if (projectileCooldown <= 0 && Input.GetMouseButton(0) && player.transform.localScale == new Vector3(1,1,1))
        {
            float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                sendProjectile(direction.normalized, rotationZ);
                projectileCooldown = projectileCD;
        }
        else
        {
            projectileCooldown -= Time.deltaTime;
        }

        if (basicAttackCooldown <= 0 && Input.GetMouseButton(1) && player.transform.localScale == new Vector3(1,1,1))
        {
           electrify();
                basicAttackCooldown = shockwaveCD;
        }
        else
        {
            basicAttackCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown("t") && player.transform.localScale == new Vector3(1,1,1))
        {
            player.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
            player.GetComponent<CharacterController2D>().speed = 80;
            player.layer = 8;
        }
        else if(Input.GetKeyDown("t"))
        {
            player.transform.localScale = new Vector3(1,1,1);
            player.GetComponent<CharacterController2D>().speed = 22;
            player.layer = 0;
        }
    }

    void sendProjectile(Vector2 direction, float rotationZ)
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = missileSound;
        audio.Play();
        GameObject p = Instantiate(projectile) as GameObject;
        p.transform.position = player.transform.position;
        p.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + offset);
        p.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        Destroy(p, projectileLifespan);
    }

    void electrify()
    {
        GameObject s = Instantiate(shockwave);
        s.transform.position = player.transform.position;
        Destroy(s, s.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}