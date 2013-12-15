using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    class RoomManager
    {
        private static RoomManager instance;

        public static RoomManager Instance
        {
            get { return instance ?? (instance = new RoomManager()); }
        }

        private int x;
        private int y;
        private int newX;
        private int newY;

        public Room CurrentRoom { get; set; }
        private Room newRoom = null;
        private bool isTransitioning = false;

        enum Direction { Up, Down, Left, Right };

        private Texture2D itemBoxTexture;

        Direction transitionDirection;
        int transitionLine;

        public Room[,] rooms = new Room[,] { 
            { new Room(Color.Blue), new Room(Color.Green) }, 
            { new Room(Color.Purple), new Room(Color.Yellow) },
        };

        public RoomManager()
        {
            CurrentRoom = rooms[0, 0];
        }

        public void LoadContent(ContentManager content)
        {
            itemBoxTexture = content.Load<Texture2D>("item_box");
        }

        public void Update(double gameTime)
        {
            if (!isTransitioning)
                CurrentRoom.Update(gameTime);
            else
            {
                transitionLine += (int)(0.5 * gameTime);
                if (((transitionDirection == Direction.Left || transitionDirection == Direction.Right) && transitionLine > 240)
                    || ((transitionDirection == Direction.Up || transitionDirection == Direction.Down) && transitionLine > 160))
                {
                    isTransitioning = false;

                    x = newX;
                    y = newY;

                    CurrentRoom = newRoom;
                    newRoom = null;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isTransitioning)
            {
                spriteBatch.Begin();

                CurrentRoom.Draw(gameTime, spriteBatch);

                DrawHud(spriteBatch);

                spriteBatch.End();
            }
            else
            {
                RenderTarget2D renderTarget = (RenderTarget2D)spriteBatch.GraphicsDevice.GetRenderTargets()[0].RenderTarget;

                RenderTarget2D oldRoomTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 240, 160);
                spriteBatch.GraphicsDevice.SetRenderTarget(oldRoomTarget);

                spriteBatch.Begin();
                CurrentRoom.Draw(gameTime, spriteBatch);
                spriteBatch.End();

                RenderTarget2D newRoomTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 240, 160);
                spriteBatch.GraphicsDevice.SetRenderTarget(newRoomTarget);

                spriteBatch.Begin();
                newRoom.Draw(gameTime, spriteBatch);
                spriteBatch.End();

                spriteBatch.GraphicsDevice.SetRenderTarget(renderTarget);
                
                spriteBatch.Begin();
                switch (transitionDirection)
                {
                    case Direction.Up:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(0, transitionLine), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(0, transitionLine - 160), Color.White);
                        break;
                    case Direction.Down:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(0, -transitionLine), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(0, 160 - transitionLine), Color.White);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(transitionLine, 0), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(transitionLine - 240, 0), Color.White);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(-transitionLine, 0), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(240 - transitionLine, 0), Color.White);
                        break;
                }
                spriteBatch.End();
            }
        }

        public void ChangeRoom(int deltaX, int deltaY)
        {
            newX = x + deltaX;
            newY = y + deltaY;

            if (newX < 0 || newY < 0 || newX >= rooms.GetLength(1) || newY >= rooms.GetLength(0))
                return;


            if (deltaX < 0)
                transitionDirection = Direction.Left;
            else if (deltaX > 0)
                transitionDirection = Direction.Right;
            else if (deltaY < 0)
                transitionDirection = Direction.Up;
            else if (deltaY > 0)
                transitionDirection = Direction.Down;

            transitionLine = 0;
            newRoom = rooms[newY, newX];
            isTransitioning = true;
        }

        private void DrawHud(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(itemBoxTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(Player.Instance.CurrentEquipment.Texture, Vector2.Zero, Color.White);
        }
    }
}
