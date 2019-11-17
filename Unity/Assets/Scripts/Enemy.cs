using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => m_Rigidbody2D != null ? m_Rigidbody2D : m_Rigidbody2D = GetComponent<Rigidbody2D>(); }

    public bool IsKilled;

    public Vector2 CoinsRandom;

    public void Kill(bool reward = true)
    {
        IsKilled = true;

        for (int i = 0; i < Random.Range(CoinsRandom.x, CoinsRandom.y); i++)
        {
            var coin = Instantiate(GameController.Instance.CoinPrefab);
            coin.transform.position = transform.position;
        }
        GameController.Instance.AddDieFX(transform.position);
        Destroy(gameObject, 0);
    }
}
