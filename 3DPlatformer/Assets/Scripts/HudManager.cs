using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HudManager : MonoBehaviour
{
    public TextMeshProUGUI scoreLabel;
    // Start is called before the first frame update
    void Start()
    {
        Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Refresh()
    {
        scoreLabel.text = "Score " + GameManager.instance.score;
    }
}
