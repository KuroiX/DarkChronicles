using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    
    [FormerlySerializedAs("cooldown")] public float projectileCD;
    public float projectileSpeed;
    public float offset;
    public float projectileLifespan;
    
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


        if (projectileCooldown <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                sendProjectile(direction.normalized, rotationZ);
                projectileCooldown = projectileCD;
            }
        }
        else
        {
            projectileCooldown -= Time.deltaTime;
        }
    }

    void sendProjectile(Vector2 direction, float rotationZ)
    {
        GameObject p = Instantiate(projectile) as GameObject;
        p.transform.position = player.transform.position;
        p.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + offset);
        p.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        Destroy(p, projectileLifespan);
    }

    void basicAttack()
    {
        
    }
}