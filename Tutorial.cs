using Mogre;
using Mogre.TutorialFramework;
//using System;
using PhysicsEng;
using System.Collections.Generic;
using RaceGame;

namespace Mogre.Tutorial
{
    class Tutorial : BaseApplication
    {
        #region Fields
        Physics physics;
        Environment environment;              // Field which will contain an instance of the ground and wall class
        Robot robot;                          // This fields is goning to contain an instance of the Robot class
        List<Robot> robots;
        List<Robot> robotsToRemove;
        PlayerModel playerModel;
        SceneNode cameraNode;
        InputsManager inputsManager = InputsManager.Instance;
        GameInterface gameHMD;
        List<Bomb> bombs;
        List<Bomb> bombsToRemove;
        static public bool shoot;
        #endregion

        public static void Main()
        {
            new Tutorial().Go();            // This method starts the rendering loop
        }

        #region Create Scene()
        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            #region Basics
            physics = new Physics();
            
            robot = new Robot(mSceneMgr);
            robot.setPosition(new Vector3(000, 0, 300));
            environment = new Environment(mSceneMgr, mWindow);
            playerModel = new PlayerModel(mSceneMgr);
            playerModel.setPosition(new Vector3(0, -80, 50));
            playerModel.hRotate(new Vector3(600, 0, 0));
            #endregion

            #region Camera
            cameraNode = mSceneMgr.CreateSceneNode();
            cameraNode.AttachObject(mCamera);
            playerModel.AddChild(cameraNode);
            inputsManager.PlayerModel = playerModel;
            #endregion

            #region Part 9
            PlayerStats playerStats = new PlayerStats();
            gameHMD = new GameInterface(mSceneMgr, mWindow, playerStats);
            #endregion

            robots = new List<Robot>();
            robots.Add(robot);
            robotsToRemove = new List<Robot>();
            bombs = new List<Bomb>();
            bombsToRemove = new List<Bomb>();
            physics.StartSimTimer();
        }
        #endregion

        #region Destroy Scene ()
        /// <summary>
        /// This method destrois the scene
        /// </summary>
        protected override void DestroyScene()
        {
            //manObjExample.Dispose();
            base.DestroyScene();
            #region Basics
            if (robot != null)
                robot.Dispose();
            if (environment != null)
                environment.Dispose();
            if (playerModel != null)
                playerModel.Dispose();
            #endregion

            #region Part 6
            //cameraNode.DetachAllObjects();
            //cameraNode.Dispose();
            #endregion

            #region Part 9
            gameHMD.Dispose();
            #endregion

            foreach (Bomb bomb in bombs)
            {
                bomb.Dispose();
            }

            physics.Dispose();
        }
        #endregion

        #region Create Camera ()
        /// <summary>
        /// This method create a new camera
        /// </summary>
        protected override void CreateCamera()
        {
            //base.CreateCamera();
            #region Part 6
            mCamera = mSceneMgr.CreateCamera("PlayerCam");
            mCamera.Position = new Vector3(000, 100, 250);
            mCamera.LookAt(new Vector3(000, 000, 000));
            mCamera.NearClipDistance = 5;
            mCamera.FarClipDistance = 1000;
            mCamera.FOVy = new Degree(70);

            mCameraMan = new CameraMan(mCamera);
            mCameraMan.Freeze = true;
            #endregion
        }
        #endregion

        #region Create Viewports()
        /// <summary>
        /// This method create a new viewport
        /// </summary>
        protected override void CreateViewports()
        {
            base.CreateViewports();
            #region Part 6
            //Viewport viewport = mWindow.AddViewport(mCamera);
            //viewport.BackgroundColour = ColourValue.Black;
            //mCamera.AspectRatio = viewport.ActualWidth / viewport.ActualHeight;
            #endregion
        }
        #endregion

        #region UpdateScene()
        /// <summary>
        /// This method update the scene after a frame has finished rendering
        /// </summary>
        /// <param name="evt"></param>
        protected override void UpdateScene(FrameEvent evt)
        {
            physics.UpdatePhysics(0.01f);
            base.UpdateScene(evt);
            robot.Animate(evt);
            //playerModel.Animate(evt);
            gameHMD.Update(evt);
            mCamera.LookAt(playerModel.Position);

            if (shoot) {    AddBomb(); }

            foreach (Bomb bomb in bombs)
            {
                bomb.Update(evt);
                if (bomb.RemoveMe)
                    bombsToRemove.Add(bomb);
            }

            foreach (Bomb bomb in bombsToRemove)
            {
                bombs.Remove(bomb);
                bomb.Dispose();
            }
            bombsToRemove.Clear();

            shoot = false;
        }
        #endregion

        #region AddBomb()
        /// <summary>
        /// This method generate a new bomb in the list
        /// </summary>
        private void AddBomb()
        {
            Bomb bomb = new Bomb(mSceneMgr);
            bomb.SetPosition(new Vector3(Mogre.Math.RangeRandom(0, 100), 100, Mogre.Math.RangeRandom(0, 100)));
            bombs.Add(bomb);
        }
        #endregion

        #region CreateFrameListiner()
        /// <summary>
        /// This method set create a frame listener to handle events before, during or after the frame rendering
        /// </summary>
        protected override void CreateFrameListeners()
        {
            base.CreateFrameListeners();
            mRoot.FrameRenderingQueued +=
                new FrameListener.FrameRenderingQueuedHandler(inputsManager.ProcessInput);
        }
        #endregion

        #region InitializeInput()
        /// <summary>
        /// This method initilize the inputs reading from keyboard adn mouse
        /// </summary>
        protected override void InitializeInput()
        {
            base.InitializeInput();
            int windowHandle;
            mWindow.GetCustomAttribute("WINDOW", out windowHandle);
            inputsManager.InitInput(ref windowHandle);
        }
        #endregion
    }
}