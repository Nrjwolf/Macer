using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private TextMeshProUGUI m_BestText;

    public GameObject CoinPrefab;
    public GameObject BulletPrefab;
    public GameObject Car;
    public GameObject DieFX;
    public GameObject Follower;

    [SerializeField]
    private Vector2 InstatilateBorders = new Vector2(11, 4.2f);

    public int CoinsCoint;
    public int CoinsCointBest;

    public GameController() => Instance = this;

    void Start()
    {
        // CoinsCoint = PlayerPrefs.GetInt("CoinsCoint");
        CoinsCointBest = PlayerPrefs.GetInt("CoinsBest");

        Macer.OnKill += ResetGame;

        StartCoroutine(AddEnemyCar());
        StartCoroutine(AddEnemyFollower());
    }

    void Update()
    {
        m_CoinsText.text = $"x{CoinsCoint}";
        m_BestText.text = $"63$7 : {CoinsCointBest}";
    }

    private void ResetGame()
    {
        DOVirtual.DelayedCall(1.5f, () =>
        {
            PlayerPrefs.SetInt("CoinsCoint", CoinsCoint);
            if (CoinsCointBest < CoinsCoint) PlayerPrefs.SetInt("CoinsBest", CoinsCoint);
            SceneManager.LoadScene("App", LoadSceneMode.Single);
        });
    }

    private IEnumerator AddEnemyCar()
    {
        yield return new WaitForSeconds(Random.Range(4, 8));
        Instantiate(Car, new Vector2(GetRandom(1f, 4f), -4.2f), Quaternion.identity);
        StartCoroutine(AddEnemyCar());
    }

    private IEnumerator AddEnemyFollower()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));

        var pos = new Vector3(GetRandom(1f, 3f), GetRandom(1f, 3f));
        while ((pos - Macer.transform.position).magnitude < 4)
        {
            pos = new Vector3(GetRandom(1f, 3f), GetRandom(1f, 3f));
        }
        Instantiate(Follower, pos, Quaternion.identity);
        StartCoroutine(AddEnemyFollower());
    }

    private float GetRandom(float from, float to)
    {
        var pos = Random.Range(from, to);
        return Random.value > .5 ? pos : -pos;
    }

    public void AddDieFX(Vector2 pos)
    {
        Instantiate(DieFX, pos, Quaternion.identity);
    }
}
