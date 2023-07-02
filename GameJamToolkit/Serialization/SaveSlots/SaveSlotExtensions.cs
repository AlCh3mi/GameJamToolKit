namespace IceBlink.GameJamToolkit.Serialization.SaveSlots
{
    public static class SaveSlotExtensions
    {
        public static void Save(this SaveSlot saveSlot)
        {
            SaveSlotSelector.UpdateSaveSlot(saveSlot);
        }
    }
}