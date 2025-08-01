namespace FurionTest.Application.Interfaces;

public interface ITestService
{
    string SayHello(string word);

    ViewOrder GetOrder(string id);

    void QueryAllList();

    void InsertStudent();

    void UpdateStudent();

    void DeleteStudent();

    List<ViewOrder> GetOrderList();

    bool BulkList();

    bool ValidateData();
}