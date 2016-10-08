using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System.IO;
using OpenTK;
using Engine.Core;

namespace Engine.Graphics
{
    public class Shader : BaseObject
    {
        private int program;
        private Dictionary<string, int> uniforms = new Dictionary<string, int>();

        public Shader()
        {
            program = CreateProgram();
        }

        public Shader(string shaderName) : this()
        {
            Compile($"Shaders/{shaderName}.vs", $"Shaders/{shaderName}.fs");
            AddUniform("M");
            AddUniform("V");
            AddUniform("P");
            AddUniform("MVP");
        }

        public override void Unload()
        {
            GL.DeleteProgram(program);
        }

        public void Bind()
        {
            GL.UseProgram(program);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        /// <summary>
        /// Gets the uniform location and adds it to the shader.
        /// </summary>
        /// <param name="name">The name of the uniform.</param>
        public void AddUniform(string name)
        {
            int location = GL.GetUniformLocation(program, name);
            if (location > -1)
                uniforms[name] = location;
            else
                Debug.Log($"Unused uniform: {name}", MessageType.Warning);
        }

        public void SetUniform(string name, float value)
        {
            if (uniforms.ContainsKey(name))
                GL.Uniform1(uniforms[name], value);
        }

        public void SetUniform(string name, Matrix4 value)
        {
            if (uniforms.ContainsKey(name))
                GL.UniformMatrix4(uniforms[name], false, ref value);
        }

        /// <summary>
        /// Compile both vertex and fragment shaders. This will also link the program.
        /// </summary>
        /// <param name="vertexShader">The file name of the vertex shader source.</param>
        /// <param name="fragmentShader">The file name of the fragment shader source.</param>
        public void Compile(string vertexShader, string fragmentShader)
        {
            int vs = Compile(vertexShader, ShaderType.VertexShader);
            int fs = Compile(fragmentShader, ShaderType.FragmentShader);

            if (vs == 0 || fs == 0) return;

            Link();

            DetachAndDelete(vs);
            DetachAndDelete(fs);
        }

        /// <summary>
        /// Compile shader source to program.
        /// </summary>
        /// <param name="shaderFile">The shader file name source.</param>
        /// <param name="shaderType">The type of shader to create</param>
        public int Compile(string shaderFile, ShaderType shaderType)
        {
            int shader = CreateShader(shaderType);
            string shaderSource = LoadShaderSource(shaderFile);
            
            GL.ShaderSource(shader, shaderSource);
            GL.CompileShader(shader);

            string info = GL.GetShaderInfoLog(shader);
            Debug.Log(info, MessageType.Error, !string.IsNullOrEmpty(info));

            GL.AttachShader(program, shader);

            return shader;
        }

        /// <summary>
        /// Link the shader program.
        /// </summary>
        public void Link()
        {
            GL.LinkProgram(program);

            string info = GL.GetProgramInfoLog(program);
            Debug.Log(info, MessageType.Error, !string.IsNullOrEmpty(info));
        }

        private void DetachAndDelete(int shader)
        {
            GL.DetachShader(program, shader);
            GL.DeleteShader(shader);
        }

        /// <summary>
        /// Creates a shader program.
        /// </summary>
        /// <returns></returns>
        private int CreateProgram()
        {
            int program = GL.CreateProgram();
            Debug.Log(
                "Could not create shader program.",
                MessageType.Error,
                program == 0
            );
            return program;
        }

        /// <summary>
        /// Creates a shader of a specified type.
        /// </summary>
        /// <param name="shaderType">The shader type to create.</param>
        /// <returns></returns>
        private int CreateShader(ShaderType shaderType)
        {
            int shader = GL.CreateShader(shaderType);
            
            if (shader == 0)
            {
                Debug.Log(
                    $"Could not create shader of type {Enum.GetName(typeof(ShaderType), shaderType)}.",
                    MessageType.Error
                );
            }

            return shader;
        }

        /// <summary>
        /// Load the shader source code.
        /// </summary>
        /// <param name="shaderFile">The path and name of the shader file.</param>
        /// <returns></returns>
        private string LoadShaderSource(string shaderFile)
        {
            string shaderSource = null;
            
            try
            {
                shaderSource = File.ReadAllText(shaderFile);
            }
            catch (Exception e)
            {
                Debug.Log(
                    $"Could not load shader source from file: {shaderFile}",
                    MessageType.Error
                );
                Debug.Log(e.Message, MessageType.Error);
            }

            return shaderSource;
        }
    }
}
