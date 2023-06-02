using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace WrappedFileDialog
{
    public abstract class FileDialog<CoClass> : CriticalFinalizerObject, IDisposable where  CoClass : new()
    {
        protected CoClass comobj;
        private bool disposedValue;

        internal FileDialog()
        {
            System.Runtime.CompilerServices.RuntimeHelpers.PrepareConstrainedRegions();
            try { }
            finally
            {
                comobj = new CoClass();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (comobj != null)
                {
                    Marshal.FinalReleaseComObject(comobj);
                }
                disposedValue = true;
            }
        }

        ~FileDialog()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
