using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator m_Animator;
    public Animator Animator { get => m_Animator != null ? m_Animator : m_Animator = GetComponent<Animator>(); }

    private BoxCollider2D m_BoxCollider2D;
    public BoxCollider2D BoxCollider2D { get => m_BoxCollider2D != null ? m_BoxCollider2D : m_BoxCollider2D = GetComponent<BoxCollider2D>(); }

    private Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => m_Rigidbody2D != null ? m_Rigidbody2D : m_Rigidbody2D = GetComponent<Rigidbody2D>(); }

    [SerializeField] private Rigidbody2D[] m_Parts;

    [SerializeField]
    private bool m_IsAnimationActive;
    public bool IsAnimationActive
    {
        get => m_IsAnimationActive;
        set
        {
            m_IsAnimationActive = value;

            Animator.enabled = m_IsAnimationActive;
            BoxCollider2D.enabled = m_IsAnimationActive;

            foreach (var item in m_Parts)
            {
                item.bodyType = Animator.enabled ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
                item.GetComponent<Collider2D>().enabled = !m_IsAnimationActive;


                item.GetComponent<Rigidbody2D>().AddForce(Rigidbody2D.velocity * 1.2f, ForceMode2D.Impulse);
            }
        }
    }

    void Start()
    {

    }

    void OnDestroy()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        if (!gameObject.activeInHierarchy)
            return;

        IsAnimationActive = m_IsAnimationActive;
    }
#endif
}
