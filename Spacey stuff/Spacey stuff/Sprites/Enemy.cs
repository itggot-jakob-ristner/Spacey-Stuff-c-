using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Spacey_stuff
{

    class Enemy
    {
        private int attackSpeed;
        private Texture2D texture;
        private Texture2D bulletTexture;
        private float rotation;
        private Game1 game;
        private int bulletSpeed;
        private int speed;
        private int bulletTimer;
        private int width, height;

        public Vector2 pos;
        public int health;
        public List<Bullet> bulletList;

        public Texture2D GetTexture2D => texture;
        public Texture2D GetBulletTexture => bulletTexture;
        public int GetBulletSpeed => bulletSpeed;
        public float GetRotation => rotation;
        public List<Bullet> GetBulletList => bulletList;



        public Enemy(Game1 game)
        {
            this.game = game;
            rotation = (float)Math.PI * 1.5f;
            bulletTimer = 20;
            bulletSpeed = 5;
            speed = 1;
            bulletList = new List<Bullet> { };
            pos = new Vector2(1920, 500);

        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
            width = texture.Width;
            height = texture.Height;
        }

        public void SetBulletTexture(Texture2D bulletTexture)
        {
            this.bulletTexture = bulletTexture;
        }

        public void Update()
        {
            pos.X -= speed;
            if (bulletTimer > 60)
            {
                Shoot();
                bulletTimer = 0;
            }
            bulletTimer++;
        }

        public void Shoot()
        {
            bulletList.Add(new Bullet(this));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(width / 2, height / 2);
            spriteBatch.Draw(texture, pos, null, Color.White, rotation, origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
