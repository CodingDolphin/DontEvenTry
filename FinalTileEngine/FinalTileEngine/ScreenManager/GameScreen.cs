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
    class GameScreen
    {
        //Provide LoadContent

        public virtual void LoadContent(ContentManager content)
        {
        }

        //Provide Update

        public virtual void Update(GameTime gameTime)
        {
        }

        //Provide Draw

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
