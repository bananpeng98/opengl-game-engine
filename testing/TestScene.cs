using Engine;
using Engine.Components;
using Engine.Core;
using Engine.Graphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testing
{
    public class TestScene : Scene
    {
        private GameObject player = new GameObject();
        private GameObject thing = new GameObject();

        public TestScene()
        {
            MeshRenderer mr = thing.AddComponent<MeshRenderer>();
            mr.Mesh = new Mesh("Models/test.obj");
            
            player.Transform.WorldPosition = new Vector3(0, 0, 4);
            
            Camera.MainCamera = player.AddComponent<Camera>();
            player.AddComponent<FPController>();

            AddGameObject(thing);
            AddGameObject(player);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            thing.Transform.Rotate(new Vector3(0.0f, 0.5f, 0.5f), 0.01f);
        }
    }
}
