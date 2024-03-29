﻿namespace DemoSchool.WebAPI.Repository.Interfaces;

public interface IStudentRepository
{
    Task<(bool, int)> CreateStudentAsync(Student student);
    Task<Student> GetStudentByIdAsync(int id);
    Task<Student> GetStudentByOidAsync(Guid oid);
    Task<(IEnumerable<Student> Students, int Count)> GetStudentsAsync(int page, int size);
    Task<bool> UpdateStudentByOidAsync(Guid oid, Student student);
}