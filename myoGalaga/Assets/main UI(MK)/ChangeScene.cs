using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    public GameObject Myo;

    public void Scene_Manager(string sceneToChangeTo)
    {
       if (Myo.GetComponent<ThalmicMyo>().armSynced == true)
        {
            ChangeToScene(sceneToChangeTo);
        }           
    }

	public void ChangeToScene (string sceneToChangeTo)
    {

        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
