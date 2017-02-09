using System;
using System.Security.Cryptography.X509Certificates;

namespace GatariSwitcher
{
    class CertificateManager
    {
        public bool Status()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection c = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);
            bool result = (c.Count > 0);
            store.Close();

            return result;
        }

        public void Install()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2 certificate = new X509Certificate2(GatariSwitcher.Properties.Resources.gatari);
            store.Add(certificate);
            store.Close();
        }

        public void Uninstall()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadWrite);
            X509Certificate2Collection certs = store.Certificates.Find(X509FindType.FindBySubjectName, "*.ppy.sh", true);
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
