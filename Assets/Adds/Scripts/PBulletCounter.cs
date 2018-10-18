using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBulletCounter : MonoBehaviour {

    private int m_PlayerNumber;

    [SerializeField]
    private int m_ShellNumber = 6;
    private int shellNow;

    [SerializeField]
    private float m_ReloadTime = 1.0f;
    private float time;
    private bool isReloading = false;

    //Key for Reload 1p:C 2p:BS
    [SerializeField]
    private KeyCode[] m_ReloadKeys = new KeyCode[] { KeyCode.C, KeyCode.Backspace};

    // Use this for initialization
    void Start () {
        shellNow = m_ShellNumber;
	}

    void ReStart()
    {
        shellNow = m_ShellNumber;
    }

    public void SetNumber(int playerNumber)
    {
        this.m_PlayerNumber = playerNumber - 1;
    }
	
	// Update is called once per frame
	void Update () {

        //Reloading?
        if (isReloading)
        {
            if (time < m_ReloadTime)
            {
                time += Time.deltaTime;
            }
            else
            {
                isReloading = false;
                shellNow = m_ShellNumber;
            }
        }

        //Stand-By?
        if (Input.GetKeyDown(m_ReloadKeys[m_PlayerNumber]))
        {
            time = 0f;
            isReloading = true;
        }
	}

    public void Launch()
    {
        if (shellNow > 0) shellNow--;
    }

    public bool IsReloading { get { return isReloading; } }

    public bool IsAbleLaunch { get { return shellNow > 0; } }

    //リロードの進み具合
    public float ReloadingRate { get { return time / m_ReloadTime; } }
}
