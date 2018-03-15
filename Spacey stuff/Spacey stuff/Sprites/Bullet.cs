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
        float size;
        int damage;

        public bool remove = false;

        public Vector2 Pos => pos;
        public int GetHeight => height;
        public int GetWidth => width;
        public int GetBullet => damage;

        public Bullet(Player player)
        {
            this.player = player;
            size = 1.0f;
            texture = player.GetBulletTexture;
            width = texture.Width;
            height = texture.Height;
            damage = 10;
            speed = player.GetBulletVel;
            pos = player.Pos;
            vel = new Vector2((float)Math.Sin(player.Rotation) * speed, -(float)Math.Cos(player.Rotation) * speed);
            rotation = player.Rotation;
        }

        public Bullet(Enemy enemy, float size)
        {
            this.enemy = enemy;
            this.size = size;
            damage = (int)(10 * size);
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
                    enemy.health -= player.GetDagame;
                    remove = true;
                }
            }
        }

        public void Update(Player player)
        {
            pos += vel;
            if (Collide(player))
            {
                Console.WriteLine("hej");
                player.color = Color.Red;
                player.Health -= damage;
                remove = true;
            }

        }

        public void Update()
        {
            pos += vel;
            //if (pos.X + width )
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(width / 2, height / 2);
            spriteBatch.Draw(texture, pos, null, Color.White, rotation, origin, size, SpriteEffects.None, 0f);
        }

        bool Collide(Enemy enemy)
        {
            return (pos.X < enemy.GetRectangle.Right && pos.X > enemy.GetRectangle.Left &&
                    pos.Y > enemy.GetRectangle.Top && pos.Y < enemy.GetRectangle.Bottom);
        }
        bool Collide(Player enemy)
        {
            return (pos.X < enemy.Rectangle.Right && pos.X > enemy.Rectangle.Left &&
                    pos.Y > enemy.Rectangle.Top && pos.Y < enemy.Rectangle.Bottom);
        }
    }
}