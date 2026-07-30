using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using Windows.Win32;

namespace InkCanvasForClass_Remastered.Helpers
{
    public static class Marshal2
    {
        internal const string OLEAUT32 = "oleaut32.dll";
        internal const string OLE32 = "ole32.dll";

        [System.Security.SecurityCritical]  // auto-generated_required
        public unsafe static object GetActiveObject(string progID)
        {
            Guid clsid;

            // Call CLSIDFromProgIDEx first then fall back on CLSIDFromProgID if
            // CLSIDFromProgIDEx doesn't exist.
            try
            {
                PInvoke.CLSIDFromProgIDEx(progID, out clsid);
            }
            //            catch
            catch (Exception)
            {
                PInvoke.CLSIDFromProgID(progID, out clsid);
            }

            PInvoke.GetActiveObject(ref clsid, null, out object obj);
            return obj;
        }
    }
}
