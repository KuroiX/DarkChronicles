using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public float projectileLifespan;
    public float projectileSpeed;
    public float offset;

    public GameObject projectile;

    public void LookPlayer()
    {

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void Explode()
    {
        Vector3 difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x,
                                 gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        sendWave(difference);
        difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x + 1f,
                         gameObject.transform.position.y + 1f, gameObject.transform.position.z);
        sendWave(difference);
        difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x + 1f,
                         gameObject.transform.position.y, gameObject.transform.position.z);
        sendWave(difference);
        difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x + 1f,
                         gameObject.transform.position.y - 1f, gameObject.transform.position.z);
        sendWave(difference);
        difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x,
                         gameObject.transform.position.y - 1f, gameObject.transform.position.z);
        sendWave(difference);
        difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x - 1f,
                         gameObject.transform.position.y - 1f, gameObject.transform.position.z);
        sendWave(difference);
        difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x - 1f,
                         gameObject.transform.position.y, gameObject.transform.position.z);
        sendWave(difference);
        difference = gameObject.transform.position - new Vector3(gameObject.transform.position.x - 1f,
                         gameObject.transform.position.x + 1f, gameObject.transform.position.z);
        sendWave(difference);
    }

    public void sendWave(Vector3 difference)
    {
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction = direction.normalized;

        GameObject p = Instantiate(projectile) as GameObject;
        p.transform.position = gameObject.transform.position;
        p.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + offset);
        p.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        Destroy(p, projectileLifespan);
    }

    public void Attack()
    {
        player.position = new Vector3(40f,20f);
    }
}
