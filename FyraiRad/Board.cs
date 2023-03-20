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

    public (bool succesful, bool win) AddPiece(int mouseX, Color c)
    {
        int pos = (int)Math.Floor(mouseX / 100d);

        if (pos >= 0 && pos < board.Count)
        {
            if (board[pos].Count < 6)
            {
                board[pos].Add(new(c));
                bool win = CheckWinAt(pos, board[pos].Count - 1);
                return (true, win);
            }
            else
            {
                Console.WriteLine("Filled column!");
                return (false, false);
            }
        }
        else
        {
            Console.WriteLine("Index out of range! -- i : " + pos);
            Console.WriteLine("Range from 0 to " + (board.Count - 1));
            return (false, false);
        }
    }

    public bool CheckWinAt(int x, int y)
    {
        for (int iteration = 0; iteration < 8; iteration++)
        {
            Vector2 offsetCheck = new(x, y);
            Vector2 checkDir = new(0, 0);

            int rowAmount = 1;

            switch (iteration)
            {
                case 0:
                    checkDir = new(0, -1);
                    break;
                case 1:
                    checkDir = new(1, -1);
                    break;
                case 2:
                    checkDir = new(1, 0);
                    break;
                case 3:
                    checkDir = new(1, 1);
                    break;
                case 4:
                    checkDir = new(0, 1);
                    break;
                case 5:
                    checkDir = new(-1, 1);
                    break;
                case 6:
                    checkDir = new(-1, 0);
                    break;
                case 7:
                    checkDir = new(-1, -1);
                    break;

                default:
                    Console.WriteLine("This is illegal!\nShould be unreachable code!");
                    break;
            }

            Color startColor = board[x][y].col;
            string potentialWinner = "";

            // Printing color ToString and just color results in "{R:253 G:249 B:0 A:255}" - not readable for a player really
            if
            (
                Color.RED.r == startColor.r &&
                Color.RED.g == startColor.g &&
                Color.RED.b == startColor.b
            ) potentialWinner = "Red";
            else potentialWinner = "Yellow";

            for (int i = 0; i < 3; i++)
            {
                offsetCheck += checkDir;

                if (!(offsetCheck.X >= 0 && offsetCheck.X < board.Count)) break;
                if (!(offsetCheck.Y >= 0 && offsetCheck.Y < board[(int)offsetCheck.X].Count)) break;

                Color tileColor = board[(int)offsetCheck.X][(int)offsetCheck.Y].col;

                if
                (
                    tileColor.r == startColor.r &&
                    tileColor.g == startColor.g &&
                    tileColor.b == startColor.b
                )
                {
                    rowAmount++;
                }

                if (rowAmount == 4)
                {
                    Console.WriteLine("A winning move!");
                    Win(potentialWinner, tileColor);
                    return true;
                }
            }
        }
        Console.WriteLine("Not a winning move!");
        return false;
    }

    private void Win(string winnerColor, Color c)
    {
        Raylib.EndDrawing();

        KeyboardKey key = KeyboardKey.KEY_NULL;
        int fontSize = 32;

        string winnerText = "Color " + winnerColor + " wins!";
        string exitText = "Press any key to exit";

        int winnerTextSize = Raylib.MeasureText(winnerText, fontSize);
        int exitTextSize = Raylib.MeasureText(exitText, fontSize);

        while (key == KeyboardKey.KEY_NULL && !Raylib.WindowShouldClose())
        {
            key = (KeyboardKey)Raylib.GetKeyPressed();

            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.GRAY);

            Raylib.DrawText(winnerText, winnerTextSize / 2, 250, fontSize, c);
            Raylib.DrawText(exitText, exitTextSize / 2, 350, fontSize, Color.BLACK);

            Raylib.EndDrawing();
        };
    }
}

