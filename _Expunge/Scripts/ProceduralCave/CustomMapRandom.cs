using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

/// <summary>
/// Custom map random Class.
/// 
/// A Placeholder class for MapGenerator's Pseudo Random entities
/// MapGenerator was getting bloated, so.
/// 
/// </summary>
public class CustomMapRandom
{
	public int GetRandomFillPercentage()
	{
        //Make a PsuedoRandom Fill Percent
        //47% = Terrible Case [Single Coordinate Problems]
        //48% = Average Case [2-4 Coordinates]: useRandomWH for better Results
        //49% = Best Balance Scenario [ 4-6 Coordinates] : Consistancy Found
        //50% = Stable Scenario [7-9 Coordinates] : ignoreWH
        //51 and 52% = Cave Scenario [10-12 Coordinates Stable Case] : [12 - 16 Worst Case]
        //51 and 52% will get reduced in coordinates due to the rescale of WH to 75.

        //Stable cases - Returns 49-50%

        if (UnityEngine.Random.value <= 0.8f)
		{
			int fill1 = UnityEngine.Random.Range (48, 53);
			int fill2 = UnityEngine.Random.Range (49, 52);
			int fill3 = UnityEngine.Random.Range (49, 51);

			return (fill1 + fill2 + fill3) / 3;
		} 
		else return UnityEngine.Random.Range (50, 52);

	}

	public string GetRandomSeed()
	{
        UnityEngine.Random.InitState(DateTime.Now.Second);

        string randomSeed = "";
		string timeStr = DateTime.Now.Second.ToString() + UnityEngine.Random.value * Math.PI;

		for (int i = 0; i < timeStr.Length; i++)
		{
            randomSeed += (UnityEngine.Random.value > 0.5f) ? char.ConvertFromUtf32(UnityEngine.Random.Range(97, 124)) : timeStr[i].ToString();
        }

		return randomSeed;
	}
}
