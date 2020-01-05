using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;

    public float projectileSpeed;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) + Mathf.Rad2Deg;

        if (Input.GetMouseButton(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            sendProjectile(direction, rotationZ);
        }
    }

    void sendProjectile(Vector2 direction, float rotationZ)
    {
        GameObject p = Instantiate(projectile) as GameObject;
        p.transform.position = player.transform.position;
        p.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        p.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
    }
}
