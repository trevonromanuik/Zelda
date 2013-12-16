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
        private Dictionary<string, Animation> _animations;
        private Animation _currentAnimation;
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
            if (_currentAnimation == _animations[name])
                return;

            _currentAnimation = _animations[name];
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
            if (_currentAnimation == null)
                throw new NotSupportedException("No animation is currently playing.");

            // Process passing time.
            // Only if not frozen.
            if (!_frozen)
            {
                _time += gameTime;
                while (_time > _currentAnimation.FrameTime)
                {
                    _time -= _currentAnimation.FrameTime;

                    // Advance the frame index; looping or clamping as appropriate.
                    if (_currentAnimation.IsLooping)
                    {
                        _frameIndex = (_frameIndex + 1) % _currentAnimation.FrameCount;
                    }
                    else
                    {
                        _frameIndex = Math.Min(_frameIndex + 1, _currentAnimation.FrameCount - 1);
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
                (int)position.X - _currentAnimation.FrameWidth / 2, 
                (int)position.Y - _currentAnimation.FrameHeight / 2, 
                _currentAnimation.FrameWidth, 
                _currentAnimation.FrameHeight);
            
            Rectangle source = new Rectangle(
                frameIndex * _currentAnimation.FrameWidth, 
                0, 
                _currentAnimation.FrameWidth, 
                _currentAnimation.FrameHeight);
            
            spriteBatch.Draw(_currentAnimation.Texture, destination, source, Color.White);
        }
    }
}
