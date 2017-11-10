using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AGameManager : MonoBehaviour {


    private static AGameManager instance;
	// Use this for initialization
	void Awake () {
		if(instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }

	}

    public static AGameManager Instance()
    {
        return instance;
    }

    public abstract Vector2 MovementDirection();
}
