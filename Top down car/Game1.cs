using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Top_down_car
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private InputManager inputMan;
        private Sprite Road;
        private Sprite pointer;
        private Sprite RearLine;
        private Sprite Line;
        private Sprite Ref1;
        private Sprite COR;
        private Sprite supra;
        private Sprite FrontAxle;
        private Sprite RearAxle;
        private Sprite COM;
        private Vector2 FrontAx;
        private Vector2 RearAx;
        private Vector2 COG;
        private Texture2D loadContent;
        private float rotation;
        private float carot;
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

        private MyMatrix matrix2 = new MyMatrix();
        private Rectangle rearL = new Rectangle();

        private int millisecondsPerFrame = 1; //Update every 1 millisecond
        private double timeSinceLastUpdate = 0; //Accumulate the elapsed time


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
            pointer = new Sprite(loadContent, new Vector2(-100, -500), new Vector2(30, 30));
            RearLine = new Sprite(loadContent, new Vector2(-100, -500), new Vector2(4000000, 30));
           
            Line = new Sprite(loadContent, new Vector2(10, 10), new Vector2(10, 10));
            COR = new Sprite(loadContent, new Vector2(-100, -500), new Vector2(15, 15));
            Ref1 = new Sprite(loadContent, new Vector2(10, 10), new Vector2(10, 10));


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
        public void posReset(int x) 
        {
            pointer.spritePosition = FrontAxle.spritePosition;
            move = 50;
            RearLine.spritePosition = RearAxle.spritePosition;
            COR.spritePosition =new Vector2(COR.spritePosition.X, RearAxle.spritePosition.Y);

            

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            rotation = inputMan.Steering(_graphics,Keys.A,Keys.D);
            scroll = inputMan.zoom(_graphics);
            FrontAxle.spritePosition = new Vector2(RearAxle.spritePosition.X, RearAxle.spritePosition.Y - 255 );
            COM.spritePosition = new Vector2(RearAxle.spritePosition.X, supra.spritePosition.Y * 0.97f);
            rearL = new Rectangle((int)RearLine.spritePosition.X, (int)RearLine.spritePosition.Y, 4000000, 100);

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                carot= 0.15f;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                carot+= 0.15f;
            }
            //Road1.spritePosition = Offset = matrix.transAxis(new Vector2(FrontAxle.spritePosition.X + move, FrontAxle.spritePosition.Y), rotation);

            a = FrontAxle.spritePosition.Y - RearAxle.spritePosition.Y;
            B = 90;
            C = rotation;

            timeSinceLastUpdate += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timeSinceLastUpdate >= millisecondsPerFrame)
            {

                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                {
                    posReset(1);
                }
                //if (Math.Round(Ref1.spritePosition.Y,10) == Math.Round(Ref2.spritePosition.Y,10))
                //{
                //    COR.spritePosition = new Vector2(Ref1.spritePosition.X, COR.spritePosition.Y);
                //    Debug.WriteLine(1);
                //}
                Debug.WriteLine("y:{0}, y:{1}", Math.Round(pointer.spritePosition.Y / 10), Math.Round(RearLine.spritePosition.Y / 10));
                if (Math.Round(pointer.spritePosition.Y/5) == Math.Round(RearLine.spritePosition.Y/5) || Math.Round(pointer.spritePosition.Y / 5) == Math.Round(RearLine.spritePosition.Y / 5 +1))
                {
                    COR.spritePosition = new Vector2(pointer.spritePosition.X, COR.spritePosition.Y);
                    
                }
                timeSinceLastUpdate = 0;
            }



            // TODO: Add your update logic here
            matrix.Follow(FrontAxle,rotation,origin,scroll);

            matrix2.rotate(supra, carot, COR.spritePosition);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            Road.DrawSprite(_spriteBatch, Road.spriteTexture, new Vector2(0,0), 0, Color.White, matrix);

            supra.DrawSprite(_spriteBatch, supra.spriteTexture , new Vector2(supra.spriteTexture.Width / 2, supra.spriteTexture.Height / 2), 0,Color.White,matrix2);
            FrontAxle.DrawSprite(_spriteBatch, FrontAxle.spriteTexture, new Vector2(FrontAxle.spriteTexture.Width /2,FrontAxle.spriteTexture.Height/2), rotation, Color.Green,matrix);
            RearAxle.DrawSprite(_spriteBatch, RearAxle.spriteTexture, new Vector2(RearAxle.spriteTexture.Width / 2, RearAxle.spriteTexture.Height / 2), 0, Color.Green,matrix);
            COM.DrawSprite(_spriteBatch, COM.spriteTexture, new Vector2(COM.spriteTexture.Width / 2,COM.spriteTexture.Height / 2), 0, Color.Red,matrix);

            pointer.DrawSprite(_spriteBatch, FrontAxle.spriteTexture, new Vector2(pointer.spriteTexture.Width / 2, pointer.spriteTexture.Height / 2), 0, Color.Red, matrix);
            RearLine.DrawSprite(_spriteBatch, FrontAxle.spriteTexture, new Vector2(pointer.spriteTexture.Width / 2, pointer.spriteTexture.Height / 2), 0, Color.Red, matrix);
            COR.DrawSprite(_spriteBatch, FrontAxle.spriteTexture, new Vector2(pointer.spriteTexture.Width / 2, pointer.spriteTexture.Height / 2), 0, Color.Green, matrix);
            Ref1.DrawSprite(_spriteBatch, FrontAxle.spriteTexture, new Vector2(pointer.spriteTexture.Width / 2, pointer.spriteTexture.Height / 2), 0, Color.Blue, matrix);

            COR.spritePosition = Line.DrawLines(_spriteBatch, loadContent, FrontAxle.spritePosition, COR.spritePosition,matrix,rotation,FrontAxle.spritePosition,RearLine.spritePosition,rearL);



            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
