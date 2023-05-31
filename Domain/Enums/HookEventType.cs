using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Enums
{
    /// <summary>
    /// Format with normal name: [entity name]_[action]. ex: webhook_create, webhook_delete, webhook_update...
    /// </summary>
    public enum HookEventType
    {
        [Description("Create a Device")]
        device_create,
        [Description("Update a Device")]
        device_update,
        [Description("Delete a Device")]
        device_delete,
    }
}
