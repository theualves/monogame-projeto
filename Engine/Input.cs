using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

public static class Input
{
    private static List<Keys> _consumedKeys = new List<Keys>();

    public static bool GetKey(Keys key)
    {
        return Keyboard.GetState().IsKeyDown(key);
    }

    public static bool GetKeyDown(Keys key)
    {
        bool result = false;

        if (!_consumedKeys.Contains(key))
        {
            if (GetKey(key))
            {
                _consumedKeys.Add(key);
                result = true;
            }
        }

        return result;
    }

    public static void Update()
    {
        if (_consumedKeys.Count > 0)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            for (int i = _consumedKeys.Count - 1; i >= 0; i--)
            {
                Keys key = _consumedKeys[i];
                if (keyboardState.IsKeyUp(key))
                {
                    _consumedKeys.RemoveAt(i);
                }
            }
        }
    }
}
