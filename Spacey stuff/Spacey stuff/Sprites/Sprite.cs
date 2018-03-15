using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spacey_stuff
{
    public class Sprite
    {
        protected Texture2D texture;

        protected Vector2 pos;
        public bool remove = false;

        public Rectangle Rectangle => new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);

        public Sprite(Texture2D texture, List<List<Sprite>> spriteGroups)
        {
            this.texture = texture;
            foreach (List<Sprite> list in spriteGroups)
                list.Add(this);
        }

        public Sprite() { }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, pos, Color.White);
        }
    }

    public class Child : Sprite
    {
    }
}
