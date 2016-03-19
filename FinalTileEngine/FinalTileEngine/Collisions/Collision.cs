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
    class Collision
    {
        //Objekte Deklarieren

        TileMap map;
        Projectiles bullets;

        Color[] colorData1;
        Color[] colorData2;

        //Konstruktor

        public Collision(TileMap map, Projectiles bullets)
        {
            this.map = map;
            this.bullets = bullets;
        }

        //Map Collision Prüfen

        public void checkMapCollision(ICollide collObject)
        {
            for (int i = 0; i < map._layerCount; i++)
            {
                foreach (Tile tile in map.layerList[i])
                {
                    if (tile._passable == false)
                        if (tile.destiRect.Intersects(collObject.getCollisionRect()))
                        {
                            collObject.setOldPos();
                        }
                }
            }
        }

        public void checkBulletCollision(ICollide collObject)
        {
            if (bullets.projectileList.Capacity != 0)
            {
                for (int i = 0; i < bullets.projectileList.Count; i++)
                {
                    if (bullets.projectileList[i].collRect.Intersects(collObject.getCollisionRect()))
                    {
                        if (bullets.projectileList[i].source is Player)
                        {
                            bullets.projectileList[i].isDeath = true;
                            collObject.gotHit();
                        }
                    }
                }
            }
        }

        public void checkBulletCollisionPlayer(ICollide collObject)
        {
            if (bullets.projectileList.Capacity != 0)
            {
                for (int i = 0; i < bullets.projectileList.Count; i++)
                {
                    if (bullets.projectileList[i].collRect.Intersects(collObject.getCollisionRect()))
                    {
                        if (bullets.projectileList[i].source is Enemy)
                        {
                            bullets.projectileList[i].isDeath = true;
                            collObject.gotHit();
                        }
                    }
                }
            }
        }


        //Pixel Collision

        public void checkPixelCollision(ICollide object1, ICollide object2)
        {

        if (getColorData(object1.getTexture(), object1.getSourceRect(), object2.getTexture(), object2.getSourceRect(),object1.getCollisionRect(),object2.getCollisionRect()))
        {
            object1.setOldPos();
            object2.setOldPos();
        }

        }

        //Color Arrays erstellen

        public bool getColorData(Texture2D tex1, Rectangle rect1, Texture2D tex2, Rectangle rect2, Rectangle collRect1, Rectangle collRect2)
        {
            colorData1 = new Color[collRect1.Width * collRect1.Height];
            colorData2 = new Color[collRect2.Width * collRect2.Height];

            tex1.GetData(0, rect1, colorData1, rect1.X * rect1.Y, rect1.Width * rect1.Height);
            tex2.GetData(0, rect1, colorData2, rect2.Y * rect2.Y, rect1.Width * rect1.Height);

            int top = Math.Max(collRect1.Top, collRect2.Top);
            int bottom = Math.Min(collRect1.Bottom, collRect2.Bottom);
            int left = Math.Max(collRect1.Left, collRect2.Left);
            int right = Math.Min(collRect1.Right, collRect2.Right);

            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    Color colorA = colorData1[(x - collRect1.Left) +
                                         (y - collRect1.Top) * collRect1.Width];
                    Color colorB = colorData2[(x - collRect2.Left) +
                                         (y - collRect2.Top) * collRect2.Width];

                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}

