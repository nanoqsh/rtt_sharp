﻿using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using RT.Engine;
using RT.Render.FrameBuffers;

namespace RT.Render
{
    class Frame
    {
        private readonly ShaderProgram shader;
        private readonly Camera camera;
        public Rectangle Size { get; private set; }
        public Matrix4 Ortho { get; private set; }
        private readonly Postprocessor post;
        private readonly int pixelSize = 1;

        private readonly DirectionalLight light;
        private readonly DepthBuffer depthBuffer;
        private readonly ShaderProgram depthShader;

        public Frame()
        {
            shader = new ShaderProgram(
                new Shader("def_vs.glsl", ShaderType.VertexShader),
                new Shader("def_fs.glsl", ShaderType.FragmentShader)
                );

            depthShader = new ShaderProgram(
                new Shader("depth_vs.glsl", ShaderType.VertexShader),
                new Shader("empty_fs.glsl", ShaderType.FragmentShader)
                );

            light = new DirectionalLight();

            depthBuffer = new DepthBuffer();

            camera = Core.Unit.Player.Camera;

            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Enable(EnableCap.Blend);

            GL.Enable(EnableCap.Multisample);

            post = new Postprocessor();
        }

        public void Draw()
        {
            shader.Enable();
            var meshes = Core.Unit.Map.World.GetMeshes(shader);

            depthShader.Enable();

            Matrix4 lightSpace = light.View * light.Projection;
            GL.UniformMatrix4(depthShader.GetUniformIndex("light_space"), false, ref lightSpace);

            GL.Viewport(0, 0, DepthBuffer.SHADOW_WIDTH, DepthBuffer.SHADOW_HEIGHT);

            depthBuffer.Bind();
            GL.Clear(ClearBufferMask.DepthBufferBit);

            foreach ((Mesh m, Vector3 v) in meshes)
            {
                Matrix4 model = Matrix4.CreateTranslation(v);
                GL.UniformMatrix4(depthShader.GetUniformIndex("model"), false, ref model);

                m.Draw();
            }

            depthBuffer.Unbind();


            GL.Viewport(Size);

            post.Bind();

            GL.Enable(EnableCap.DepthTest);

            GL.ClearColor(Color.MidnightBlue);
            GL.Clear(
                  ClearBufferMask.ColorBufferBit
                | ClearBufferMask.DepthBufferBit
                );

            shader.Enable();

            Matrix4 projection = camera.Projection(Size);
            GL.UniformMatrix4(shader.GetUniformIndex("projection"), false, ref projection);

            Matrix4 view = camera.View;
            GL.UniformMatrix4(shader.GetUniformIndex("view"), false, ref view);

            GL.Uniform1(shader.GetUniformIndex("layer0"), 1);
            GL.ActiveTexture(TextureUnit.Texture0 + 1);
            GL.BindTexture(TextureTarget.Texture2D, Core.Unit.SpriteMap.Texture.Index);

            depthBuffer.BindFrameTexture(shader.GetUniformIndex("shadow_map"));

            Color4 fogColour = Color4.MidnightBlue;
            GL.Uniform4(shader.GetUniformIndex("fog_colour"), fogColour);

            foreach ((Mesh m, Vector3 v) in meshes)
            {
                Matrix4 model = Matrix4.CreateTranslation(v);
                GL.UniformMatrix4(shader.GetUniformIndex("model"), false, ref model);

                m.Draw();
            }

            shader.Disable();

            GL.Disable(EnableCap.DepthTest);

            post.Draw();
        }

        public void Resize(Rectangle size)
        {
            Size = size;
            Ortho = Matrix4.CreateOrthographic(size.Width, size.Height, -100, 100);

            post.Resize(size.Width, size.Height, 0, pixelSize);
        }
    }
}
