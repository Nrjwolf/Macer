using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;
using System;

public class Macer : MonoBehaviour
{
    private Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D { get => m_Rigidbody2D != null ? m_Rigidbody2D : m_Rigidbody2D = GetComponent<Rigidbody2D>(); }

    private LineRenderer m_LineRenderer;
    public LineRenderer LineRenderer { get => m_LineRenderer != null ? m_LineRenderer : m_LineRenderer = GetComponent<LineRenderer>(); }

    [SerializeField] private Transform m_Eye;
    [SerializeField] private TouchZone m_TouchZone;
    [SerializeField] private Transform m_Mace;
    [SerializeField] private CinemachineVirtualCamera m_VCam;

    [SerializeField] private float m_MaxMoveForce = 10;
    [SerializeField] private float m_Friction = 1;

    [Space(10)]
    [Header("Camera")]
    [SerializeField] private float k_MinPlayerVelocityToScaleCamera = 15;
    [SerializeField] private float k_ScaleCameraKof = 40;
    [SerializeField] private float k_SmoothScaleCameraKof = .25f;
    [SerializeField] private float k_CameraScaleMaxOrtoSizeDelta = .25f;

    private Vector2 m_StartDragPoint;
    private Vector2 m_CurrentDelta;
    private bool m_IsDragging;
    private float m_MovePower;

    private float k_EyeMoveMax = .17f;

    public event Action OnKill;

    void Start()
    {
        m_TouchZone.OnDragBegin += OnDragBegin;
        m_TouchZone.OnDragEvent += OnDragEvent;
        m_TouchZone.OnDragEnd += OnDragEnd;
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
        m_MovePower = 0;
        Rigidbody2D.velocity = Vector3.zero;
        Rigidbody2D.angularVelocity = 0;
    }

    void FixedUpdate()
    {
        var force = m_CurrentDelta.normalized * m_MovePower * m_MaxMoveForce;
        Rigidbody2D.MovePosition(Rigidbody2D.position + force);
        if (!m_IsDragging) m_MovePower *= m_Friction;

        var playerVelocity = force;
        float targetOrto = 5;
        targetOrto = 5 + m_MovePower * k_ScaleCameraKof;
        targetOrto = Mathf.Clamp(targetOrto, 5, k_CameraScaleMaxOrtoSizeDelta);
        m_VCam.m_Lens.OrthographicSize = Mathf.Lerp(m_VCam.m_Lens.OrthographicSize, targetOrto, k_SmoothScaleCameraKof);

        // движение глаза
        m_Eye.localPosition = m_CurrentDelta.normalized * m_MovePower * k_EyeMoveMax;

        // Линия
        Vector3[] points = new Vector3[]
        {
            transform.position,
            m_Mace.position,
        };
        LineRenderer.SetPositions(points);
    }

    private void OnDragBegin(PointerEventData pointerEvent)
    {
        m_IsDragging = true;
        m_StartDragPoint = pointerEvent.position;
    }

    private void OnDragEvent(PointerEventData pointerEvent)
    {
        m_CurrentDelta = pointerEvent.position - m_StartDragPoint;
        m_MovePower = m_TouchZone.Power;
        // var delta = (pointerEvent.position - m_StartDragPoint) * m_MoveKof;
        // delta = Vector2.ClampMagnitude(delta, m_MaxMoveForce);

        // var force = pointerEvent.delta.normalized * m_TouchZone.Power * m_MaxMoveForce;

        // Debug.Log(m_TouchZone.Power);
        // Rigidbody2D.velocity += force;

    }

    private void OnDragEnd(PointerEventData pointerEvent)
    {
        m_IsDragging = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bad")
        {
            OnKill?.Invoke();
        }
    }
}
