using Raylib_cs;

public class Game
{
    int xSize, ySize;
    Board b;

    bool turnToggle = false;
    (Color player1, Color player2) playerColors = new(Color.RED, Color.YELLOW);
    Color PlayerColor
    {
        get
        {
            if (turnToggle) return playerColors.player1;
            else return playerColors.player2;
        }
    }

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
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);


            b.DisplayBoard();

            MouseHandler();

            Raylib.EndDrawing();
        }
    }

    public void MouseHandler()
    {
        var mouseCords = Raylib.GetMousePosition();

        b.DisplayNext((int)mouseCords.X);

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
        {
            bool succesful = b.AddPiece((int)mouseCords.X, PlayerColor);
            if (succesful) turnToggle = !turnToggle;
        }
    }
}
