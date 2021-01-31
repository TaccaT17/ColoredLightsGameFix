using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UpdateText : MonoBehaviour
{
    [SerializeField]
    FloatVariable floatRef;
    Text txt;
    [SerializeField]
    string prefixText;
    [SerializeField, Tooltip("Truncate decimals and use whole numbers")]
    bool wholeNumbers = true;

    private void Start()
    {
        txt = GetComponent<Text>();
        txt.text = prefixText;
    }

    // Update is called once per frame
    void Update()
    {
        if (txt.text != floatRef.Value.ToString())
        {
            float value = floatRef.Value;

            if (wholeNumbers)
            {
                value = Mathf.Round(value);
            }

            txt.text = prefixText + value.ToString();
        }
    }
}
