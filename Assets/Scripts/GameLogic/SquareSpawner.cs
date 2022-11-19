using DG.Tweening;
using System;
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

        ///<value>square, level</value>
        private readonly Dictionary<GameSquareElement, int> _spawnedElements = new();
        private int _currentTap = 0;
        private bool _squareSpawnAllowed = true;
        private int _mergeCounter = 0;

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
            if (!_squareSpawnAllowed)
                return;
            SpawnSquare(0);
            _currentTap++;
            if (_currentTap > 9)
            {
                Merge();
            }
        }


        private void Merge()
        {
            _squareSpawnAllowed = false;

            var elementsGroupByLevel = _spawnedElements
                .Where(x => x.Value == _mergeCounter)
                .Select(x => x.Key)
                .ToList();

            if (elementsGroupByLevel.Count >= 10)
            {
                elementsGroupByLevel.Sort(GameSquareElement.ElementsVerticalComparer);
                for (int i = 0; i < elementsGroupByLevel.Count; i++)
                {
                    GameSquareElement square = elementsGroupByLevel[i];
                    _spawnedElements.Remove(square);

                    DOVirtual.DelayedCall(i * .05f, () =>
                    {
                        square.MoveToPoolWithScaleAnim(1.25f);
                        if (i == elementsGroupByLevel.Count)
                        {
                            DOVirtual.DelayedCall(++i * .05f + .5f, () =>
                            {
                                SpawnSquare(++_mergeCounter);
                                Merge();//check for next level merge;
                            });
                        }
                    });
                }
            }
            else
            {
                _mergeCounter = 0;
                _squareSpawnAllowed = true;
            }
        }

    }
}