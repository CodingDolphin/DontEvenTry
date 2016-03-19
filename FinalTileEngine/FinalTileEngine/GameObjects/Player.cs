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
   public class Player : GameObject
    {

      //Objekte Deklarieren

      public HUD hud;
      public Projectiles projectiles;
      public SpriteFont font;
      public Texture2D crossHair;
      public Vector2 mousePos;
      double shootSpeed { get; set; }
      double dmgTime { get; set; }

      //Konstruktor

       public Player(Projectiles projectiles)
       {
           this.projectiles = projectiles;
           hud = new HUD();
           noClip = false;
           speed = 50 * 60;
           collRect = new Rectangle(1000, 300, 64, 64);
           position = new Vector2(1000, 300);
           shootSpeed = 1f;
           bulletCollision = false;
           currentHealth = 10000;
           maxHealth = 10000;
           isDeath = false;

           //Animation Initialisieren

           currentAnimation = new Animation(0.30f, 2, 64, 64);
       }

       //Load Content

       public override void LoadContent(ContentManager content, string assetName)
       {
           currentAnimation.animationSheet = content.Load<Texture2D>(assetName);
           hud.LoadContent(content,"MenuGraphics/WhitePixel");
       }

       //Spieler Aktualisieren

       public override void Update(GameTime gameTime)
       {
           //Alte Position sichern

           oldPosition.X = (int)position.X;
           oldPosition.Y = (int)position.Y;
           oldCollRect = collRect;

           currentAnimation.Update(gameTime, position);
           hud.Update(this);

           //Bewegung berechnen

           deltaTime = gameTime.ElapsedGameTime.TotalSeconds;
           velocity = velocity / 0.15f * (float)deltaTime;

           //Neue Position setzen

           position.X += (int)velocity.X * (float)deltaTime;
           position.Y += (int)velocity.Y * (float)deltaTime;
           collRect.X = (int)position.X;
           collRect.Y = (int)position.Y;

           changeHP();

           shootSpeed += gameTime.ElapsedGameTime.TotalSeconds;
           dmgTime += gameTime.ElapsedGameTime.TotalSeconds;
       }

       //Spieler HP ändern

       public void changeHP()
       {
           if (bulletCollision == true)
           {
               currentAnimation.currentColor = Color.Red;
           }

           if (dmgTime > 0.5f)
           {
               currentAnimation.currentColor = Color.White;
               bulletCollision = false;
               dmgTime = 0f;
           }
       }

       //Bewegungsmethoden

       public void moveUp()
       {
           velocity = new Vector2(velocity.X, -speed);
       }

       public void moveDown()
       {
           velocity = new Vector2(velocity.X, speed);
       }

       public void moveLeft()
       {
           velocity = new Vector2(-speed,velocity.Y);
       }

       public void moveRight()
       {
           velocity = new Vector2(speed, velocity.Y);
       }

       //Säure Schuss erstellen

       public void shootBullet(Vector2 mousePos)
       {
           if (shootSpeed > 0.25f)
           {
               this.mousePos = mousePos;
               shootSpeed = 0f;
               Vector2 origin = new Vector2(oldPosition.X + collRect.Width / 2, oldPosition.Y + collRect.Height / 2);
               Vector2 target = new Vector2(mousePos.X, mousePos.Y);

               projectiles.createSpit(origin, target, velocity, this);
           }
       }

       //Spieler Zeichnen

       public override void Draw(SpriteBatch spriteBatch)
       {
           currentAnimation.Draw(spriteBatch);
       }
    }
}
