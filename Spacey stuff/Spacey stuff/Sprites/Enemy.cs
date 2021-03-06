﻿using System;
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

    public class Enemy
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
        private float size;
        private bool remove;

        public Vector2 pos;
        public int health;
        public List<Bullet> bulletList;
        public Color color;

        public Texture2D GetTexture2D => texture;
        public bool Remove => remove;
        public Texture2D GetBulletTexture => bulletTexture;
        public int GetBulletSpeed => bulletSpeed;
        public float GetRotation => rotation;
        public List<Bullet> GetBulletList => bulletList;
        public Rectangle GetRectangle => new Rectangle((int)pos.X - width / 2, (int)pos.Y - width / 2, width, height);

        public Enemy(Game1 game, List<Enemy> list, float size, int posY)
        {
            this.game = game;
            this.size = size;
            rotation = (float)Math.PI * 1.5f;
            bulletTimer = 20;
            bulletSpeed = 5;
            speed = 1;
            remove = false;
            health = (int)(100 * size);
            bulletList = new List<Bullet> { };
            pos = new Vector2(1920, posY);

        }

        public Enemy(Game1 game, List<Enemy> list, float size, int posY, Texture2D texture, Texture2D bulletText)
        {
            this.game = game;
            this.size = size;
            rotation = (float)Math.PI * 1.5f;
            bulletTimer = 20;
            bulletSpeed = 5;
            speed = 1;
            remove = false;
            health = (int)(100 * size);
            bulletList = new List<Bullet> { };
            pos = new Vector2(1920, posY);
            this.texture = texture;
            width = texture.Width;
            height = texture.Height;
            color = Color.White;
            bulletTexture = bulletText;
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
            width = texture.Width;
            height = texture.Height;
            color = Color.White;
        }

        public void SetBulletTexture(Texture2D bulletTexture)
        {
            this.bulletTexture = bulletTexture;
        }

        public void Update()
        {

            pos.X -= speed;
            if (GetRectangle.Right < 0)
            {
                remove = true;
            }
            if (bulletTimer > 60)
            {
                Shoot();
                bulletTimer = 0;
            }
            bulletTimer++;
            color = Color.White;
        }

        public void Shoot()
        {
            game.enemyBullets.Add(new Bullet(this, size));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(width / 2, height / 2);
            spriteBatch.Draw(texture, pos, null, color, rotation, origin, size, SpriteEffects.None, 0f);
        }
    }
}
