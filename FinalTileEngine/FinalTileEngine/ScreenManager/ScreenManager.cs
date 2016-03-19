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
    class ScreenManager
    {

        //Singleton

        public static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                    return instance;
            }
        }

        //Klassen Variablen

        public ContentManager content { get; set; }
        public Viewport viewPort { get; set; }
        public int alpha { get; set; }
        Texture2D fadePixel { get; set; }
        public bool transitionActive { get; set; }
        int transitionSpeed{ get; set; }

        //GameScreens

        GameScreen currentScreen;
        GameScreen menuScreen;
        GameScreen playScreen;
        public Game game;
        public GraphicsDeviceManager graphics;
        public ContentManager Content;

        //Initialize Screens

        public void Initialize(Game game,GraphicsDeviceManager graphics)
        {
            //Fade Animation

            alpha = 255;
            transitionSpeed = 5;
            transitionActive = true;  

            //Game Objekt

            this.game = game;
            this.graphics = graphics;
            

            //Screen Objekte erstellen

            menuScreen = new Menu();
            playScreen = new PlayScreen();
            currentScreen = menuScreen;
        }

        //LoadContent

        public void LoadContent(ContentManager content)
        {
            fadePixel = content.Load<Texture2D>("MenuGraphics/BlackPixel");
            this.Content = new ContentManager(content.ServiceProvider, "Content");
            menuScreen.LoadContent(content);
            playScreen.LoadContent(content);
        }

        //Aktuellen Screen Updaten

        public void Update(GameTime gameTime)
        {
            screenTransition();
            currentScreen.Update(gameTime);
        }

        //Transition Method

        public void screenTransition()
        {
            if (transitionActive == true)
            {
                alpha -= transitionSpeed;
            }

            if(alpha <= 0)
            {
                transitionActive = false;
                alpha = 255;
            }
        }

        //CurrentScreen zu PlayScreen wechseln

        public void activatePlayScreen()
        {
            transitionActive = true;
            alpha = 255;
            playScreen = new PlayScreen();
            playScreen.LoadContent(Content);
            currentScreen = playScreen;
        }

        //CurrentScreen zu Menü wechseln

        public void activateMenuScreen()
        {
            transitionActive = true;
            alpha = 255;
            currentScreen = menuScreen;
        }

        //Zeichen Aktuellen Bildschirm

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);

            if (transitionActive == true)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                spriteBatch.Draw(fadePixel,new Rectangle(0,0,1280,720),new Color(255,255,255,alpha));
                spriteBatch.End();
            }
        }
    }
}
