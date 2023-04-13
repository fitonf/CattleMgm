using CattleMgm.Helpers;

namespace CattleMgm.ViewModels
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Title { get; set; } = "Error";

        public ErrorStatus ErrorNumber { get; set; }

        public string ErrorDescription { get; set; } = string.Empty;

        public bool IsRawContent { get; set; }

        public string Icon { get; set; } = string.Empty;

        public int? Id { get; set; }

        public string? ide { get; set; }
    }
}
