using System;
using FIVES;
using System.Collections.Generic;
using ClientManagerPlugin;

namespace LocationPlugin
{
    /// <summary>
    /// Plugin that registers two components - position and orientation. Does not provide any associated functionality.
    /// </summary>
    public class LocationPluginInitializer : IPluginInitializer
    {
        #region IPluginInitializer implementation

        public string Name
        {
            get
            {
                return "Location";
            }
        }

        public List<string> PluginDependencies
        {
            get
            {
                return new List<string>();
            }
        }

        public List<string> ComponentDependencies
        {
            get
            {
                return new List<string>();
            }
        }

        public void Initialize()
        {
            DefineComponents();

            PluginManager.Instance.AddPluginLoadedHandler("ClientManager", RegisterClientServices);
        }

        public void Shutdown()
        {
        }

        #endregion

        void DefineComponents()
        {
            ComponentDefinition location = new ComponentDefinition("location");
            location.AddAttribute<Vector>("position", new Vector(0, 0, 0));
            location.AddAttribute<Quat>("orientation", new Quat(0, 0, 0, 1));
            ComponentRegistry.Instance.Register(location);
        }

        void RegisterClientServices()
        {
            ClientManager.Instance.RegisterClientService("location", true, new Dictionary<string, Delegate> {
                {"updatePosition", (Action<string, Vector, int>) UpdatePosition},
                {"updateOrientation", (Action<string, Quat, int>) UpdateOrientation}
            });
        }

        private void UpdatePosition(string guid, Vector position, int timestamp)
        {
            var entity = World.Instance.FindEntity(guid);
            entity["location"]["position"] = position;

            // We currently ignore timestamp, but may it in the future to implement dead reckoning.
        }

        private void UpdateOrientation(string guid, Quat orientation, int timestamp)
        {
            var entity = World.Instance.FindEntity(guid);
            entity["location"]["orientation"] = orientation;

            // We currently ignore timestamp, but may it in the future to implement dead reckoning.
        }
    }
}

