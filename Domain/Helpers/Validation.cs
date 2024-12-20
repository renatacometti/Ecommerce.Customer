namespace Domain.Helpers
{
    public class Validation
    {
        public Validation(string error = null)
        {
            Error = error;
        }

        private List<ValidationError> _warnings;
        public List<ValidationError> Warnings
        {
            get => _warnings ?? (_warnings = new List<ValidationError>());
            set => _warnings = value;
        }

        public string Error { get; private set; }

        public void AddWarning(string text, string field = null)
        {
            Warnings.Add(new ValidationError
            {
                Field = field,
                Text = text
            });
        }

        public void SetError(string erro)
        {
            Error = erro;
        }

        public void Union(Validation validation)
        {
            if (validation != null)
            {
                Error = validation.Error;
                Warnings.AddRange(validation.Warnings);
            }
        }

        public bool IsValid => (_warnings == null || _warnings.Count == 0) && string.IsNullOrWhiteSpace(Error);

        public void Throw(string message)
        {
            throw new Exception($"{message}, {ToString()}");
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Error))
            {
                return Error;
            }
            else
            {
                return string.Join(";", Warnings.Select(w => w.Text));
            }
        }
    }

    public class ValidationError
    {
        public string Field { get; set; }

        public string Text { get; set; }
    }
}
