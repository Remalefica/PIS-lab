using System.Collections.Generic;

namespace WalletServices.DataService
{
    public interface IRepository<T> where T : class
    {
        public void Create(T item);
        public IEnumerable<T> GetAll();
        public T GetById(int? id);
        public void Delete(int? id);
    }
}