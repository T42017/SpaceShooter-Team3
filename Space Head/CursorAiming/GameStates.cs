using System;

namespace CursorAiming
{
    [Flags]
    public enum GameState
    {
        None = 0,
        Loading = 1,
        Playing = 2,
        Paused = 4,
        MainMenu = 8,
        ShopUpgradeMenu = 16,
        GameOver = 32,
        All = 32 | 16 | 8 | 4 | 2 | 1 | 0,
    }
}