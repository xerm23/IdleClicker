using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private GameObject _blocker;

        private Image _pauseButtonImage;
        private Image _resumeButtonImage;


        private void Start()
        {
            _pauseButtonImage = _pauseButton.GetComponent<Image>();
            _resumeButtonImage = _resumeButton.GetComponent<Image>();
            _pauseButton.onClick.AddListener(PauseGame);
            _resumeButton.onClick.AddListener(ResumeGame);
        }


        private void ResumeGame()
        {
            SwapButtonAnim(_pauseButtonImage, _resumeButtonImage, false);
            _blocker.SetActive(false);
        }

        private void PauseGame()
        {
            SwapButtonAnim(_resumeButtonImage, _pauseButtonImage, true);
            _blocker.SetActive(true);
        }

        private void SwapButtonAnim(Image toActivate, Image toDeactivate, bool pause)
        {
            DOTween.Complete(toActivate);
            DOTween.Complete(toActivate.transform);
            DOTween.Complete(toDeactivate);
            DOTween.Complete(toDeactivate.transform);

            Time.timeScale = pause ? 0 : 1f;

            toDeactivate.DOFade(0, .25f).SetUpdate(true);
            toDeactivate.transform.DOScale(Vector3.zero, .25f).SetUpdate(true).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    toDeactivate.gameObject.SetActive(false);
                    toDeactivate.transform.localScale = Vector3.one;
                });

            toActivate.gameObject.SetActive(true);

            toActivate.DOFade(1, .25f).SetUpdate(true);
            toActivate.transform.DOPunchScale(Vector3.one * .25f, .25f).SetUpdate(true);
        }
    }
}