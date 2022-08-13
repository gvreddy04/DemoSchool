using WebUI.HttpInterceptor;

namespace WebUI.Pages.Students;

public partial class StudentDetail : ComponentBase, IDisposable
{
    #region Members

    private Student student;
    private StudentAddress address;

    private List<int> days = new();

    private bool disableMonth => string.IsNullOrWhiteSpace(studentEditForm.YearAsString);
    private bool disableDay => string.IsNullOrWhiteSpace(studentEditForm.YearAsString) || string.IsNullOrWhiteSpace(studentEditForm.MonthAsString);

    private StudentEditForm studentEditForm { get; set; } = new();
    private StudentAddressEditForm studentAddressEditForm { get; set; } = new();
    private EditContext? studentEditContext;
    private EditContext? studentAddressEditContext;
    private bool studentFormInvalid = false;
    private bool studentAddressFormInvalid = false;

    private Button? studentSaveButton;
    private Button? addressSaveButton;

    private List<ToastMessage> messages = new();

    #endregion Members

    #region Properties

    [Parameter] public int StudentId { get; set; }

    [Inject] private IStudentService _studentService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; }
    [Inject] private PreloadService _preloadService { get; set; }
    [Inject] private HttpInterceptorService _interceptor { get; set; } = null!;

    #endregion Properties

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        try
        {
            _interceptor.RegisterEvent();

            _preloadService?.Show();

            studentEditContext = new(studentEditForm);
            studentAddressEditContext = new(studentAddressEditForm);

            var studentVm = await _studentService.GetStudentByIdAsync(StudentId);
            if (studentVm is null || studentVm.Student is null || studentVm.Address is null)
            {
                // TODO: redirect
            }
            student = studentVm.Student;
            address = studentVm.Address;

            // student
            studentEditForm.Oid = student.Oid;
            studentEditForm.StudentName = student.StudentName;
            studentEditForm.GaurdianName = student.GaurdianName;
            studentEditForm.Gender = (Gender)student.Gender;
            studentEditForm.Year = student.DOB.Year;
            studentEditForm.YearAsString = $"{student.DOB.Year}";
            studentEditForm.Month = student.DOB.Month;
            studentEditForm.MonthAsString = $"{student.DOB.Month}";
            resetDays();
            studentEditForm.Day = student.DOB.Day;
            studentEditForm.DayAsString = $"{student.DOB.Day}";
            studentEditForm.Email = student.Email;
            studentEditForm.CountryCode = student.CountryCode;
            studentEditForm.CountryCodeAsString = $"{student.CountryCode}";
            studentEditForm.PhoneNumber = student.PhoneNumber;

            // address
            studentAddressEditForm.Oid = address.Oid;
            studentAddressEditForm.StudentId = student.StudentId;
            studentAddressEditForm.AddressLine1 = address.AddressLine1;
            studentAddressEditForm.AddressLine2 = address.AddressLine2;
            studentAddressEditForm.AddressLine3 = address.AddressLine3;
            studentAddressEditForm.City = address.City;
            studentAddressEditForm.State = address.State;
            studentAddressEditForm.Country = address.Country;
            studentAddressEditForm.ZipCode = address.ZipCode;
        }
        catch
        {
            // catch exception
        }
        finally
        {
            _preloadService?.Hide();
        }
    }

    #region Student

    private void OnYearChanged(ChangeEventArgs e)
    {
        studentEditForm.YearAsString = e.Value.ToString();

        int.TryParse(studentEditForm.YearAsString, out int year);

        studentEditForm.Year = year;

        // reset
        studentEditForm.MonthAsString = string.Empty;
        studentEditForm.Month = 0;
        resetDays();
    }

    private void OnMonthChanged(ChangeEventArgs e)
    {
        studentEditForm.MonthAsString = e.Value.ToString();

        int.TryParse(studentEditForm.MonthAsString, out int month);

        studentEditForm.Month = month;

        // reset
        resetDays();
    }

    private void OnDayChanged(ChangeEventArgs e)
    {
        studentEditForm.DayAsString = e.Value.ToString();

        int.TryParse(studentEditForm.DayAsString, out int day);

        studentEditForm.Day = day;
    }

    private void resetDays()
    {
        // reset
        studentEditForm.DayAsString = string.Empty;
        studentEditForm.Day = 0;
        days = new();

        int year = studentEditForm.Year;
        int month = studentEditForm.Month;
        if (month < 1 || month > 12 || year < 1 || year > 9999)
            return;

        var daysInMonth = DateTime.DaysInMonth(year, month);
        for (int i = 1; i <= daysInMonth; i++)
        {
            days.Add(i);
        }
    }

    private void OnCountryCodeChanged(ChangeEventArgs e)
    {
        studentEditForm.CountryCodeAsString = e.Value.ToString();

        int.TryParse(studentEditForm.CountryCodeAsString, out int countryCode);

        studentEditForm.CountryCode = countryCode;
    }

    private void StudentHandleFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (studentEditContext != null)
        {
            studentFormInvalid = !studentEditContext.Validate();
            StateHasChanged();
        }
    }

    private async Task StudentHandleValidSubmit()
    {
        try
        {
            studentSaveButton?.ShowLoading("Saving...");

            if (studentEditContext is null || !studentEditContext.Validate())
                return;

            studentEditContext.MarkAsUnmodified();

            var student = new Student
            {
                Oid = studentEditForm.Oid,
                StudentName = studentEditForm.StudentName,
                GaurdianName = studentEditForm.GaurdianName,
                Gender = (int)studentEditForm.Gender,
                DOB = new(studentEditForm.Year, studentEditForm.Month, studentEditForm.Day),
                CountryCode = studentEditForm.CountryCode,
                PhoneNumber = studentEditForm.PhoneNumber,
                Email = studentEditForm.Email
            };

            var isUpdateSuccess = await _studentService.UpdateStudentByOidAsync(student);

            if (isUpdateSuccess)
                ShowSuccessToastMessage("Student basic information updated.");
        }
        catch
        {
            ShowErrorToastMessage("An error occurred while saving the student's basic information. Please try again.");
        }
        finally
        {
            studentSaveButton?.HideLoading();
        }
    }

    private void StudentHandleInvalidSubmit() => studentFormInvalid = true;

    #endregion Student

    #region Address

    private void StudentAddressHandleFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (studentAddressEditContext != null)
        {
            studentAddressFormInvalid = !studentAddressEditContext.Validate();
            StateHasChanged();
        }
    }

    private async Task StudentAddressHandleValidSubmit()
    {
        try
        {
            addressSaveButton?.ShowLoading("Saving...");

            if (studentAddressEditContext is null || !studentAddressEditContext.Validate())
                return;

            studentAddressEditContext.MarkAsUnmodified();

            var address = new StudentAddress
            {
                Oid = studentAddressEditForm.Oid,
                StudentId = studentAddressEditForm.StudentId,
                AddressLine1 = studentAddressEditForm?.AddressLine1,
                AddressLine2 = studentAddressEditForm?.AddressLine2,
                AddressLine3 = studentAddressEditForm?.AddressLine3,
                City = studentAddressEditForm?.City,
                State = studentAddressEditForm?.State,
                Country = studentAddressEditForm?.Country,
                ZipCode = studentAddressEditForm?.ZipCode
            };

            var isUpdateSuccess = await _studentService.UpdateStudentAddressAsync(address);

            if (isUpdateSuccess)
                ShowSuccessToastMessage("Student address updated.");
        }
        catch
        {
            ShowErrorToastMessage("An error occurred while saving the student's address. Please try again.");
        }
        finally
        {
            addressSaveButton?.HideLoading();
        }
    }

    private void StudentAddressHandleInvalidSubmit() => studentAddressFormInvalid = true;

    #endregion Address

    #region Toast Messages

    private void ShowSuccessToastMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message) || messages is null)
            return;

        messages.Add(new() { Type = ToastType.Success, Message = message });
    }

    private void ShowErrorToastMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message) || messages is null)
            return;

        messages.Add(new() { Type = ToastType.Danger, Message = message });
    }

    #endregion Toast Messages

    #endregion Methods

    public void Dispose()
    {
        _interceptor.DisposeEvent();
    }
}


