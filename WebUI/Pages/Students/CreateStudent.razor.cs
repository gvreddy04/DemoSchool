namespace WebUI.Pages.Students;

public partial class CreateStudent : ComponentBase
{
    #region Members

    private List<int> days = new();

    private bool disableMonth => string.IsNullOrWhiteSpace(studentForm.YearAsString);
    private bool disableDay => string.IsNullOrWhiteSpace(studentForm.YearAsString) || string.IsNullOrWhiteSpace(studentForm.MonthAsString);

    private int studentId;

    private StudentForm studentForm { get; set; } = new();
    private EditContext? editContext;
    private bool formInvalid = false;

    private ConfirmDialog confirmDialog;
    private Modal modal;

    #endregion

    #region Properties

    [Inject] private IMapper _mapper { get; set; } = null!;
    [Inject] private IStudentService _studentService { get; set; } = null!;
    [Inject] private NavigationManager _navigationManager { get; set; }

    #endregion

    #region Methods

    protected override void OnInitialized()
    {
        editContext = new(studentForm);
    }

    private void OnYearChanged(ChangeEventArgs e)
    {
        studentForm.YearAsString = e.Value.ToString();

        int.TryParse(studentForm.YearAsString, out int year);

        studentForm.Year = year;

        // reset
        studentForm.MonthAsString = string.Empty;
        studentForm.Month = 0;
        resetDays();
    }

    private void OnMonthChanged(ChangeEventArgs e)
    {
        studentForm.MonthAsString = e.Value.ToString();

        int.TryParse(studentForm.MonthAsString, out int month);

        studentForm.Month = month;

        // reset
        resetDays();
    }

    private void OnDayChanged(ChangeEventArgs e)
    {
        studentForm.DayAsString = e.Value.ToString();

        int.TryParse(studentForm.DayAsString, out int day);

        studentForm.Day = day;
    }

    private void OnCountryCodeChanged(ChangeEventArgs e)
    {
        studentForm.CountryCodeAsString = e.Value.ToString();

        int.TryParse(studentForm.CountryCodeAsString, out int countryCode);

        studentForm.CountryCode = countryCode;
    }

    private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (editContext != null)
        {
            formInvalid = !editContext.Validate();
            StateHasChanged();
        }
    }

    private async Task HandleValidSubmit()
    {
        var confirmation = await confirmDialog?.ShowAsync(
            title: "Are you sure you want to create the student?", 
            message1: "This will save the details entered in the form.", 
            message2: "Do you want to proceed?");

        if (confirmation)
        {
            if (editContext is null || !editContext.Validate())
                return;

            var student = new Student
            {
                StudentName = studentForm.StudentName,
                GaurdianName = studentForm.GaurdianName,
                Gender = (int)studentForm.Gender,
                DOB = new(studentForm.Year, studentForm.Month, studentForm.Day),
                CountryCode = studentForm.CountryCode,
                PhoneNumber = studentForm.PhoneNumber,
                Email = studentForm.Email
            };

            var address = new StudentAddress
            {
                AddressLine1 = studentForm.Address?.AddressLine1,
                AddressLine2 = studentForm.Address?.AddressLine2,
                AddressLine3 = studentForm.Address?.AddressLine3,
                City = studentForm.Address?.City,
                State = studentForm.Address?.State,
                Country = studentForm.Address?.Country,
                ZipCode = studentForm.Address?.ZipCode
            };

            studentId = await _studentService.CreateStudentAsync(student, address);

            await modal?.ShowAsync();
        }
    }

    private void HandleInvalidSubmit() => formInvalid = true;

    private void resetDays()
    {
        // reset
        studentForm.DayAsString = string.Empty;
        studentForm.Day = 0;
        days = new();

        int year = studentForm.Year;
        int month = studentForm.Month;
        if (month < 1 || month > 12 || year < 1 || year > 9999)
            return;

        var daysInMonth = DateTime.DaysInMonth(year, month);
        for (int i = 1; i <= daysInMonth; i++)
        {
            days.Add(i);
        }
    }

    #region Modal

    private void OnModalOkClick()
    {
        modal?.HideAsync();
    }

    private void OnModalHidden()
    {
        if (studentId > 0)
            _navigationManager.NavigateTo($"/students/{studentId}");
        else
            _navigationManager.NavigateTo($"/students");
    }

    #endregion Modal

    #endregion
}

public class StudentForm
{
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

    [ValidateComplexType]
    public StudentAddressModel Address { get; set; } = new();
}

public class StudentAddressModel
{
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
