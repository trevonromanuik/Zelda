using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Maps
{
    class Map
    {
        private static Map _instance;
        public static Map Instance
        {
            get { return _instance ?? (_instance = new Map()); }
        }

        private Panel[,] _panels;
        private int x, y;
        private int newX, newY;

        private bool _isTransitioning;
        int _transitionLine;
        private Direction _transitionDirection;

        public Map()
        {
            x = y = 0;
            newX = newY = 0;

            _isTransitioning = false;

            _panels = new Panel[,] {
                {
                    new Panel(new int[,] {
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
                    }),
                    new Panel(new int[,] {
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
                    })
                },
                {
                    new Panel(new int[,] {
                        {1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    }),
                    new Panel(new int[,] {
                        {1, 1, 1, 1, 0, 0, 1, 1, 1, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 0, 0, 0, 0, 0, 0, 0, 0, 1},
                        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
                    })
                }
            };
        }

        public Panel CurrentPanel
        {
            get { return _panels[y, x]; }
        }

        private Panel NewPanel
        {
            get { return _panels[newY, newX]; }
        }

        public void ChangePanel(Direction direction)
        {
            newX = x;
            newY = y;

            switch (direction)
            {
                case Direction.Up:
                    newY -= 1;
                    break;
                
                case Direction.Down:
                    newY += 1;
                    break;

                case Direction.Left:
                    newX -= 1;
                    break;

                case Direction.Right:
                    newX += 1;
                    break;
            }

            if (newX < 0 || newY < 0 || newX >= _panels.GetLength(1) || newY >= _panels.GetLength(0))
                return;

            _transitionLine = 0;
            _transitionDirection = direction;
            _isTransitioning = true;
        }

        public void Update(double gameTime)
        {
            if (!_isTransitioning)
                CurrentPanel.Update(gameTime);
            else
            {
                _transitionLine += (int)(0.5 * gameTime * 1000);
                if (((_transitionDirection == Direction.Left || _transitionDirection == Direction.Right) && _transitionLine > 160)
                    || ((_transitionDirection == Direction.Up || _transitionDirection == Direction.Down) && _transitionLine > 128))
                {
                    _isTransitioning = false;

                    x = newX;
                    y = newY;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_isTransitioning)
                CurrentPanel.Draw(spriteBatch);
            else
            {
                spriteBatch.End();

                RenderTarget2D renderTarget = (RenderTarget2D)spriteBatch.GraphicsDevice.GetRenderTargets()[0].RenderTarget;

                RenderTarget2D oldRoomTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 160, 128);
                spriteBatch.GraphicsDevice.SetRenderTarget(oldRoomTarget);

                spriteBatch.Begin();
                CurrentPanel.Draw(spriteBatch);
                spriteBatch.End();

                RenderTarget2D newRoomTarget = new RenderTarget2D(spriteBatch.GraphicsDevice, 160, 128);
                spriteBatch.GraphicsDevice.SetRenderTarget(newRoomTarget);

                spriteBatch.Begin();
                NewPanel.Draw(spriteBatch);
                spriteBatch.End();

                spriteBatch.GraphicsDevice.SetRenderTarget(renderTarget);

                spriteBatch.Begin();
                switch (_transitionDirection)
                {
                    case Direction.Up:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(0, _transitionLine), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(0, _transitionLine - 128), Color.White);
                        break;
                    case Direction.Down:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(0, -_transitionLine), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(0, 128 - _transitionLine), Color.White);
                        break;
                    case Direction.Left:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(_transitionLine, 0), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(_transitionLine - 160, 0), Color.White);
                        break;
                    case Direction.Right:
                        spriteBatch.Draw(oldRoomTarget, new Vector2(-_transitionLine, 0), Color.White);
                        spriteBatch.Draw(newRoomTarget, new Vector2(160 - _transitionLine, 0), Color.White);
                        break;
                }
            }
        }
    }
}
