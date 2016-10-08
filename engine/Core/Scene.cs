using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Engine.Core
{
    public class Scene : GameWindow
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        
        public Scene()
        {
            
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Input.Initialize(this);

            GL.CullFace(CullFaceMode.Back);
            GL.FrontFace(FrontFaceDirection.Cw);
            GL.Enable(EnableCap.DepthTest);
            GL.Disable(EnableCap.CullFace);

            Input.CenterMouse();
            
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Start();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Screen.Width = Width;
            Screen.Height = Height;
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Unload();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            Input.Update();

            base.OnUpdateFrame(e);

            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Update();

            Input.AfterUpdate();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            for (int i = 0; i < gameObjects.Count; i++)
                gameObjects[i].Render();

            SwapBuffers();
        }

        public void AddGameObject(GameObject gameObject)
        {
            gameObjects.Add(gameObject);
        }
    }
}
