using System.Collections.Generic;

namespace ProjectEuler.Common
{
    /// <summary>
    /// Cache for generating ordered sequences, up to a given value.
    /// </summary>
    public interface ISequenceGenerator
    {
        /// <summary>
        /// Get next value in sequence after the provided value. 
        /// </summary>
        long NextValue(long currentValue);
        
        /// <summary>
        /// Get all values in sequence up to the provided value.
        /// </summary>
        IEnumerable<long> GetValues(long maxValue);
    }
}