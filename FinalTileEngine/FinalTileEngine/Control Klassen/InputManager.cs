using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace FinalTileEngine
{
    class InputManager
    {
        //Klassen Variablen

        KeyboardState keyState;
        KeyboardState prevKeyState;

        MouseState mouseState;
        MouseState prevMouseState;
        Vector2 mousePos;
      
        //Tastatur Status abrufen

        public void Update()
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        //Tasten Schnell und Flüssig

        public bool keyPressed(Keys key)
        {
            return keyState.IsKeyDown(key);
        }

        //Tasten zum 1 x Aktivieren

        public bool keyPressedShort(Keys key)
        {
            if (keyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                return true;
            else
                return false;
        }

        //Mehrere Tasten 1 x drücken

        public bool keysPressedShort(params Keys[] keys)
        {  
            foreach (Keys key in keys)
            {
                if (keyState.IsKeyDown(key))
                    return true;
            }

            return false;
        }

        //Maus Position in Relation zur Kamera

        public Vector2 currentMousePos(Camera cam)
        {
            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y;
            mousePos = Vector2.Transform(mousePos, Matrix.Invert(cam.transform));

            return mousePos;
        }

        //Maus ohne Matrix Relation

        public Vector2 currentMousePos()
        {
            mousePos.X = mouseState.X;
            mousePos.Y = mouseState.Y;

            return mousePos;
        }

        //Linke Taste dauernd Aktiv

        public bool leftMouseClicked()
        {
            return (mouseState.LeftButton == ButtonState.Pressed);
        }

        //Linke Taste 1 x Clicken

        public bool leftMouseClickedShort()
        {
            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                return true;
            else
                return false;
        }

        //Rechte Taste 1 x Clicken

        public bool rightMouseClicked()
        {
            return (mouseState.RightButton == ButtonState.Pressed);
        }

    }
}
