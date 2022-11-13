using UnityEngine;

namespace Project
{
    public class SelectScene : MonoBehaviour
    {
        public void SelectSceneByID(int id)
        {
            _ = LoadSceneManager.Instance.LoadScene(id);
        }
    }
}
