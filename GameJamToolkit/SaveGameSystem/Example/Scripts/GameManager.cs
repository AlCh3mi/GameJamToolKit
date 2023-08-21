using IceBlink.GameJamToolkit.SaveGameSystem.Example.SavingSystem.UI;
using IceBlink.GameJamToolkit.Singletons;
using UnityEngine;

namespace IceBlink.GameJamToolkit.SaveGameSystem.Example
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private WorldSaveManager worldSaveManager;
        [SerializeField] private LoadSlotSelector loadSlotSelector;
        
        public GameState GameState { get; set; } = GameState.InMenu;

        public void PlayGame()
        {
            if(!SaveSystem.Instance.HasSaveGames())
            {
                worldSaveManager.NewWorld();
                return;
            }
            
            loadSlotSelector.gameObject.SetActive(true);
        }
    }
}