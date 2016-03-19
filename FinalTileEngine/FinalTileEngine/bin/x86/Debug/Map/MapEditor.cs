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
    class MapEditor
    {
        //Klassen Variablen

        Texture2D crossHair;
        Rectangle sourceRect;
        List<Texture2D> tileSheets;
        List<Tile> currentList;
        TileMap map;
        Vector2 mousePos;

        //Eigenschaften Tile

        int _tileWidth { get; set; }
        int _tileHeight { get; set; }
        bool _passable { get; set; }
        bool _tileChanged { get; set; }
        bool _isActive { get; set; }

        //Eigenschaften Layer

        int _currentLayer { get; set; }
        int _currentTile { get; set; }
        int _currentIndex { get; set; }
        int _lastIndex { get; set; }
        int _tileRow { get; set; }
        int _tileColum { get; set; }
        int _maxTile { get; set; }
        int _layerCount { get; set; }

        //Konstruktor

        public MapEditor(TileMap map)
        {
            //Variablen zuweisen

            this.map = map;
            this._layerCount = map._layerCount;
            this._tileWidth = Game1.tileWidth;
            this._tileHeight = Game1.tileHeight;
            this._tileRow = Game1.tileRows;
            this._tileColum = Game1.tileColumns;
            this._maxTile = _tileRow * _tileColum - 1;

            //Standardwerte festlegen

            _currentLayer = 0;
            _currentTile = 0;
            _currentIndex = 0;
            _lastIndex = 1;
            _tileChanged = false;
            _isActive = false;
            _passable = true;

            //Textur Atlas ausrechnen

            _tileRow = _currentTile / _tileRow;
            _tileColum = _currentTile % _tileColum;

            //Objekte erstellen

            tileSheets = new List<Texture2D>();
            currentList = new List<Tile>();
            sourceRect = new Rectangle(_tileWidth * _tileColum, _tileHeight * _tileRow, _tileWidth, _tileHeight);
            currentList = map.layerList[_currentLayer];
        }

        //LoadContent

        public void LoadContent(ContentManager content, string crossHair, params string[] paths)
        {
            this.crossHair = content.Load<Texture2D>(crossHair);

            foreach (string path in paths)
            {
                this.tileSheets.Add(content.Load<Texture2D>(path));
            }
        }

        //Update

        public void Update(Vector2 mousePos)
        {
            this.mousePos.X = (float)Math.Floor(mousePos.X / _tileWidth) * _tileWidth;
            this.mousePos.Y = (float)Math.Floor(mousePos.Y / _tileWidth) * _tileWidth;
        }


        //Tile setzen

        public void setTile()
        {
            if (_isActive)
            {

                //Aktuelles Tile herausfinden

                _currentIndex = currentList.FindIndex(item => item.destiRect.X == mousePos.X && item.destiRect.Y == mousePos.Y);

                //Tile ersetzen oder erstellen

                if ((_currentIndex != -1) && (_currentIndex != _lastIndex || _tileChanged))
                {

                    _lastIndex = _currentIndex;
                    _tileChanged = false;

                    //Tile verändern

                    currentList[_currentIndex] = new Tile((int)mousePos.X, (int)mousePos.Y, _tileWidth, _tileHeight, _currentTile, _passable, tileSheets[_currentLayer]);
                }
                else if (_currentIndex == -1 && _currentIndex != _lastIndex)
                {
                    //Neues Tile erstellen

                    currentList.Add(new Tile((int)mousePos.X, (int)mousePos.Y, _tileWidth, _tileHeight, _currentTile, _passable, tileSheets[_currentLayer]));

                    _lastIndex = currentList.Count - 1;
                    _tileChanged = false;
                }
            }
        }

        //Tile Löschen

        public void deleteTile()
        {
            if (_isActive)
            {
                _currentIndex = currentList.FindIndex(item => item.destiRect.X == mousePos.X && item.destiRect.Y == mousePos.Y);

                if (_currentIndex != -1)
                {
                    map.layerList[_currentLayer].RemoveRange(_currentIndex, 1);
                }
            }
        }

        //Nächstes Tile auswählen

        public void nextTile()
        {
            _currentTile++;

            if (_currentTile > _maxTile)
                _currentTile = 0;

            _tileChanged = true;
        }

        //Vorheriges Tile auswählen

        public void prevTile()
        {
            _currentTile--;

            if (_currentTile < 0)
                _currentTile = _maxTile;

            _tileChanged = true;
        }

        //Collision einstellen

        public void changePassable()
        {
            if (_passable == true)
                _passable = false;
            else
                _passable = true;

            _tileChanged = true;
        }

        //Layer wechseln
        
        public void switchLayer()
        {
            _currentLayer++;

            if (_currentLayer >= _layerCount)
                _currentLayer = 0;

            currentList = map.layerList[_currentLayer];
        }

        //Editior einschalten / ausschalten

        public void activateEditor()
        {
            if (_isActive == false)
                _isActive = true;
            else
                _isActive = false;
        }

        //Map Speichern

        public void saveMap()
        {
            map.SaveMap();
        }

        //Editor Hud zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_isActive)
            spriteBatch.Draw(crossHair, mousePos, Color.White);
        }
    }
}
