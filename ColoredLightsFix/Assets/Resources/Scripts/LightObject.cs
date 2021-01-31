using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightObject : MonoBehaviour
{

    #region VARIABLES
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public bool Stepable = false;
    
    public bool redLitsNeeded, yellowLitsNeeded, blueLitsNeeded;

    private Dictionary<GameManager.ColorOfLight, Light> litBy;
    private Light initRedLight, initYellowLight, initBlueLight;

    [SerializeField]
    bool debugRedLit;

    [SerializeField]
    MeshRenderer meshRendererRef;
    [SerializeField]
    Collider colliderRef;
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    #endregion



    #region FUNCTIONS
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void Start()
    {
        Init();
    }

    void Update()
    {
        if (debugRedLit && !litBy[GameManager.ColorOfLight.red].currentLit)
        {
            Light tempLight;
            tempLight = litBy[GameManager.ColorOfLight.red];
            tempLight.currentLit = true;
            litBy[GameManager.ColorOfLight.red] = tempLight;
        }
    }
    
    private void LateUpdate()
    {
        /*
        CheckAndChangeOpacity();

        Light tempLight;
        tempLight = litBy[GameManager.ColorOfLight.blue];
        tempLight.currentLit = false;
        litBy[GameManager.ColorOfLight.blue] = tempLight;

        tempLight = litBy[GameManager.ColorOfLight.red];
        tempLight.currentLit = false;
        litBy[GameManager.ColorOfLight.red] = tempLight;

        tempLight = litBy[GameManager.ColorOfLight.yellow];
        tempLight.currentLit = false;
        litBy[GameManager.ColorOfLight.yellow] = tempLight;
        */
    }
    

    public void Init()
    {
        GameManager.S.GetOrCreateComponent(out colliderRef, this.gameObject);
        GameManager.S.GetOrCreateComponent(out meshRendererRef, this.gameObject);

        Constructor(redLitsNeeded, yellowLitsNeeded, blueLitsNeeded);

        CheckAndChangeOpacity();
        /*
        Color color = meshRendererRef.material.GetColor("_BaseColor");
        color.a = 0;
        meshRendererRef.material.SetColor("_BaseColor", color);
        */
    }
    /// <summary>
    /// Parameters are how many of each color light needed to be lighting that object in order to "solidify" the object
    /// </summary>
    /// <param name="redLits"></param>
    /// <param name="yellowLits"></param>
    /// <param name="blueLits"></param>
    public void Constructor(bool redLits, bool yellowLits, bool blueLits)
    {
        initRedLight.neededLit = redLits;
        initRedLight.currentLit = false;
        initYellowLight.neededLit = yellowLits;
        initYellowLight.currentLit = false;
        initBlueLight.neededLit = blueLits;
        initBlueLight.currentLit = false;

        //create Dictionaries
        litBy = new Dictionary<GameManager.ColorOfLight, Light>();
        litBy.Add(GameManager.ColorOfLight.red, initRedLight);
        litBy.Add(GameManager.ColorOfLight.yellow, initYellowLight);
        litBy.Add(GameManager.ColorOfLight.blue, initBlueLight);
    }
    
    public bool GetLitAmount(GameManager.ColorOfLight lightColor)
    {
        return litBy[lightColor].currentLit;
    }

    /// <summary>
    /// Call when light shines on this object
    /// </summary>
    /// <param name="lightColor"></param>
    public void Lit(GameManager.ColorOfLight lightColor)
    {
        //Debug.Log("lit by: " + lightColor);
        Light tempLight = litBy[lightColor];
        tempLight.currentLit = true;
        litBy[lightColor] = tempLight;

        CheckAndChangeOpacity();
    }

    /// <summary>
    /// Call when light stops shining on object
    /// </summary>
    /// <param name="lightColor"></param>
    public void UnLit(GameManager.ColorOfLight lightColor)
    {
        //Debug.Log("Unlit by: " + lightColor);
        Light tempLight = litBy[lightColor];
        tempLight.currentLit = false;
        litBy[lightColor] = tempLight;

        CheckAndChangeOpacity();
    }
    private void CheckAndChangeOpacity()
    {
        int currentLitness = 0;
        int neededLitNess = 0;

        //get total lights lighting
        foreach (KeyValuePair<GameManager.ColorOfLight, Light> item in litBy)
        {
            //if needed amount is 0 don't count it
            if (item.Value.neededLit == true)
            {
                neededLitNess++;

                if (item.Value.currentLit == true)
                {
                    currentLitness++;
                }
            }

            //Debug.Log(currentLitness);
            //Debug.Log(neededLitNess);
        }

        //don't divide by 0
        if (neededLitNess.Equals(0))
        {
            print("Oops. Trying to divide by 0. You forgot to set a LightObject's LitsNeeded");
            return;
        }

        float opacityPercentage = (float)currentLitness / (float)neededLitNess;

        //set new opacity
        Color color = meshRendererRef.material.GetColor("_BaseColor");
        color.a = opacityPercentage;
        meshRendererRef.material.SetColor("_BaseColor", color);

        if (opacityPercentage >= 1)
        {
            //colliderRef.isTrigger = true;
            Stepable = true;
        }
        else
        {
            //colliderRef.isTrigger = false;
            Stepable = false;
        }

    }
    //------------------------------------------------------------------------------------------------------------------------------------------------------------------
    #endregion

    //STRUCT
    private struct Light
    {
        public bool currentLit;
        public bool neededLit;
    }

    
}
