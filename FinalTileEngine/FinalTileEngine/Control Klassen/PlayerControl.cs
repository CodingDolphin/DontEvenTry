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
    class PlayerControl
    {
        //Klassen Variablen

        InputManager input;
        Camera cam;
        Texture2D crossHair;
        Vector2 mousePos;

        //Konstruktor

        public PlayerControl (InputManager input,Camera cam)
        {
            this.input = input;
            this.cam = cam;
        }

        //LoadContent

        public void LoadContent(ContentManager content, string assetName)
        {
            crossHair = content.Load<Texture2D>(assetName);
        }

        //Bewegung Updaten
        
        public void Update(Player player)
        {
            if (input.keyPressed(Keys.W))
                player.moveUp();

            if (input.keyPressed(Keys.S))
                player.moveDown();

            if (input.keyPressed(Keys.A))
                player.moveLeft();

            if (input.keyPressed(Keys.D))
                player.moveRight();

            if (input.keyPressed(Keys.LeftControl))
                player.shootBullet(input.currentMousePos(cam));

            if (input.keyPressedShort(Keys.N))
                player.setNoClip();

            mousePos = input.currentMousePos(cam);
        }

        //CrossHair zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(crossHair, new Rectangle((int)mousePos.X, (int)mousePos.Y, crossHair.Width, crossHair.Height), null, Color.White, 0, new Vector2(crossHair.Width / 2, crossHair.Height / 2), SpriteEffects.None, 0f);
        }
    }
}
