using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public float cooldown;
    public float projectileSpeed;
    public float offset;
    
    private float remainingCooldown;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;


        if (remainingCooldown <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                sendProjectile(direction.normalized, rotationZ);
                remainingCooldown = cooldown;
            }
        }
        else
        {
            remainingCooldown -= Time.deltaTime;
        }
    }

    void sendProjectile(Vector2 direction, float rotationZ)
    {
        GameObject p = Instantiate(projectile) as GameObject;
        p.transform.position = player.transform.position;
        p.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + offset);
        p.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }
}