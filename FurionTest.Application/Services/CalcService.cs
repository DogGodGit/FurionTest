using FurionTest.Application.Interfaces;

namespace FurionTest.Application.Services;

public class CalcService : ICalcService, ITransient // 支持任何生命周期
{
    public int Plus(int i, int j)
    {
        return i + j;
    }
}