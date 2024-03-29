using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Game
{
    public class SnowballScaleHandler
    {
        private readonly Snowball _snowball;
        private readonly Dictionary<SnowballState, int> _incrementValues = new();
        private int _growthIncrements;
        public int GrowthIncrements => _growthIncrements;
        public float NominalizedIncrements => GetNominalizedIncrements();

        public SnowballScaleHandler(Snowball snowball)
        {
            _snowball = snowball;
            _growthIncrements = 0;
            var increment = 0;
            foreach (var state in snowball.SnowballStates)
            {
                increment += state.GrowthIncrements;
                _incrementValues.Add(state, increment);
            }
        }

        private void SetScale(int amount)
        {
            _growthIncrements += amount;
            _snowball.CheckState(GetIncrementsInState());
            var state = _snowball.ActiveState;
            var t = GetNominalizedIncrements();
            var scale = Mathf.Lerp(state.Scale.From, state.Scale.To, t);
            _snowball.transform.localScale = Vector3.one * scale;
        }

        private int GetIncrementsInState()
        {
            var increment = 0;
            foreach (var state in _snowball.SnowballStates)
            {
                if (state == _snowball.ActiveState) return _growthIncrements - increment;
                increment += state.GrowthIncrements;
            }
            return increment;
        }

        private float GetNominalizedIncrements()
            => (float)GetIncrementsInState() / _snowball.ActiveState.GrowthIncrements;
        
        public void Grow() => SetScale(1);
        public void Shrink(int increments) => SetScale(-increments);
        public void Reset()
        {
            _growthIncrements = 0;
            _snowball.transform.localScale = Vector3.one;
        }
    }
}