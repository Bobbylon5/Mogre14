using Mogre;
using Mogre.TutorialFramework;
//using System;
using PhysicsEng;
using System.Collections.Generic;
using RaceGame;

namespace Mogre.Tutorial
{
    class Game : BaseApplication
    {

        #region Fields
        Physics physics;
        Env env1;              // Field which will contain an instance of the ground and wall class
        Robot robot;                          // This fields is goning to contain an instance of the Robot class
        List<Robot> robots;
        List<Robot> robotsToRemove;
        //PlayerModel playerModel;
        //SceneNode cameraNode;
        //InputsManager inputsManager = InputsManager.Instance;
        //GameInterface gameHMD;
        //List<Bomb> bombs;
        //List<Bomb> bombsToRemove;
        //static public bool shoot;
        #endregion

        public static void Main()
        {
            new Game().Go();            // This method starts the rendering loop
        }

        #region Create Scene()
        /// <summary>
        /// This method create the initial scene
        /// </summary>
        protected override void CreateScene()
        {
            #region Basics
            physics = new Physics();

            //robot = new Robot(mSceneMgr);
            //robot.setPosition(new Vector3(000, 0, 300));
            env1 = new Env(mSceneMgr, mWindow);
            //playerModel = new PlayerModel(mSceneMgr);
            //playerModel.setPosition(new Vector3(0, -80, 50));
            //playerModel.hRotate(new Vector3(600, 0, 0));
            #endregion

            #region Camera
            //cameraNode = mSceneMgr.CreateSceneNode();
            //cameraNode.AttachObject(mCamera);
            //playerModel.AddChild(cameraNode);
            //inputsManager.PlayerController = playerControler;
            #endregion

            #region Part 9
            //PlayerStats playerStats = new PlayerStats();
            //gameHMD = new GameInterface(mSceneMgr, mWindow, playerStats);
            #endregion

            robots = new List<Robot>();
            robots.Add(robot);
            robotsToRemove = new List<Robot>();
            //bombs = new List<Bomb>();
            //bombsToRemove = new List<Bomb>();
            //physics.StartSimTimer();
        }
        #endregion

    }
}