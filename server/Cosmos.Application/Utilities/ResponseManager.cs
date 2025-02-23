using Cosmos.Application.Enums;

namespace Cosmos.Application.Utilities;
public static class ResponseManager
{
    public static ResponseDTO Success(dynamic? data = null, string? description = null) =>
        new(true, 200, EResponseType.SUCCESSFUL, description, data);

    public static ResponseDTO Created(dynamic? data = null, string? description = null) =>
        new(true, 201, EResponseType.CREATED_SUCCESSFUL, description, data);

    public static ResponseDTO NotFound(string? description = null) =>
        new(false, 404, EResponseType.NOT_FOUND, description, null);

    public static ResponseDTO Unauthorized(string? description = null) =>
        new(false, 401, EResponseType.INVALID_CREDENTIALS, description, null);

    public static ResponseDTO Forbidden(string? description = null) =>
        new(false, 403, EResponseType.FORBIDDEN, description, null);

    public static ResponseDTO Conflict(string? description = null) =>
        new(false, 409, EResponseType.ALREADY_EXIST, description, null);

    public static ResponseDTO InvalidModel(string? description = null) =>
        new(false, 406, EResponseType.INVALID_MODEL, description, null);

    public static ResponseDTO BadRequest(string? description = null) =>
        new(false, 400, EResponseType.BAD_REQUEST, description, null);

    public static ResponseDTO BadRequest(dynamic? data = null, string? description = null) =>
        new(false, 400, EResponseType.BAD_REQUEST, description, data);

    public static ResponseDTO InternalBadRequest(dynamic? data = null, string? description = null) =>
        new(false, 400, EResponseType.BAD_REQUEST, $"Internal error message: {description}", data);


}