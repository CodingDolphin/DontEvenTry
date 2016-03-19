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
    class Menu : GameScreen
    {
        //Objekte Deklarieren

        Texture2D menuBG;
        Texture2D gameNameText;
        Texture2D cursor;
        InputManager input;
        MenuControl menuControl;
        SpriteFont menuText;
        List<MenuEntry> menuEntrys;

        public Menu()
        {
            //Objekte erstellen

            input = new InputManager();
            menuControl = new MenuControl(input);
            menuEntrys = new List<MenuEntry>();

            //Menü Einträge hier einfügen

            menuEntrys.Add(new MenuEntry("Beginne dein Ende", 0));
            menuEntrys.Add(new MenuEntry("Optionen", 1));
            menuEntrys.Add(new MenuEntry("Highscore", 2));
            menuEntrys.Add(new MenuEntry("Beenden", 3));
        }

        //Load Content

        public override void LoadContent(ContentManager content)
        {
            menuBG = content.Load<Texture2D>("MenuGraphics/MenuBg");
            menuText = content.Load<SpriteFont>("Fonts/LeagueFont");
            gameNameText = content.Load<Texture2D>("MenuGraphics/GameLogo");
            cursor = content.Load<Texture2D>("GameObjectGraphics/AimCrossHair");

            //Menü Einträge Abstände berechnen

            foreach (MenuEntry entry in menuEntrys)
            {
                Vector2 size = menuText.MeasureString(entry._entryString);
                entry.width = (int)size.X;
                entry.height = (int)size.Y;

                if (entry._entryIndex != 0)
                {
                    Vector2 position = menuText.MeasureString(entry._entryString);
                    entry.textPosition.Y += position.Y * entry._entryIndex + 135;
                }
                else
                {
                    entry.textPosition.Y += 135;
                }
                entry.textPosition.X += 25;
            }
        }

        //Menu Updaten

        public override void Update(GameTime gameTime)
        {
            input.Update();
            menuControl.Update(this);

            foreach (MenuEntry entry in menuEntrys)
            {
                entry.Update(input.currentMousePos());
            }

        }

        //Menu Kontrolle

        public void selectEntry()
        {
            foreach (MenuEntry entry in menuEntrys)
            {
                if(entry.isSelected && entry._entryIndex == 0)
                    ScreenManager.Instance.activatePlayScreen();

                if (entry.isSelected && entry._entryIndex == 3)
                    ScreenManager.Instance.game.Exit();
            }
        }

        //Zeichne Menu

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(menuBG, new Rectangle(0, 0, 1280, 720), Color.White);

            foreach (MenuEntry entry in menuEntrys)
            {
                spriteBatch.DrawString(menuText, entry._entryString, entry.textPosition,new Color(220,0,0),0,Vector2.Zero,entry._scale,SpriteEffects.None,0.0f);
            }

            spriteBatch.Draw(gameNameText, new Rectangle(ScreenManager.Instance.viewPort.Width / 2 - (gameNameText.Width / 2), 20,gameNameText.Width ,gameNameText.Height ), Color.White);
            spriteBatch.Draw(cursor, input.currentMousePos(), Color.Black);

            spriteBatch.End();
        }
    }
}
