using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Macer m_Macer;
    public Macer Macer { get => m_Macer != null ? m_Macer : m_Macer = FindObjectOfType<Macer>(); }

    private TouchZone m_TouchZone;
    public TouchZone TouchZone { get => m_TouchZone != null ? m_TouchZone : m_TouchZone = FindObjectOfType<TouchZone>(); }

    private void Start()
    {
        Macer.OnKill += ResetGame;
    }

    private void ResetGame()
    {
        Macer.Reset();
        TouchZone.Reset();
    }
}
