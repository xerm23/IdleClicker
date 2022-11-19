using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleClicker.Managers
{
    public static class ScoreManager
    {
        public static int TotalTaps = 0;
        public static int TotalMerges = 0;

        public static Action OnTotalTapChanged;
        public static Action OnTotalMergesChanged;

        public static void IncreaseTaps(int amount)
        {
            TotalTaps += amount;
            OnTotalTapChanged?.Invoke();
        }

        public static void IncreaseMerges(int amount)
        {
            TotalMerges += amount;
            OnTotalMergesChanged?.Invoke();
        }

    }
}