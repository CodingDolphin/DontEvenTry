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
    class Enemy : GameObject
    {
        //Enums

        enum state
        {
            lurking,
            chasing,
            idle
       
        }

        enum facing
        {
            up,
            down,
            left,
            right,
            upLeft,
            upRight,
            downLeft,
            downRight,

        }

        //Objekte Deklarieren

        Bar healthBar;
        Player target;
        state currentState;
        facing currentFaceDirection;
        Random rand;
        Projectiles projectiles;
        double shootSpeed { get; set; }
        double dmgTime { get; set; }
        float randCycle { get; set; }

        //Static

        public static int seed;

        //Eigenschaften

        Vector2 difference;

        //Konstruktor

        public Enemy(Player target, Projectiles projectiles)
        {
            //Static erhöhen

            seed++;

            //Kollision größe einstellen

            noClip = false;
            collRect = new Rectangle(350, 350, 64, 64);
            position = new Vector2(350, 350);
            bulletCollision = false;
            isDeath = false;

            //Animation einstellen

            currentAnimation = new Animation(0.30f, 2, collRect.Width, collRect.Height);

            //Target und Geschwindigkeit festlegen

            this.target = target;
            this.projectiles = projectiles;
            speed = 25 * 50f;
            shootSpeed = 1f;
            currentState = state.lurking;
            currentFaceDirection = facing.right;

            //ZufallsGenerator

            rand = new Random(seed);
            randCycle = 0f;
            
            //Leben festlegen

            currentHealth = 300;
            maxHealth = 300;
            healthBar = new Bar(new Vector2((float)collRect.X, (float)collRect.Y), 70, 5, Color.Red, Color.DarkRed);
        }

        //Load Content

        public override void LoadContent(ContentManager content, string assetName)
        {
            currentAnimation.animationSheet = content.Load<Texture2D>(assetName);
            healthBar.barTexture = content.Load<Texture2D>("MenuGraphics/WhitePixel");
        }

        //Aktualisieren

        public override void Update(GameTime gameTime)
        {
            oldPosition.X = (int)position.X;
            oldPosition.Y = (int)position.Y;
            oldCollRect = collRect;
            currentAnimation.Update(gameTime, position);

            deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

            if (state.lurking != currentState)
            {
                difference.X = target.oldPosition.X - position.X;
                difference.Y = target.oldPosition.Y - position.Y;

                velocity.X += MathHelper.Clamp(difference.X * 60, -speed, speed);
                velocity.Y += MathHelper.Clamp(difference.Y * 60, -speed, speed);

                shoot();

                float distant = Vector2.Distance(position, target.position);

                if (distant >= 500f)
                {
                    currentState = state.lurking;
                }
            }
            else
            {
                lurking(gameTime);
            }

            velocity = velocity / 0.15f * (float)deltaTime;


            changeHP();

            position.X += (int)velocity.X * (float)deltaTime;
            position.Y += (int)velocity.Y * (float)deltaTime;
            collRect.X = (int)position.X;
            collRect.Y = (int)position.Y;

            healthBar.Update(currentHealth, maxHealth, oldPosition);
            dmgTime += gameTime.ElapsedGameTime.TotalSeconds;
            shootSpeed += gameTime.ElapsedGameTime.TotalSeconds;
        }

        //Random Movement

        public void lurking(GameTime gameTime)
        {
            randCycle += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (randCycle > 2f)
            {
                int randNumber = rand.Next(0, 9);
                switch (randNumber)
                {
                    case 1: currentFaceDirection = facing.up; break;
                    case 2: currentFaceDirection = facing.down; break;
                    case 3: currentFaceDirection = facing.right; break;
                    case 4: currentFaceDirection = facing.left; break;
                    case 5: currentFaceDirection = facing.upLeft; break;
                    case 6: currentFaceDirection = facing.upRight; break;
                    case 7: currentFaceDirection = facing.downLeft; break;
                    case 8: currentFaceDirection = facing.downRight; break;
                }

                randCycle = 0f;
            }

            switch (currentFaceDirection)
            {
                case facing.up: velocity = new Vector2(velocity.X, -speed); break;
                case facing.down: velocity = new Vector2(velocity.X, speed); break;
                case facing.right: velocity = new Vector2(speed, velocity.Y); break;
                case facing.left: velocity = new Vector2(-speed, velocity.Y); break;
                case facing.upLeft: velocity = new Vector2(-speed, -speed); break;
                case facing.upRight: velocity = new Vector2(speed, -speed); break;
                case facing.downLeft: velocity = new Vector2(-speed, speed); ; break;
                case facing.downRight: velocity = new Vector2(speed, speed); ; break;
            }

            float distant = Vector2.Distance(position,target.position);

            if (distant <= 300f)
            {
                currentState = state.chasing;
            }
        }

        //Schießen

        public void shoot()
        {
            if (shootSpeed > 0.25f)
            {
                shootSpeed = 0;
                Vector2 origin = new Vector2(oldPosition.X + collRect.Width / 2, oldPosition.Y + collRect.Height / 2);
                Vector2 targetPos = new Vector2(target.position.X + target.collRect.Width / 2, target.position.Y + target.collRect.Height / 2);
                projectiles.createSpit(origin, targetPos, velocity, this);
            }
        }

        //Getroffen

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

        //Gegner Zeichnen
         
        public override void Draw(SpriteBatch spriteBatch)
        {
            currentAnimation.Draw(spriteBatch);
            healthBar.Draw(spriteBatch);
        }
    }
}
