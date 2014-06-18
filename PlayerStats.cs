using Mogre;
using Mogre.TutorialFramework;
using System;

namespace RaceGame
{
    /// <summary>
    /// This class implements the player stats
    /// </summary>
    class PlayerStats:CharacterStats    // This class inherits from the character stas class
    {
        private Stat score;             // Field to contain the player score

        /// <summary>
        /// Read only. This property get the player score
        /// </summary>
        public Stat Score
        {
            get { return score; }
        }

        /// <summary>
        /// This class set up the initial values for each player stat
        /// </summary>
        protected override void InitStats()
        {
            base.InitStats();
            score = new Score();
            score.InitValue(0);
            health.InitValue(100);
            shield.InitValue(100);
            lives.InitValue(3);
        }
    }
}
