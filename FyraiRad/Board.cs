using System.Numerics;
using System.Timers;
using Raylib_cs;

public class Board
{
    List<List<Piece>> board = new List<List<Piece>>();
    Vector2 screenDimension;
    int pieceSize = 100;
    int maxHeight;



    public Board(int bottomX, int bottomY)
    {
        screenDimension = new(bottomX, bottomY);
        int boardSizeX = bottomX / 100;
        maxHeight = bottomY / 100;
        for (int i = 0; i < boardSizeX; i++)
        {
            board.Add(new List<Piece>());
        }
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
                int yPos = (int)(screenDimension.Y - 100) + 50;

                foreach (Piece p in board[x])
                {
                    Raylib.DrawCircle(xPos, yPos, 35, p.col);
                    yPos -= 100;
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

        if (pos < 0 || pos >= board.Count) return;

        var tempStack = board[(int)pos];
        int height = (int)screenDimension.Y - 50;
        height -= tempStack.Count * 100;
        Raylib.DrawCircle((int)(pos * 100) + 50, height, 35, Color.BLUE);

    }

    public bool AddPiece(int mouseX, Color c)
    {
        int pos = (int)Math.Floor(mouseX / 100d);

        if (pos >= 0 && pos < board.Count)
        {
            if (board[pos].Count < 6)
            {
                board[pos].Add(new(c));
                return true;
            }
            else
            {
                Console.WriteLine("Filled column!");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Index out of range! -- i : " + pos);
            Console.WriteLine("Range from 0 to " + (board.Count - 1));
            return false;
        }
    }

    public void CheckWins()
    {
        for (int x = 0; x < board.Count; x++)
        {
            if (board[x].Count <= 0) continue;

            for (int y = 0; y < board[x].Count; y++)
            {
                CheckAt(x, y);
            }
        }

        void CheckAt(int x, int y)
        {
            bool win = false;

            for (int iteration = 0; iteration < 5; iteration++)
            {
                Vector2 offsetCheck = new(0, 0);
                Vector2 checkDir;

                switch (iteration)
                {
                    case 0:
                        checkDir = new(-1, 0);
                        break;
                    case 1:
                        checkDir = new(-1, 1);
                        break;
                    case 2:
                        checkDir = new(0, 1);
                        break;
                    case 3:
                        checkDir = new(1, 1);
                        break;
                    case 4:
                        checkDir = new(1, 0);
                        break;
                }

                // It is not necessary to check bakwards, we check up, front up, front, down front and down
                for (int i = 0; i < 4; i++)
                {

                }
            }
        }
    }
}
