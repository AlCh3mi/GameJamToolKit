namespace IceBlink.GameJamToolkit.SaveGameSystem.SaveSlots
{
    public static class SaveSlotExtensions
    {
        public static void Save(this SaveSlot saveSlot)
        {
            SaveSlotSelector.UpdateSaveSlot(saveSlot);
        }
    }
}