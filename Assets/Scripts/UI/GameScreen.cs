using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : MonoBehaviour
{
    #region Private Varaiables
    [SerializeField]
    private GameObject gameScreen;
    [SerializeField]
    private GameObject drop1;
    [SerializeField]
    private GameObject drop2;
    [SerializeField]
    private GameObject drop3;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ClimateManager.Instance.currentState == ClimateManager.State.Water)
        {
            fallGameScreen.SetActive(true);
            summerGameScreen.SetActive(false);
            winterGameScreen.SetActive(false);
        }
        if (ClimateManager.Instance.currentState == ClimateManager.State.Ice)
        {
            fallGameScreen.SetActive(false);
            summerGameScreen.SetActive(false);
            winterGameScreen.SetActive(true);
        }
        if (ClimateManager.Instance.currentState == ClimateManager.State.Gas)
        {
            fallGameScreen.SetActive(false);
            summerGameScreen.SetActive(true);
            winterGameScreen.SetActive(false);
        }
    }
    private void UpdateObjectColors()
    {
        SetObjectColor(drop1, Color.gray);//Make sure the objects are in their original colours
        SetObjectColor(drop2, Color.gray);
        SetObjectColor(drop3, Color.gray);

        if (CollectibleManager.Instance.collectibleCount >= 1) SetObjectColor(drop1, Color.white);//Refresh the object colours based in this variable value
        if (CollectibleManager.Instance.collectibleCount >= 2) SetObjectColor(drop2, Color.white);
        if (CollectibleManager.Instance.collectibleCount >= 3) SetObjectColor(drop3, Color.white);
    }
    private void SetObjectColor(GameObject obj, Color color)
    {
        if (obj != null)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = color;
            }
            else
            {
                Image image = obj.GetComponent<Image>();
                if (image != null)
                {
                    image.color = color;
                }
            }
        }
    }
    private void OnEnable()
    {
        UpdateObjectColors();
    }
}
