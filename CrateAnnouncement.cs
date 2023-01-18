using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oxide.Core.Libraries;
using System.Collections.Generic;

namespace Oxide.Plugins
{
    [Info("Create Announcement", "4yzhen", "1.0.0")]
    public class CrateAnnouncement : RustPlugin
    {
        #region TODO
        //Adding Location to Messages
        //Adding Player Name to Messages
        //Adding Grid to Messages
        #endregion
        #region Config
        private static ConfigData configData;
        private class ConfigData
        {
            [JsonProperty(PropertyName = "Prefix")]
            public string prefix { get; set; } = "<color=#ffd700>[Crate Announcement]</color>";
        }

        protected override void LoadConfig()
        {
            base.LoadConfig();
            try
            {
                configData = Config.ReadObject<ConfigData>();
                Config.WriteObject(configData, true);
            }
            catch
            {
                PrintError("!!! YOUR CONFIG IS OUTDATED. NEW CONFIG CREATED !!!");
                GetBaseConfig();
            }
        }

        protected override void LoadDefaultConfig() => configData = GetBaseConfig();
        private ConfigData GetBaseConfig()
        {
            return new ConfigData
            {
                prefix = "<color=#ffd700>[Crate Announcement]</color>",
            };
        }
        #endregion
        #region Hooks
        void OnCrateLanded(HackableLockedCrate crate)
        {
            string landedmessage = lang.GetMessage("CrateLandedMessage", this);
            string prefix = configData.prefix;
            Server.Broadcast(prefix  +  landedmessage);
        }

        void OnCrateHack(HackableLockedCrate crate)
        {
            string startedmessage = lang.GetMessage("StartedHackMessage", this);
            string prefix = configData.prefix;
            Server.Broadcast(prefix  +  startedmessage);
        }


        void OnCrateHackEnd(HackableLockedCrate crate)
        {
            string endedmessage = lang.GetMessage("EndedHackMessage", this);
            string prefix = configData.prefix;
            Server.Broadcast(prefix  +  endedmessage);
        }
        #endregion
        #region Localization
        protected override void LoadDefaultMessages()
        {
            lang.RegisterMessages(new Dictionary<string, string>
            {
                ["CrateLandedMessage"] = "<size=12>Locked crate landed!</size>",
                ["StartedHackMessage"] = "<size=12>Someone started to hack the locked crate!</size1",
                ["EndedHackMessage"] = "<size=12>Someone ended hacking the crate and took the loot!"
            }, this);
        }
        #endregion
    }
}