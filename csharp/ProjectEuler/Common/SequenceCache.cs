using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace ProjectEuler.Common
{
    /// <summary>
    /// Cache for generating ordered sequences, up to a given value.
    /// </summary>
    /// <remarks>
    /// Not thread safe.
    /// </remarks>
    public abstract class SequenceCache : ISequenceCache
    {
        protected long[] Sequence;

        protected SequenceCache()
        {
            Sequence = Array.Empty<long>();
        }

        public IEnumerable<long> GetCache()
            => Sequence.Select(v => v);

        public long NextValue(long currentValue)
        {
            // Check if the current value is in the cache
            long i = 0;
            foreach (var value in Sequence)
            {
                if (value > currentValue)
                    break;
                ++i;
            }

            if (i != Sequence.LongLength)
                return Sequence[i];

            // Generate the sequence to at least the next value
            GenerateSequence(currentValue);
            GenerateNextValue();
            
            // And go through all the new values (as may have been more efficient to overshoot than generate just one more value)
            for (long j = 0; j < Sequence.Length; ++j)
            {
                if (Sequence[i + j] > currentValue)
                    return Sequence[i + j];
            }

            throw new InvalidOperationException($"Could not generate next value in sequence starting from '{currentValue}'.");
        }
    
        public IEnumerable<long> GetValues(long maxValue)
        {
            if (maxValue > Sequence.LastOrDefault())
                GenerateSequence(maxValue);
            
            // The sequence must be sorted so can break as soon as the limit is reached
            foreach (var value in Sequence)
            {
                if (value > maxValue)
                    yield break;
    
                yield return value;
            }
        }

        protected abstract void GenerateNextValue();

        protected abstract void GenerateSequence(long maxValue);
    }
}