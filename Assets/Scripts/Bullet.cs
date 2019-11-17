using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => m_Rigidbody2D != null ? m_Rigidbody2D : m_Rigidbody2D = GetComponent<Rigidbody2D>(); }

    private void Update()
    {
        float AngleRad = Mathf.Atan2(transform.position.y - (transform.position.y + Rigidbody2D.velocity.y), transform.position.x - (transform.position.x + Rigidbody2D.velocity.x));
        float angle = (180 / Mathf.PI) * AngleRad + 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
