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
        //SceneManager mSceneMgr;

        ManualObject manual;
        Entity wallEntity;
        SceneNode wallNode;

        int wallWidth = 0;
        int wallHeight = 0;
        int wallXSegs = 1;
        int wallZSegs = 1;
        int uTiles = 10;
        int vTiles = 10;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference of the scene manager</param>
        public Wall(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
            wallWidth = 1000;
            wallHeight = 50;
            CreateWall();
        }

        /// <summary>
        /// This method initializes the ground mesh and its node
        /// </summary>
        private void CreateWall()
        {
            WallPlane();
        }
        
        /// <summary>
        /// This method generates a plane in an Entity which will be used as a ground
        /// </summary>
        private void WallPlane()
        {
            manual = mSceneMgr.CreateManualObject("manualQuad");
            manual.Begin("void", RenderOperation.OperationTypes.OT_TRIANGLE_LIST);

            // ----Vertex Buffer-----
            manual.Position(new Vector3(500, 50, -500));      // --- Vertex 0 ---
            manual.TextureCoord(0, 0);
            manual.Position(new Vector3(-500, 50, -500));      // --- Vertex 1 ---
            manual.TextureCoord(1, 0);
            manual.Position(new Vector3(500, -50, -500));      // --- Vertex 2 ---
            manual.TextureCoord(0, 1);
            manual.Position(new Vector3(-500, -50, -500));      // --- Vertex 3 ---
            manual.TextureCoord(1, 1);

            // ---- Index Buffer ----
            // --- Triangle 0
            manual.Index(0);
            manual.Index(1);
            manual.Index(2);

            // --- Triangle1
            manual.Index(1);
            manual.Index(3);
            manual.Index(2);

            manual.End();

            manual.ConvertToMesh("Quad");

            wallEntity = mSceneMgr.CreateEntity("Quad_Entity", "Quad");

            //...
            wallEntity.SetMaterialName("TexAddressMode");
            wallNode = mSceneMgr.RootSceneNode.CreateChildSceneNode("Quad_Node");

            wallNode.AttachObject(wallEntity);
        }

        /// <summary>
        /// This method disposes of the scene node and enitity
        /// </summary>
        public void Dispose()
        {
            wallNode.DetachAllObjects();
            wallNode.Parent.RemoveChild(wallNode);
            wallNode.Dispose();
            wallEntity.Dispose();
        }
    }
}