public class StudentEditForm
{
    [Required]
    public Guid Oid { get; set; }

    [Required]
    [Display(Name = "Student name")]
    [MaxLength(500, ErrorMessage = "Student name is too long (500 character limit).")]
    public string StudentName { get; set; } = null!;

    [Required]
    [Display(Name = "Gaurdian name")]
    [MaxLength(500, ErrorMessage = "Gaurdian name is too long (500 character limit).")]
    public string GaurdianName { get; set; } = null!;

    [Required]
    [Range(typeof(Gender), nameof(Gender.Male), nameof(Gender.Other), ErrorMessage = "Please select gender.")]
    public Gender Gender { get; set; }

    [Required]
    [Display(Name = "Year")]
    public string? YearAsString { get; set; }

    public int Year { get; set; }

    [Required]
    [Display(Name = "Month")]
    public string? MonthAsString { get; set; }

    public int Month { get; set; }

    [Required]
    [Display(Name = "Day")]
    public string? DayAsString { get; set; }

    public int Day { get; set; }

    [Required]
    [Display(Name = "Country code")]
    public string? CountryCodeAsString { get; set; }

    public int CountryCode { get; set; }

    [Required]
    [Display(Name = "Phone number")]
    [MaxLength(10)]
    public string? PhoneNumber { get; set; }

    [Required, EmailAddress]
    [Display(Name = "Email")]
    [MaxLength(100)]
    public string? Email { get; set; }
}

public class StudentAddressEditForm
{
    [Required]
    public Guid Oid { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    [Display(Name = "Address Line1")]
    [MaxLength(200)]
    public string? AddressLine1 { get; set; }

    [Display(Name = "Address Line2")]
    [MaxLength(200)]
    public string? AddressLine2 { get; set; }

    [Display(Name = "Address Line3")]
    [MaxLength(200)]
    public string? AddressLine3 { get; set; }

    [Required]
    [Display(Name = "City")]
    [MaxLength(200)]
    public string? City { get; set; }

    [Required]
    [Display(Name = "State")]
    [MaxLength(100)]
    public string? State { get; set; }

    [Required]
    [Display(Name = "Country")]
    [MaxLength(100)]
    public string? Country { get; set; }

    [Required]
    [Display(Name = "Zip code")]
    [MaxLength(10)]
    public string? ZipCode { get; set; }
}
