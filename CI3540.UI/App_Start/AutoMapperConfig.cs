using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace CI3540.UI.App_Start
{
    public class AutoMapperConfig
    {
        // Load from Assembly
        // From here:
        // http://stackoverflow.com/questions/2651613/how-to-scan-and-auto-configure-profiles-in-automapper

        public static void RegisterConfig()
        {
            Mapper.Initialize(config => GetConfiguration(Mapper.Configuration));
        }
        
        private static void GetConfiguration(IConfiguration configuration)
        {
            configuration.AllowNullDestinationValues = true;
            configuration.AllowNullCollections = true;

            IEnumerable<Type> profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(type => typeof (Profile).IsAssignableFrom(type));

            foreach (var profile in profiles)
            {
                configuration.AddProfile(Activator.CreateInstance(profile) as Profile);
            }
        }
    }
}