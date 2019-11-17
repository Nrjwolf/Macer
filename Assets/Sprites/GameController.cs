using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private static Macer m_Macer;
    public static Macer Macer { get => m_Macer != null ? m_Macer : m_Macer = FindObjectOfType<Macer>(); }

    private static TouchZone m_TouchZone;
    public static TouchZone TouchZone { get => m_TouchZone != null ? m_TouchZone : m_TouchZone = FindObjectOfType<TouchZone>(); }

    [SerializeField] private TextMeshProUGUI m_CoinsText;

    public GameObject CoinPrefab;
    public GameObject BulletPrefab;

    public int CoinsCoint;

    public GameController() => Instance = this;

    void Start()
    {
        Macer.OnKill += ResetGame;
    }

    void Update()
    {
        m_CoinsText.text = $"x{CoinsCoint}";
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("App", LoadSceneMode.Single);
        // Macer.Reset();
        // TouchZone.Reset();
    }


}
