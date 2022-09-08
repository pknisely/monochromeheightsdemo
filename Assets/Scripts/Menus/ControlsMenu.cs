using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private GameObject controlMapper;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p") == true && controlMapper.activeInHierarchy == true)
        {
            controlMapper.SetActive(false);
        }
        else if (Input.GetKeyDown("p") == true && controlMapper.activeInHierarchy == false)
        {
            controlMapper.SetActive(true);
        }
    }

    public void OpenControls()
    {
        controlMapper.SetActive(true);
    }

    public void CloseControls()
    {
        controlMapper.SetActive(false);
    }

    public void LoadV1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadV2()
    {
        SceneManager.LoadScene(2);
    }


}
