using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PlayPanel : MonoBehaviour
    {
        [SerializeField] private Button _buttonStartTheGame;

        private CanvasGroup _canvasGroup;
        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _buttonStartTheGame.onClick.AddListener(StartTheGame);
        }

        private void StartTheGame()
        {
            GameManager.PlayerStartedGame();
            _canvasGroup.interactable = _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0, .15f);
        }
    }
}