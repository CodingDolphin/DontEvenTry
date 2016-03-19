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
    class Spit : Projectiles
    {
        //Konstruktor

        public Spit(Animation bulletAnimation, Vector2 origin, Vector2 target, Vector2 currentVelocity, GameObject source)
        {
            this.source = source;
            this.bulletAnimation = bulletAnimation;
            this.position = origin;
            this.speed = 500f;
            this.collRect = new Rectangle((int)origin.X, (int)origin.Y, 32, 32);

            //Richtung errechnen

            this.direction = target - origin;
            this.direction.Normalize();
        }

        public override void LoadContent(ContentManager content, string assetName)
        {
            base.LoadContent(content, assetName);
        }

        //Flugbahn Updaten

        public override void Update(GameTime gameTime)
        {
            position.X += direction.X * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.Y += direction.Y * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            collRect.X = (int)position.X;
            collRect.Y = (int)position.Y;

            bulletAnimation.Update(gameTime, this.position);
        }

        //Bullet Animation zeichnen

        public override void Draw(SpriteBatch spriteBatch)
        {
            bulletAnimation.Draw(spriteBatch);
        }
    }
}

