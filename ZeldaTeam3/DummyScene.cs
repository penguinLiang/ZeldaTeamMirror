using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda;

public class DummyScene : IScene
{

    public DummyScene()
    {

    }

    public void TransitionToRoom(int row, int column)
    {
        Console.WriteLine("Dummy called! Row is " + row + ". Column is " + column + ".");
    }
}
