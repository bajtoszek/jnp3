using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayBackgroundMusic(0);
    }
}
