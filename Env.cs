using System;
using Mogre;
using PhysicsEng;
using System.Collections.Generic;

namespace Mogre.Tutorial
{
    /// <summary>
    /// This class implements the game environment
    /// </summary>
    class Env : Environment
    {
        RenderWindow mWindow;               // This field will contain a reference to the rendering window
        protected Wall wall1;                         // This field will contain an istance of the wall object
        protected Wall wall2;                         // This field will contain an istance of the wall object
        protected Wall wall3;                         // This field will contain an istance of the wall object

        protected String bob = "Vector3.UNIT_X";

        #region Environment Env
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mSceneMgr">A reference to the scene manager</param>
        public Env(SceneManager mSceneMgr, RenderWindow mWindow)
        {
            this.mSceneMgr = mSceneMgr;
            this.mWindow = mWindow;

            Load();                                 // This method loads  the environment
        }
        #endregion


        #region Load
        private void Load()
        {
            SetLights();
            SetSky();
            SetFog();
            SetShadows();

            wall1 = new Wall(mSceneMgr);
            //wall2 = new Wall(mSceneMgr);
            //wall2.Yaw(180);
            //wall1.SetPosition(new Vector3(000, 500, 000));
            ground = new Ground(mSceneMgr);
            //walls = new List<Wall>();
            //walls.Add(wall1);
            //Physics.AddBoundary(ground.Plane);
        }
        #endregion


        #region Sky
        private void SetSky()
        {
            //mSceneMgr.SetSkyDome(true, "Sky", 1f, 10, 500, true);

            //Plane sky = new Plane(Vector3.NEGATIVE_UNIT_Y, -100);
            //mSceneMgr.SetSkyPlane(true, sky, "Sky", 10, 5, true, 0.5f, 100, 100,
            //    ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

            mSceneMgr.SetSkyBox(true, "Examples/SpaceSkyBox", 10, true);
        }
        #endregion

        #region Fog
        private void SetFog()
        {
            ColourValue fadeColour = new ColourValue(0.1f, 0.1f, 1f);
            //mSceneMgr.SetFog(FogMode.FOG_LINEAR, fadeColour, 0.1f, 100, 1000);
            //mSceneMgr.SetFog(FogMode.FOG_EXP, fadeColour, 0.001f);
            mSceneMgr.SetFog(FogMode.FOG_EXP2, fadeColour, 0.002f);
            mWindow.GetViewport(0).BackgroundColour = fadeColour;
        }
        #endregion

        #region Lights
        /// <summary>
        /// This method sets the lights in the environment
        /// </summary>
        private void SetLights()
        {
            mSceneMgr.AmbientLight = new ColourValue(0.3f, 0.3f, 0.3f);                 // Set the ambient light in the scene

            light = mSceneMgr.CreateLight();                                            // Set an instance of a light;

            light.DiffuseColour = ColourValue.Red;                                      // Sets the color of the light
            light.Position = new Vector3(0, 100, 0);                                    // Sets the position of the light

            //light.Type = Light.LightTypes.LT_DIRECTIONAL;                               // Sets the light to be a directional Light

            //light.Type = Light.LightTypes.LT_SPOTLIGHT;                                 // Sets the light to be a spot light
            //light.SetSpotlightRange(Mogre.Math.PI / 6, Mogre.Math.PI / 4, 0.001f);      // Sets the spot light parametes

            //light.Direction = Vector3.NEGATIVE_UNIT_Y;                                  // Sets the light direction

            light.Type = Light.LightTypes.LT_POINT;                                     // Sets the light to be a point light

            float range = 500;                                                         // Sets the light range
            float constantAttenuation = 0;                                              // Sets the constant attenuation of the light [0, 1]
            float linearAttenuation = 0;                                                // Sets the linear attenuation of the light [0, 1]
            float quadraticAttenuation = 0.0001f;                                       // Sets the quadratic  attenuation of the light [0, 1]

            light.SetAttenuation(range, constantAttenuation,
                      linearAttenuation, quadraticAttenuation); // Not applicable to directional ligths
        }
        #endregion

        #region Shadows
        private void SetShadows()
        {
            //mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_MODULATIVE;
            mSceneMgr.ShadowTechnique = ShadowTechnique.SHADOWTYPE_STENCIL_ADDITIVE;
        }
        #endregion
    }
}