namespace WebUI.Services;

public class StudentService : BaseService, IStudentService
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    private readonly NavigationManager _navigationManager;
    private readonly IMapper _mapper;

    public StudentService(
        ILogger<StudentService> logger,
        IConfiguration configuration,
        HttpClient httpClient,
        NavigationManager navigationManager,
        IMapper mapper) : base(configuration, logger)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _mapper = mapper;
    }

    public async Task<int> CreateStudentAsync(Student student, StudentAddress address)
    {
        string url = $"{BaseUrl}/api/students";
        var data = new RequestModels.Students.CreateStudent.StudentDto
        {
            StudentName = student.StudentName,
            GaurdianName = student.GaurdianName,
            Gender = student.Gender,
            DOB = student.DOB,
            CountryCode = student.CountryCode,
            PhoneNumber = student.PhoneNumber,
            Email = student.Email,
            Address = _mapper.Map<RequestModels.Students.CreateStudent.StudentAddressDto>(address)
        };

        var response = await _httpClient.PostAsJsonAsync(url, data);
        var studentId = await response.Content.ReadAsStringAsync(); // returns studentId
        return int.TryParse(studentId, out int id) ? id : id;
    }

    public async Task<StudentVm> GetStudentByIdAsync(int id)
    {
        string url = $"{BaseUrl}/api/students/{id}";
        return await _httpClient.GetFromJsonAsync<StudentVm>(url);
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        string url = $"{BaseUrl}/api/students";
        return await _httpClient.GetFromJsonAsync<IEnumerable<Student>>(url);
    }

    public async Task<bool> UpdateStudentByOidAsync(Student student)
    {
        string url = $"{BaseUrl}/api/students";
        var data = _mapper.Map<RequestModels.Students.UpdateStudent.StudentDto>(student);

        var response = await _httpClient.PutAsJsonAsync(url, data);
        var studentId = await response.Content.ReadAsStringAsync(); // returns studentId
        return bool.TryParse(studentId, out bool id) ? id : id;
    }

    public async Task<bool> UpdateStudentAddressAsync(StudentAddress address)
    {
        string url = $"{BaseUrl}/api/students/address";
        var data = _mapper.Map<RequestModels.Students.UpdateStudent.StudentAddressDto>(address);

        var response = await _httpClient.PutAsJsonAsync(url, data);
        var studentId = await response.Content.ReadAsStringAsync(); // returns studentId
        return bool.TryParse(studentId, out bool id) ? id : id;
    }
}
