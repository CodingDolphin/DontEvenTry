using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FinalTileEngine
{
   public class HUD
    {
        //Objekte Deklarieren

        Bar playerHpBar;

       //Konstruktor

        public HUD()
        {
            playerHpBar = new Bar(new Vector2(20, 20), 300,20,Color.Red,Color.DarkRed);   
        }

       //Load Content

        public void LoadContent(ContentManager content, string assetName)
        {
            playerHpBar.LoadContent(content, assetName);
        }

       //Bar Aktualisieren

        public void Update(Player player)
        {
            playerHpBar.Update(player);
        }

       //Bar Zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            playerHpBar.Draw(spriteBatch);
        }

    }
}
