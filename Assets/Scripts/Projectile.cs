using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject hitEffectPrefab;

    Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.magnitude > 1000f)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController controller = other.collider.GetComponent<EnemyController>();

        if (controller != null)
        {
            controller.Fix();
            PlayHitEffect();
        }

        Destroy(gameObject);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void PlayHitEffect()
    {
        Instantiate(hitEffectPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
