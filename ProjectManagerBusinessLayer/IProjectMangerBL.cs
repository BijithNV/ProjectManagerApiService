using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagerBusinessLayer
{
  public  interface IProjectMangerBL<T>
    {
        int CreateNew(T objDetails);
        T Update(T objDetails);        
        void Delete(int Id);
        IEnumerable<T> RetrieveAllData();
        IEnumerable<T> SearchByKey(string searchText);
        T GetById(int id);
    }
}
