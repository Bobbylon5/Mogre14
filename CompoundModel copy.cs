using System;
using Mogre;

namespace Mogre.Tutorials
{
    /// <summary>
    /// This class shows how to assemble a model using multiple meshes as a sub-graph of the scenegraph
    /// </summary>
    class CompoundModel
    {
        SceneManager mSceneMgr;

        SceneNode mainHull;
        Entity hullEntity;
        SceneNode sphere;
        Entity sphereEntity;

        SceneNode model;                            // Root for the sub-graph

        /// <summary>
        /// This method returns the root node for the sub-scenegraph representing the compound model
        /// </summary>
        public SceneNode Model
        {
            get { return model; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene graph</param>
        public CompoundModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;

            Load();
            AssembleModel();
        }

        /// <summary>
        /// This method loads the nodes and entities needed by the compound model
        /// </summary>
        private void Load()
        {
            mainHull = mSceneMgr.CreateSceneNode();
            hullEntity = mSceneMgr.CreateEntity("Main.mesh");

            sphere = mSceneMgr.CreateSceneNode();
            sphereEntity = mSceneMgr.CreateEntity("Sphere.mesh");

            model = mSceneMgr.CreateSceneNode();
        }

        /// <summary>
        /// This method assemble the model attaching the entities to 
        /// each node and appending the nodes to each other
        /// </summary>
        private void AssembleModel()
        {
            mainHull.AttachObject(hullEntity);
            sphere.AttachObject(sphereEntity);

            mainHull.AddChild(sphere);
            model.AddChild(mainHull);
            
            mSceneMgr.RootSceneNode.AddChild(model);
        }

        /// <summary>
        /// This method moves the model as a whole
        /// </summary>
        /// <param name="direction">The direction along which move the model</param>
        public void Move(Vector3 direction)
        {
            model.Translate(direction);             // Notice that only the root od the sub-scenegraph is transformed, 
                                                    // all the sub-nodes are tranformed as a consequence of this transformation
        }

        /// <summary>
        /// This method rotate the model as a whole
        /// </summary>
        /// <param name="quaternion">The quaternion describing the rotation</param>
        /// <param name="transformSpace">The transformation on which rotate the model</param>
        public void Rotate(Quaternion quaternion, 
                     Node.TransformSpace transformSpace = Node.TransformSpace.TS_LOCAL)
        {
            model.Rotate(quaternion, transformSpace);
        }

        /// <summary>
        /// This method detaches and dispode of all the elements of the compound model
        /// </summary>
        public void Dispose()
        {
            if (sphere != null)                     // Start removing from the leaves of the sub-graph
            {
                if (sphere.Parent != null)
                    sphere.Parent.RemoveChild(sphere);
                sphere.DetachAllObjects();
                sphere.Dispose();
                sphereEntity.Dispose();
            }

            if (mainHull != null)
            {
                if (mainHull.Parent != null)
                    mainHull.Parent.RemoveChild(mainHull);
                mainHull.DetachAllObjects();
                mainHull.Dispose();
                hullEntity.Dispose();
            }

            if (model != null)                      // Stop removing with the sub-graph root
            {
                if (model.Parent != null)
                    model.Parent.RemoveChild(model);
                model.Dispose();
            }

        }
    }
}
