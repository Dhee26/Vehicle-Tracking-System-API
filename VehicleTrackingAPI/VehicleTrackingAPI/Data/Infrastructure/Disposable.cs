using System;

namespace VehicleTrackingAPI.Data.Infrastructure
{
    public class Disposable
    {
        private bool _mIsDisposed;

        ~Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!_mIsDisposed && disposing)
            {
                DisposeCore();
            }
            _mIsDisposed = true;
        }
        protected virtual void DisposeCore()
        {

        }
    }
}