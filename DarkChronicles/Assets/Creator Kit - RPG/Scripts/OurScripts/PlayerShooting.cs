using System.Collections;
using System.Collections.Generic;
using RPGM.Gameplay;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.Audio;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectile;
    public GameObject shockwave;
    public GameObject player;
    public AudioClip missileSound;
    
    public float projectileCD;
    public float shockwaveCD;
    public float projectileSpeed;
    public float offset;
    public float projectileLifespan;
    
    private float projectileCooldown;
    private float basicAttackCooldown;
    
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


        if (ProgressManager.Manager.missile && projectileCooldown <= 0 && Input.GetMouseButton(0) && player.transform.localScale == new Vector3(1,1,1))
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

        if (ProgressManager.Manager.shockwave && basicAttackCooldown <= 0 && Input.GetMouseButton(1) && player.transform.localScale == new Vector3(1,1,1))
        {
           electrify();
                basicAttackCooldown = shockwaveCD;
        }
        else
        {
            basicAttackCooldown -= Time.deltaTime;
        }

        if (ProgressManager.Manager.shrink && Input.GetKeyDown("t") && player.transform.localScale == new Vector3(1,1,1))
        {
            player.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);
            player.GetComponent<CharacterController2D>().speed = 80;
            player.layer = 8;
            
            if (AudioManager.Manager != null)
            {
                AudioManager.Manager.soundSource.clip = AudioManager.Manager.soundClips[2];
                AudioManager.Manager.soundSource.Play();
            }
        }
        else if(ProgressManager.Manager.shrink && Input.GetKeyDown("t"))
        {
            player.transform.localScale = new Vector3(1,1,1);
            player.GetComponent<CharacterController2D>().speed = 22;
            player.layer = 0;
            
            if (AudioManager.Manager != null)
            {
                AudioManager.Manager.soundSource.clip = AudioManager.Manager.soundClips[2];
                AudioManager.Manager.soundSource.Play();
            }
        }
    }

    void sendProjectile(Vector2 direction, float rotationZ)
    {
        if (AudioManager.Manager != null)
        {
            AudioManager.Manager.soundSource.clip = AudioManager.Manager.soundClips[1];
            AudioManager.Manager.soundSource.Play();
        }
        
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