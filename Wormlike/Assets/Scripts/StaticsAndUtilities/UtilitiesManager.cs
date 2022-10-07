using ThirdPersonScripts;
using UnityEngine;

namespace StaticsAndUtilities
{
    public static class UtilitiesManager
    {
        public static Color SetColor(TeamColorEnum colorEnum)
        {
            switch (colorEnum)
            {
                case TeamColorEnum.BLACK:
                    return Color.black;
                case TeamColorEnum.WHITE:
                    return Color.white;
                case TeamColorEnum.BLUE:
                    return Color.blue;
                case TeamColorEnum.CYAN:
                    return Color.cyan;
                case TeamColorEnum.YELLOW:
                    return Color.yellow;
                case TeamColorEnum.GRAY:
                    return Color.gray;
                case TeamColorEnum.MAGENTA:
                    return Color.magenta;
                case TeamColorEnum.GREEN:
                    return Color.green;
            }
            Debug.LogError("Unsupported team colour");
            return Color.red;
        }
    }
}
