using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zelda;
using System.Diagnostics;

public class DummyScene : IScene
{

    public DummyScene()
    {
        Debug.WriteLine("DummyScene has started up!");
    }

    public void TransitionToRoom(int row, int column)
    {
        Debug.WriteLine("Dummy called! Row is " + row + ". Column is " + column + ".");
    }
}
