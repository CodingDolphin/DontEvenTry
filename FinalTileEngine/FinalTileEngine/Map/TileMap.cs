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
    class TileMap
    {
        //Tile Liste

        public List<List<Tile>> layerList;
        public int _layerCount { get; set; }
        public int _iterationIndex { get; set; }

        //Tile Größe

        int _tileWidth { get; set; }
        int _tileHeight { get; set; }

        //Map Größe

        int _mapWidth { get; set; }
        int _mapHeight { get; set; }

        //Map Speicherort

        string[] _paths { get; set; }

        //Konstruktor

        public TileMap(int layerCount)
        {
            this._layerCount = layerCount;
            _paths = new string[_layerCount];
            layerList = new List<List<Tile>>();

            for (int i = 0; i < _layerCount; i++)
            {
                layerList.Add(new List<Tile>());
            }
        }

        //Load Content

        public void LoadContent(ContentManager content, params string[] assetName)
        {
            for (int i = 0; i < _layerCount; i++)
            {
                foreach (Tile tile in layerList[i])
                {
                    tile.LoadContent(content, assetName[i]);
                }
            }
        }

        //Map von Datei lesen und Tiles Erstellen

        public void ReadMap(params string[] path)
        {
            for (int i = 0; i < _layerCount; i++)
            {

                //Path an TileMap übergeben

                _paths[i] = path[i];

                //Datei auslesen

                string[] map = File.ReadAllLines(_paths[i]);
                string[] tempData = new string[map.Length];

                //Jede Zeile auslesen und Objekt erstellen

                for (int j = 0; j < map.Length; j++)
                {
                    tempData = map[j].Split(',');

                    #region Werte aus Temp Array holen

                    int x = Convert.ToInt32(tempData[0]);
                    int y = Convert.ToInt32(tempData[1]);
                    int width = Convert.ToInt32(tempData[2]);
                    int height = Convert.ToInt32(tempData[3]);
                    int index = Convert.ToInt32(tempData[4]);
                    int isNotPassable = 1;
                    bool passable = isNotPassable != Convert.ToInt32(tempData[5]);

                    #endregion

                    layerList[i].Add(new Tile(x, y, width, height, index, passable));
                }
            }
        }

        //Map in Datei schreiben

        public void SaveMap()
        {
            for (int i = 0; i < _layerCount; i++)
            {
                StreamWriter write = new StreamWriter(_paths[i]);

                //Alle Objekte via ToString in Datei schreiben

                foreach (Tile tile in layerList[i])
                {
                    write.WriteLine(tile);
                }

                write.Flush();
                write.Close();
            }
        }

        //Map Aktualisieren

        public void Update()
        {
        }

        //Map Zeichnen

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _layerCount; i++)
		    {
                foreach (Tile tile in layerList[i])
                {
                    tile.Draw(spriteBatch);
                }
            }
        }
    }
}
