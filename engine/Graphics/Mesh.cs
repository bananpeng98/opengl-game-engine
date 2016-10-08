using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;
using Assimp;
using OpenTK;
using OpenTK.Graphics;
using Engine.Core;

namespace Engine.Graphics
{
    public class Mesh : BaseObject
    {
        private const int VERTEX_BUFFER = 0;
        private const int INDEX_BUFFER = 1;

        private int vao;
        private int[] buffers = new int[2];

        private List<Vertex> vertices = new List<Vertex>();
        private List<int> indices = new List<int>();

        private int vertexCount;
        private int indexCount;

        public Mesh()
        {
            vao = GL.GenVertexArray();
            GL.CreateBuffers(2, buffers);
        }

        public Mesh(string fileName)
            : this()
        {
            LoadMesh(fileName);
        }

        /// <summary>
        /// Bind the mesh.
        /// </summary>
        public void Bind()
        {
            GL.BindVertexArray(vao);
        }

        /// <summary>
        /// Unbind the mesh.
        /// </summary>
        public void Unbind()
        {
            GL.BindVertexArray(0);
        }
        
        public override void Unload()
        {
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffers(2, buffers);
        }
        
        /// <summary>
        /// Set the mesh data.
        /// </summary>
        /// <param name="vertices">The vertices to use for the mesh.</param>
        /// <param name="indices">The indices to use for the mesh.</param>
        public void SetData(List<Vertex> vertices, List<int> indices)
        {
            this.vertices = vertices;
            this.indices = indices;
        }

        /// <summary>
        /// Apply the vertex and index data to the mesh.
        /// </summary>
        public void Apply()
        {
            Bind();

            vertexCount = vertices.Count * 3;
            indexCount = indices.Count;

            int vertexSize = Marshal.SizeOf(typeof(Vertex));
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, buffers[VERTEX_BUFFER]);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(vertexCount * vertexSize), vertices.ToArray(), BufferUsageHint.StaticDraw);

            // Position
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, vertexSize, 0);

            // Normal
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, vertexSize, sizeof(float) * 3);

            // Tangent
            GL.EnableVertexAttribArray(2);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, vertexSize, sizeof(float) * 6);

            // Color
            GL.EnableVertexAttribArray(3);
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, vertexSize, sizeof(float) * 9);

            // UV
            GL.EnableVertexAttribArray(4);
            GL.VertexAttribPointer(4, 2, VertexAttribPointerType.Float, false, vertexSize, sizeof(float) * 13);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, buffers[INDEX_BUFFER]);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indexCount * sizeof(int)), indices.ToArray(), BufferUsageHint.StaticDraw);

            Unbind();
        }

        public override void Render()
        {
            GL.DrawElements(BeginMode.Triangles, vertexCount, DrawElementsType.UnsignedInt, 0);
        }

        private void LoadMesh(string fileName)
        {
            AssimpContext context = new AssimpContext();
            Assimp.Scene scene = context.ImportFile(fileName,
                PostProcessSteps.GenerateSmoothNormals | PostProcessSteps.Triangulate |
                PostProcessSteps.CalculateTangentSpace | PostProcessSteps.FlipUVs);
            Debug.Log("Could not load mesh scene.", MessageType.Error, scene.RootNode == null);

            ProcessMesh(scene.Meshes[0], scene);

            Apply();
        }

        private void ProcessMesh(Assimp.Mesh mesh, Assimp.Scene scene)
        {
            List<Vertex> mVerices = new List<Vertex>();
            List<int> mIndices = new List<int>();

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                Vector3 pos = new Vector3(mesh.Vertices[i].X, mesh.Vertices[i].Y, mesh.Vertices[i].Z);
                Vector3 nor = new Vector3(mesh.Normals[i].X, mesh.Normals[i].Y, mesh.Normals[i].Z);
                Vector3 tan = new Vector3();
                Color4 col = new Color4(1.0f, 1.0f, 1.0f, 1.0f);
                Vector2 uv = new Vector2();
                if(mesh.HasTangentBasis)
                    tan = new Vector3(mesh.Tangents[i].X, mesh.Tangents[i].Y, mesh.Tangents[i].Z);
                if (mesh.HasVertexColors(0))
                    col = new Color4(mesh.VertexColorChannels[0][i].R, mesh.VertexColorChannels[0][i].G, mesh.VertexColorChannels[0][i].B, mesh.VertexColorChannels[0][i].A);
                mVerices.Add(new Vertex(pos, nor, tan, col, uv));
            }

            for (int i = 0; i < mesh.FaceCount; i++)
            {
                Face face = mesh.Faces[i];
                for (int j = 0; j < face.IndexCount; j++)
                    mIndices.Add(face.Indices[j]);
            }

            SetData(mVerices, mIndices);
        }
    }
}
