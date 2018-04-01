using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ProceduralCave
{
	[Serializable]
	public class CaveColors
	{
		public string name;

		[Space]
		public Color wallColor;
		public Color caveToonColor;
		public Color caveToonBorder;
	}

    public class ColorManager : MonoBehaviour
    {
        [Header("Overrides the <Material> <Colors>")]
        public bool killOnWorkDone = false;

        [Space]
        public Material wallMaterial;
        public Material caveMaterial;
        public Material skyboxMaterial;

        [Header("List of Colours")]	
		public List<CaveColors> caveColors;
        public List<Color> skyboxColorInverted;

        void OnEnable()
        {
            //Trying something (-_-)
            UnityEngine.Random.InitState( DateTime.Now.Second);

            SetWallAndCaveColors();
            SetSkyBoxColor();

            if (killOnWorkDone)
            {
                Destroy(this);
            }
        }

        void SetWallAndCaveColors()
        {
			int index =  UnityEngine.Random.Range(0, caveColors.Count);

			//wallMaterial.SetColor("_Color", wallColorsHex[index]);
			//caveMaterial.SetColor("_Color", caveToonColorHex[index]);
			//caveMaterial.SetColor("_OutlineColor", caveToonBorderHex[index]);

			wallMaterial.SetColor("_Color", caveColors[index].wallColor);
			caveMaterial.SetColor("_Color", caveColors[index].caveToonColor);
			caveMaterial.SetColor("_OutlineColor", caveColors[index].caveToonBorder);
        }

        void SetSkyBoxColor()
        {
			int index = UnityEngine.Random.Range(0, skyboxColorInverted.Count);
            skyboxMaterial.SetColor("_SkyTint", skyboxColorInverted[index]);
        }
    }
}


