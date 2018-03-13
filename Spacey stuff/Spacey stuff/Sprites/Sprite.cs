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
        #region Private Feilds

        private Texture2D texture;

        #endregion

        #region Public Fields

        public Vector2 Position;

        #endregion

        #region Public Properties
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y,
                                          texture.Width, texture.Height);
            }
        }
        #endregion

        #region Constructers
        public Sprite(Texture2D texture)
        {
            this.texture = texture;
        }
        #endregion

        #region Public Methods

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Position, Color.White);
        }
        #endregion
    }
}
