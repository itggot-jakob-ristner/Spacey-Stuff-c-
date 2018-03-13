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
    class Bullet
    {
        Vector2 pos;
        int speed, width, height;
        Vector2 vel;
        Texture2D texture;
        Player player;

        public Vector2 Pos => pos;
        public int GetHeight => height;
        public int GetWidth => width;

        public Bullet(Player player)
        {
            this.player = player;
            texture = player.GetBulletTexture;
            width = texture.Width;
            height = texture.Height;
            speed = player.GetBulletVel;
            pos = new Vector2(player.Pos.X - texture.Width / 2,
                              player.Pos.Y - texture.Height / 2);
            vel = new Vector2((float)Math.Sin(player.Rotation) * speed, -(float)Math.Cos(player.Rotation) * speed);


        }

        public void Update()
        {
            pos += vel;
        }
    }
}