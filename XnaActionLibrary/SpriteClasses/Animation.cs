﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace XnaActionLibrary.SpriteClasses
{
    public class Animation
    {
        #region Fields

        public Texture2D spriteStrip; // The image representing the collection of images used for animation
        float scale; // The scale used to display the sprite strip
        public float elapsedTime; // The time since we last updated the frame
        int frameTime; // The time we display a frame until the next one
        int frameCount; // The number of frames that the animation contains
        public int currentFrame; // The index of the current frame we are displaying
        Color color; // The color of the frame we will be displaying
        Rectangle sourceRect = new Rectangle(); // The area of the image strip we want to display
        Rectangle destinationRect = new Rectangle(); // The area where we want to display the image strip in the game
        public int FrameWidth; // Width of a given frame
        public int FrameHeight; // Height of a given frame
        public bool isActive; // The state of the Animation
        public bool Looping;  // Determines if the animation will keep playing or deactivate after one run
        public Vector2 Position;

        public Rectangle SourceRectangle
        {
            get { return sourceRect; }
        }

        #endregion

        #region Constructor

        public Animation(Texture2D texture, Vector2 position, int frameWidth, int frameHeight, int frameCount, int frametime, Color color,
                                float scale, bool looping)
        {
            // Keep a local copy of the values passed in
            this.color = color;
            this.FrameWidth = frameWidth;
            this.FrameHeight = frameHeight;
            this.frameCount = frameCount;
            this.frameTime = frametime;
            this.scale = scale;


            Looping = looping;
            Position = position;
            spriteStrip = texture;


            // Set the time to zero
            elapsedTime = 0;
            currentFrame = 0;


            // Set the Animation to active by default
            isActive = true;
        }

        #endregion

        #region Update

        public void Update(GameTime gameTime)
        {
            // Do not update the game if we are not active
            if (isActive == false)
                return;


            // Update the elapsed time
            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;


            // If the elapsed time is larger than the frame time
            // we need to switch frames
            if (elapsedTime > frameTime)
            {
                // Move to the next frame
                currentFrame++;


                // If the currentFrame is equal to frameCount reset currentFrame to zero
                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    // If we are not looping deactivate the animation
                    if (Looping == false)
                        isActive = false;
                }


                // Reset the elapsed time to zero
                elapsedTime = 0;
            }


            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            sourceRect = new Rectangle(currentFrame * FrameWidth, 0, FrameWidth, FrameHeight);


            // Grab the correct frame in the image strip by multiplying the currentFrame index by the frame width
            destinationRect = new Rectangle((int)Position.X - (int)(FrameWidth * scale) / 2,
            (int)Position.Y - (int)(FrameHeight * scale) / 2,
            (int)(FrameWidth * scale),
            (int)(FrameHeight * scale));
        }

        #endregion

    }
}
