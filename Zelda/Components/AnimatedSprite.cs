using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda.Components
{
    class AnimatedSprite : Component
    {
        public Animation Animation { get; set; }

        private Dictionary<string, Animation> _animations;
        private int _frameIndex;
        private double _time;
        private bool _frozen;

        public AnimatedSprite()
        {
            _animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(string name, Animation animation)
        {
            _animations.Add(name, animation);
        }

        public void PlayAnimation(string name)
        {
            if (Animation == _animations[name])
                return;

            Animation = _animations[name];
            _frameIndex = 0;
            _time = 0.0f;
        }

        public void Freeze()
        {
            _frozen = true;
        }

        public void Unfreeze()
        {
            _frozen = false;
        }

        public override void Update(double gameTime)
        {
            if (Animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            // Process passing time.
            // Only if not frozen.
            if (!_frozen)
            {
                _time += gameTime;
                while (_time > Animation.FrameTime)
                {
                    _time -= Animation.FrameTime;

                    // Advance the frame index; looping or clamping as appropriate.
                    if (Animation.IsLooping)
                    {
                        _frameIndex = (_frameIndex + 1) % Animation.FrameCount;
                    }
                    else
                    {
                        _frameIndex = Math.Min(_frameIndex + 1, Animation.FrameCount - 1);
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var position = GetComponent<Position>();
            if (position == null)
                return;

            int frameIndex = _frozen ? 0 : _frameIndex;

            // Calculate the source rectangle of the current frame.
            Rectangle destination = new Rectangle(
                (int)position.X - Animation.FrameWidth / 2, 
                (int)position.Y - Animation.FrameHeight / 2, 
                Animation.FrameWidth, 
                Animation.FrameHeight);
            
            Rectangle source = new Rectangle(
                frameIndex * Animation.FrameWidth, 
                0, 
                Animation.FrameWidth, 
                Animation.FrameHeight);
            
            spriteBatch.Draw(Animation.Texture, destination, source, Color.White);
        }
    }
}
