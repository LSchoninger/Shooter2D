using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour {

    
	void Start () {
		
	}
	
	void Update () {
		
	}

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
