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
    public class Player
    {
        Settings Settings = new Settings();
        Texture2D texture;
        Texture2D bulletText;
        int width, height;
        int health = 100;
        private float rotation;
        private float rotationAcc;
        int bulletTimer = 0;
        int bulletspeed = 10;
        Vector2 Vel = new Vector2(400, 500);
        Vector2 mousePos;
        float mouseAngle;
        int damage;

        public List<Bullet> BulletList;
        public Texture2D GetTexture => texture;
        public float Rotation => rotation;
        public int Getheight => width;
        public Vector2 GetVel => Vel;
        public int GetWidth => height;
        public int GetBulletVel => bulletspeed;
        public Texture2D GetBulletTexture => bulletText;
        public List<Bullet> GetBullets => BulletList;
        public int GetDagame => damage;

        private SoundEffect shootSound;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Pos.X - width / 2, (int)Pos.Y - width / 2, width, height);
            }
        }

        public int Health
        {
            get { return health; }

            set { health = value; }
        }

        public Vector2 Pos;

        public Player()
        { 
            Pos = Vector2.Zero;
            BulletList = new List<Bullet> { };
            damage = 20;
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
            width = texture.Width;
            height = texture.Height;
        }

        public void SetFX(SoundEffect soundEffect)
        {
            shootSound = soundEffect;
        }

        public void SetBulletTexture(Texture2D BulletTexture)
        {
            bulletText = BulletTexture;
        }

        public void Update()
        {
            Vector2 Acc = Vector2.Zero;
            rotationAcc = 0f;

            KeyboardState keystate = Keyboard.GetState();
            MouseState state = Mouse.GetState();

            #region Keyinputs
            if (keystate.IsKeyDown(Keys.W))
            {
                Acc.Y -= Settings.PlayerAcc;
            } // Move Up
            else if (keystate.IsKeyDown(Keys.S))
            {
                Acc.Y += Settings.PlayerAcc;
            } // Move Down
            if (keystate.IsKeyDown(Keys.A))
            {
                Acc.X -= Settings.PlayerAcc;
            } // Move Left  
            else if (keystate.IsKeyDown(Keys.D))
            {
                Acc.X += Settings.PlayerAcc;
            } // Move Right
            if ((keystate.IsKeyDown(Keys.Space) && bulletTimer > 20) || state.LeftButton == ButtonState.Pressed && bulletTimer > 20) 
            {
                Shoot();
                bulletTimer = 0;
            } // Shoot
            if (keystate.IsKeyDown(Keys.Q))
            {
                rotationAcc -= Settings.PlayerRotAcc;
            } 
            else if (keystate.IsKeyDown(Keys.E))
            {
                rotationAcc += Settings.PlayerRotAcc;
            }

                #endregion
                bulletTimer++;

            mousePos = new Vector2(state.X - Pos.X, state.Y - Pos.Y);
            Console.WriteLine(mousePos.Y);
            if (mousePos.Y  < 0)
            {
                mouseAngle = -(float)Math.Atan(mousePos.X / -mousePos.Y);
                mouseAngle = (float)Math.PI * 2 - mouseAngle;
            } else
            {
                mouseAngle = (float)Math.PI - -(float)Math.Atan(mousePos.X / -mousePos.Y);
            }

           
            //mouseAngle = (float)Math.PI * mouseAngle / 180;


            // Apply friction
            Acc += Vel * Settings.PlayerFric;
            //rotationAcc += rotation * Settings.PlayerRotFric;

            // Apply acceleratio  to velocity
            Vel += Acc;
            //rotation += rotationAcc;
            rotation = mouseAngle;
            rotation %= (float)(2 * Math.PI);

            if (Rectangle.Left + Vel.X < 0 || Rectangle.Right + Vel.X >= Settings.WindowWidth)
            {
                Vel.X = 0;
            }
            if (Rectangle.Top + Vel.Y < 0 || Rectangle.Bottom +  Vel.Y >= Settings.WindowHeight)
            {
                Vel.Y = 0;
            }

            Pos += Vel;

        }

        public Vector2 Center()
        {
            return new Vector2(Pos.X + width / 2, Pos.Y + height / 2);
        }

        private void Shoot()
        {
            BulletList.Add(new Bullet(this));
            shootSound.Play();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(width / 2, height / 2);
            spriteBatch.Draw(texture, Pos, null, Color.White, rotation, 
                             origin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
