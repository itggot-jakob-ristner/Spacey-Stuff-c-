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
    public class Bullet
    {
        Vector2 pos;
        int speed, width, height;
        Vector2 vel;
        Texture2D texture;
        Player player;
        Enemy enemy;
        float rotation;

        public bool remove = false;

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
            pos = player.Pos;
            vel = new Vector2((float)Math.Sin(player.Rotation) * speed, -(float)Math.Cos(player.Rotation) * speed);
            rotation = player.Rotation;
        }

        public Bullet(Enemy enemy)
        {
            this.enemy = enemy;
            texture = enemy.GetBulletTexture;
            width = texture.Width;
            height = texture.Height;
            speed = enemy.GetBulletSpeed;
            pos = enemy.pos;
            vel = new Vector2((float)Math.Sin(enemy.GetRotation) * speed, -(float)Math.Cos(enemy.GetRotation) * speed);
            rotation = enemy.GetRotation;
        }

        public void Update(List<Enemy> enemies)
        {
            pos += vel;
            foreach (Enemy enemy in enemies)
            {
                if (Collide(enemy)) {
                    enemy.color = Color.Red;
                    Console.WriteLine("YEs");
                    enemy.health -= player.GetDagame;
                    remove = true;
                }
            }
        }

        public void Update()
        {
            pos += vel;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(width / 2, height / 2);
            spriteBatch.Draw(texture, pos, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }

        bool Collide(Enemy enemy)
        {
            return (pos.X < enemy.GetRectangle.Right && pos.X > enemy.GetRectangle.Left &&
                    pos.Y > enemy.GetRectangle.Top && pos.Y < enemy.GetRectangle.Bottom);
        }
    }
}