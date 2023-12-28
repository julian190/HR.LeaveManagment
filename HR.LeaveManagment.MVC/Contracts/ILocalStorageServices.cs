namespace HR.LeaveManagment.MVC.Contracts
{
    public interface ILocalStorageServices
    {
        void ClearStorage(List<string> Keys);

        bool Exists (string Key);
        T GetStorageValue<T> (string Key);
        void SetStorageValue<T>(string Key, T value);
    }
}
