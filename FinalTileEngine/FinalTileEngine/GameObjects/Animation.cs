using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalTileEngine
{
   public class Animation
    {
        //KlassenVariablen

        public Texture2D animationSheet;
        public Rectangle sourceRect;
        public Rectangle destiRect;
        public Color currentColor;

        public int currentFrame { get; set; }
        float frameCycle { get; set; }
        int maxFrame { get; set; }
        float deltaTime { get; set; }
        int width;
        int height;

        //Konstrukor

        public Animation(float frameCycle, int maxFrame,int width, int height)
        {
            this.frameCycle = frameCycle;
            this.maxFrame = maxFrame;
            this.currentFrame = 0;
            this.width = width;
            this.height = height;
            this.currentColor = Color.White;
        }

        //Loadcontent

        public void LoadContent(ContentManager content, string assetName)
        {
            animationSheet = content.Load<Texture2D>(assetName);
        }

        public void Update(GameTime gametime, Vector2 position)
        {
            deltaTime += (float)gametime.ElapsedGameTime.TotalSeconds;

            if (deltaTime >= frameCycle)
            {
                currentFrame++;
                deltaTime = 0f;
            }

            if (currentFrame >= maxFrame)
                currentFrame = 0;

            sourceRect = new Rectangle(currentFrame * width,0 , width, height);
            destiRect = new Rectangle((int)position.X,(int)position.Y, width, height);
        }

        //Zeichne Animation

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animationSheet, destiRect, sourceRect, currentColor);
        }
    }
}
