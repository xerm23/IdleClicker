using DG.Tweening;
using IdleClicker.Tools.ObjectPooling;
using TMPro;
using UnityEngine;

namespace IdleClicker.GameLogic
{
    public class GameSquareElement : Poolable<GameSquareElement>
    {
        private int _level;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] TextMeshPro _levelTMP;

        public void SetLevel(int level)
        {
            _level = level;
            //float colorWave = 1 / (level + 1);
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

    }
}