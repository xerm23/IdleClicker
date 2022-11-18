using DG.Tweening;
using IdleClicker.Tools.ObjectPooling;
using System;
using TMPro;
using UnityEngine;

namespace IdleClicker.GameLogic
{
    public class GameSquareElement : Poolable<GameSquareElement>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] TextMeshPro _levelTMP;

        public void SetLevel(int level)
        {
            _spriteRenderer.color = new Color(Mathf.Sin(level), Mathf.Cos(level), Mathf.Atan(level));
            _levelTMP.SetText(level.ToString());
        }

        protected override void OnGetFromPool()
        {
            transform.DOPunchScale(Vector3.one * 1.25f, .25f).OnComplete(() => transform.localScale = Vector3.one);
        }

        protected override void Reset()
        {
            gameObject.SetActive(false);
        }

        public static int ElementsVerticalComparer(GameSquareElement element1, GameSquareElement element2)
        {
            if (element1.transform.localPosition.y > element2.transform.localPosition.y)
                return -1;
            if (element1.transform.localPosition.y < element2.transform.localPosition.y)
                return 1;

            return 0;
        }

    }
}