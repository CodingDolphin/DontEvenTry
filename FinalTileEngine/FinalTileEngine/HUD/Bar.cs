using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FinalTileEngine
{
    class Bar
    {
        //Größe und Position festlegen

        public Vector2 position;
        int sizeX, sizeY;
        int currentValue, maxValue;

        //Bar Texture

        public Texture2D barTexture;

        //Größe der Bar festlegen

        Rectangle maxRecBar;
        Rectangle currentRecBar;

        //Farben Festlegen

        Color currentColor;
        Color maxColor;

        //Konstruktor

        public Bar(Vector2 pos, int _sizeX,int _sizeY, Color _currentColor,Color _maxColor)
        {
            this.position = pos;
            this.sizeX = _sizeX;
            this.sizeY = _sizeY;
            this.currentColor = _currentColor;
            this.maxColor = _maxColor;
        }

        //Load Content

        public void LoadContent(ContentManager content, string assetName)
        {
            barTexture = content.Load<Texture2D>(assetName);
            
        }

        //Werte Aktualisieren

        public void Update(int _currentValue,int _maxValue, Vector2 position)
        {
            currentValue = _currentValue;
            maxValue = _maxValue;

            this.position = position;

            currentValue = (int)MathHelper.Clamp(currentValue, 0, maxValue);

            maxRecBar = new Rectangle((int)position.X,(int)position.Y, sizeX, sizeY);
            currentRecBar = new Rectangle((int)position.X, (int)position.Y, (int)(sizeX / ((maxValue / (float)currentValue))), sizeY);
        }

        //Werte Aktualisieren

        public void Update(Player player)
        {
            currentValue = (int)player.currentHealth;
            maxValue = (int)player.maxHealth;

            currentValue =(int) MathHelper.Clamp(currentValue, 0, maxValue);

            maxRecBar = new Rectangle((int)position.X, (int)position.Y, sizeX, sizeY);
            currentRecBar = new Rectangle((int)position.X, (int)position.Y, (int)(sizeX / ((maxValue / (float)currentValue))), sizeY);
        }

        //Bar Zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barTexture, maxRecBar, maxColor);
            spriteBatch.Draw(barTexture, currentRecBar, currentColor);
        }
    }
}
