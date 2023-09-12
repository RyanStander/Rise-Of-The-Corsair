namespace Crew
{
    public class CrewLevelSystem
    {
        /// <summary>
        /// Manages leveling up
        /// </summary>
        public static void DetermineLevelGain(CrewLevelData levelData, int currentXp, int currentLevel, int addedXp)
        {
            var totalXp = currentXp + addedXp;

            var xpRequirement = GetXpForNextLevel(levelData, currentLevel);

            var levelsToGain = 0;
            //if player has enough to level up check how many levels are gained
            while (totalXp >= xpRequirement)
            {
                levelsToGain++;
                xpRequirement = GetXpForNextLevel(levelData, currentLevel, levelsToGain + 1);
            }
        }

        private static float GetXpForNextLevel(CrewLevelData levelData, int currentLevel, int levelIncrease = 1)
        {
            var xpRequired = levelData.LevelCurve.Evaluate((float)(currentLevel + levelIncrease) / levelData.MaxLevel) *
                             levelData.MaxXP;
            //Get the xp required for the next level
            return xpRequired;
        }

        public static int GetCurrentLevel(CrewLevelData levelData, int currentXp)
        {
            var currentLevel = 0;
            var xpRequired = GetXpForNextLevel(levelData, currentLevel);

            //if player has enough to level up check how many levels are gained
            while (currentXp >= xpRequired)
            {
                currentLevel++;
                xpRequired = GetXpForNextLevel(levelData, currentLevel);
            }

            return currentLevel;
        }

        public static float GetXpForCurrentLevel(CrewLevelData levelData, int level)
        {
            return GetXpForNextLevel(levelData, level - 1);
        }
    }
}
