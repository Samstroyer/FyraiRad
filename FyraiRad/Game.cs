using Raylib_cs;

public class Game
{
    int xSize, ySize;
    Board b;

    public Game(int x, int y)
    {
        b = new(x, y);
        xSize = x;
        ySize = y;
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            MouseHandler();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            b.DisplayBoard();

            Raylib.EndDrawing();
        }
    }

    public void MouseHandler()
    {
        var mouseCords = Raylib.GetMousePosition();

        b.DisplayNext((int)mouseCords.X);

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {

        }
    }
}
