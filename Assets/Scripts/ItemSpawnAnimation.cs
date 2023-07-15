using System;
using DG.Tweening;
using UnityEngine;

public  class ItemSpawnAnimation : MonoBehaviour
{
    public Action<GameObject[,]> IsBoardSpawned;

    private void Awake()
    {
        IsBoardSpawned = ShowSpawnAnimation;
    }

    private void ShowSpawnAnimation(GameObject[,] items)
    {
        var sequence = DOTween.Sequence();

        foreach (var item in items)
        {
            var tween = item.transform.DOScale(new Vector3(0.55f, 0.55f, 0.55f), 0.5f);
            sequence.Join(tween);
        }

        sequence.Play();
    }
}