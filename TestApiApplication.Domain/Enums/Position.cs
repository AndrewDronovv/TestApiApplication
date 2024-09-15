using System.ComponentModel;

namespace TestApiApplication.Domain.Enums
{
    public enum Position
    {
        [Description("менеджер")]
        Manager,

        [Description("инженер")]
        Engineer,

        [Description("тестировщик")]
        Tester,
    }
}
