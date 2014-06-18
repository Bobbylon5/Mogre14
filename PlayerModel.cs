using System;
using Mogre;
using Mogre.TutorialFramework;
using PhysicsEng;

namespace RaceGame
{
    /// <summary>
    /// This class shows how to assemble a model using multiple meshes as a sub-graph of the scenegraph
    /// </summary>
    class PlayerModel : CharacterModel
    {
        #region Fields
        SceneManager mSceneMgr;

        PhysObj physObj;
 
        SceneNode model;                            // Root for the sub-graph

        SceneNode mainHull;
        Entity hullEntity;
        SceneNode sphere;
        Entity sphereEntity;
        SceneNode powerCells;
        Entity powerCellsEntity;
        SceneNode hullGroup;
        SceneNode wheelGroup;
        SceneNode gunGroup;

        Radian angle;                              // Angle for the mesh rotation
        Vector3 direction;                           // Direction of motion of the mesh for a single frame
        float radius;                         // Radius of the circular trajectory of the mesh

        //const float maxTime = 2000f;        // Time when the animation have to be changed
        //Timer time;                         // Timer for animation changes
        //AnimationState animationState1;      // Animation state, retrieves and store an animation from an Entity
        //bool animationChanged;              // Flag which tells when the mesh animation has changed
        //string animationName;               // Name of the animation to use

        #endregion

        /// <summary>
        /// This method returns the root node for the sub-scenegraph representing the compound model
        /// </summary>
        public SceneNode Model
        {
            get { return model; }
        }

        //
        #region AnimationName()
        /// <summary>
        /// Write only. This property allows to change the animation 
        /// passing the name of one of the animations in the animation state set
        /// </summary>
        //public string AnimationName
        //{
        //    set
        //    {
        //        HasAnimationChanged(value);
        //        if (IsValidAnimationName(value))
        //            animationName = value;
         //       else
        //            animationName = "Idle";
        //    }
        //}
        #endregion

        #region Position()
        /// <summary>
        /// Read only. This property gets the postion of the robot in the scene
        /// </summary>
        public Vector3 Position
        {
            get { return model.Position; }
        }
        #endregion

        #region AddChild()
        /// <summary>
        /// This method adds a child to the node of this model element
        /// </summary>
        /// <param name="childNode"></param>
        public void AddChild(SceneNode childNode)
        {
            //YOUR NODE FOR ATTACHING A CHILDNODE TO THE GAMENODE GOES HERE
            model.AddChild(childNode);
        }
        #endregion

        #region PlayerModel()
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene graph</param>
        public PlayerModel(SceneManager mSceneMgr)
        {
            this.mSceneMgr = mSceneMgr;

            LoadModelElements();
            AssembleModel();
            //AnimationSetup();
        }
        #endregion

        #region Load ()
        /// <summary>
        /// This method loads the nodes and entities needed by the compound model
        /// </summary>
        protected override void LoadModelElements()
        {
            hullGroup = mSceneMgr.CreateSceneNode();
            wheelGroup = mSceneMgr.CreateSceneNode();
            gunGroup = mSceneMgr.CreateSceneNode();

            mainHull = mSceneMgr.CreateSceneNode();
            hullEntity = mSceneMgr.CreateEntity("Main.mesh");
            hullEntity.GetMesh().BuildEdgeList();

            sphere = mSceneMgr.CreateSceneNode();
            sphereEntity = mSceneMgr.CreateEntity("Sphere.mesh");
            sphereEntity.GetMesh().BuildEdgeList();

            powerCells = mSceneMgr.CreateSceneNode();
            powerCellsEntity = mSceneMgr.CreateEntity("PowerCells.mesh");
            powerCellsEntity.GetMesh().BuildEdgeList();

            model = mSceneMgr.CreateSceneNode();


            

            float radius = 50;
            model.Position += radius * Vector3.UNIT_Y;
            hullGroup.Position -= radius * Vector3.UNIT_Y;

            physObj = new PhysObj(radius, "PlayerModel", 0.1f, 0.21f, 0.1f);
            physObj.SceneNode = model;
            physObj.Position = model.Position;
            physObj.AddForceToList(new WeightForce(physObj.InvMass));
            physObj.AddForceToList(new FrictionForce(physObj));
            Physics.AddPhysObj(physObj);
        }
        #endregion

