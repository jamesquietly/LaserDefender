using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public void LoadLevel(string name) {
        Application.LoadLevel(name);
    }

	public void QuitRequest() {
        Application.Quit();
    }

    public void LoadNextLevel() {
    	Application.LoadLevel(Application.loadedLevel + 1);
    }
}
