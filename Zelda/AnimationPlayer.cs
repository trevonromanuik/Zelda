using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zelda
{
    /// <summary>
    /// Controls playback of an Animation.
    /// </summary>
    public struct AnimationPlayer
    {
        /// <summary>
        /// Gets the animation which is currently playing.
        /// </summary>
        public Animation Animation { get; private set; }

        /// <summary>
        /// Gets the index of the current frame in the animation.
        /// </summary>
        public int FrameIndex { get; private set; }

        /// <summary>
        /// The amount of time in seconds that the current frame has been shown for.
        /// </summary>
        private float time;

        /// <summary>
        /// Begins or continues playback of an animation.
        /// </summary>
        public void PlayAnimation(Animation animation)
        {
            // If this animation is already running, do not restart it.
            if (Animation == animation)
                return;

            // Start the new animation.
            Animation = animation;
            FrameIndex = 0;
            time = 0.0f;
        }

        /// <summary>
        /// Advances the time position and draws the current frame of the animation.
        /// </summary>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, SpriteEffects spriteEffects)
        {
            if (Animation == null)
                throw new NotSupportedException("No animation is currently playing.");

            // Process passing time.
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (time > Animation.FrameTime)
            {
                time -= Animation.FrameTime;

                // Advance the frame index; looping or clamping as appropriate.
                if (Animation.IsLooping)
                {
                    FrameIndex = (FrameIndex + 1) % Animation.FrameCount;
                }
                else
                {
                    FrameIndex = Math.Min(FrameIndex + 1, Animation.FrameCount - 1);
                }
            }

            // Calculate the source rectangle of the current frame.
            Rectangle source = new Rectangle(FrameIndex * Animation.Texture.Height, 0, Animation.Texture.Height, Animation.Texture.Height);

            // Draw the current frame.
            Vector2 origin = new Vector2(Math.Max(0, Animation.FrameWidth - 16), Math.Max(0, Animation.FrameHeight - 16));
            spriteBatch.Draw(Animation.Texture, position, source, Color.White, 0.0f, origin, 1.0f, spriteEffects, 0.0f);
        }
    }
}
