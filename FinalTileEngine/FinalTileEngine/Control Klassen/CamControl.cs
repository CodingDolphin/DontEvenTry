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
    class CamControl
    {
        //Klassen Variablen

        InputManager input;

        //Konstruktor

        public CamControl (InputManager input)
        {
            this.input = input;
        }

        //Bewegung Updaten
        
        public void Update(Camera cam)
        {
            if (input.keyPressed(Keys.Up))
                cam.moveUp();

            if (input.keyPressed(Keys.Down))
                cam.moveDown();

            if (input.keyPressed(Keys.Left))
                cam.moveLeft();

            if (input.keyPressed(Keys.Right))
                cam.moveRight();

            if (input.keyPressed(Keys.PageUp))
                cam.zoomIn();

            if (input.keyPressed(Keys.PageDown))
                cam.zoomOut();

            if (input.keyPressed(Keys.Q))
                cam.rotateLeft();

            if (input.keyPressed(Keys.E))
                cam.rotateRight();

            if (input.keyPressed(Keys.Enter))
                cam.resetCamPos();

            if (input.keyPressed(Keys.G))
                cam.followStop();

            if (input.keyPressed(Keys.F))
                cam.camFollow();
        }
    }
}
