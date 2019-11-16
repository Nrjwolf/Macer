using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Macer : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => m_Rigidbody2D != null ? m_Rigidbody2D : m_Rigidbody2D = GetComponent<Rigidbody2D>(); }

    [SerializeField] private Transform m_Eye;
    [SerializeField] private TouchZone m_TouchZone;

    void Start()
    {
        m_TouchZone.OnDragBegin += OnDragBegin;
        m_TouchZone.OnDragEvent += OnDragEvent;
        m_TouchZone.OnDragEnd += OnDragEnd;
    }

    void FixedUpdate()
    {
    }

    private void OnDragBegin(PointerEventData pointerEvent)
    {
        
    }

    private void OnDragEvent(PointerEventData pointerEvent)
    {
        // Rigidbody2D.vel
    }

    private void OnDragEnd(PointerEventData pointerEvent)
    {

    }
}
