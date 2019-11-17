using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : Enemy
{
    [SerializeField] private float m_Speed;

    void FixedUpdate()
    {
        // rotate
        var target = GameController.Macer.transform.position;
        float AngleRad = Mathf.Atan2(transform.position.y - target.y, transform.position.x - target.x);
        float angle = (180 / Mathf.PI) * AngleRad + 90;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // move
        Rigidbody2D.AddForce(transform.up * m_Speed, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Kill(false);
    }
}
