using AutoMapper;
using DemoSchool.WebUI.Models;
using DemoSchool.WebUI.ViewModels;

namespace DemoSchool.WebUI;

public class StudentApiClient(HttpClient _httpClient, IMapper _mapper)
{
    public async Task<int> CreateStudentAsync(Student student, StudentAddress address)
    {
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

        var response = await _httpClient.PostAsJsonAsync("/api/students", data);
        var studentId = await response.Content.ReadAsStringAsync(); // returns studentId
        return int.TryParse(studentId, out int id) ? id : id;
    }

    public async Task<StudentVm> GetStudentByIdAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<StudentVm>($"/api/students/{id}");
    }

    public async Task<StudentsVm> GetStudentsAsync(int page, int size)
    {
        return await _httpClient.GetFromJsonAsync<StudentsVm>($"/api/students?page={page}&size={size}");
    }

    public async Task<bool> UpdateStudentByOidAsync(Student student)
    {
        var data = _mapper.Map<RequestModels.Students.UpdateStudent.StudentDto>(student);

        var response = await _httpClient.PutAsJsonAsync("/api/students", data);
        var studentId = await response.Content.ReadAsStringAsync(); // returns studentId
        return bool.TryParse(studentId, out bool id) ? id : id;
    }

    public async Task<bool> UpdateStudentAddressByOidAsync(StudentAddress address)
    {
        var data = _mapper.Map<RequestModels.Students.UpdateStudent.StudentAddressDto>(address);

        var response = await _httpClient.PutAsJsonAsync("/api/students/address", data);
        var studentId = await response.Content.ReadAsStringAsync(); // returns studentId
        return bool.TryParse(studentId, out bool id) ? id : id;
    }
}
