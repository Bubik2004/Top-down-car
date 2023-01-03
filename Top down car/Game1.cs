using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
namespace Top_down_car
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputManager inputMan;
        private Sprite Road;
        private Sprite Road1;
        private Sprite supra;
        private Sprite FrontAxle;
        private Sprite RearAxle;
        private Sprite COM;
        private Vector2 FrontAx;
        private Vector2 RearAx;
        private Vector2 COG;
        private Texture2D loadContent;
        private float rotation;
        private Vector2 origin;
        private int Weight = 1570;
        private float Iz = 1536.403f;
        private float distFromF = 140.27998f;
        private float distFromR = 114.72002f;
        private float MagnitudeOfVehiclesAngularVelocity;
        private float LongVX1;
        private float latVY1;
        private float scroll;
        public static int dispX;
        public static int dispY;
        private Vector2 FAaxis;
        private Vector2 Offset;
        private MyMatrix matrix = new MyMatrix();


        private float a;
        private float b;
        private float c;
        private float A;
        private float B;
        private float C;

        private float move = 0;
        private float moveY = 0;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            inputMan = new InputManager();

            dispX = GraphicsDeviceManager.DefaultBackBufferWidth;
            dispY = GraphicsDeviceManager.DefaultBackBufferHeight;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            loadContent = Content.Load<Texture2D>("road");
            Road1 = new Sprite(loadContent, new Vector2(-100, -500), new Vector2(400, 30));

            loadContent = Content.Load<Texture2D>("road");
            Road = new Sprite (loadContent, new Vector2 (-4000,-4000), new Vector2(8000,8000));

            loadContent = Content.Load<Texture2D>("98Supra");
            supra = new Sprite(loadContent, new Vector2(500, 250), new Vector2(181, 452));

            loadContent = Content.Load<Texture2D>("test");
            FrontAxle = new Sprite(loadContent, new Vector2(200,400), new Vector2(44, 63));
            
            COM = new Sprite(loadContent, new Vector2(400 , 200), new Vector2(10, 10));

            RearAxle = new Sprite(loadContent, new Vector2(supra.spritePosition.X , supra.spritePosition.Y + 127), new Vector2(44,63));
            FrontAx = new Vector2(0, 0);



            //FORCE ON FRONT AND REAR AXLE = MASS and ACCELERATION
            // FrontAxleForce + RearAxleForce = Mass * Acceleration




            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            rotation = inputMan.Steering(_graphics);
            scroll = inputMan.zoom(_graphics);
            FrontAxle.spritePosition = new Vector2(RearAxle.spritePosition.X, RearAxle.spritePosition.Y - 255 );
            COM.spritePosition = new Vector2(RearAxle.spritePosition.X, supra.spritePosition.Y * 0.97f);

            Road1.spritePosition = Offset = matrix.transAxis(new Vector2(FrontAxle.spritePosition.X + move, FrontAxle.spritePosition.Y), rotation);
           
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                move -= 30;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                move += 30;
            }
            a = FrontAxle.spritePosition.Y - RearAxle.spritePosition.Y;
            B = 90;
            C = rotation;


            // TODO: Add your update logic here
            matrix.Follow(FrontAxle,rotation,origin,scroll);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Road.DrawSprite(_spriteBatch, Road.spriteTexture, new Vector2(0,0), 0, Color.White, matrix);

            supra.DrawSprite(_spriteBatch, supra.spriteTexture , new Vector2(supra.spriteTexture.Width / 2, supra.spriteTexture.Height / 2), 0,Color.White,matrix);
            FrontAxle.DrawSprite(_spriteBatch, FrontAxle.spriteTexture, new Vector2(FrontAxle.spriteTexture.Width /2,FrontAxle.spriteTexture.Height/2), rotation, Color.Green,matrix);
            RearAxle.DrawSprite(_spriteBatch, RearAxle.spriteTexture, new Vector2(RearAxle.spriteTexture.Width / 2, RearAxle.spriteTexture.Height / 2), 0, Color.Green,matrix);
            COM.DrawSprite(_spriteBatch, COM.spriteTexture, new Vector2(COM.spriteTexture.Width / 2,COM.spriteTexture.Height / 2), 0, Color.Red,matrix);

            Road1.DrawSprite(_spriteBatch, Road.spriteTexture, new Vector2(Road1.spriteTexture.Width / 2, Road1.spriteTexture.Height / 2), rotation, Color.Red, matrix);
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
