using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace IdleClicker.GameLogic
{
    public class SquareSpawner : MonoBehaviour
    {
        [SerializeField] private GameSquareElement _squareElementPrefab;

        [SerializeField] private Button _tapTheScreenBtn;

        private int _maxLevel = 1;
        ///<value>square, level</value>
        private readonly Dictionary<GameSquareElement, int> _spawnedElements = new();
        private int _currentTap = 0;

        void Start()
        {
            GameSquareElement.Pool.SetParent(transform);
            GameSquareElement.Pool.SetOriginal(_squareElementPrefab);
            _tapTheScreenBtn.onClick.AddListener(TapTheScreen);
        }

        private void OnDestroy()
        {
            _tapTheScreenBtn.onClick.RemoveAllListeners();
        }

        private void SpawnSquare(int level)
        {
            var square = GameSquareElement.Pool.Get();
            square.SetLevel(level);
            square.transform.position = Vector3.up * _spawnedElements.Count;
            _spawnedElements.Add(square, level);
        }

        private void TapTheScreen()
        {
            SpawnSquare(0);
            _currentTap++;
            if (_currentTap > 9)
            {
                Merge();
            }
        }

        private void Merge()
        {
            for (int i = 0; i < _maxLevel; i++)
            {
                var elementsGroupByLevel = _spawnedElements.Where(x => x.Value == i).Select(x => x.Key).ToList();

                if (elementsGroupByLevel.Count >= 10)
                {
                    foreach (var square in elementsGroupByLevel)
                    {
                        square.MoveToPool();
                        _spawnedElements.Remove(square);
                    }
                    SpawnSquare(i + 1);
                    if (i + 2 > _maxLevel)
                        _maxLevel++;
                }
            }
        }
    }
}