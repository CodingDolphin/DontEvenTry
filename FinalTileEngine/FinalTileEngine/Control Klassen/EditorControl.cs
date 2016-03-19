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
    class EditorControl
    {
        //Klassen Variablen

        InputManager input;

        //Konstruktor

        public EditorControl(InputManager input)
        {
            this.input = input;
        }

        //Bewegung Updaten

        public void Update(MapEditor editor)
        {

            if (input.keyPressedShort(Keys.Add))
                editor.nextTile();

            if (input.keyPressedShort(Keys.Subtract))
                editor.prevTile();

            if (input.keyPressed(Keys.Space))
                editor.setTile();

            if (input.leftMouseClicked())
                editor.setTile();

            if (input.keyPressedShort(Keys.B))
                editor.changePassable();

            if (input.keyPressedShort(Keys.M))
                editor.saveMap();

            if (input.keyPressedShort(Keys.D1))
                editor.switchLayer();

            if (input.keyPressedShort(Keys.Tab))
                editor.activateEditor();

            if (input.rightMouseClicked())
                editor.deleteTile();
        }
    }
}

