﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadMainMenu", 20);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
