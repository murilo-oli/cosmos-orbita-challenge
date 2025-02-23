using System;
using Cosmos.Application.Enums;

namespace Cosmos.Application.Utilities;

public class ResponseDTO
{
    public bool Success { get; private set; }
    public int StatusCode { get; private set; }
    public EResponseType ResponseType { get; private set; }
    public string? Description { get; private set; }
    public dynamic? Data { get; private set; }


    public ResponseDTO(bool success, int statusCode, EResponseType responseType, string? description = null, dynamic? data = null)
    {
        Success = success;
        StatusCode = statusCode;
        ResponseType = responseType;
        Description = description;
        Data = data;
    }

}
