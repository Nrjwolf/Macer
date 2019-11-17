using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Enemy
{
    [SerializeField] private Transform m_Pushka;
    [SerializeField] private Transform m_FirePoint;
    [SerializeField] private float m_FirePower;
    private Vector2 m_CurrentForce;



    void Start()
    {
        StartCoroutine(MoveRandomDirrection());
        StartCoroutine(ShootCoroutine());
    }

    void Update()
    {
        if (IsKilled) return;
        // поворот пушки
        var mousePos = GameController.Macer.transform.position;
        float AngleRad = Mathf.Atan2(m_Pushka.position.y - mousePos.y, m_Pushka.position.x - mousePos.x);
        float angle = (180 / Mathf.PI) * AngleRad + 90;
        m_Pushka.rotation = Quaternion.Euler(0, 0, angle);

        // движение
        Rigidbody2D.AddForce(m_CurrentForce, ForceMode2D.Force);
    }

    private IEnumerator MoveRandomDirrection()
    {
        Vector2 r = new Vector2(.005f, .015f);
        m_CurrentForce = new Vector2(Random.value > .5 ? Random.Range(r.x, r.y) : -Random.Range(r.x, r.y), 0);
        yield return new WaitForSeconds(Random.Range(.2f, .8f));
        m_CurrentForce = Vector2.zero;
        yield return new WaitForSeconds(Random.Range(1, 5));
        StartCoroutine(MoveRandomDirrection());
    }

    private IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(1f, 4f));
        Shoot();
        StartCoroutine(ShootCoroutine());
    }

    private void Shoot()
    {
        var bullet = Instantiate(GameController.Instance.BulletPrefab, m_FirePoint.transform.position, m_Pushka.rotation).GetComponent<Bullet>();
        bullet.Rigidbody2D.AddForce(m_FirePoint.up * m_FirePower, ForceMode2D.Impulse);
    }

}
