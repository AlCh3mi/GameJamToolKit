namespace IceBlink.GameJamToolkit.SaveGameSystem.Profiles
{
    public static class SaveSystemExtensions
    {
        public static void Save(this Profile profile)
        {
            ProfileSelector.UpdateProfile(profile);
        }
    }
}