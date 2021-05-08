using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    public class SpawnWindowManager
    {
        private List<(float time, SpawnWindowStats window)> _invervalIncreases;
        private SpawnWindowStats _current;
        private int _currentIndex;

        public SpawnWindowManager(List<(float time, SpawnWindowStats stats)> intervalIncreases)
        {
            _current = intervalIncreases[0].stats;
            _invervalIncreases = intervalIncreases;
        }

        public SpawnWindowStats GetCurrentSpawnWindowStats()
        {
            if (_currentIndex < _invervalIncreases.Count && _invervalIncreases[_currentIndex].time <= Time.realtimeSinceStartup)
            {
                return _current;
            }

            return _invervalIncreases[_currentIndex++].window;
        }
    }
}