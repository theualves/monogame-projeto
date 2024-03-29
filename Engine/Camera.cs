using Microsoft.Xna.Framework;

public class Camera
{
    public void Update(Vector2 playerOffset, ref Rectangle scenePosition)
    {
        scenePosition.X -= (int)playerOffset.X;
        scenePosition.Y -= (int)playerOffset.Y;

        if (scenePosition.X > 0)
        {
            scenePosition.X = 0;
        }
        else if (scenePosition.X + scenePosition.Width < Globals.SCREEN_WIDTH)
        {
            scenePosition.X = Globals.SCREEN_WIDTH - scenePosition.Width;
        }
        if (scenePosition.Y > 0)
        {
            scenePosition.Y = 0;
        }
        else if (scenePosition.Y + scenePosition.Height < Globals.SCREEN_HEIGHT)
        {
            scenePosition.Y  = Globals.SCREEN_HEIGHT - scenePosition.Height;
        }
    }
}
