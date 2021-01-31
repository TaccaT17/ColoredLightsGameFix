using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{   
    public enum ColorOfLight
    {
        red,
        yellow,
        blue
    }

    public bool debugRedBoolLight = false;

    #region VARIABLES
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public int redLitsNeeded, yellowLitsNeeded, blueLitsNeeded;

    private Dictionary<ColorOfLight, Light> litBy;
    private Light initRedLight, initYellowLight, initBlueLight;

    private MeshRenderer meshRendererRef;
    private Collider colliderRef;
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    #endregion



    #region FUNCTIONS
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Start()
    {
        Init();
    }

    public void Update()
    {
        if (debugRedBoolLight && litBy[ColorOfLight.red].currentLits < 1)
        {
            LightTouched(ColorOfLight.red);
        }
    }

    public void Init()
    {
        GameManager.S.GetOrCreateComponent(out colliderRef, this.gameObject);
        GameManager.S.GetOrCreateComponent(out meshRendererRef, this.gameObject);

        Constructor(redLitsNeeded, yellowLitsNeeded, blueLitsNeeded);

        CheckAndChangeOpacity();
    }
    /// <summary>
    /// Parameters are how many of each color light needed to be lighting that object in order to "solidify" the object
    /// </summary>
    /// <param name="redLits"></param>
    /// <param name="yellowLits"></param>
    /// <param name="blueLits"></param>
    public void Constructor(int redLits, int yellowLits, int blueLits)
    {
        initRedLight.neededLits = redLits;
        initRedLight.currentLits = 0;
        initYellowLight.neededLits = yellowLits;
        initYellowLight.currentLits = 0;
        initBlueLight.neededLits = blueLits;
        initBlueLight.currentLits = 0;

        //create Dictionaries
        litBy = new Dictionary<ColorOfLight, Light>();
        litBy.Add(ColorOfLight.red, initRedLight);
        litBy.Add(ColorOfLight.yellow, initYellowLight);
        litBy.Add(ColorOfLight.blue, initBlueLight);
    }
    
    public int GetLitAmount(ColorOfLight lightColor)
    {
        return litBy[lightColor].currentLits;
    }
    public void LightTouched(ColorOfLight lightColor)
    {
        Light tempLight = litBy[lightColor];
        tempLight.currentLits++;
        litBy[lightColor] = tempLight;

        CheckAndChangeOpacity();
    }
    public void LightLeft(ColorOfLight lightColor)
    {
        Light tempLight = litBy[lightColor];
        tempLight.currentLits--;
        litBy[lightColor] = tempLight;

        CheckAndChangeOpacity();
    }
    private void CheckAndChangeOpacity()
    {
        int currentLitness = 0;
        int neededLitNess = 0;

        //get total lights lighting
        foreach (KeyValuePair<ColorOfLight, Light> item in litBy)
        {
            //if needed amount is 0 don't count it
            if (!(item.Value.neededLits.Equals(0)))
            {
                currentLitness += item.Value.currentLits;
                neededLitNess += item.Value.neededLits;
            }
        }

        //don't divide by 0
        if (neededLitNess.Equals(0))
        {
            print("Oops. Trying to divide by 0. You forgot to set a LightObject's LitsNeeded");
            return;
        }

        float opacityPercentage = (float)currentLitness / (float)neededLitNess;

        //set new opacity
        Color color = meshRendererRef.material.color;
        color.a = opacityPercentage;
        meshRendererRef.material.color = color;

        if (opacityPercentage >= 1)
        {
            colliderRef.enabled = true;
        }
        else
        {
            colliderRef.enabled = false;
        }

    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    #endregion

    //STRUCT
    private struct Light
    {
        public int currentLits;
        public int neededLits;
    }

    
}
