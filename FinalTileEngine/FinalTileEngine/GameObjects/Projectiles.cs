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
    public class Projectiles : GameObject
    {
        //Klassen Variablen

        public GameObject source;
        protected Vector2 direction;
        protected Texture2D bulletSheet;
        protected Animation bulletAnimation;
        protected double timeToLife;
        public List<Projectiles> projectileList;

        //Konstruktor

        public Projectiles()
        {
            projectileList = new List<Projectiles>();
            isDeath = false;
        }

        //Load Content

        public override void LoadContent(ContentManager content, string assetName)
        {
            bulletSheet = content.Load<Texture2D>(assetName);
        }

        //Projektile Aktualisieren

        public override void Update(GameTime gameTime)
        {
            foreach (Projectiles bullet in projectileList)
            {
                bullet.Update(gameTime);
            }
        }

        //Neuen Säure schuss erstellen

        public void createSpit(Vector2 origin, Vector2 target,Vector2 currentVelocity, GameObject source)
        {
            Animation newBulletAnimation = new Animation(0.1f,3,16,16);
            newBulletAnimation.animationSheet = bulletSheet;

            projectileList.Add(new Spit(newBulletAnimation, origin, target, currentVelocity, source));
        }

        //Projektile Zeichnen

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectiles bullet in projectileList)
            {
                bullet.Draw(spriteBatch);
            }
        }
    }
}
