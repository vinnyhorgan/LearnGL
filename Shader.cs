using System;
using System.IO;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace LearnGL
{
    class Shader
    {
        public readonly int ID;

        private Dictionary<string, int> uniformLocations;

        public Shader(string vertPath, string fragPath, Dictionary<int, string> attributes)
        {
            var shaderSource = File.ReadAllText(vertPath);
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, shaderSource);
            CompileShader(vertexShader);

            shaderSource = File.ReadAllText(fragPath);
            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, shaderSource);
            CompileShader(fragmentShader);

            ID = GL.CreateProgram();

            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);

            foreach (var attribute in attributes)
            {
                GL.BindAttribLocation(ID, attribute.Key, attribute.Value);
            }

            LinkProgram(ID);

            GL.DetachShader(ID, vertexShader);
            GL.DetachShader(ID, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            GL.GetProgram(ID, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

            uniformLocations = new Dictionary<string, int>();

            for (int i = 0; i < numberOfUniforms; i++)
            {
                var name = GL.GetActiveUniform(ID, i, out _, out _);
                uniformLocations.Add(name, GL.GetUniformLocation(ID, name));
            }
        }

        public void Attach()
        {
            GL.UseProgram(ID);
        }

        public void Detach()
        {
            GL.UseProgram(0);
        }

        public void Unload()
        {
            GL.DeleteProgram(ID);
        }

        public void SetInt(string name, int value)
        {
            GL.UseProgram(ID);
            GL.Uniform1(uniformLocations[name], value);
        }

        public void SetFloat(string name, float value)
        {
            GL.UseProgram(ID);
            GL.Uniform1(uniformLocations[name], value);
        }

        public void SetBoolean(string name, bool value)
        {
            float toLoad = 0;

            if (value)
            {
                toLoad = 1;
            }

            GL.Uniform1(uniformLocations[name], toLoad);
        }

        public void SetVector3(string name, Vector3 value)
        {
            GL.UseProgram(ID);
            GL.Uniform3(uniformLocations[name], value);
        }

        public void SetMatrix(string name, Matrix4 value)
        {
            GL.UseProgram(ID);
            GL.UniformMatrix4(uniformLocations[name], true, ref value);
        }

        private static void CompileShader(int shader)
        {
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error compiling shader: {infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            GL.LinkProgram(program);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                var infoLog = GL.GetProgramInfoLog(program);
                throw new Exception($"Error linking shader program: {infoLog}");
            }
        }
    }
}
