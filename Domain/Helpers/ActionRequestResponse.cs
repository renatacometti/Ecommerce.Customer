using FluentValidation.Results;

namespace Domain.Helpers
{
    public class ActionPagedRequest
    {
        public int Skip { get; set; }
        public int ItemsPerPage { get; set; }
        public string? Sort { get; set; }
        public bool SortAsc { get; set; }
        public bool AllItems { get; set; }
    }

    public class ActionResponse
    {
        public bool Success { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public List<ValidationError> Warnings { get; set; }

        public ActionResponse()
        {
            Success = true;
            Error = false;
        }

        public ActionResponse(Exception ex)
        {
            Success = false;
            Error = true;
            Message = ex.Message + " | " + ex.InnerException?.Message;
        }

        public ActionResponse(ValidationResult validation)
        {
            if (validation == null || validation.IsValid)
            {
                Success = true;
                Error = false;
            }
            else
            {
                Success = false;
                Error = validation.Errors.Any();
                Message = string.Join("|", validation.Errors.Select(x => x.ErrorMessage));
            }
        }

        public ActionResponse(string errorMessage)
        {
            Success = false;
            Error = true;
            Message = errorMessage;
        }
    }

    public class ActionItemResponse<T> : ActionResponse
    {
        public T Item { get; set; }

        public ActionItemResponse(T item)
        {
            Item = item;
            Success = true;
        }

        public ActionItemResponse((ValidationResult validation, T item) obj) : base(obj.validation)
        {
            Item = obj.item;
        }

        public ActionItemResponse(Exception ex) : base(ex) { }
    }

    public class ActionPagedResponse<T> : ActionResponse
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }

        public ActionPagedResponse(int totalItems, List<T> items)
        {
            TotalItems = totalItems;
            Items = items;
        }

        public ActionPagedResponse(Exception ex) : base(ex) { }
    }
}
