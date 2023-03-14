using System.Numerics;
using Raylib_cs;

public class Board
{
    List<Stack<Piece>> board = new List<Stack<Piece>>();
    Vector2 screenDimension;
    int pieceSize = 100;

    public Board(int bottomX, int bottomY)
    {
        screenDimension = new(bottomX, bottomY);
        int boardSizeX = bottomX / 100;
        for (int i = 0; i < boardSizeX; i++)
        {
            board.Add(new Stack<Piece>());
        }
    }

    public void AddPiece(int mouseX)
    {

    }

    public void DisplayBoard()
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                RenderBlock(x, y);
            }
        }

        DisplayPieces();
    }

    private void DisplayPieces()
    {
        for (int x = 0; x < 7; x++)
        {
            if (board[x].Count > 0)
            {
                int xPos = 50 + (x * 100);
                int startY = (int)(screenDimension.Y - 100) + 50;

                foreach (Piece p in board[x])
                {
                    Raylib.DrawCircle(xPos, startY, 35, p.col);
                    startY -= 100;
                }
            }
        }
    }

    private void RenderBlock(int x, int y)
    {
        int xPos = x * 100;
        int yPos = (int)(screenDimension.Y - 100) - (y * 100);

        Raylib.DrawRectangle(xPos, yPos, 100, 100, Color.GRAY);
        Raylib.DrawRectangleLines(xPos, yPos, 100, 100, Color.DARKGRAY);
        Raylib.DrawCircle(xPos + 50, yPos + 50, 35, Color.LIGHTGRAY);
    }

    public void DisplayNext(int mouseX)
    {
        double pos = Math.Floor(mouseX / 100d);
    }
}