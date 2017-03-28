using System;
using System.Security.Cryptography.X509Certificates;

namespace GatariSwitcher
{
    class CertificateManager
    {
        public bool Status()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            var c = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);
            bool result = (c.Count > 0);
            store.Close();

            return result;
        }

        public void Install()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            var certificate = new X509Certificate2(GatariSwitcher.Properties.Resources.gatari);
            store.Add(certificate);
            store.Close();
        }

        public void Uninstall()
        {
            var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            var certs = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);
            try
            {
                foreach (var c in certs)
                {
                    store.Remove(c);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                store.Close();
            }
        }
    }
}
