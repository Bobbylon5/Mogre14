using System;
using Mogre;

namespace Mogre.Tutorials
{
    /// <summary>
    /// This class exemplify how to describe manually a mesh setting up the vertex and index buffer
    /// </summary>
    class ManualObjectExample
    {
        private SceneManager mSceneMgr;

        private ManualObject manualObj;
        private Entity manualObjEntity;
        private SceneNode manualObjNode;

        /// <summary>
        /// Constructor, it stores a reference to the scenegraph into a private field
        /// </summary>
        /// <param name="mSceneMgr"> A reference to the scene manager </param>
        public ManualObjectExample(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;
        }

        /// <summary>
        /// This private method defines the vertex and index buffers of a quad as a triangle list.
        /// </summary>
        private MeshPtr Quad()
        {
            manualObj = mSceneMgr.CreateManualObject("maualObjQuad");
            manualObj.Begin("void", RenderOperation.OperationTypes.OT_TRIANGLE_LIST);    // Starts filling the manual object as a triangle list
            
            // ---- Vertex Buffer ----
            manualObj.Position(new Vector3(5, 5, 0));               // --- Vertex 0 ---
            manualObj.Position(new Vector3(-5, 5, 0));              // --- Vertex 1 ---
            manualObj.Position(new Vector3(5, -5, 0));              // --- Vertex 2 ---
            manualObj.Position(new Vector3(-5, -5, 0));             // --- Vertex 3 ---

            // ---- Index Buffer ----
            // --- Triangle 0 ---
            manualObj.Index(0);
            manualObj.Index(1);
            manualObj.Index(2);

            // --- Triangle 1 ---
            manualObj.Index(1);
            manualObj.Index(3);
            manualObj.Index(2);

            manualObj.End();                                        // Closes the manual objet

            return manualObj.ConvertToMesh("Quad");                 // Prepares the data to be sent to the GPU
        }

        /// <summary>
        /// This metod loads a quad in an entity and attaches it to the scenegraph
        /// </summary>
        public void Load()
        {
            Quad();

            manualObjEntity = mSceneMgr.CreateEntity("Quad_Endtity", "Quad");    // Creates a new entity which contains the geometry
            manualObjNode = mSceneMgr.CreateSceneNode("Quad_Node");              // Creates a new node for the scene graph
            manualObjNode.AttachObject(manualObjEntity);                         // Attaches the entity (geometry) to the node
            mSceneMgr.RootSceneNode.AddChild(manualObjNode);                     // Adds the node as child of the root of the scene graph
        }

        /// <summary>
        /// This method detaches the node from the scene graph and destroies the node and the entity
        /// </summary>
        public void Dispose()
        {
            if (manualObjNode != null)
            {
                manualObjNode.Parent.RemoveChild(manualObjNode);
                manualObjNode.DetachAllObjects();
                manualObjNode.Dispose();
                manualObjNode = null;
            }
            if (manualObjEntity != null)
            {
                manualObjEntity.Dispose();
                manualObjEntity = null;
            }
        }

        #region NonStaticGeo
        /// <summary>
        /// This method creates 1000 quads and attaches them to the scenegraph
        /// </summary>
        public void NonStaticGeometry()
        {
            Quad();

            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++)
                {
                    manualObjEntity = mSceneMgr.CreateEntity("Quad");
                    manualObjNode = mSceneMgr.RootSceneNode.CreateChildSceneNode(new Vector3(i * 5, j * 5, 0));
                    manualObjNode.AttachObject(manualObjEntity);
                }
        }
        #endregion

        #region StaticGeo
        private StaticGeometry staticGeo;
        /// <summary>
        /// This method creates 4000 quads and attaches them to the scenegraph as static geometry
        /// </summary>
        public void StaticGeometry()
        {
            Quad();
            
            staticGeo = mSceneMgr.CreateStaticGeometry("staticQuads");                   // Initializes the static geometry container
            for(int i=0; i<200; i++)
                for (int j = 0; j < 200; j++)
                {
                    manualObjEntity = mSceneMgr.CreateEntity("Quad");                    // Loads the quad in an entity
                    staticGeo.AddEntity(manualObjEntity, new Vector3(i * 5, j * 5, 0));  // adds it to the scenegraph as static geo
                }
            staticGeo.Build();                                                           // Prepares the static geometry to be rendered
        }
        #endregion
    }
}
