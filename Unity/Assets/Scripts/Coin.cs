using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public bool collected;
    public bool AnimateAtAwake = true;
    public void Collected()
    {
        if (!collected)
        {
            GameController.Instance.CoinsCoint++;
            Destroy(gameObject);
            collected = true;
        }
    }

    private void Start()
    {
        if (AnimateAtAwake)
        {
            var x = Random.Range(0.2f, 1);
            var y = Random.Range(0.2f, 1);
            transform.DOMove(transform.position + new Vector3(Random.value > .5f ? x : -x, Random.value > .5f ? y : -y), Random.Range(0.1f, .25f)).SetEase(Ease.OutSine);
        }
    }
}
