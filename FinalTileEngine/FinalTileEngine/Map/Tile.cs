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
    class Tile : ICloneable
    {
        //Klassen Variablen

        public Texture2D tileTex;
        public Rectangle sourceRect;
        public Rectangle destiRect;

        //Klassen Eigenschaften

        int _tileWidth { get; set; }
        int _tileHeight { get; set; }
 public int _index { get; set; }
        int _tileRow { get; set; }
        int _tileColum { get; set; }
        public bool _passable { get; set; }

        //Konstruktor

        public Tile(int x, int y, int tileWidth, int tileHeight, int index, bool passable)
        {
            this._tileWidth = tileWidth;
            this._tileHeight = tileHeight;
            this._index = index;
            this._passable = passable;
            _tileRow = Game1.tileRows;
            _tileColum = Game1.tileColumns;

            //Reihe und Spalte festlegen für Source Rectangel

            _tileRow = index / _tileRow;
            _tileColum = index % _tileColum;

            //Source und Destination Rechteck erstellen

            destiRect = new Rectangle(x, y, tileWidth, tileHeight);
            sourceRect = new Rectangle(tileWidth * _tileColum, tileHeight * _tileRow, tileWidth, tileHeight);
        }

        //Konstruktor überladung mit Textur

        public Tile(int x, int y, int tileWidth, int tileHeight, int index, bool passable, Texture2D tileTex)
        {
            this._tileWidth = tileWidth;
            this._tileHeight = tileHeight;
            this._index = index;
            this._passable = passable;
            this.tileTex = tileTex;
            _tileRow = Game1.tileRows;
            _tileColum = Game1.tileColumns;

            _tileRow = index / _tileRow;
            _tileColum = index % _tileColum;

            //Source und Destination Rechteck erstellen

            destiRect = new Rectangle(x, y, tileWidth, tileHeight);
            sourceRect = new Rectangle(tileWidth * _tileColum, tileHeight * _tileRow, tileWidth, tileHeight);
        }

        //Load Content

        public void LoadContent(ContentManager content, string assetName)
        {
           tileTex = content.Load<Texture2D>(assetName);
        }

        //Objekt als String ausgeben

        public override string ToString()
        {
            int passable;

            if (_passable == true)
                passable = 0;
            else
                passable = 1;

            return destiRect.X + "," + destiRect.Y + "," + _tileWidth + "," + _tileHeight + "," + _index + "," + passable;
        }

        //Tile Aktualisieren

        public void Update()
        {
        }

        //Tile Zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileTex, destiRect, sourceRect, Color.White);
        }

        //Tile Clonen für die Undo Funktion

        public object Clone()
        {
            Tile tile = (Tile)this.MemberwiseClone();
            return tile;
        }
    }
}
