using FurionTest.Core.Models;

namespace FurionTest.BAS.Business;

public interface IBusinessService
{
    Person Get(int id);

    string GetName();
}
