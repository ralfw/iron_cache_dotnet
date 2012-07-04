using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using io.iron.ironcache;

namespace io.iron.ironcache
{
    public static class CredentialsRepository
    {
        public static Credentials LoadFrom(string filename) {
            using (var sr = new StreamReader(filename))
            {
                return LoadFrom(sr);
            }
        }

        public static Credentials LoadFrom(TextReader reader)
        {
            return new Credentials(reader.ReadLine(), reader.ReadLine());
        }

        public static Credentials LoadFrom(string resourceName, Assembly resourceAssembly)
        {
            using (var stream = resourceAssembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) throw new InvalidOperationException(string.Format("Credentials resource {0} not found in assembly {1}", resourceName, resourceAssembly.FullName));

                using (var sr = new StreamReader(stream))
                {
                    return LoadFrom(sr);
                }
            }
        }

        public static Credentials LoadFromAppConfig()
        {
            return new Credentials(ConfigurationManager.AppSettings["IRONIO_PROJECT_ID"],
                                   ConfigurationManager.AppSettings["IRONIO_TOKEN"]);
        }


        public static void SaveTo(string filename, Credentials token) {
            using (var sw = new StreamWriter(filename, false)) {
                sw.WriteLine(token.ProjectId);
                sw.WriteLine(token.Token);
            }
        }
    }
}