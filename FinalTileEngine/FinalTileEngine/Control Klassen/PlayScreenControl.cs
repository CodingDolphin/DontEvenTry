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
    class PlayScreenControl
    {
        //Klassen Variablen

        InputManager input;

        //Konstruktor

        public PlayScreenControl(InputManager input)
        {
            this.input = input;
        }

        //Bewegung Updaten

        public void Update(PlayScreen playScreen)
        {
            if (input.keyPressed(Keys.Escape))
                playScreen.backToMenu();
        }
    }
}