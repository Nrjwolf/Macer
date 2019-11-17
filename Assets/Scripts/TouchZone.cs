using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class TouchZone : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool IsDrag { get; private set; }
    public Action<PointerEventData> OnDragEvent = delegate { };
    public Action<PointerEventData> OnDragBegin = delegate { };
    public Action<PointerEventData> OnDragEnd = delegate { };

    [SerializeField] private Image m_JoystickImage;
    [SerializeField] private Image m_JoystickInner;

    private Vector2 m_BeginDragPosition;

    public float Power;

    public void Reset()
    {
        m_JoystickImage.gameObject.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDrag = true;
        OnDragBegin(eventData);

        m_BeginDragPosition = eventData.position;
        m_JoystickImage.GetComponent<RectTransform>().anchoredPosition = m_BeginDragPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent(eventData);

        var rectT = m_JoystickInner.GetComponent<RectTransform>();
        var delta = eventData.position - m_BeginDragPosition;
        delta = Vector2.ClampMagnitude(delta, 40);
        rectT.anchoredPosition = delta;

        Power = delta.magnitude / 40;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDrag = false;
        OnDragEnd(eventData);

        m_JoystickImage.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_JoystickImage.gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_JoystickImage.gameObject.SetActive(false);
        m_JoystickImage.GetComponent<RectTransform>().anchoredPosition = m_BeginDragPosition;
    }
}
