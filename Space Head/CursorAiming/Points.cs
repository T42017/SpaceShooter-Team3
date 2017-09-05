using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace CursorAiming
{
    public class Points
    {
        public static int Score;
        public static void AddPoints(EnemyPoints points)
        {
            
            switch (points)
            {
                case EnemyPoints.Enemy50:
                    Score += 50;
                    return;
                case EnemyPoints.Enemy100:
                    Score += 100;
                    return;
                case EnemyPoints.Enemy200:
                    Score += 200;
                    return;
                case EnemyPoints.Enemy400:
                    Score += 400;
                    return;
            }
        }
    }

    public enum EnemyPoints
    {
        Enemy50,
        Enemy100,
        Enemy200,
        Enemy400
    }
}