        #region Assemble()
        /// <summary>
        /// This method assemble the model attaching the entities to 
        /// each node and appending the nodes to each other
        /// </summary>
        protected override void AssembleModel()
        {
            sphere.AttachObject(sphereEntity);
            mainHull.AttachObject(hullEntity);

            wheelGroup.AddChild(sphere);
            hullGroup.AddChild(mainHull);
            hullGroup.AddChild(powerCells);
            
            model.AddChild(hullGroup);
            hullGroup.AddChild(wheelGroup);
            hullGroup.AddChild(gunGroup);

            mSceneMgr.RootSceneNode.AddChild(model);
        }
        #endregion

        #region Set Position ()
        /// <summary>
        /// This methods set the position of the robot
        /// </summary>
        /// <param name="position"></param>
        public void setPosition(Vector3 position)
        {
            hullGroup.Translate(position);
        }
        #endregion

        //
        #region Annimation Setup()
        /// <summary>
        /// This method set up all the field needed for animation
        /// </summary>
       // private void AnimationSetup()
       // {
        //    radius = 0.01f;
        //    direction = Vector3.ZERO;
        //    angle = 0f;

        //    time = new Timer();
        //    PrintAnimationNames();
         //   animationChanged = false;
        //    animationName = "Walk";
        //    LoadAnimation();
        //}
        #endregion
        //
        #region Circular Motion()
        /// <summary>
        /// This method this method makes the mesh move in circle
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        //private void CircularMotion(FrameEvent evt)
        //{
        //    angle += (Radian)evt.timeSinceLastFrame;
        //    direction = radius * new Vector3(Mogre.Math.Cos(angle), 0, Mogre.Math.Sin(angle));
        //    model.Translate(direction);
        //    model.Yaw(-evt.timeSinceLastFrame);
        //}
        #endregion
        //
        #region Has annimation changed()
        /// <summary>
        /// This method sets the animationChanged field to true whenever the animation name changes
        /// </summary>
        /// <param name="newName"> The new animation name </param>
        //private void HasAnimationChanged(string newName)
        //{
         //   if (newName != animationName)
        //        animationChanged = true;
        //}
        #endregion
        //
        #region Print annimation names()
        /// <summary>
        /// This method prints on the console the list of animation tags
        /// </summary>
        //private void PrintAnimationNames()
        //{
       //     AnimationStateSet animStateSet = sphereEntity.AllAnimationStates;                    // Getd the set of animation states in the Entity
        //    AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();     // Iterates through the animation states

        //    while (animIterator.MoveNext())                                                     // Gets the next animation state in the set
        //    {
        //        Console.WriteLine(animIterator.CurrentKey);                                     // Print out the animation name in the current key
        //    }
        //}
        #endregion
        //
        #region Is valid animation name()
        /// <summary>
        /// This method deternimes whether the name inserted is in the list of valid animation names
        /// </summary>
        /// <param name="newName">An animation name</param>
        /// <returns></returns>
        //private bool IsValidAnimationName(string newName)
        //{
         //   bool nameFound = false;

         //   AnimationStateSet animStateSet = sphereEntity.AllAnimationStates;
         //   AnimationStateIterator animIterator = animStateSet.GetAnimationStateIterator();

         //   while (animIterator.MoveNext() && !nameFound)
         //   {
         //       if (newName == animIterator.CurrentKey)
         //       {
        //            nameFound = true;
        //        }
        //    }

