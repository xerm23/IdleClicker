using IdleClicker.Managers;
using UnityEngine;

namespace IdleClicker.UI
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _totalTapsTMP;
        [SerializeField] private TMPro.TextMeshProUGUI _totalMergesTMP;

        private void Start()
        {
            SetTotalTapsInfo();
            SetTotalMergesInfo();
            ScoreManager.OnTotalTapChanged += SetTotalTapsInfo;
            ScoreManager.OnTotalMergesChanged += SetTotalMergesInfo;
        }

        private void OnDestroy()
        {
            ScoreManager.OnTotalTapChanged -= SetTotalTapsInfo;
            ScoreManager.OnTotalMergesChanged -= SetTotalMergesInfo;
        }

        private void SetTotalMergesInfo()
        {
            _totalMergesTMP.SetText("Merges: " + ScoreManager.TotalMerges);
        }
        private void SetTotalTapsInfo()
        {
            _totalTapsTMP.SetText("Taps: " + ScoreManager.TotalTaps);
        }
    }
}