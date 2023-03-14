using System.Numerics;
using Raylib_cs;

Game g;

Setup();
Draw();

void Setup()
{
    int x = 700;
    int y = 600;

    Raylib.InitWindow(x, y, "Fyra i rad");
    Raylib.SetTargetFPS(30);
    g = new(x, y);
}

void Draw()
{
    g.Run();
}