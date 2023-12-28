using Hanssens.Net;
using HR.LeaveManagment.MVC.Contracts;

namespace HR.LeaveManagment.MVC.Services
{
    public class LocalStorageService : ILocalStorageServices
    {
        private LocalStorage _storage;

        public LocalStorageService() {
            var config = new LocalStorageConfiguration()
            {
                AutoLoad = true,
                AutoSave = true,
                Filename = "HR.LEAVEMGMT"
            };
            _storage = new LocalStorage(config);
        }
        public void ClearStorage(List<string> Keys)
        {
            foreach (var key in Keys)
            {
                _storage.Remove(key);
            }
        }

        public bool Exists(string Key)
        {
            return _storage.Exists(Key);
        }

        public T GetStorageValue<T>(string Key)
        {
            return _storage.Get<T>(Key);
        }

        public void SetStorageValue<T>(string Key, T value)
        {
            _storage.Store(Key, value);
            _storage.Persist();
        }
    }
}
