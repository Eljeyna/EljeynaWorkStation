using Project;
using UnityEngine;

namespace UserInterface
{
    public class ReloadScene : MonoBehaviour
    {
        public void Exec()
        {
            _ = LoadSceneManager.Instance.LoadScene((int)Scenes.Maze);
        }
    }
}
