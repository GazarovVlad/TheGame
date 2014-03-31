using System;
using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;
using ProgramObjects.ScreenGroup;
using TheGameDrawing.MeshRendering.RenderingObjects;
using System.Drawing;
using SupportedStructures;

namespace TheGameDrawing.MeshRendering
{
    public static class MeshRenderer
    {
        public static Device device { get; private set; }

        //rendering objects
        public static Planet planet { get; private set; }
        public static PlanetIndicator planetIndicator { get; private set; }

        public static void Initialize(Device targetDevice)
        {
            device = targetDevice;
            View.Initialize();

			MeshObject earth = MeshLoader.LoadMesh(device, "Meshes\\Sphere_Eath.x", "Textures\\Earth_.jpg");
			MeshObject clouds = MeshLoader.LoadMesh(device, "Meshes\\Sphere_Clouds.x", "Textures\\clouds.png");
            Vector2 planetPos = new Vector2();
			planetPos.X = 0;// - WorkSpace.Space.Width / 2.0);// -WorkSpace.DownBorder.Height / 2;
			planetPos.Y = 0;// - WorkSpace.Space.Height / 2.0);// -WorkSpace.RightBorder.Width / 2 + Screen.BorderHigh / 2;
            planet = new Planet(earth, clouds, planetPos);

			planet.LoadExplosion(device, "Meshes\\Plane_CratersAndExplosions.x", "Textures\\explosion\\");
			planet.LoadCraters(device, "Meshes\\Plane_CratersAndExplosions.x", "Textures\\craters\\");

			MeshObject indicator = MeshLoader.LoadMesh(device, "Meshes\\Sphere_EathIndicator.x", "Textures\\Earth_.jpg");
			planetIndicator = new PlanetIndicator(indicator, new Vector2((Screen.Width / 2.0f - WorkSpace.MiniMapBorder.Width / 2.0f) * View.ConstXCoef, -1-(Screen.Height / 2.0f - WorkSpace.MiniMapBorder.Height / 2.0f) * View.ConstYCoef));

            View.SetupCameraAndLight(device);
        }
    }
}
