using System;
using System.Collections.Generic;
using System.Linq;
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
    class PlayScreen : GameScreen
    {
        //Objekte Deklarieren

        TileMap map;
        MapEditor editor;
        Camera cam;
        Collision collision;

        //Control Klassen Deklaration

        EditorControl editorControl;
        PlayerControl playerControl;
        PlayScreenControl playScreenControl;
        CamControl camControl;
        InputManager input;

        //Game Objekte

        List<GameObject> gamePlayObjects;
        List<GameObject> removeList;
        Projectiles projectiles;
        Player player;
        Enemy enemy;

        //Konstruktor

        public PlayScreen()
        {
            input = new InputManager();
            cam = new Camera(ScreenManager.Instance.viewPort);
            map = new TileMap(2);
            projectiles = new Projectiles();
            collision = new Collision(map, projectiles);
            editor = new MapEditor(map);
            editorControl = new EditorControl(input);
            playerControl = new PlayerControl(input, cam);
            playScreenControl = new PlayScreenControl(input);
            camControl = new CamControl(input);

            //GamePlay Objects

            gamePlayObjects = new List<GameObject>();
            removeList = new List<GameObject>();
            gamePlayObjects.Add(projectiles);
            gamePlayObjects.Add(player = new Player(projectiles));

            for (int i = 0; i < 20; i++)
            {
                gamePlayObjects.Add(enemy = new Enemy(player, projectiles));
            }
        }

        //Konstruktor

        public override void LoadContent(ContentManager content)
        {
            //Path zur Map erstellen und einlesen

            string path = Path.Combine(content.RootDirectory, "MapLayers/" + "LayerData1" + ".map");
            string path2 = Path.Combine(content.RootDirectory, "MapLayers/" + "LayerData2" + ".map");

            map.ReadMap(path, path2);

            //Map und TileSheet laden

            map.LoadContent(content, "MapGraphics/Layer1", "MapGraphics/Layer2");
            editor.LoadContent(content, "MapGraphics/EditorCrossHair", "MapGraphics/Layer1", "MapGraphics/Layer2"); 
       
            //GameObject Content Laden

            foreach(GameObject gameObject in gamePlayObjects)
            {
                if(gameObject is Enemy)
                    gameObject.LoadContent(content, "GameObjectGraphics/BugAnimation");

                if(gameObject is Player)
                    player.LoadContent(content, "GameObjectGraphics/BugAnimation");

                if(gameObject is Projectiles)
                    projectiles.LoadContent(content, "GameObjectGraphics/BulletGraphics/AcidShot");
            }

            //Player CrossHair

            playerControl.LoadContent(content, "GameObjectGraphics/AimCrossHair");
        }

        //PlayScreen Aktualisieren

        public override void Update(GameTime gameTime)
        {
            //Control Klassen Updaten

            input.Update();
            editorControl.Update(editor);
            camControl.Update(cam);
            playerControl.Update(player);
            playScreenControl.Update(this);

            //Game Objekte Updaten

            foreach (GameObject gameObject in gamePlayObjects)
            {
                collision.checkMapCollision(gameObject);

                if (gameObject is Enemy)
                {
                    collision.checkBulletCollision(gameObject);
                    if (gameObject.currentHealth <= 0)
                        removeList.Add(gameObject);
                }

                if (gameObject is Player)
                {
                    collision.checkBulletCollisionPlayer(gameObject);
                    if (gameObject.currentHealth <= 0)
                        ScreenManager.instance.activateMenuScreen();
                }

                collision.checkMapCollision(gameObject);
                gameObject.Update(gameTime);

            }

            foreach (GameObject go in removeList)
            {
                gamePlayObjects.Remove(go);
            }
          
            cam.Update(player);
            editor.Update(input.currentMousePos(cam));
        }

        //Zurück zum Menu

        public void backToMenu()
        {
            ScreenManager.Instance.activateMenuScreen();
        }

        //PlayScreen Zeichnen

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.transform);
            map.Draw(spriteBatch);
            editor.Draw(spriteBatch);

            //GameObjekte Zeichnen

            foreach (GameObject gameObject in gamePlayObjects)
            {
                if(gameObject.isDeath != true)
                gameObject.Draw(spriteBatch);           
            }

            //Crosshair Zeichnen

            playerControl.Draw(spriteBatch);
            spriteBatch.End();

            //Hud nach Kamera zeichnen

            spriteBatch.Begin();
            player.hud.Draw(spriteBatch);
            spriteBatch.End();
            
        }
    }
}
