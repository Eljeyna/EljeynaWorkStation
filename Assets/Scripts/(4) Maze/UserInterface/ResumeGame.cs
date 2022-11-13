using Game.GameData;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Game.UserInterface
{
    public class ResumeGame : MonoBehaviour
    {
        public void Exec()
        {
            UserInterfaceManager.HideQuickMenuInterface();
            GameManager.ResumeGame(WorldHandler.GetMainWorld());
        }
    }
}
