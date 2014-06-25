using Mogre;
using Mogre.TutorialFramework;
using System;

namespace Mogre.Tutorial
{
    /// <summary>
    /// This class implements the ground of the environment
    /// </summary>
    class Wall : StaticElement 
    {
        Plane plane1;
        Plane plane2;
        Plane plane3;
        Plane plane4;        //ManualObject manual;
        Entity wallEntity1;
        SceneNode wallNode1;
        Entity wallEntity2;
        SceneNode wallNode2;
        Entity wallEntity3;
        SceneNode wallNode3;
        Entity wallEntity4;
        SceneNode wallNode4;
        Vector3 vect;

        Degree bob = 180;

        public Plane Plane1
        {
            get { return plane1; }
        }

        public Plane Plane2
        {
            get { return plane2; }
        }

        public Plane Plane3
        {
            get { return plane3; }
        }

        public Plane Plane4
        {
            get { return plane4; }
        }

        int wallWidth = 10;
        int wallHeight = 10;
        int wallXSegs = 100;
        int wallZSegs = 100;
        int uTiles = 10;
        int vTiles = 10;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of the scene manager</param>
        public Wall(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            wallWidth = 50;
            wallHeight = 1000;
            CreateWall();
        }

        /// <summary>
        /// This method initializes the ground mesh and its node
        /// </summary>
        private void CreateWall()
        {
            WallPlane1();
            WallPlane2();
            WallPlane3();
            WallPlane4();
        }

        /// <summary>
        /// This method generates a plane in an Entity which will be used as a wall
        /// </summary>
        private void WallPlane1()
        {
            plane1 = new Plane(Vector3.UNIT_X, -0);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall1",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane1, wallWidth,
                wallHeight, wallXSegs, wallZSegs, true, 1, uTiles, vTiles,
                Vector3.UNIT_Z);

            wallEntity1 = mSceneMgr.CreateEntity("wall1");
            wallNode1 = mSceneMgr.CreateSceneNode();
            wallNode1.Translate(new Vector3(500, 000, 000));
            wallNode1.Yaw(bob);
            wallNode1.AttachObject(wallEntity1);
            mSceneMgr.RootSceneNode.AddChild(wallNode1);
            //wallEntity.SetMaterialName("Meteor");    
        }

        /// <summary>
        /// This method generates a plane in an Entity which will be used as a wall
        /// </summary>
        private void WallPlane2()
        {
            plane2 = new Plane(Vector3.UNIT_X, -0);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall2",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane2, wallWidth,
                wallHeight, wallXSegs, wallZSegs, true, 1, uTiles, vTiles,
                Vector3.UNIT_Z);

            wallEntity2 = mSceneMgr.CreateEntity("wall2");
            wallNode2 = mSceneMgr.CreateSceneNode();
            wallNode2.Translate(new Vector3(-500, 000, 000));
            wallNode2.AttachObject(wallEntity2);
            mSceneMgr.RootSceneNode.AddChild(wallNode2);
            //wallEntity.SetMaterialName("Meteor");    
        }

        /// <summary>
        /// This method generates a plane in an Entity which will be used as a wall
        /// </summary>
        private void WallPlane3()
        {
            plane3 = new Plane(Vector3.UNIT_Z, -0);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall3",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane3, wallWidth,
                wallHeight, wallXSegs, wallZSegs, true, 1, uTiles, vTiles,
                Vector3.UNIT_X);

            wallEntity3 = mSceneMgr.CreateEntity("wall3");
            wallNode3 = mSceneMgr.CreateSceneNode();
            wallNode3.Translate(new Vector3(000, 000, -500));
            wallNode3.AttachObject(wallEntity3);
            mSceneMgr.RootSceneNode.AddChild(wallNode3);
            //wallEntity.SetMaterialName("Meteor");    
        }

        /// <summary>
        /// This method generates a plane in an Entity which will be used as a wall
        /// </summary>
        private void WallPlane4()
        {
            plane4 = new Plane(Vector3.UNIT_Z, 0);
            MeshPtr wallMeshPtr = MeshManager.Singleton.CreatePlane("wall4",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane4, wallWidth,
                wallHeight, wallXSegs, wallZSegs, true, 1, uTiles, vTiles,
                Vector3.UNIT_X);

            wallEntity4 = mSceneMgr.CreateEntity("wall4");
            wallNode4 = mSceneMgr.CreateSceneNode();
            wallNode4.Translate(new Vector3(000, 000, 500));

            wallNode4.Yaw(bob);
            wallNode4.AttachObject(wallEntity4);
            mSceneMgr.RootSceneNode.AddChild(wallNode4);
            //wallEntity.SetMaterialName("Meteor");    
        }

    }
}
