using System;
using Cosmos.Application.DTOs;

namespace Cosmos.Test.ApplicationTests.ModelsData;

public static class StudentServiceTestData
{
    public static IEnumerable<object[]> ValidCreateModels()
    {
        yield return new object[]
        {
            new CreateStudentDTO {Name = "Lucrécia Alberta de Souza", Email = "lu_23@gmail.com", RA = "101235", CPF = "36094849000"},
            CancellationToken.None
        };
        yield return new object[]
        {
            new CreateStudentDTO {Name = "Ernesto Silvério Ferreira", Email = "ernesto@hotmail.com.br", RA = "101236", CPF = "209.241.710-06"},
            CancellationToken.None
        };
        yield return new object[]
        {
            new CreateStudentDTO {Name = "Evaristo Nogueira da Silva", Email = "evaristo.silva@bol.com", RA = "111259", CPF = "02941724834"},
            new CancellationTokenSource().Token
        };
    }

    public static IEnumerable<object[]> InvalidCreateModels()
    {
        yield return new object[]
        {
            new CreateStudentDTO {Name = "", Email = "", RA = "", CPF = ""},
            CancellationToken.None
        };
        yield return new object[]
        {
            new CreateStudentDTO {Name = "João Alberto Pereira", Email = "", RA = "111259", CPF = "02941724834"},
            CancellationToken.None
        };
        yield return new object[]
        {
            new CreateStudentDTO {Name = "Adalberto Rigas", Email = "rigas@bol.com", RA = "101236", CPF = "000.000.000-00"},
            new CancellationTokenSource().Token
        };
        yield return new object[]
        {
            new CreateStudentDTO {Name = "Evaristo Nogueira da Silva", Email = "evaristo.silva.com", RA = "101236", CPF = "02941724834"},
            new CancellationTokenSource().Token
        };
        yield return new object[]
        {
            new CreateStudentDTO {Name = "ca", Email = "carlos.romao@gmail.com", RA = "101235", CPF = "02941724834"},
            new CancellationTokenSource().Token
        };
    }
    public static IEnumerable<object[]> ConflictCreateModels()
    {
        yield return new object[]
        {
            new CreateStudentDTO {Name = "Lucrécia Alberta de Souza", Email = "lu_23@gmail.com", RA = "101235", CPF = "36094849000"},
            CancellationToken.None
        };
    }

    public static IEnumerable<object[]> ValidUpdateModels()
    {
        yield return new object[]
        {
            1,
            new UpdateStudentDTO {Name = "Luciana", Email = "lu_23@gmail.com"},
            CancellationToken.None
        };

        yield return new object[]
        {
            1,
            new UpdateStudentDTO {Name = "Adalberto Rigas", Email = "rigas@bol.com"},
            CancellationToken.None
        };
    }

    public static IEnumerable<object[]> InvalidUpdateModels()
    {
        yield return new object[]
        {
            5,
            404,
            new UpdateStudentDTO {Name = "Luciana", Email = "lu_23@gmail.com"},
            CancellationToken.None
        };

        yield return new object[]
        {
            1,
            406,
            new UpdateStudentDTO {Name = "", Email = "rigas@bol.com"},
            CancellationToken.None
        };

        yield return new object[]
        {
            1,
            406,
            new UpdateStudentDTO {Name = "a", Email = "rigas@bol.com"},
            CancellationToken.None
        };
    }
}
