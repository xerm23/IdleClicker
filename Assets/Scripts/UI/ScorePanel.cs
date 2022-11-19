using DG.Tweening;
using IdleClicker.Managers;
using UnityEngine;

namespace IdleClicker.UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _totalTapsTMP;
        [SerializeField] private TMPro.TextMeshProUGUI _totalMergesTMP;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup= GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
            GameManager.OnPlayerStartedGame += ActivateCanvasGroup;
            ScoreManager.OnTotalTapChanged += SetTotalTapsInfo;
            ScoreManager.OnTotalMergesChanged += SetTotalMergesInfo;
        }

        private void OnDestroy()
        {
            ScoreManager.OnTotalTapChanged -= SetTotalTapsInfo;
            ScoreManager.OnTotalMergesChanged -= SetTotalMergesInfo;
            GameManager.OnPlayerStartedGame -= ActivateCanvasGroup;
        }

        private void ActivateCanvasGroup() => _canvasGroup.DOFade(1, .15f);
        private void SetTotalMergesInfo(int amount) => _totalMergesTMP.SetText("Merges: " + amount);
        private void SetTotalTapsInfo(int amount) => _totalTapsTMP.SetText("Taps: " + amount);
    }
}