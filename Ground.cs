using Mogre;
using Mogre.TutorialFramework;
using System;

namespace Mogre.Tutorial
{
    /// <summary>
    /// This class implements the ground of the environment
    /// </summary>
    class Ground : StaticElement
    {
        //SceneManager mSceneMgr;
        Plane plane;
        Entity groundEntity;
        SceneNode groundNode;


        public Plane Plane
        {
            get { return plane; }
        }


        int groundWidth = 10;
        int groundHeight = 10;
        int groundXSegs = 100;
        int groundZSegs = 100;
        int uTiles = 10;
        int vTiles = 10;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of the scene manager</param>
        public Ground(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            groundWidth = 1000;
            groundHeight = 1000;
            CreateGround();
        }

        /// <summary>
        /// This method initializes the ground mesh and its node
        /// </summary>
        private void CreateGround()
        {
            GroundPlane();
            
        }


        /// <summary>
        /// This method generates a plane in an Entity which will be used as a ground
        /// </summary>
        private void GroundPlane()
        {
            plane = new Plane(Vector3.UNIT_Y, 0);

            MeshPtr groundMeshPtr = MeshManager.Singleton.CreatePlane("ground",
                ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, groundWidth,
                groundHeight, groundXSegs, groundZSegs, true, 1, uTiles, vTiles,
                Vector3.UNIT_Z);

            groundEntity = mSceneMgr.CreateEntity("ground");
            groundNode = mSceneMgr.CreateSceneNode();
            groundNode.AttachObject(groundEntity);
            mSceneMgr.RootSceneNode.AddChild(groundNode);
            groundEntity.SetMaterialName("Meteor");
        }

        /// <summary>
        /// This method disposes of the scene node and enitity
        /// </summary>
        public void Dispose()
        {
           groundNode.DetachAllObjects();
           groundNode.Parent.RemoveChild(groundNode);
           groundNode.Dispose();
           groundEntity.Dispose();
        }
    }
}