        //    return nameFound;
        //}
        #endregion
        //
        #region change animation name()
        /// <summary>
        /// This method changes the animation name randomly
        /// </summary>
        //private void changeAnimationName()
        //{
        //    switch ((int)Mogre.Math.RangeRandom(0, 4.5f))       // Gets a random number between 0 and 4.5f
        //    {
        //        case 0:
        //            {
        //                animationName = "Walk";
        //                break;
        //            }
         //       case 1:
        //            {
        //                animationName = "Shoot";
        //                break;
        //            }
        //        case 2:
         //           {
         //               animationName = "Idle";
         //               break;
         //           }
          //      case 3:
          //          {
          //              animationName = "Slump";
          //              break;
          //          }
         //       case 4:
         //           {
         //               animationName = "Die";
         //               break;
         //           }
         //   }
       // }
        #endregion
        //
        #region LoadAnimation()
        /// <summary>
        /// This method loads the animation from the animation name
        /// </summary>
        //private void LoadAnimation()
        //{
        //    animationState1 = sphereEntity.GetAnimationState(animationName);
        //    animationState1.Loop = true;
        //    animationState1.Enabled = true;
        //}
        #endregion
        //
        #region AnimateMesh()
        /// <summary>
        /// This method puts the mesh in motion
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        //private void AnimateMesh(FrameEvent evt)
       // {
         //   if (time.Milliseconds > maxTime)
         //   {
          //      changeAnimationName();
         //       time.Reset();
         //   }

        //    if (animationChanged)
        //    {
         //       LoadAnimation();
         //       animationChanged = false;
         //   }

         //   animationState1.AddTime(evt.timeSinceLastFrame);
        //}
        #endregion
        //
        #region Animate()
        /// <summary>
        /// This method animates the robot mesh
        /// </summary>
        /// <param name="evt">A frame event which can be used to tune the animation timings</param>
        //public void Animate(FrameEvent evt)
        //{
        //    CircularMotion(evt);
        //    AnimateMesh(evt);
        //}
        #endregion


        #region Move()
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
        /// This method moves the model as a whole
        /// </summary>
        /// <param name="direction">The direction along which move the model</param>
        public void hMove(Vector3 direction)
        {
            hullGroup.Translate(direction);             // Notice that only the root od the sub-scenegraph is transformed, 
            // all the sub-nodes are tranformed as a consequence of this transformation
        }
        #endregion

        #region Rotate()
        /// <summary>
        /// This method rotate the model as a whole
        /// </summary>
        /// <param name="quaternion">The quaternion describing the rotation</param>
        /// <param name="transformSpace">The transformation on which rotate the model</param>
        //public void Rotate(Quaternion quaternion, 
        //             Node.TransformSpace transformSpace = Node.TransformSpace.TS_LOCAL)
        //{
        //    model.Rotate(quaternion, transformSpace);
        //}

        /// <summary>
        /// This method rotate the robot accordingly  with the given angles
        /// </summary>
        /// <param name="angles">The angles by which rotate the robot along each main axis</param>
        public void hRotate(Vector3 angles)
        {
            hullGroup.Yaw(angles.x);
        }

        /// <summary>
        /// This method rotate the robot accordingly  with the given angles
        /// </summary>
        /// <param name="angles">The angles by which rotate the robot along each main axis</param>
        public void Rotate(Vector3 angles)
        {
           model.Yaw(angles.x);  
        }
        #endregion

        #region Dispose()
        /// <summary>
        /// This method detaches and dispode of all the elements of the compound model
        /// </summary>
        public void Dispose()
        {
            //if (sphere != null)                     // Start removing from the leaves of the sub-graph
            //{
            //    if (sphere.Parent != null)
            //        sphere.Parent.RemoveChild(sphere);
           //     sphere.DetachAllObjects();
             //   sphere.Dispose();
           //     sphereEntity.Dispose();
            //}

            //if (mainHull != null)
            //{
            //    if (mainHull.Parent != null)
            //        mainHull.Parent.RemoveChild(mainHull);
           //     mainHull.DetachAllObjects();
           //     mainHull.Dispose();
           //     hullEntity.Dispose();
           // }

            if (model != null)                      // Stop removing with the sub-graph root
            {
                if (model.Parent != null)
                    model.Parent.RemoveChild(model);
                model.Dispose();
                Physics.RemovePhysObj(physObj);
                physObj = null;
            }

            
        }
        #endregion
    }
}
