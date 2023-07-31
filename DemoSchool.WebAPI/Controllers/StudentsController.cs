namespace DemoSchool.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IStudentRepository _studentRepository;
    private readonly IStudentAddressRepository _studentAddressRepository;

    public StudentsController(IMapper mapper, IStudentRepository studentRepository, IStudentAddressRepository studentAddressRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
        _studentAddressRepository = studentAddressRepository;
    }

    [HttpGet()]
    public async Task<IActionResult> GetStudentsAsync()
    {
        var result = await _studentRepository.GetStudentsAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStudentAsync(int id)
    {
        // TODO: validations
        var student = await _studentRepository.GetStudentByIdAsync(id);
        var address = await _studentAddressRepository.GetStudentAddressByStudentIdAsync(id);

        return Ok(new RequestModels.Students.GetStudent.StudentVm
        {
            Student = _mapper.Map<RequestModels.Students.GetStudent.StudentDto>(student),
            Address = _mapper.Map<RequestModels.Students.GetStudent.StudentAddressDto>(address)
        });
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentAsync([FromBody] RequestModels.Students.CreateStudent.StudentDto model)
    {
        // TODO: validations
        var student = new Models.Student
        {
            StudentName = model.StudentName,
            GaurdianName = model.GaurdianName,
            Gender = model.Gender,
            DOB = model.DOB,
            CountryCode = model.CountryCode,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email
        };

        var studentResult = await _studentRepository.CreateStudentAsync(student);
        if (!studentResult.Item1)
            return Ok(studentResult.Item2);

        var studentAddress = new Models.StudentAddress
        {
            StudentId = studentResult.Item2,
            AddressLine1 = model.Address?.AddressLine1,
            AddressLine2 = model.Address?.AddressLine2,
            AddressLine3 = model.Address?.AddressLine3,
            City = model.Address?.City,
            State = model.Address?.State,
            Country = model.Address?.Country,
            ZipCode = model.Address?.ZipCode
        };
        var studentAddressResult = await _studentAddressRepository.CreateStudentAddressAsync(studentAddress);

        return Ok(studentResult.Item2);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudentAsync([FromBody] RequestModels.Students.UpdateStudent.UpdateStudentByOidDto model)
    {
        // TODO: validations
        var student = new Models.Student
        {
            StudentName = model.StudentName,
            GaurdianName = model.GaurdianName,
            Gender = model.Gender,
            DOB = model.DOB,
            CountryCode = model.CountryCode,
            PhoneNumber = model.PhoneNumber,
            Email = model.Email,
            ModifiedBy = model.ModifiedBy
        };

        var isUpdateSuccess = await _studentRepository.UpdateStudentByOidAsync(model.Oid, student);
        return Ok(isUpdateSuccess);
    }

    [HttpPut("address")]
    public async Task<IActionResult> UpdateStudentAddressAsync([FromBody] RequestModels.Students.UpdateStudent.UpdateStudentAddressDto model)
    {
        // TODO: validations
        var studentAddress = new Models.StudentAddress
        {
            StudentId = model.StudentId,
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            AddressLine3 = model.AddressLine3,
            City = model.City,
            State = model.State,
            Country = model.Country,
            ZipCode = model.ZipCode
        };

        var isUpdateSuccess = await _studentAddressRepository.UpdateStudentAddressByOidAsync(model.Oid, studentAddress);
        return Ok(isUpdateSuccess);
    }
}
