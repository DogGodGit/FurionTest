using FurionTest.Application.Interfaces;

public class TestDto
{
    [DataValidation(ValidationTypes.Integer)]
    public int Id { get; set; }

    [DataValidation(ValidationTypes.Numeric, ValidationTypes.Integer)]
    public int Cost { get; set; }

    [DataValidation(ValidationPattern.AtLeastOne, ValidationTypes.ChineseName, ValidationTypes.EnglishName, ErrorMessage = "Error.Name")]
    public string Name { get; set; }

    [DataValidation(ValidationPattern.AtLeastOne, ValidationTypes.Time, ValidationTypes.Date)]
    public string CreateTime { get; set; }

    [DataValidation(ValidationTypes.Age)]
    public int Age { get; set; }

    [DataValidation(ValidationTypes.EmailAddress)]
    public string Email { get; set; } = "monksoul@outlook.com";

    [DataValidation(ValidationTypes.IDCard, ErrorMessage = "Error.IDCard")]
    public string IDCard { get; set; }
}

public class TestDto2 : TestDto, IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // 还可以解析服务
        var service = (ITestService)validationContext.GetService(typeof(ITestService));
        //service.ValidateData();

        var s = 1.TryValidate(ValidationTypes.NegativeNumber);
        if (s.IsValid)
        {
            yield return (ValidationResult)s.ValidationResults;
        }

        if ("Furion".TryValidate("/^Furion$"))
        {
            yield return new ValidationResult(
                "不能以 Furion 开头"
                , ["Name"]
            );
        }
    }
}