using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Engine
{
    public class SaveManager
    {
        private bool saving = false;
        private bool loading = false;

        public void saveScoresToFile(HighScores myState)
        {
            lock (this)
            {
                if (!this.saving)
                {
                    this.saving = true;
                    finalizeSaveAsync(myState);
                }
            }
        }

        private async void finalizeSaveAsync(HighScores state)
        {
            await Task.Run(() =>
            {
                using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    try
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.xml", FileMode.Create))
                        {
                            if (fs != null)
                            {
                                XmlSerializer mySerializer = new XmlSerializer(typeof(HighScores));
                                mySerializer.Serialize(fs, state);
                            }
                        }
                    }
                    catch (IsolatedStorageException)
                    {
                    }
                }

                this.saving = false;
            });
        }

        public HighScores loadScoresFromFile()
        {
            lock (this)
            {
                if (!this.loading)
                {
                    this.loading = true;
                    return finalizeLoad();
                }
                return new HighScores();
            }
        }
        private HighScores finalizeLoad()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    if (storage.FileExists("HighScores.xml"))
                    {
                        using (IsolatedStorageFileStream fs = storage.OpenFile("HighScores.xml", FileMode.Open))
                        {
                            if (fs != null)
                            {
                                XmlSerializer mySerializer = new XmlSerializer(typeof(HighScores));
                                loading = false;
                                return (HighScores)mySerializer.Deserialize(fs);
                            }
                        }
                    }
                    loading = false;
                    return new HighScores();
                }
                catch (IsolatedStorageException)
                {
                    loading = false;
                    return new HighScores();
                }
            }
        }
    }
}
