using System;
using System.Security.Policy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Zelda.Dungeon;

namespace Zelda.Survival
{
    public class SurvivalManager : DungeonManager
    {
        public override bool CurrentRoomMapped { get; } = true;
        WaveManager _waveManager;

        public override void LoadDungeonContent(ContentManager content)
        {

            Scenes = new Scene[1][];
            Rooms = new Room[1][];
            BackgroundIds = new BackgroundId[2][];
            Scenes[0] = new Scene[1];
            Rooms[0] = new Room[1];
            //Scenes[1] = new Scene[1];
            //Rooms[1] = new Room[1];
            BackgroundIds[1] = new BackgroundId[] {BackgroundId.Shop};
            BackgroundIds[0] = new BackgroundId[] { BackgroundId.Dungeon};
            //var shopRoom = new SurvivalRoom(this, content.Load<int[][]>($"Shop/ShopTiles"));
            //var shopScene = new SurvivalScene(shopRoom, Player);
            //Scenes[1][0] = shopScene;
            //Rooms[1][0] = shopRoom;
            var dungeonRoom = new SurvivalRoom(this, content.Load<int[][]>($"Rooms/Survival-Dungeon"));
            Rooms[0][0] = dungeonRoom;

            _waveManager = new WaveManager(dungeonRoom,content);

            /* TODO: Implement */
        }

        public override void LoadScenes(IPlayer player)
        {
            Player = player;
            Scenes[0][0] = new SurvivalScene((SurvivalRoom)Rooms[0][0], Player);
            //Look at this and see if more to do
        }

        public override void ResetScenes()
        {
            //Should just be no op?
        }

        public override void JumpToRoom(int row, int column, Direction facing = Direction.Up)
        {
            var oldRoom = CurrentRoom;
            //TODO: update and remove this
            row = 0;
            column = 0;
            //VisitedRooms[row][column] = true;
            CurrentRoom = new Point(column, row);
            SetBackground(BackgroundIds[row][column]);
            if (oldRoom == BasementRoom && CurrentRoom == BasementAccessRoom)
            {
                Player?.Teleport(TileSize * new Point(6, 7), Direction.Down);
            }
            else if (CurrentRoom == BasementRoom)
            {
                Player?.Teleport(TileSize * new Point(3, 2), Direction.Down);
            }
            else
            {
                Player?.Teleport(TeleportLocation.Calculate(facing), facing);
            }

            Scene?.DestroyProjectiles();
            Scene = (SurvivalScene) Scenes[row][column];
            Scene.SpawnScene();
        }
    }
}