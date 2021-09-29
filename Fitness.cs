using System;
using System.Diagnostics;

namespace EnemyGenerator
{
    /// This class holds all the fitness-related functions.
    public class Fitness
    {
        /// Calculate the fitness value of the entered individual.
        ///
        /// An individual's fitness is defined by the distance of the
        /// individual's difficulty and the Elite's local difficulty goal. The
        /// local objective is defined as the average of the interval of
        /// difficulty corresponding to the position in the MAP-Elites
        /// population of the entered individual.
        public static void Calculate(
            ref Individual _individual
        ) {
            // Get the index of the corresponding difficulty range
            int d = SearchSpace.GetDifficultyIndex(_individual.difficulty);
            if (d != Common.UNKNOWN)
            {
                // The goal is the mean of the difficulty range
                (float min, float max) df = SearchSpace.AllDifficulties()[d];
                float goal = (df.min + df.max) / 2;
                // Calculate the individual fitness (the distance from the goal)
                _individual.fitness = Math.Abs(goal - _individual.difficulty);
            }
        }

        /// Return true if the first individual (`_i1`) is best than the second
        /// (`_i2`), and false otherwise.
        ///
        /// The best is the individual that is closest to the local goal in the
        /// MAP-Elites population. This is, the best is the one that's fitness
        /// has the lesser value. If `_i1` is null, then `_i2` is the best
        /// individual. If `_i2` is null, then `_i1` is the best individual. If
        /// both individuals are null, then the comparison cannot be performed.
        public static bool IsBest(
            Individual _i1,
            Individual _i2
        ) {
            Debug.Assert(
                _i1 != null || _i2 != null,
                Common.CANNOT_COMPARE_INDIVIDUALS
            );
            if (_i1 is null) { return false; }
            if (_i2 is null) { return true; }
            return _i2.fitness > _i1.fitness;
        }
    }
}