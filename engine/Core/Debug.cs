using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Engine.Components;

namespace Engine.Core
{
    public enum MessageType
    {
        Info = ConsoleColor.White,
        Warning = ConsoleColor.Yellow,
        Error = ConsoleColor.Red
    }

    static class Debug
    {
        public static void Log(string message, MessageType messageType = MessageType.Info, bool assertion = true)
        {
            if (assertion)
            {
                WriteMessageType(messageType);
                Console.Write(message + Environment.NewLine);
            }
        }

        public static void DrawVector(Vector3 origin, Vector3 vector)
        {
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 VP = Camera.MainCamera.View * Camera.MainCamera.Projection * Matrix4.CreateTranslation(origin);
            GL.LoadMatrix(ref VP);
            GL.Color3(255, 255, 255);
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(origin);
            GL.Vertex3(origin + vector);
            GL.End();
        }

        private static void WriteMessageType(MessageType messageType)
        {
            Console.ForegroundColor = (ConsoleColor)messageType;
            Console.Write($"{Enum.GetName(typeof(MessageType), messageType)}: ");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
