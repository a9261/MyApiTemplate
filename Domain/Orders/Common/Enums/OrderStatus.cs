using System.ComponentModel;

namespace Domain.Orders.Common.Enums
{
    public enum OrderStatus
    {
        [Description("Init")]
        Init = 0,

        [Description("Waiting")]
        Waiting = 1,

        [Description("Processing")]
        Processing = 2,

        [Description("Success")]
        Success = 3,

        [Description("Failed")]
        Failed = 4
    }
}