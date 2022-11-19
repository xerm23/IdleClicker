using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleClicker.Managers
{
    public static class ScoreManager
    {
        private static int _totalTaps = 0;
        private static int _totalMerges = 0;

        public static Action<int> OnTotalTapChanged;
        public static Action<int> OnTotalMergesChanged;

        public static void IncreaseTaps(int amount)
        {
            _totalTaps += amount;
            OnTotalTapChanged?.Invoke(_totalTaps);
        }

        public static void IncreaseMerges(int amount)
        {
            _totalMerges += amount;
            OnTotalMergesChanged?.Invoke(_totalMerges);
        }

    }
}