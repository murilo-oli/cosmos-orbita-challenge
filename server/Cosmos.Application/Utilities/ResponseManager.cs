using Cosmos.Application.Enums;

namespace Cosmos.Application.Utilities;
public static class ResponseManager
{
    public static ResponseModel Success(dynamic? data = null, string? description = null) =>
        new(true, 200, EResponseType.SUCCESSFUL, description, data);

    public static ResponseModel Created(dynamic? data = null, string? description = null) =>
        new(true, 201, EResponseType.CREATED_SUCCESSFUL, description, data);

    public static ResponseModel NotFound(string? description = null) =>
        new(false, 404, EResponseType.NOT_FOUND, description, null);

    public static ResponseModel Unauthorized(string? description = null) =>
        new(false, 401, EResponseType.INVALID_CREDENTIALS, description, null);

    public static ResponseModel Forbidden(string? description = null) =>
        new(false, 403, EResponseType.FORBIDDEN, description, null);

    public static ResponseModel Conflict(string? description = null) =>
        new(false, 409, EResponseType.ALREADY_EXIST, description, null);

    public static ResponseModel InvalidModel(string? description = null) =>
        new(false, 406, EResponseType.INVALID_MODEL, description, null);

    public static ResponseModel BadRequest(string? description = null) =>
        new(false, 400, EResponseType.BAD_REQUEST, description, null);

    public static ResponseModel BadRequest(dynamic? data = null, string? description = null) =>
        new(false, 400, EResponseType.BAD_REQUEST, description, data);

    public static ResponseModel InternalBadRequest(dynamic? data = null, string? description = null) =>
        new(false, 400, EResponseType.BAD_REQUEST, $"Internal error message: {description}", data);


}