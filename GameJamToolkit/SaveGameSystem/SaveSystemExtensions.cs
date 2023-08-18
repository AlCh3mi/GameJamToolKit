using IceBlink.GameJamToolkit.SaveGameSystem.Profiles;

namespace IceBlink.GameJamToolkit.SaveGameSystem
{
    public static class SaveSystemExtensions
    {
        public static void Save(this Profile profile)
        {
            ProfileSelector.UpdateProfile(profile);
        }
    }
}