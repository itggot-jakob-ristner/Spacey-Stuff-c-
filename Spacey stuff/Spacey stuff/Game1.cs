using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;



namespace Spacey_stuff
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D BackgroundTexture, ShipTexture, BulletTexture;
        Settings Settings;
        Player Player;
        

        public Game1()
        {
            Settings Settings = new Settings();
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = Settings.WindowWidth;
            graphics.PreferredBackBufferHeight = Settings.WindowHeight;
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
            Player = new Player();
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
            ShipTexture = Content.Load<Texture2D>("Space Shooter - 1/Ship/4");
            Player.SetTexture(ShipTexture);
            BackgroundTexture = Content.Load<Texture2D>("Space Shooter - 1/16-bit-wallpaper-3");
            Player.SetBulletTexture(Content.Load<Texture2D>("Space Shooter - 1/Shoot/2"));
            Player.SetFX(Content.Load<SoundEffect>("Space Shooter - 1/Sound/4"));
            Song song = Content.Load<Song>("Space Shooter - 1/Music/5");
            MediaPlayer.Volume = 0.3f;
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            


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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            Player.Update();
            foreach (Bullet bullet in Player.GetBullets)
                bullet.Update();
            for (int i = 0; i < Player.BulletList.Count; i++)
            {
                if (Player.BulletList[i].Pos.Y + Player.BulletList[i].GetHeight < 0)
                    Player.BulletList.Remove(Player.BulletList[i]);
            }
            
                                

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

            foreach (Bullet bullet in Player.GetBullets)
                spriteBatch.Draw(Player.GetBulletTexture, bullet.Pos, Color.White);



            //spriteBatch.Draw(Player.GetTexture, Player.Pos, Color.White);
            Player.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
