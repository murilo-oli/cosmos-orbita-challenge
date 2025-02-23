using System.Threading.Tasks;
using Cosmos.Application.DTOs;
using Cosmos.Application.Interfaces;
using Cosmos.Application.Services;
using Cosmos.Application.Utilities;
using Cosmos.Domain.Entities;
using Cosmos.Infrastructure.Context;
using Cosmos.Infrastructure.Interfaces;
using Cosmos.Infrastructure.Repositories;
using Cosmos.Test.ApplicationTests.ModelsData;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Cosmos.Test;

public class StudentServiceTest
{
    private readonly CosmosDbContext _context;
    private readonly IBaseRepository<Student> _studentRepository;
    private readonly IStudentService _studentService;
    private readonly CancellationToken _cancellationToken;

    public StudentServiceTest()
    {
        var options = new DbContextOptionsBuilder<CosmosDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

        _context = new CosmosDbContext(options);
        _studentRepository = new BaseRepository<Student>(_context);
        _studentService = new StudentService(_studentRepository); 
    }


    [Theory(DisplayName = "Should create a student successfully!")]
    [MemberData(nameof(StudentServiceTestData.ValidCreateModels), MemberType = typeof(StudentServiceTestData))]
    public async Task ShouldCreateAStudentSuccessfully(CreateStudentDTO studentDTO, CancellationToken cancellationToken)
    {
        //Act
        ResponseDTO response = await _studentService.Add(studentDTO, cancellationToken);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(201, response.StatusCode);
    }

    [Theory(DisplayName = "Should save a student in database successfully!")]
    [MemberData(nameof(StudentServiceTestData.ValidCreateModels), MemberType = typeof(StudentServiceTestData))]
    public async Task ShouldSaveAStudentInDatabaseSuccessfully(CreateStudentDTO studentDTO, CancellationToken cancellationToken)
    {
        //Arrange
        FilterDTO filter = new FilterDTO(){
            RA = studentDTO.RA,
            IsActive = true
        };

        //Act
        await _studentService.Add(studentDTO, _cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        //Assert
        ResponseDTO? response = await _studentService.GetFiltered(filter, cancellationToken);

        Assert.NotNull(response);
        Assert.Equal(200, response.StatusCode);

        Student studentFound = response.Data as Student;
        Assert.NotNull(studentFound);
        Assert.Equal(studentDTO.Name, studentFound.Name);
        Assert.Equal(studentDTO.Email, studentFound.Email);
        Assert.Equal(studentDTO.RA, studentFound.RA);
        Assert.Equal(studentDTO.CPF.Trim().Replace(".","").Replace("-",""), studentFound.CPF);
    }

    [Theory(DisplayName = "Should not create a student!")]
    [MemberData(nameof(StudentServiceTestData.InvalidCreateModels), MemberType = typeof(StudentServiceTestData))]
    public async Task ShouldNotCreateAStudent(CreateStudentDTO studentDTO, CancellationToken cancellationToken)
    {
        //Act
        ResponseDTO response = await _studentService.Add(studentDTO, cancellationToken);

        //Assert
        Assert.NotNull(response);
        Assert.Equal(406, response.StatusCode);
    }
}