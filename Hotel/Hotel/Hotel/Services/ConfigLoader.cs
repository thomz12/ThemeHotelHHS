using Newtonsoft.Json;
using System;
using System.IO;

namespace Hotel
{
    public class ConfigLoader : IServiceProvider
    {
        private ConfigModel _config;
        private string _fileName;

        public ConfigLoader(string fileName)
        {
            _config = null;
            _fileName = fileName;
        }

        /// <summary>
        /// Loads the config.
        /// </summary>
        /// <returns>The config, or null.</returns>
        public ConfigModel GetConfig()
        {
            if (_config != null)
            {
                return _config;
            }
            else
            {
                // Load in the settings, and boy a lot can go wrong here.
                // First up is the check if there is actually a config file present.
                if (File.Exists(_fileName))
                {
                    // Ok its present so now we can TRY to read it (geddit?)
                    try
                    {
                        // Poke the garbage collector to wake up, all the readers and stuff are not needed after this anymore.
                        using (StreamReader sr = new StreamReader(_fileName))
                        using (JsonReader jsonReader = new JsonTextReader(sr))
                        {
                            // It seems like someone neglected to make the serializer inherit from the IDisposable interface, so we will just have to instantiate it here.
                            JsonSerializer jsonSerializer = new JsonSerializer();
                            // Now its time for the one line magic!
                            // Deserialize the config file into a config object.
                            _config = jsonSerializer.Deserialize<ConfigModel>(jsonReader);

                            // We now have some data, but someone may have screwed with our files.
                            // Check again if there is a layout file on the end of the filepath that was in the config file.
                            if (!File.Exists(_config.LayoutPath))
                            {
                                // Throw some more error messages at the user.
                                Console.WriteLine($"There is no layout file on the end of this path: {_config.LayoutPath}!");
                                // Could not load.
                                return null;
                            }

                            return _config;
                        }
                    }
                    catch
                    {
                        // AYY LMAO something went wrong and I have no idea what.
                        // Queue generic error message.
                        Console.WriteLine($"Could not read the file {_fileName}.");
                        // Could not load.
                        return null;
                    }
                }
                else
                {
                    // Y U REMOVE FILE BETWEEN OK AND LOAD?
                    Console.WriteLine("The config file does not exist, please exit the simulator and make one in the 'settings' screen of the launcher.");
                    // Could not load.
                    return null;
                }
            }
        }

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
