using DG.Tweening;
using IdleClicker.Tools.ObjectPooling;
using TMPro;
using UnityEngine;

namespace IdleClicker.GameLogic
{
    public class GameSquareElement : Poolable<GameSquareElement>
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshPro _levelTMP;
        [SerializeField] private ParticleSystem[] _particles;
        [SerializeField] private AudioClip[] _spawnAudioClips;

        private AudioSource AudioSource => _audioSource ??= GetComponent<AudioSource>();
        private AudioSource _audioSource;

        public void SetLevel(int level)
        {
            _spriteRenderer.color = new Color(Mathf.Sin(level), Mathf.Cos(level), Mathf.Atan(level));
            _levelTMP.SetText(level.ToString());
        }

        protected override void OnGetFromPool()
        {
            AudioSource.pitch = UnityEngine.Random.Range(.75f, 1.25f);
            AudioSource.PlayOneShot(_spawnAudioClips[UnityEngine.Random.Range(0, _spawnAudioClips.Length)]);
            transform.DOPunchPosition(Vector3.up, .5f, 2);
            transform.DOPunchScale(Vector3.one * 1.25f, .25f).OnComplete(() => transform.localScale = Vector3.one);
        }

        protected override void Reset()
        {
            gameObject.SetActive(false);
        }

        public override void MoveToPoolWithScaleAnim(float animDuration)
        {
            base.MoveToPoolWithScaleAnim(animDuration);
            foreach (var particle in _particles)
            {
                particle.Play();
            }
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