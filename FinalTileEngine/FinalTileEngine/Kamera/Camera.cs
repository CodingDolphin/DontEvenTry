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
    class Camera
    {

        //Klassen Variablen

        public Matrix transform;
        Viewport view;
        public Vector2 center;

        //Eigenschaften

        float _width { get; set; }
        float _height { get; set; }
        float _rotation { get; set; }
        float _zoom { get; set; }
        float _camSpeed { get; set; }
        bool _follow { get; set; }

        //Konstruktor
        
        public Camera(Viewport newView)
        {
            view = newView;
            _zoom = 1f;
            _rotation = 0f;
            center = new Vector2(0, 0);
            _camSpeed = 5f;
            _follow = true;
        }

        #region Kamera Kontrolle

        public void camFollow()
        {
            _follow = true;
        }

        public void followStop()
        {
            _follow = false;
        }

        public void moveRight()
        {
            center.X += _camSpeed;
        }

        public void moveLeft()
        {
            center.X -= _camSpeed;
        }

        public void moveUp()
        {
            center.Y -= _camSpeed;
        }

        public void moveDown()
        {
            center.Y += _camSpeed;
        }

        public void zoomIn()
        {
             _zoom += 0.03f;
        }

        public void zoomOut()
        {
            _zoom -= 0.03f;
        }

        public void rotateLeft()
        {
            _rotation -= 0.05f;
        }
        
        public void rotateRight()
        {
            _rotation += 0.05f;
        }

        public void resetCamPos()
        {
            _follow = true;
            _rotation = 0f;
            _zoom = 1f;
        }

        #endregion

        //Camera Position Updaten

        public void Update(IFocus focus)
        {
 
            if(_follow == true)
            center = focus.getFocus();
            _width = focus.getWidth() / 2;
            _height = focus.getHeight() / 2;

            transform = Matrix.CreateTranslation(new Vector3(-center.X - _width, -center.Y - _height, 0)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(_zoom) *
                Matrix.CreateTranslation(new Vector3(view.Width / 2, view.Height / 2, 0));
        }
    }
}
