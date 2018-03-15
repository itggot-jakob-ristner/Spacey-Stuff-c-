using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;



namespace Spacey_stuff
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rnd;
        int spawnChance;
        int randPos;
        float randSize;
        int randIndex1;
        int randIndex2;
        List<Texture2D> shipTextures;
        List<Texture2D> bulletTextures;

        Texture2D BackgroundTexture, ShipTexture, BulletTexture, 
                  enemyShip, enemyBullet;
        Settings Settings;
        Player Player;

        public List<Enemy> enemyList;
        public List<Bullet> enemyBullets;


        Sprite sprite = new Sprite();
        

        public Game1()
        {
            Settings Settings = new Settings();
            rnd = new Random();
            spawnChance = 50;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Settings.WindowWidth;
            graphics.PreferredBackBufferHeight = Settings.WindowHeight;
            //graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Player = new Player(this);
            enemyList = new List<Enemy> { };
            enemyBullets = new List<Bullet> { };

            enemyList.Add(new Enemy(this, enemyList, 1.5f, 300));
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            shipTextures = new List<Texture2D> { Content.Load<Texture2D>("Space Shooter - 1/Ship/8"),
                                                 Content.Load<Texture2D>("Space Shooter - 1/Ship/1"),
                                                 Content.Load<Texture2D>("Space Shooter - 1/Ship/2"),
                                                 Content.Load<Texture2D>("Space Shooter - 1/Ship/3"),
                                                 Content.Load<Texture2D>("Space Shooter - 1/Ship/6")};

            bulletTextures = new List<Texture2D> { Content.Load<Texture2D>("Space Shooter - 1/Shoot/1"),
                                                   Content.Load<Texture2D>("Space Shooter - 1/Shoot/2"),
                                                   Content.Load<Texture2D>("Space Shooter - 1/Shoot/3"),
                                                   Content.Load<Texture2D>("Space Shooter - 1/Shoot/4"),
                                                   Content.Load<Texture2D>("Space Shooter - 1/Shoot/5")};

            ShipTexture = Content.Load<Texture2D>("Space Shooter - 1/Ship/4");
            Player.SetTexture(ShipTexture);
            BackgroundTexture = Content.Load<Texture2D>("Space Shooter - 1/16-bit-wallpaper-3");
            Player.SetBulletTexture(Content.Load<Texture2D>("Space Shooter - 1/Shoot/2"));
            Player.SetFX(Content.Load<SoundEffect>("Space Shooter - 1/Sound/4"));
            //Song song = Content.Load<Song>("Space Shooter - 1/Music/5");
            //MediaPlayer.Volume = 0.3f;
            //MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;

            foreach(Enemy enemy in enemyList)
            {
                enemy.SetTexture(Content.Load<Texture2D>("Space Shooter - 1/Ship/8"));
                enemy.SetBulletTexture(Content.Load<Texture2D>("Space Shooter - 1/Shoot/3"));
            }
            


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            int rand = rnd.Next(10000);

            if (rand < spawnChance)
            {
                randSize = rnd.Next(5, 20) / 10f;
                randPos = rnd.Next(100, 1700);
                randIndex1 = rnd.Next(shipTextures.Count);
                randIndex2 = rnd.Next(bulletTextures.Count);



                enemyList.Add(new Enemy(this, enemyList, randSize, randPos, shipTextures[randIndex1], bulletTextures[randIndex2]));


            }


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (Enemy enemy in enemyList)
            {
                enemy.Update();
            }
            Player.Update();
            foreach (Bullet bullet in enemyBullets)
                bullet.Update(Player);
            foreach (Bullet bullet in Player.GetBullets)
                bullet.Update(enemyList);
            for (int i = 0; i < Player.BulletList.Count; i++)
            {
                if (Player.BulletList[i].Pos.Y + Player.BulletList[i].GetHeight < 0 || Player.BulletList[i].remove) 
                    Player.BulletList.Remove(Player.BulletList[i]);
            }
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                if (enemyBullets[i].remove)
                    enemyBullets.Remove(enemyBullets[i]);
            }
                
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].health <= 0 || enemyList[i].Remove)
                    enemyList.Remove(enemyList[i]);
            }

            if (Player.Health <= 0)
                Exit();
                                

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.Draw(BackgroundTexture, Vector2.Zero, Color.White);

            foreach (Bullet bullet in enemyBullets)
                bullet.Draw(spriteBatch);
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);

            }


            foreach (Bullet bullet in Player.GetBullets)
                //spriteBatch.Draw(Player.GetBulletTexture, bullet.Pos, Color.White);
                bullet.Draw(spriteBatch);
            //spriteBatch.Draw(Player.GetTexture, Player.Pos, Color.White);
            Player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
