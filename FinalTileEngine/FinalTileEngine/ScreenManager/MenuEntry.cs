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
    class MenuEntry
    {
        //KlassenVariablen

        public Vector2 textPosition;
        public int width;
        public int height;

        //Eigenschaften

        public  string _entryString { get; set; }
        public  float _scale { get; set; }
        public  bool isSelected { get; set; }
        public  float pulseFrequency { get; set; }
        public int _entryIndex { get; set; }
        public float _pulseValue { get; set; }

        //Konstruktor

        public MenuEntry(string entryString, int index)
        {
            this._entryString = entryString;
            this._entryIndex = index;
            this._scale = 1f;
            this._pulseValue = 0.005f;
        }

        //Update

        public void Update(Vector2 mousePos)
        {
            Rectangle selected = new Rectangle((int)textPosition.X, (int)textPosition.Y, width, height);
            Rectangle mouseRect = new Rectangle((int)mousePos.X,(int)mousePos.Y,2,2);

            if (selected.Intersects(mouseRect))
            {
                this.isSelected = true;
                this.pulse();
            }
            else
            {
                _scale = 1f;
                this.isSelected = false;
            }
        }

        //Pulse Animation

        public void pulse()
        {
            if (isSelected)
            {
                _scale = MathHelper.Clamp(_scale += _pulseValue, 0.95f, 1.05f);

                if (_scale == 1.05f)
                    _pulseValue = _pulseValue * -1;

                if (_scale == 0.95f)
                    _pulseValue = _pulseValue * -1;
            }
            else
            {
                _scale = 1f;
            }
        }
    }
}
