namespace BigBlue
{
    public static class GameStatus
    {
        public static bool playerMoved = false;
        public static bool playerWon = false;
        public static bool winConditionChanged = false;

        public static void resetDefaults()
        {
            playerMoved = false;
            playerWon = false;
            winConditionChanged = false;
        }
    }
}
