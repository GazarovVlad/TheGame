using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using ProgramObjects.ScreenGroup;
using System.Drawing;
using SupportedStructures;

namespace TheGameDrawing.MeshRendering
{
    static class View
    {
        static private float zNearPlane; //размеры пространства в глубину
        static private float zFarPlane;
        static private Vector3 cameraPosition;
        static private Vector3 cameraTarget;
        static private Vector3 cameraUpVector;

		static public float ConstCoef { get; private set; }
		static public float ConstXCoef { get; private set; }
		static public float ConstYCoef { get; private set; }

        public static void Initialize()
        {
			ConstXCoef = (Screen.ResolutionW * 1.0f) / Screen.Width;
			ConstYCoef = (Screen.ResolutionH * 1.0f) / Screen.Height;
			ConstCoef = ConstXCoef / 2.0f + ConstYCoef / 2.0f;
            zNearPlane = 1.0f;
            zFarPlane = 10000.0f;
            cameraPosition = new Vector3(0.0f, 0, -500.0f);
            cameraTarget = new Vector3(0.0f, 0.0f, 0.0f);
            cameraUpVector = new Vector3(0, 1, 0);
        }

        public static void SetupCameraAndLight(Device targetDevice)
        {
			targetDevice.Transform.Projection = Matrix.OrthoLH(Screen.ResolutionW, Screen.ResolutionH, zNearPlane, zFarPlane);
            targetDevice.Transform.View = Matrix.LookAtLH(cameraPosition, cameraTarget, cameraUpVector);
            targetDevice.RenderState.Ambient = System.Drawing.Color.White;
            targetDevice.Lights[0].Type = LightType.Directional;
            targetDevice.Lights[0].Ambient = System.Drawing.Color.White;
            targetDevice.Lights[0].Diffuse = System.Drawing.Color.White;
            targetDevice.Lights[0].Falloff = 10.0f;
            targetDevice.Lights[0].Direction = new Vector3(0, 0, 1);
            targetDevice.Lights[0].Enabled = true;
        }

        public static void DrawMesh(MeshObject mesh, bool correction)
        {
            Vector3 pos;
            if (correction)
                pos = CoordinateCorrection(mesh.Pos);
            else
            {
                pos.X = mesh.Pos.X;
                pos.Y = mesh.Pos.Y;
                pos.Z = 0;
            }
            MeshRenderer.device.Transform.World = Matrix.RotationYawPitchRoll(mesh.Turn.X, mesh.Turn.Y, mesh.Turn.Z) * Matrix.Translation(pos.X, pos.Y, pos.Z);
            for (int i = 0; i < mesh.meshMaterials.Length; i++)
            {
                MeshRenderer.device.Material = mesh.meshMaterials[i];
                MeshRenderer.device.SetTexture(0, mesh.meshTextures[i]);
                mesh.meshObject.DrawSubset(i);
            }
        }

        private static Vector3 CoordinateCorrection(Vector2 pos)
        {
            Vector3 coords = new Vector3();
			coords.X = ((WorkSpace.MapLen / 2.0f - Screen.Width / 2.0f) - WorkSpace.Space.X) * ConstXCoef;
			coords.Y = (WorkSpace.Space.Y - (WorkSpace.MapLen / 2.0f - Screen.Height / 2.0f)) * ConstYCoef;
			coords.Z = 0;
            return coords;
        }
    }
}
