using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    private void Update()
    {
        if(Input.anyKey)
            SceneFader.LoadScene("SampleScene", FadeType.Loding);
    }
}
