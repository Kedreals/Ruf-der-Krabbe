using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Call_of_Crabs
{
    public static class Sound
    {
        public static string[] soundnames = {"Blob", "Bösesschlitzschlitz", "PewPew", "PewPew2", "Schlitzschlitz", "SchnitzSchnitz"}; 
        public static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

        public static void Load(ContentManager contentManager)
        {
            foreach (string a in soundnames)
            {
                SoundEffect ef = contentManager.Load<SoundEffect>("Sounds/" + a);
                sounds.Add(a, ef);
            }
        }
    }
}
