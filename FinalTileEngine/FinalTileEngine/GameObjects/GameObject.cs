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
    public abstract class GameObject : ICollide, IFocus, ICloneable
    {
        //Enums

       public enum status
        {
            activ,
            inactiv
        }

       //Animation

       protected Animation currentAnimation;

       //Collision Rectangle und Geschwindigkeit

       public Rectangle collRect;
       public Rectangle oldCollRect;
       public Vector2 oldPosition;
       public Vector2 position;
       public Vector2 velocity;
       public float speed;
       public double deltaTime;
       protected bool bulletCollision { get; set; }
       public bool isDeath { get; set; }

       //Rotation Scale

       protected float rotation;
       protected float scale;

       //Status

       public int currentHealth;
       public int maxHealth;
       protected bool noClip;

       //Load Content

       public virtual void LoadContent(ContentManager content, string assetName)
       {
       }

       //Game Objekte Updaten

       public virtual void Update(GameTime gameTime)
       {
       }

       //Game Objekte Zeichnen

       public virtual void Draw(SpriteBatch spriteBatch)
       {
       }

       //ICollide implementieren

       public virtual Rectangle getCollisionRect()
       {
           return collRect;
       }

       public virtual void setOldPos()
       {
           if (noClip == false)
           {
               position = oldPosition;
               collRect = oldCollRect;
           }
       }

        //Pixel Collision

       public virtual Texture2D getTexture()
       {
           return currentAnimation.animationSheet;
       }

       public virtual Rectangle getSourceRect()
       {
           return currentAnimation.sourceRect;
       }

        //NoClip Mode

       public virtual void setNoClip()
       {
           if (noClip == false)
               noClip = true;
           else
               noClip = false;
       }

        //BulletCollision

       public void gotHit()
       {
           bulletCollision = true;
           currentHealth -= 10;
       }

        //IFocus implementieren

       public virtual Vector2 getFocus()
       {
           return oldPosition;
       }

       public virtual float getWidth()
       {
           return collRect.Width;
       }

       public virtual float getHeight()
       {
           return collRect.Height;
       }

       public object Clone()
       {
           GameObject tile = (GameObject)this.MemberwiseClone();
           return tile;
       }
    }
}
